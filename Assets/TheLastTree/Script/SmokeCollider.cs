using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeCollider : MonoBehaviour
{
    // Start is called before the first frame update
    private bool gameOver=false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

      private void OnCollisionEnter2D(Collision2D other){

             if(other.gameObject.tag=="Player" && !gameOver){
                 //gameOver=true;
                //Camera.main.transform.position =  new Vector3(Camera.main.transform.position.x, 1.18f,Camera.main.transform.position.z);
                //PlayUIManager.instance.GameOver();
                //return;
             }
             
                
        
     }

     private void OnCollisionExit2D(Collision2D other) {
         
         if(other.gameObject.tag=="Player" ){
                  
             }
     }
}
