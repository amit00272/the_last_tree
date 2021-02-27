using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerPlatformerController : PhysicsObject
{
   public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public float moveSpeed=0.8f;
    public bool jumpbool;

    public bool runactive ;

    // Use this for initialization
    void Awake () 
    {
        spriteRenderer = GetComponent<SpriteRenderer> ();    
        animator = GetComponent<Animator> ();
        
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;
        move.x =Input.GetAxis("Horizontal")*moveSpeed;
        
        if ((Input.GetButtonDown ("Jump"))&& grounded) {

            velocity.y = jumpTakeOffSpeed;
           
            
        } else if ((Input.GetButtonUp ("Jump"))) 
        {
            if (velocity.y > 0) {
            
                velocity.y = jumpTakeOffSpeed*0.5f;
                
            }
          
        }

        animator.SetBool ("grounded", grounded);
        animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }   

    public void jumpClicked(){
        Debug.Log("jump clicked");
        jumpbool =  true;

    } 

    public void runClickedDown(){

        Debug.Log("run clicked down");
        //movexval = 0.8f;
        runactive = true;
        run();
    }

     public void runClickedUP(){
       //movexval = 0f;
       runactive = false;
       Debug.Log("run clicked up");

    }

    IEnumerator jumpi(){

        animator.SetInteger("state",3);
        yield return new WaitForSeconds(0.40f);
        yield return null;
    }


    public void jump(){

       // animator.SetInteger("state",3);
       // StartCoroutine(jumpi());
    }
    public void stand(){
       // animator.SetInteger("state",1);
    }
    public void run(){

        //animator.SetInteger("state",2);
    }

// private void OnCollisionEnter2D(Collision2D other){

//             if(other.gameObject.tag=="movingPlatform")
//                 transform.parent=other.gameObject.transform;
        
//     }
//     private void OnCollisionExit2D(Collision2D other){
        
//         if(other.gameObject.tag=="movingPlatform")
//             transform.parent=null;
//     }


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    
}
