using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    public GameObject targetObject;
    public string hiddenLayerName = "hiddenLayer";
    public string hiddenLayerName2 = "hiddenLayer2";
    public string hiddenLayerName3 = "hiddenLayer3";
    public float chaseDistance = 10f;
    public float detectionRange = 5f;
    public Transform[] originalPath;
    public float stoppingDistance = 2;
    private NavMeshAgent navMeshAgent;
    private bool isChasing = false;
    private int currentPathIndex = 0;
    private Animator anim;
    private bool isCollidingWithPlayer = false;
    private float animationSlowdownTimer = 0f;
    private BoxCollider boxCollider;
    public HealthBar healthBar;


    private void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (originalPath.Length > 0)
        {
            navMeshAgent.SetDestination(originalPath[0].position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollidingWithPlayer = true;
            animationSlowdownTimer = 5f;
            healthBar.TakeDamage(20);
        }
    }

    private void Update()
    {
        if (isChasing)
        {
            if (CanSeePlayer())
            {
                ChasePlayer();
                boxCollider.enabled = true;
            }
            else
            {
                StopChasing();
                boxCollider.enabled = false;
            }
        }
        else
        {
            if (CanDetectPlayer())
            {
                StartChasing();
                boxCollider.enabled = false;
            }
            else
            {
                WalkOriginalPath();
                boxCollider.enabled = false;
            }
        }

        if (isCollidingWithPlayer)
        {
            if (animationSlowdownTimer > 0)
            {
                anim.speed = 0.2f;
                animationSlowdownTimer -= Time.deltaTime;
            }
            else
            {
                anim.speed = 1.5f;
                isCollidingWithPlayer = false;
                animationSlowdownTimer = 0; // reset the timer here
            }
        }
    }

    private bool CanSeePlayer()
    {
        Vector3 playerDirection = (targetObject.transform.position - transform.position).normalized;
        Ray ray = new Ray(transform.position, playerDirection);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, chaseDistance))
        {
            if (hit.transform.gameObject.layer != LayerMask.NameToLayer(hiddenLayerName) && hit.transform.gameObject.layer != LayerMask.NameToLayer(hiddenLayerName2) && hit.transform.gameObject.layer != LayerMask.NameToLayer(hiddenLayerName3))
            {
                if (hit.transform.gameObject == targetObject)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool CanDetectPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, targetObject.transform.position);
        return distanceToPlayer <= detectionRange;
    }

    private void StartChasing()
    {
        isChasing = true;
        currentPathIndex = 0;
        navMeshAgent.enabled = false;
        
    }

    private void ChasePlayer()
    {
        float distanceToTarget = Vector3.Distance(transform.position, targetObject.transform.position);
        GameObject.FindGameObjectsWithTag("PlayerCube");
        transform.LookAt(targetObject.transform);
        
        if (distanceToTarget >= stoppingDistance)
        {
            StopChasing();
        }
    }

    private void StopChasing()
    {
        isChasing = false;
        
        navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(originalPath[currentPathIndex].position);
    }

    private void WalkOriginalPath()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            boxCollider.enabled = false;
            currentPathIndex = (currentPathIndex + 1) % originalPath.Length;
            navMeshAgent.SetDestination(originalPath[currentPathIndex].position);
        }
    }
}