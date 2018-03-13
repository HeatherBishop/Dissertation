using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    public float delay = 0.1f;
    public float playerDelay;
    public float sisterDelay;
    public string fullText;
    public string currentText = "";




    public GameObject player;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}

    public void StartShowText()
    {
        Debug.Log("Show text");
        StartCoroutine(ControlTextOrder());


    }

    IEnumerator ControlTextOrder()
    {
        Debug.Log("Control text");
        //choose which text displays first

        StartCoroutine(ShowSisterText());
        yield return new WaitForSeconds(2f);

        if (player.GetComponent<Player>().sisterObj != null)
        {
            player.GetComponent<Player>().sisterMove = true;

        }

        yield return new WaitForSeconds(2f);
        player.GetComponent<Player>().playerMove = true;

        if(player.GetComponent<Player>().sisterObj != null)
        player.GetComponent<Player>().sisterObj.SetActive(false);

        player.GetComponent<Player>().sisterObj = null;

    }

    IEnumerator ShowSisterText()
    {

        Debug.Log("Sister talk");
       // yield return new WaitForSeconds(sisterDelay);
        for (int i = 0; i < fullText.Length; i++)
        {

            currentText = fullText.Substring(0, i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }

    }

 /*   IEnumerator ShowPlayerText()
    {

        // yield return new WaitForSeconds(sisterDelay);
        for (int i = 0; i < fullText.Length; i++)
        {

            currentText = fullText.Substring(0, i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }

    }*/
}

