using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDetection : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collect ammo




        //light lamp
        if (collision.gameObject.tag == "Bullet")
        {
            //add alpha mask to sprites over light.
            Debug.Log("hit light");

           //ollision.transform.GetChild(3).gameObject.SetActive(true);
            this.gameObject.SetActive(false);

        }

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("hit player");
            this.gameObject.SetActive(false);
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //add alpha mask to sprites over light.
            Debug.Log("hit light");

           //ollision.transform.GetChild(3).gameObject.SetActive(true);
            this.gameObject.SetActive(false);

        }
    }
}
