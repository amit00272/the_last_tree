using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;

    public Animator animator;
	public float runSpeed = 5f;
    private float recoveryTime = 1.25f;

    float horizontalMove = 0f;
	bool jump = false;
    bool moveBack=false;
	bool crouch = false;
    public bool isCatchedByMoster = false;
    public bool playerHit = false;

    public float leftEndX=0.0f;


    public float localX=0.0f;


    //Upgrade Value of Player for [(shoes, tshirt, trouser) (0 - 5)]
    private int shoeUpgradeVal;
    private int tshirtUpgradeVal;
    private int trouserUpgradeVal;
    //**************************************************************/



    private void Start()
    {

        shoeUpgradeVal =  PrefsManager.instance.getShoesVal();
        tshirtUpgradeVal  =  PrefsManager.instance.getTShirtVal();
        trouserUpgradeVal =  PrefsManager.instance.getTrouserVal();
        //Debug.Log("SHoe"+shoeUpgradeVal);
        //Debug.Log("Tshirt"+tshirtUpgradeVal);
        //Debug.Log("Trouser"+trouserUpgradeVal);

        //animator.SetFloat("Speed", 0);
        //animator.SetBool("isGrounded", false);
        //animator.SetFloat("yVelocity",0);
        HandlePlayerUpgrades();


    }

    public void onResume() {
        Debug.Log("In Player onEnable.....");
        if(isCatchedByMoster){
            isCatchedByMoster = false;
             animator.SetBool("isDeath", false);
            animator.SetBool("isJumping",true);
            Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
                    tmp.a = 1.0f;
            gameObject.GetComponent<SpriteRenderer>().color = tmp;
        }
        if(playerHit){
            playerHit = false;
             animator.SetBool("plyrHit", false);
            animator.SetBool("isJumping",true);
        }
    }

    public void HandlePlayerUpgrades(){

        recoveryTime -= (tshirtUpgradeVal * 0.25f);

        runSpeed += shoeUpgradeVal;

        gameObject.GetComponent<CharacterController2D>().increaseJumpForce(trouserUpgradeVal*10);

    }

    // Update is called once per frame
    void Update () {


        if (isCatchedByMoster)
        {
            horizontalMove = 0.0f;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            jump = false;
            animator.SetBool("isGrounded", controller.m_Grounded);
            animator.SetFloat("yVelocity", controller.m_Rigidbody2D.velocity.y);
            animator.SetBool("isDeath", true);
            return;

        }

        if (playerHit)
        {
            //horizontalMove = 0.0f;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            animator.SetBool("plyrHit", true);
            animator.SetBool("isGrounded",true);
            //animator.SetFloat("yVelocity",0.0f);
            animator.SetFloat("yVelocity",controller.m_Rigidbody2D.velocity.y);
            return;
        }



        if(Application.platform==RuntimePlatform.OSXEditor || Application.platform==RuntimePlatform.WindowsEditor)
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Application.platform==RuntimePlatform.Android)
            horizontalMove = CrossPlatformInputManager.GetAxisRaw("Horizontal") * runSpeed;


    if(transform.position.x<leftEndX && horizontalMove<0)
        horizontalMove = 0.0f;

        animator.SetFloat("Speed",Mathf.Abs(horizontalMove));

        if(Application.platform==RuntimePlatform.OSXEditor || Application.platform==RuntimePlatform.WindowsEditor)
        {
		    if (Input.GetButtonDown("Jump"))
		    {
			    jump = true;
			    animator.SetBool("isJumping",true);
			    //Debug.Log("grounded false");
		    }
        }

        if (Application.platform==RuntimePlatform.Android){
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
			jump = true;
			animator.SetBool("isJumping",true);
			//Debug.Log("grounded false");
		    }
        }

        // if (Input.GetButtonDown("Crouch"))
        // {
        // 	crouch = true;
        // } else if (Input.GetButtonUp("Crouch"))
        // {
        // 	crouch = false;
        // }

        animator.SetBool("isGrounded",controller.m_Grounded);
		animator.SetFloat("yVelocity",controller.m_Rigidbody2D.velocity.y);

	}

	public void onLanding(){

		animator.SetBool("isJumping",false);
		//Debug.Log("grounded true");
	}

	void FixedUpdate ()
	{

           if(!moveBack){

                controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
           }
           else{

                controller.MoveBack(horizontalMove*Time.fixedDeltaTime,jump);
            }
            jump = false;

	}




public void catchedByMonster(){

        if( !isCatchedByMoster)
        {
            isCatchedByMoster = true;
            StartCoroutine("openGameOver");
        }
}

    IEnumerator openGameOver()
    {
        yield return new WaitForSeconds(2);
        StopCoroutine("openGameOver");
        PlayUIManager.instance.onfailed();

    }

    IEnumerator MoveBack(){

       // controller.MoveBack(horizontalMove*Time.fixedDeltaTime,moveBack);
        yield return new WaitForSeconds(0.5f);
        StopCoroutine("MoveBack");
        horizontalMove=0.0f;
        moveBack=false;

    }
    IEnumerator playerHitAnim()
    {
        yield return new WaitForSeconds(recoveryTime);
        playerHit = false;

        animator.SetBool("plyrHit", false);
        //Debug.Log("On playerHit stopped");
        StopCoroutine("playerHitAnim");

    }

   public void onPlayerHit()
    {

        //Debug.Log("On playerHit");
        if (playerHit)
            return;
        playerHit = true;
        moveBack=true;

         var rigidBody=gameObject.GetComponent<Rigidbody2D>();

         if(rigidBody.velocity.y>0)
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 2.0f),ForceMode2D.Impulse);
         else
          gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 2.0f),ForceMode2D.Impulse);
//transform.DOShakeRotation(2,20, 5, 20);
        //Camera.main.transform.DOShakeRotation(1,2, 5, 20);
        animator.SetBool("plyrHit", true);
        animator.SetBool("isJumping", false);
        StartCoroutine("playerHitAnim");
        StartCoroutine("MoveBack");
    }

    /// <summary>
    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionExit2D(Collision2D other)
    {

    }

    /// <summary>
    /// Sent each frame where a collider on another object is touching
    /// this object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionStay2D(Collision2D other)
    {
       // Debug.Log(other.gameObject.name);
        if(other.gameObject.name == "Tilemap" && controller.m_Grounded){

            //controller.lastGroundPos = controller.m_GroundCheck.position;
        }
    }



}
