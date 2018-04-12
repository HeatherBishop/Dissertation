using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour {

    //screen fade
    public Animator fadeAnim;
    public Image fadeImage;

    public float loadingTime;

    public GameObject loadingImg;
    public GameObject loadingscreen01;
    public GameObject loadingScreen02;
    public GameObject loadingScreen03;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void StartGame()
    {

        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        
        Application.Quit();
    }
    public void RetryGame()
    {

        SceneManager.LoadScene(3);
    }




}
