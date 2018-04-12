using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastGun : MonoBehaviour {

    public GameObject gameCont;

    public GameObject shotPrefab;

    public GameObject exit_Point;

    private Rigidbody2D rb;

    public float speed;

    public int bulletPool;
    public int bulletShot;

    //Bullet UI
    public Text ammoText;

    //score UI
    public Text scoreText;
    public int scoreTotal;

    public AudioSource audiosurce;
    public AudioClip shot;

    public Text textControlker;

    public GameObject mother;

    private void Start()
    {
        
        bulletPool = 100;
        bulletShot = 100;
        SetAmmoText(0);
        
    }

    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            if (bulletShot > 0)
            {
                Shoot();
                audiosurce.clip = shot;
                audiosurce.Play();
            }
        }

        
        if (scoreTotal >= 200)
        {
            mother.SetActive(true);


      
            //kill mother
            // switch to scene (night) where she is still on floor but player can move to her.
        }
    }


    public void Shoot()
    {

        Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        Vector2 exitPoint = new Vector2(exit_Point.transform.position.x, exit_Point.transform.position.y);
        Vector2 direction = target - exitPoint;
        direction.Normalize();

        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        transform.rotation = rotation;

        if (bulletShot <= bulletPool) // if we have shot less bullets than the bullet pool
        {
            SetAmmoText(1); // add one to the shot counter
            GameObject shot = (GameObject)Instantiate(shotPrefab, exitPoint, rotation); // instantiate bullet
            rb = shot.GetComponent<Rigidbody2D>(); // get the rigidbody component of bullet
            rb.AddForce(direction * speed); // add force to rigidbody
        }


    }
    public void SetAmmoText(int damage)
    {
        bulletShot = bulletShot - damage;

        if (bulletShot >= bulletPool) //if bullet shot greater than bullet pool
        {
            bulletShot = bulletPool; // bullet shot = bullet pool.
        }
        if (bulletShot <= 0) // if bullet shot less than 0
        {
            bulletShot = 0; //bullet shot = 0
        }
        ammoText.text = bulletShot.ToString();
   
    }

    public void UpdateScore(int score)
    {
        scoreTotal = scoreTotal + score;
        scoreText.text = scoreTotal.ToString();

      
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ammo")
        {
            bulletShot += 1;
            SetAmmoText(0);
            collision.gameObject.SetActive(false);
        }
    }



}
