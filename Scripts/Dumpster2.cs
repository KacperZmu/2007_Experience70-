using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dumpster2 : MonoBehaviour
{
    // The layer that the player should be moved to while inside the dumpster
    public string hiddenLayerName = "hiddenLayer2";
    // The distance at which the player can interact with the dumpster
    public float interactDistance = 2f;

    // The position and rotation to place the player while inside the dumpster
    public Transform playerInsideTransform;

    // The position and rotation to place the player while outside the dumpster
    public Transform playerOutsideTransform;

    // The current state of whether the player is inside the dumpster or not
    private bool playerInside = false;

    // A reference to the player controller singleton instance
    public PlayerMovement playerController;

    public AudioSource DumpsterAudio;



    void Start()
    {
        playerController = PlayerMovement.Instance;
        playerController.RB = playerController.GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        if (playerInside)
        {
            playerController.RB.constraints = RigidbodyConstraints.FreezeAll;
            // Allow the player to exit the dumpster by pressing the "E" key
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerInside = false;
                playerController.transform.position = playerOutsideTransform.position;
                playerController.transform.rotation = playerOutsideTransform.rotation;
                playerController.gameObject.layer = LayerMask.NameToLayer("playerLayer");
                DumpsterAudio.Play();
                playerController.RB.constraints = RigidbodyConstraints.None;
                playerController.RB.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
        else
        {
            // Cast a ray from the camera to check if the player is looking at the dumpster
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, interactDistance))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    // If the player is looking at the dumpster, check if they press the interact button
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        // Move the player inside the dumpster and change their layer to hidden
                        playerInside = true;
                        playerController.transform.position = playerInsideTransform.position;
                        playerController.transform.rotation = playerInsideTransform.rotation;
                        playerController.gameObject.layer = LayerMask.NameToLayer(hiddenLayerName);
                        DumpsterAudio.Play();
                    }
                }
            }
        }
    }
}
