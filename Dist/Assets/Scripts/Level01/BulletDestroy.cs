using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour {

    public GameObject gun;
    public GameObject gameController;

    public GameObject textControlker;



    // Use this for initialization
    void Start () {
        gun = GameObject.Find("Gun");
        gameController = GameObject.Find("GameController");
        textControlker = GameObject.Find("Text_l");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "hurt")
        {
            collision.transform.GetChild(0).gameObject.SetActive(true);
            Destroy(collision.gameObject, 3);
            Destroy(gameObject, 2);
        }

        if (collision.gameObject.tag == "10points")
        {
            gun.GetComponent<RaycastGun>().UpdateScore(10);
            collision.transform.GetChild(0).gameObject.SetActive(true);
            Destroy(collision.gameObject, 0.5f);
            Destroy(gameObject, 0);
        }
        if (collision.gameObject.tag == "100points")
        {
            gun.GetComponent<RaycastGun>().UpdateScore(100);
            collision.transform.GetChild(0).gameObject.SetActive(true);
            Destroy(collision.gameObject, 0.5f);
            Destroy(gameObject, 0);
        }
        if (collision.gameObject.tag == "Player")
        {

            collision.transform.GetChild(0).gameObject.SetActive(true);
            collision.gameObject.GetComponent<NPCController>().SwapSprite();
            Destroy(gameObject, 3f);
        }

        Destroy(gameObject, 2);
    }

    
}
