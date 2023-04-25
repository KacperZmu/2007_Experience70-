using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }
    public Camera mainCamera;
    public string hiddenLayerName = "hiddenLayer";
    public string hiddenLayerName2 = "hiddenLayer2";
    public string hiddenLayerName3 = "hiddenLayer3";
    public Transform dumpster1CameraTransform;
    public Transform dumpster2CameraTransform;
    public Transform dumpster3CameraTransform;
    public Transform defaultCameraTransform;
    private BoxCollider boxCollider;



    //set speed
    [Header("Movement")]
    public float movSpeed;

    public float groundDrag;
    //set heigh to ground
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float HorizontalInput;
    float VerticalInput;

    Vector3 movDirection;
    public Rigidbody RB;
    private Animator anim;

   

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Application.targetFrameRate = 80;
    }
    public void SetPlayerLayer(LayerMask layerMask)
    {
        gameObject.layer = layerMask;
    }


    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        RB.freezeRotation = true;
        boxCollider = GetComponent<BoxCollider>();
        //anim = GetComponentInChildren<Animator>();
        //Get rigid body, find animation and freeze rotation



    }



    private void Update()
    {
        bool isHidden = gameObject.layer == LayerMask.NameToLayer(hiddenLayerName);
        bool isHidden2 = gameObject.layer == LayerMask.NameToLayer(hiddenLayerName2);
        bool isHidden3 = gameObject.layer == LayerMask.NameToLayer(hiddenLayerName3);


        // Update the camera's position and rotation if the player is inside the dumpster
        if (isHidden && dumpster1CameraTransform.gameObject.CompareTag("Dumpster1"))
        {
            mainCamera.transform.position = dumpster1CameraTransform.position;
            Debug.Log("Camera set to Dumpster1");
            boxCollider.enabled = false;

            //mainCamera.transform.rotation = dumpsterCameraTransform.rotation;
        }
        // Otherwise, update the camera's position and rotation to the default position and rotation
        else if (isHidden2 && dumpster2CameraTransform.gameObject.CompareTag("Dumpster2"))
        {
            mainCamera.transform.position = dumpster2CameraTransform.position;
            Debug.Log("Camera set to Dumpster2");
            boxCollider.enabled = false;

            // mainCamera.transform.rotation = defaultCameraTransform.rotation;
        }
        else if (isHidden3 && dumpster3CameraTransform.gameObject.CompareTag("Dumpster3"))
        {
            mainCamera.transform.position = dumpster3CameraTransform.position;
            Debug.Log("Camera set to Dumpster3");
            boxCollider.enabled = false;

            // mainCamera.transform.rotation = defaultCameraTransform.rotation;
        }
        else
        {
            mainCamera.transform.position = defaultCameraTransform.position;
            boxCollider.enabled = true;

            // mainCamera.transform.rotation = defaultCameraTransform.rotation;
        }


        //cast rays from player to ground to find ground, with which we can then apply drag
        //so that the player doesnt walk like he slips on ice
        //then we set which buttons from within unity will activate the animation for walking
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.5f, whatIsGround);
        
        
            MyInput();
        
        

        if (grounded)
            RB.drag = groundDrag;
        else
            RB.drag = 0;

      //  if (Input.GetButton("Vertical"))
        //{
       //     anim.SetBool("IsWalking", true);
        //}
        //else if (Input.GetButton("Horizontal"))
        //{
          //  anim.SetBool("IsWalking", true);

        //}
        //else
        //{
       //     anim.SetBool("IsWalking", false);

        //}
        






    }

   

    private void FixedUpdate()
    {
        MovePlayer();

      
    }
    // here we actually move the player around the map
    private void MyInput()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");

    }

    private void MovePlayer()
    {
        movDirection = orientation.forward * VerticalInput + orientation.right * HorizontalInput;
        RB.AddForce(movDirection.normalized * movSpeed * 2f, ForceMode.Force);
        


    }

    

    

   
}
