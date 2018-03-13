using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamecontroller : MonoBehaviour {

    public GameObject player;

    public GameObject sister;

    //This script controls the events of the game.

        //at start player and sister arrive on boat. 
        //Boat reaches land and they get off.
        //sister falls in first water pool, world becomes darker.
        //brother carrys sister and tries to give her Light.
        //She runs away.
        //

	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartShowText()
    {
        Debug.Log("Show text");
        StartCoroutine(ControlTextOrder());


    }

    IEnumerator ControlTextOrder()
    {

        //choose which text displays first


        yield return new WaitForSeconds(2f);
        StartCoroutine(ShowPlayerText());

        yield return new WaitForSeconds(2f);
        player.GetComponent<Player>().playerMove = true;
    }
}
