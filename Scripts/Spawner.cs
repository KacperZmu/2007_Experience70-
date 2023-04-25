using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Candle2;
    public GameObject Candle3;
    public GameObject Candle4;
    public GameObject Candle5;
    public GameObject Smiley2;
    public GameObject Smiley3;
    public GameObject Smiley4;
    public GameObject Smiley5;
    public GameObject Smiley6;
    public GameObject HideImage;
    public GameObject Corpse1;
    public GameObject Corpse2;
    public GameObject Corpse3;
    public GameObject Corpse4;
    public GameObject Corpse5;
    public GameObject Corpse6;
    public GameObject Corpse7;
    [SerializeField] public PickupObjects pickupScript;
    public PortalTeleporter portalTeleporter;
    public string hiddenLayerName = "hiddenLayer";
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        
        bool isHidden = Player.gameObject.layer == LayerMask.NameToLayer(hiddenLayerName);
        if (portalTeleporter.counter == 3 && isHidden)
        {
            Enemy1.SetActive(true);
            
            Corpse1.SetActive(true);
        }
        else if (portalTeleporter.counter == 1)
        {
            Smiley2.SetActive(true);
        }
        else if (portalTeleporter.counter == 3)
        {
            if (portalTeleporter.counter == 3 && pickupScript.inventoryCount == 3)
            {

                Candle3.SetActive(false);

            }
            else
            {
                Candle3.SetActive(true);
            }
            HideImage.SetActive(true);
            Candle2.SetActive(false);
            

        }
        else if(portalTeleporter.counter == 2)
        {
            if (portalTeleporter.counter == 2 && pickupScript.inventoryCount == 2)
            {

                Candle2.SetActive(false);

            }
            else
            {
                Candle2.SetActive(true);

            }
            
            
            Smiley3.SetActive(true);
        }
        else if (portalTeleporter.counter == 4)
        {
            if (portalTeleporter.counter == 4 && pickupScript.inventoryCount == 4)
            {

                Candle4.SetActive(false);

            }
            else
            {
                Candle4.SetActive(true);

            }

            Enemy2.SetActive(true);
            Smiley4.SetActive(true);
            Enemy1.SetActive(false);
            
            Candle3.SetActive(false);

        }
        else if (portalTeleporter.counter == 5)
        {
            if (portalTeleporter.counter == 5 && pickupScript.inventoryCount == 5)
            {

                Candle5.SetActive(false);

            }
            else
            {
                Candle5.SetActive(true);

            }
            
            Smiley5.SetActive(true);
            Enemy2.SetActive(false);
            Corpse2.SetActive(true);
            Corpse3.SetActive(true);
            Corpse4.SetActive(true);
            Corpse5.SetActive(true);
            Corpse6.SetActive(true);
            Corpse7.SetActive(true);
            Candle4.SetActive(false);
        }
        else if (portalTeleporter.counter == 6)
        {
            
            Smiley6.SetActive(true);
            Candle5.SetActive(false);
        }

        else
        {
            
            Candle3.SetActive(false);
            Candle2.SetActive(false);
            Candle4.SetActive(false);
            Candle5.SetActive(false);
            Enemy2.SetActive(false);
            HideImage.SetActive(false);
            Corpse1.SetActive(false);
            Corpse2.SetActive(false);
            Corpse3.SetActive(false);
            Corpse4.SetActive(false);
            Corpse5.SetActive(false);
            Corpse6.SetActive(false);
            Corpse7.SetActive(false);

        }




      
        
        
        
       
    }
}
