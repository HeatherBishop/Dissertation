using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NPCController : MonoBehaviour {

    public Sprite currentSprite;
    public GameObject injuredSprite;
    public Animator anim;
    
  public bool iswalking;
    public bool isMother;

    public GameObject levelController;



    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
       
	}
	
	// Update is called once per frame
	void Update () {
        if (iswalking)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(20, 0, 0), 4f * Time.deltaTime);
            anim.SetBool("walk", true);
        }
        if (iswalking == false)
        {
            anim.enabled = false;
        }


	}

    public void SwapSprite()
    {
        iswalking = false;
        injuredSprite.SetActive(true);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = null;
        levelController.GetComponent<Level01GameController>().spawnEnemies = false;


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hurt")
        {
            iswalking = false;
            GetComponent<SpriteRenderer>().sprite = currentSprite;
        
            if(isMother)
                levelController.GetComponent<Level01GameController>().spawnEnemies = false;

        }
    }




}
