using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01GameController : MonoBehaviour {





    //spawn enemies for player to shoot and gain points

    //activate player after 60s, player walk onto screen.
    //activate text from father
    //swap player sprite to unconscious one, drop apple.
    //more text from father.
    //father go to player and give her apple.
    //another guard comes on screen and shoots at you both.
    //run, as exit screen new scene is loaded.


    public GameObject hazard;
    public GameObject otherHAzard;
    public int otherHazardCount;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public bool spawnEnemies = true;


    //screen fade
    public Animator fadeAnim;
    public Image fadeImage;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        if (spawnEnemies == false)
        {
            //end the level

            SceneManager.LoadScene(2);
        }
    }


    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        if(spawnEnemies)
        {
            while (true)
            {
                for (int i = 0; i < hazardCount; i++)
                {
                    Vector3 spawnPosition = new Vector3(spawnValues.x, Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, spawnRotation);
                    yield return new WaitForSeconds(spawnWait);
                }

                for (int i = 0; i < otherHazardCount; i++)
                {
                    Vector3 spawnPosition = new Vector3(spawnValues.x, Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(otherHAzard, spawnPosition, spawnRotation);
                    yield return new WaitForSeconds(spawnWait);
                }
                yield return new WaitForSeconds(waveWait);
            }
        }
     
    }



    
}
