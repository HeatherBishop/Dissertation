using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerTowerScript : MonoBehaviour {
    public float speed;
    public float tilt;
    public Rigidbody2D rigidbodys;
    public bool canmove = true;

    public GameObject mother;
    public GameObject appleFloor;
    public GameObject appleDad;
    public GameObject appleMum;

    public GameObject guard;
    public GameObject guards;
    public AudioClip siren;
    public AudioSource thisAudio;
    public GameObject spotlight;

    public GameObject motherAttach;

    public bool guardsMove = false;


    //surrounding guards
    public GameObject spotlight2;
    public GameObject guardsDeath;
    public Sprite surrenderSprite;
    public AudioClip shot;
    public Animator animators;
    public Animator anim;
    public GameObject finish;

    public GameObject heart;



    //screen fade
    public Animator fadeAnim;
    public Image fadeImage;
    // Use this for initialization
    void Start () {
        thisAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if (canmove)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rigidbodys.velocity = movement * speed;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }



        if (guardsMove)
        {
            guards.transform.position  = Vector3.MoveTowards(guards.transform.position, new Vector3(20, 0, 0), 2f * Time.deltaTime);
           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canmove = false;

            rigidbodys.velocity = Vector2.zero;
            StartCoroutine(Eventss());
        }

        if (collision.gameObject.tag == "Finish")
        {
            canmove = false;
            guardsMove = false;

            rigidbodys.velocity = Vector2.zero;

            StartCoroutine(Ending());
        }
        if (collision.gameObject.tag == "Hurt")
        {

            thisAudio.clip = shot;
            thisAudio.Play();
           
            SceneManager.LoadScene(1);
        }
    }



    public IEnumerator Eventss()
    {
        heart.SetActive(true);
        appleFloor.SetActive(false);
        yield return new WaitForSeconds(2f);
        //pick up apple
        appleFloor.SetActive(false);
        appleDad.SetActive(true);
        yield return new WaitForSeconds(2f);
        heart.SetActive(false);
        //give mother the apple
        appleDad.SetActive(false);
        appleMum.SetActive(true);
        yield return new WaitForSeconds(2f);
        //mother gets up
        appleMum.SetActive(false);
        mother.SetActive(true);
        heart.SetActive(true);
        yield return new WaitForSeconds(2f);
        spotlight.SetActive(true);
        heart.SetActive(false);
        yield return new WaitForSeconds(1f);
        //guard enters the screen
        guard.SetActive(true);

        //shoots at them.

        //siren sounds
        thisAudio.clip = siren;
        thisAudio.Play();
        yield return new WaitForSeconds(2f);
        //a wall of guard begins to move towards
        
        guards.SetActive(true);
        guardsMove = true;

        yield return new WaitForSeconds(1f);
        mother.SetActive(false);
        motherAttach.SetActive(true);
        finish.SetActive(true);
        canmove = true;
        //player hit next trigger


    }

    public IEnumerator Ending()
    {
        // becomes surrounded by guards

        spotlight2.SetActive(true);
        yield return new WaitForSeconds(1f);
        guardsDeath.SetActive(true);
        yield return new WaitForSeconds(1f);

        //kiss mothers stomach
        GetComponent<SpriteRenderer>().flipX = true;

        anim.SetTrigger("Kiss");
        yield return new WaitForSeconds(2f);
        anim.SetTrigger("Kiss");

        yield return new WaitForSeconds(2f);
        //surrender, shot dead
        anim.SetBool("Surrender", true);

        yield return new WaitForSeconds(3f);

        thisAudio.clip = shot;
        thisAudio.Play();
        anim.SetBool("Die", true);
        animators.SetTrigger("Distraught");
        yield return new WaitForSeconds(2f);


        yield return new WaitForSeconds(3f);
    
        SceneManager.LoadScene(3);
    }


}
