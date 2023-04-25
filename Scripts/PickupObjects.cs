using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObjects : MonoBehaviour
{
    [SerializeField] private LayerMask pickupMask;
    [SerializeField] private LayerMask inventoryMask;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform pickupTarget;
    [Space]
    [SerializeField] private Spawner spawnerScript;
    [SerializeField] private float pickupDistance;
    private Rigidbody currentObject;
    public int inventoryCount = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentObject)
            {
                currentObject.useGravity = true;
                currentObject = null;
                return;
            }

            // Check for inventory item clicks
            Ray cameraRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, pickupDistance, inventoryMask))
            {
                GameObject hitObject = hitInfo.transform.gameObject;
                if (hitObject.CompareTag("InventoryItem"))
                {
                    spawnerScript.enabled = false;
                    hitObject.SetActive(false);
                    Debug.Log("Candle Disabled");
                    inventoryCount++;
                    spawnerScript.enabled = true;
                    return;

                }
            }

            // Check for pickupable objects
            if (Physics.Raycast(cameraRay, out RaycastHit hitInfo2, pickupDistance, pickupMask))
            {
                currentObject = hitInfo2.rigidbody;
                currentObject.useGravity = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (currentObject)
        {
            Vector3 directionToPoint = pickupTarget.position - currentObject.position;
            float distanceToPoint = directionToPoint.magnitude;

            currentObject.velocity = directionToPoint * 12f * distanceToPoint;
        }
    }
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Candles: " + inventoryCount);
    }
    public void RemoveFromInventory()
    {
        inventoryCount--;
    }

}





