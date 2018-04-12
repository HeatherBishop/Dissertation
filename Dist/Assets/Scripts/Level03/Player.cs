using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Player : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Rigidbody2D rigidbodys;
    public bool canMove;
    public bool isHiding;
    public GameObject bleeding;


    //security cam
    public bool isSeen;
    public GameObject cameaobj;

    //audio
    AudioSource audios;
   public AudioClip shoot;
   public AudioClip die;
   public AudioClip hide;
   public AudioClip siren;

    public AudioClip eat;


    //mother death
    public SpriteRenderer motherAttached;
    public GameObject deadMother;
    public GameObject finalSpotlight;

    public GameObject child;

    //Father object
    public GameObject fatherObj;

    public Animator playerAnim;
    public Animator childAnim;

    public Sprite currentSprite;
    public Sprite birthSprite;

    public GameObject heart;


    //screen fade
    public Animator fadeAnim;
    public Image fadeImage;

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerAnim.SetBool("walk", true);
        child = transform.GetChild(0).gameObject;
        rigidbodys = GetComponent<Rigidbody2D>();
        canMove = true;
        isSeen = false;
        audios = GetComponent<AudioSource>();
        motherAttached = this.gameObject.GetComponent<SpriteRenderer>();
        
    }


    void FixedUpdate()
    {

        if (canMove)
        {

            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rigidbodys.velocity = movement * speed;

            playerAnim.SetBool("cryWalk", true);
        }
        else
        {
            playerAnim.SetBool("cryWalk", false);
            rigidbodys.velocity = Vector2.zero;
        }




        if (isSeen)
        {

            cameaobj.transform.position = this.gameObject.transform.position;

        }







        //rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt);
    }




    IEnumerator ChildBorn()
    {
         canMove = false; //causes bug where player move automatically to the right.
        playerAnim.SetTrigger("Birth");

        Color childStartCol = Color.white;
        Color birthColor = Color.grey;
        Color reds = Color.grey;
        

        gameObject.GetComponent<SpriteRenderer>().color = reds;
        child.GetComponent<SpriteRenderer>().color = birthColor;
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().color = childStartCol;
        child.GetComponent<SpriteRenderer>().color = childStartCol;
        heart.gameObject.SetActive(true);
        playerAnim.SetTrigger("Birth");
        gameObject.GetComponent<SpriteRenderer>().color = reds;
        child.GetComponent<SpriteRenderer>().color = birthColor;
        yield return new WaitForSeconds(0.5f);
        heart.gameObject.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().color = childStartCol;
        child.GetComponent<SpriteRenderer>().color = childStartCol;
        //make the child object bigger
        gameObject.transform.GetChild(0).localScale += new Vector3(0.1f, 0.1f, 0);
        gameObject.GetComponent<SpriteRenderer>().color = reds;
        child.GetComponent<SpriteRenderer>().color = birthColor;
        playerAnim.SetTrigger("Birth");
        heart.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        heart.gameObject.SetActive(false);
        gameObject.transform.GetChild(0).localScale += new Vector3(0.1f, 0.1f, 0);
        gameObject.GetComponent<SpriteRenderer>().color = childStartCol;
        child.GetComponent<SpriteRenderer>().color = childStartCol;
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().color = reds;
        child.GetComponent<SpriteRenderer>().color = birthColor;
        playerAnim.SetTrigger("Birth");
        gameObject.transform.GetChild(0).localPosition = new Vector2(-0.3f, -0.08f);
        //make the child object bigger
        gameObject.GetComponent<SpriteRenderer>().color = childStartCol;
        child.GetComponent<SpriteRenderer>().color = childStartCol;
        heart.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        heart.gameObject.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().color = reds;
        child.GetComponent<SpriteRenderer>().color = birthColor;
        playerAnim.SetTrigger("Birth");
        gameObject.transform.GetChild(0).localScale += new Vector3(0.1f, 0.1f, 0);
    
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().color = childStartCol;
        child.GetComponent<SpriteRenderer>().color = childStartCol;
        gameObject.transform.GetChild(0).localPosition = new Vector2(-0.5f, -0.12f);
        child.GetComponent<SpriteRenderer>().color = birthColor;
        gameObject.GetComponent<SpriteRenderer>().color = reds;
        playerAnim.SetTrigger("Birth");

        yield return new WaitForSeconds(1);

        gameObject.GetComponent<SpriteRenderer>().color = childStartCol;
        child.GetComponent<SpriteRenderer>().color = childStartCol;
        child.GetComponent<SpriteRenderer>().sprite = birthSprite;
        heart.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        heart.gameObject.SetActive(false);
        child.GetComponent<SpriteRenderer>().sprite = currentSprite;
        childAnim.SetBool("walk", true);
        canMove = true;
        
    }

    IEnumerator GunShot()
    {
        //make the siren play

        //world gets darker

        //yield return new WaitForSeconds(2);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        audios.clip = shoot;
        audios.volume = 1f;
            audios.Play();

            yield return new WaitForSeconds(1);
        isSeen = false;
        StartCoroutine(ScreenFade());
        
        SceneManager.LoadScene(4);

        

    }
    IEnumerator ScreenFade()
    {
        fadeAnim.SetBool("fade", true);
        yield return new WaitUntil(() => fadeImage.color.a == 1);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(2);
    }
    IEnumerator Foods()
    {
        Color childStartCol = Color.white;

        Color reds = Color.clear;

        gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = reds;
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = childStartCol;
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = reds;
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = childStartCol;


    }

    IEnumerator MotherDie()
    {
        canMove = false;
        // audios.clip = siren;
        cameaobj = finalSpotlight;
        isSeen = true;
        finalSpotlight.GetComponent<AudioSource>().clip = siren;
        finalSpotlight.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2);
        audios.clip = shoot;
        audios.volume = 1f;
        audios.Play();
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        //world gets darker
        motherAttached.enabled = false;
        deadMother.SetActive(true);
        finalSpotlight.SetActive(false);
        yield return new WaitForSeconds(2);
        canMove = true;

        // transform.position = new Vector3(Mathf.PingPong(Time.time, 3), transform.position.y, transform.position.z);


        // yield return new WaitForSeconds(4);

        //change to end scene

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

 

        if (collision.gameObject.tag == "End")
        {
            StartCoroutine(ScreenFade());
            SceneManager.LoadScene(4);
        }
        if (collision.gameObject.tag == "Hide")
        {
            isHiding = true;
            isSeen = false;
            cameaobj = null;
        //   collision.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
            tmp.a = 0.5f;
            gameObject.GetComponent<SpriteRenderer>().color = tmp;

            audios.clip = hide;
            audios.volume = 0.3f;
            audios.Play();


        }
        if (collision.gameObject.tag == "Food")
        {
            StartCoroutine(Foods());


            audios.clip = eat;
            audios.Play();
            gameObject.transform.GetChild(0).localScale += new Vector3(0.1f, 0.1f,0);



            collision.gameObject.SetActive(false);

        }


        if (collision.gameObject.tag == "Security")
        {
            if (isHiding == false)
            {
                isSeen = true;
                collision.GetComponent<PlatformController>().canMove = false;
                collision.GetComponent<PlatformController>().audiosource.clip = siren;
                collision.GetComponent<PlatformController>().audiosource.Play();
                cameaobj = collision.gameObject;
                StartCoroutine(GunShot());
            }


        }

        if (collision.gameObject.tag == "Birth")
        {


            collision.GetComponent<Collider2D>().enabled = false;            
            StartCoroutine(ChildBorn());
        }

        if (collision.gameObject.tag == "Finish")
        {
            finalSpotlight.SetActive(true);
            StartCoroutine(MotherDie());
        }

        if (collision.gameObject.tag == "Hurt")
        {
            bleeding.SetActive(true);

            speed = speed - 0.5f;
            
        }
        if (collision.gameObject.tag == "FatherDie")
        {
            fatherObj.GetComponent<FatherControler>().fatherDie = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Hide")
        {
            audios.Stop();
            isHiding = false;
            Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
            tmp.a = 1f;
            gameObject.GetComponent<SpriteRenderer>().color = tmp;
          // collision.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "Finish")
        {
            finalSpotlight.SetActive(false);
            //collision.gameObject.SetActive(false);

        }
    }
}