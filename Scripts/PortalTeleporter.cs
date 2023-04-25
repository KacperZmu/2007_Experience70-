using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    public GameObject player;
    public GameObject teleportTo;
    public int counter = 0;
    public void Start()
    {
        counter = 0;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Teleporter"))
        {
            player.transform.position = teleportTo.transform.position;
            Counter();
            player.transform.Rotate(0, 90, 0);
        }
    }
   



    public void Counter()
   {
        counter = counter + 1;
   }
}
