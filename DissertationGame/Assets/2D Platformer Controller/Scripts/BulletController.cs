using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public GameObject gun;
    public GameObject player;

    public Sprite door3;

    public int doorCost;
    public int lightCost;

    bool countTime = true;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        gun = GameObject.FindGameObjectWithTag("Gun");
    }
	
	// Update is called once per frame
	void Update () {
        float time = 10f;
        float currentTime = 0f;


        if (countTime)
        {
            Destroy(this.gameObject, 5);
        }

		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collect ammo


        //open door
        if (collision.gameObject.tag == "Door")
        {

            //move the camera to look at the door so the player sees it open

            //if the player has 2 bullets
            if (gun.GetComponent<RaycastGun>().bulletShot >= doorCost)
            {
                //open door
                Debug.Log("hit door");
                collision.gameObject.GetComponent<DoorMovement>().enabled = true;

                gun.GetComponent<RaycastGun>().SetAmmoText(doorCost);
               // collision.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = door3;             //make the door switch glow

                this.gameObject.SetActive(false);

            }


        }
            

        //light lamp
        if (collision.gameObject.tag == "Light")
        {

            if (gun.GetComponent<RaycastGun>().bulletShot >= lightCost)
            {
                //add alpha mask to sprites over light.
                Debug.Log("hit light");

                collision.transform.GetChild(2).gameObject.SetActive(true);

                gun.GetComponent<RaycastGun>().SetAmmoText(lightCost);
                this.gameObject.SetActive(false);
            }


        }

        
    }



}
