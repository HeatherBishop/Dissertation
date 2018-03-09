using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayetTextController : MonoBehaviour {

    // Use this for initialization

    public float delay = 0.1f;
    public float playerDelay;

    public string fullText;
    public string currentText = "";



    public GameObject player;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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


       IEnumerator ShowPlayerText()
       {

           // yield return new WaitForSeconds(sisterDelay);
           for (int i = 0; i < fullText.Length; i++)
           {

               currentText = fullText.Substring(0, i);
               this.GetComponent<Text>().text = currentText;
               yield return new WaitForSeconds(delay);
           }

       }
}
