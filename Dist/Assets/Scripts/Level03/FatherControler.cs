using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatherControler : MonoBehaviour {

    //moving to cell
    public bool canMoveToCell;
    float time;
   Vector3 startposition;
     Vector3 target;
    float timeToReachTarget;

    bool isSeen;

    //father die trigger
    public GameObject fatherDieTrigger;
    AudioSource audios;
    AudioClip gunshot;

    public SpriteRenderer motherAttached;
    public GameObject deadMother;
    public GameObject finalSpotlight;

    public Transform playerObj;
    public bool fatherDie;
    public GameObject fatherDeadObj;

    // Use this for initialization
    void Start () {
        audios = gameObject.GetComponent<AudioSource>();
        motherAttached = gameObject.GetComponent<SpriteRenderer>();
        startposition = target = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (canMoveToCell)
        {
            time += Time.deltaTime / timeToReachTarget;
            transform.position = Vector3.Lerp(startposition, target, time);
        }

        if (isSeen)
        {

            finalSpotlight.transform.position = this.gameObject.transform.position;

        }
        if (fatherDie)
        {
            StartCoroutine(FatherDie());
        }

    }

    //enter cell

    public void SetDestination(Vector3 destination, float time)
    {
        time = 0;
        startposition = transform.position;
        timeToReachTarget = time;
        target = destination;
    }

    //dance/meet mother   

    public void DanceWMother()
    {
    }

    ////NPC goes for food and dies, becomes hiding spot

    public void NPCEating()
    {

    }

    //eat food and grow

    public void Eating()
    {

    }

    //smash wall on trigger enter & swap scene
    public void EscapeCell()
    {

    }

    //father die protecting mother

   public IEnumerator FatherDie()
    {

        isSeen = true; //set the spotlight to follow the father
       // finalSpotlight.GetComponent<AudioSource>().Play(); // play siren sound

        transform.position = playerObj.position;
        yield return new WaitForSeconds(2);
        audios.Play(); // play gunshot

        //world gets darker
        motherAttached.enabled = false; //disable father sprite
        //enable dead father hiding spot
            
        deadMother.SetActive(true);
        fatherDeadObj.SetActive(true);

        yield return new WaitForSeconds(2);
        finalSpotlight.SetActive(false);
        deadMother.SetActive(false);
        this.gameObject.SetActive(false);



        // transform.position = new Vector3(Mathf.PingPong(Time.time, 3), transform.position.y, transform.position.z);


        // yield return new WaitForSeconds(4);

        //change to end scene

    }

    //disable this object, enable hide object.


}
