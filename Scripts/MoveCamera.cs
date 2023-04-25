using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;

    //here we move camera based on position
    private void Update()
    {
        transform.position = cameraPosition.position;
    }
}
