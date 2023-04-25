using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;
    public Transform PlayerBody;
    

    private void Start()
    {
        //we lock the cursor so it doesnt fly around on screen while in game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    //public void Teleport(Vector3 position, Quaternion rotation)
    //{
        //transform.position = position;
        //Physics.SyncTransforms();
        //xRotation = rotation.eulerAngles.y;
      //  yRotation = rotation.eulerAngles.z;
        
    //}
    private void Update()
    {
        //here we allow for the camera to ratate based on the input of the mouse
        float mousX = Input.GetAxisRaw("Mouse X")  * sensX;
        float mousY = Input.GetAxisRaw("Mouse Y")  * sensY;

        yRotation += mousX;
        xRotation -= mousY;

        xRotation = Mathf.Clamp(xRotation, -80f, 80f);


        PlayerBody.Rotate(Vector3.up * mousX);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
