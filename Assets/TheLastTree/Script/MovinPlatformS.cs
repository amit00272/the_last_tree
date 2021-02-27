using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovinPlatformS : MonoBehaviour
{

    public Transform startPos;
    public Transform endPos;
    public float speed;
    Vector3 nextPos;
    private Vector3 offset;
    private Vector3 initPlatPos;

    public bool isForFalling=false;
    private bool startFalling=false;
    // Start is called before the first frame update
    void Start()
    {
        if(!isForFalling){
    	    nextPos=startPos.position;
        }
        if(isForFalling){

            nextPos=endPos.position;

        }
        initPlatPos = startPos.position;

        Invoke("register", 0.5f);

    }
    private void register() {
         PlayUIManager.instance.OnPlayerResumed += resetPlatrformPos;
    }

    // Update is called once per frame
    void Update()
    {

            if(!isForFalling){
                if(transform.position==startPos.position){

                    nextPos=endPos.position;

                }else if(transform.position==endPos.position){

                    nextPos=startPos.position;

                }
                transform.position=Vector3.MoveTowards(transform.position,nextPos,speed*Time.deltaTime);
            }
            if(startFalling){

                transform.position=Vector3.MoveTowards(transform.position,nextPos,speed*Time.deltaTime);
            }


    }
    public void resetPlatrformPos(){
        Debug.Log("REsetPLatfrom called");
        gameObject.transform.position = initPlatPos;
            startFalling=false;
    }

     private void OnCollisionEnter2D(Collision2D other){

             if(other.gameObject.tag=="Player" && isForFalling){
                startFalling=true;
             }
             if(other.gameObject.tag=="Player"){
                    other.gameObject.transform.parent=gameObject.transform;
             }


     }

    //  private void OnCollisionExit2D(Collision2D other) {

    //      if(other.gameObject.tag=="Player" ){
    //                 other.gameObject.transform.parent=null;
    //          }
    //  }
     public void reattachOldParent(){

     }

     private void OnDisable() {
           PlayUIManager.instance.OnPlayerResumed -= resetPlatrformPos;
     }



}
