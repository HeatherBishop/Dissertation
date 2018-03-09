using UnityEngine;
using UnityEngine.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public float maxJumpHeight = 4f;
    public float minJumpHeight = 1f;
    public float timeToJumpApex = .4f;
    private float accelerationTimeAirborne = .2f;
    private float accelerationTimeGrounded = .1f;
    private float moveSpeed = 6f;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    public bool canDoubleJump;
    private bool isDoubleJumping = false;

    public float wallSlideSpeedMax = 3f;
    public float wallStickTime = .25f;
    private float timeToWallUnstick;

    private float gravity;
    private float maxJumpVelocity;
    private float minJumpVelocity;
    private Vector3 velocity;
    private float velocityXSmoothing;

    private Controller2D controller;

    private Vector2 directionalInput;
    private bool wallSliding;
    private int wallDirX;

    // for gun
   public GameObject gunObj;

    //for checkpoints
    public Transform startPos;
    public Sprite checkpointActive;

    //for carrying light
    private bool isCarrying;
    public GameObject lightObj;
    public GameObject lightTarget;


    //for the text that displays throughout the levels.
    public GameObject sisterCanvasObj;
    public GameObject playerCanvasObj;
    public Text sisterDialogue;
    public Text playerDialogue;

    public string sister01 = "Stop giving me your light. It's too risky. I don't trust it.";
    public string player01 = "Please, trust me.";

    public string sister02 = "Why do you keep doing this!?";
    public string player02 = "Take your damn light!";

    public string sister03 = "Brother, please! You can't convince me.";
    public string player03 = "Don't give up, we're nearly there.";

    public string sister04 = "I deserve this... You can't save me.";
    public string player04 = "Please, someone help us!";

    public string sister05 = "Thank you, my brother.";
    public string player05 = "Take my light.";

    public int level;

    public bool playerMove;

    //sister AI
    public GameObject sisterObj;
    public bool sisterMove;
    public bool carrySister;
    public GameObject sisterTarget;


    //animation controller
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        sisterObj = GameObject.FindGameObjectWithTag("Sister");
        playerMove = true;
        controller = GetComponent<Controller2D>();
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    private void Update()
    {

        if (playerMove)
        {
            CalculateVelocity();
            HandleWallSliding();

            controller.Move(velocity * Time.deltaTime, directionalInput);
            anim.SetFloat("Movement", 1);

            if (controller.collisions.above || controller.collisions.below)
            {
                velocity.y = 0f;
            }

            if (isCarrying)
            {
                //set that objects transform to the shoot from transform
                lightObj.transform.position = lightTarget.transform.position;
               // moveSpeed = 4f;

            }
            if (carrySister)
            {
                sisterTarget.SetActive(true);
            }
           
        }
 

        if (sisterMove)
        {
            sisterObj.transform.position += new Vector3(5, 0, 0) * Time.deltaTime;
        }
    }

    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

    public void OnJumpInputDown()
    {
        anim.SetFloat("Movement", 0);
        anim.SetTrigger("isJumping");
        if (wallSliding)
        {
            if (wallDirX == directionalInput.x)
            {
                velocity.x = -wallDirX * wallJumpClimb.x;
                velocity.y = wallJumpClimb.y;
            }
            else if (directionalInput.x == 0)
            {
                velocity.x = -wallDirX * wallJumpOff.x;
                velocity.y = wallJumpOff.y;
            }
            else
            {
                velocity.x = -wallDirX * wallLeap.x;
                velocity.y = wallLeap.y;
            }
            isDoubleJumping = false;
        }
        if (controller.collisions.below)
        {
            velocity.y = maxJumpVelocity;
            isDoubleJumping = false;
        }
        if (canDoubleJump && !controller.collisions.below && !isDoubleJumping && !wallSliding)
        {
            velocity.y = maxJumpVelocity;
            isDoubleJumping = true;
        }
    }

    public void OnJumpInputUp()
    {
        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
        }
    }

    private void HandleWallSliding()
    {
        wallDirX = (controller.collisions.left) ? -1 : 1;
        wallSliding = false;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            wallSliding = true;

            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0f)
            {
                velocityXSmoothing = 0f;
                velocity.x = 0f;
                if (directionalInput.x != wallDirX && directionalInput.x != 0f)
                {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }
            }
            else
            {
                timeToWallUnstick = wallStickTime;
            }
        }
    }






    private void CalculateVelocity()
    {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below ? accelerationTimeGrounded : accelerationTimeAirborne));
        velocity.y += gravity * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collect ammo

        if (collision.gameObject.tag == "Ammo")
        {
            //set the light object to collision object
            lightObj = collision.gameObject;
            isCarrying = true;



          //  if (gunObj.GetComponent<RaycastGun>().bulletShot <= gunObj.GetComponent<RaycastGun>().bulletPool)
            //{
              //  gunObj.GetComponent<RaycastGun>().bulletShot += 1;
                //gunObj.GetComponent<RaycastGun>().SetAmmoText(0);
            //}

            //collision.gameObject.SetActive(false);

        }
        if (collision.gameObject.tag == "Finish")
        {

            collision.gameObject.GetComponent<Collider2D>().enabled = false;


         
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (collision.gameObject.tag == "Drop")
        {
            playerMove = false;
            isCarrying = false;
            lightObj = null;
            sisterDialogue.GetComponent<TextController>().fullText = ".....";
            sisterDialogue.GetComponent<TextController>().StartShowText();
            playerDialogue.GetComponent<PlayetTextController>().fullText = "";
            playerDialogue.GetComponent<PlayetTextController>().StartShowText();
        }

        if (collision.gameObject.tag == "Sister")
        {
            carrySister = true;
            sisterObj.SetActive(false);
        }

        //if collide with water die and restart
        if (collision.gameObject.tag == "Hazard")
        {
            anim.SetTrigger("isDead");
            transform.position = new Vector2 (startPos.position.x, startPos.position.y + 2);
        }

        if (collision.gameObject.tag == "Respawn")
        {
            startPos = collision.gameObject.transform;
            collision.GetComponent<SpriteRenderer>().sprite = checkpointActive;
        }

        if (collision.gameObject.tag == "01")
        {

            playerMove = false;
            isCarrying = false;
            lightObj = null;
          //  sisterDialogue.text = sister01;
         //   playerDialogue.text = player01;
         //   playerDialogue.GetComponent<TextController>().fullText = player01;
            sisterDialogue.GetComponent<TextController>().fullText = sister01;            
            sisterDialogue.GetComponent<TextController>().StartShowText();
            playerDialogue.GetComponent<PlayetTextController>().fullText = player01;
            playerDialogue.GetComponent<PlayetTextController>().StartShowText();
            
            Debug.Log("End 01");
            


        }

        if (collision.gameObject.tag == "02")

        {
            playerMove = false;
            isCarrying = false;
            lightObj = null;


            sisterDialogue.GetComponent<TextController>().fullText = sister02;            
            sisterDialogue.GetComponent<TextController>().StartShowText();
            playerDialogue.GetComponent<PlayetTextController>().fullText = player02;
            playerDialogue.GetComponent<PlayetTextController>().StartShowText();
            //sister ai - sister walks away

        }

        if (collision.gameObject.tag == "03")
        {
            playerMove = false;
            isCarrying = false;
            lightObj = null;

            sisterDialogue.GetComponent<TextController>().fullText = sister03;          
            sisterDialogue.GetComponent<TextController>().StartShowText();

            playerDialogue.GetComponent<PlayetTextController>().fullText = player03;
            playerDialogue.GetComponent<PlayetTextController>().StartShowText();
            //sister ai - sister is dizzy, then walks away slowly
        }
        if (collision.gameObject.tag == "04")
        {
            playerMove = false;
            isCarrying = false;
            lightObj = null;

            sisterDialogue.GetComponent<TextController>().fullText = sister04;
           
            sisterDialogue.GetComponent<TextController>().StartShowText();

            playerDialogue.GetComponent<PlayetTextController>().fullText = player04;
            playerDialogue.GetComponent<PlayetTextController>().StartShowText();
            //sister ai - sister is hurt and falls and crawls.
        }
        if (collision.gameObject.tag == "05")
        {
            sisterTarget.SetActive(false);
            sisterObj.SetActive(true);
            playerMove = false;
            isCarrying = false;

            lightObj = null;
            sisterDialogue.GetComponent<TextController>().fullText = sister05;

            sisterDialogue.GetComponent<TextController>().StartShowText();

            playerDialogue.GetComponent<PlayetTextController>().fullText = player05;
            playerDialogue.GetComponent<PlayetTextController>().StartShowText();
            //sister ai - sister is laid and is carried.
        }
        if (collision.gameObject.tag == "06")
        {
            playerMove = false;
            isCarrying = false;
            lightObj = null;

            //sister ai - sister is laid on altar, brother gives her light.
        }
    }

  


}
