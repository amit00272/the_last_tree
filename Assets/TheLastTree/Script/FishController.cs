using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    // Start is called before the first frame update

    public float gravityModifier=1f;

    Vector3 gravity=Vector3.up * -4.81f;
   // public Vector3 velocity;
    private float _angle=60;

    public Vector3 targetPos;

    public bool targetReady=false;
    bool _rotate=true;

    public float y_posTrkr=0;

    public bool isDiving=false;



public bool isParabolic=false;
  



private void Awake() {
    
    
}
    void Start()
    {

        y_posTrkr=-14.0f;
        targetReady=true;
        if(targetReady)
         Launch();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!isDiving && transform.gameObject.activeInHierarchy){
          if( y_posTrkr<=transform.position.y){
              //Debug.Log("not diving");
             y_posTrkr=transform.position.y;
          }else{
              isDiving=true;
              transform.rotation=new Quaternion(0,0,180,0);
          }
         }
       // Debug.Log(isDiving);
       
	}

public void OnDisable() {
    
    y_posTrkr=-14.0f;
    isDiving=false;
   // Debug.Log("on disable called+++++++++++++");
    
    
}





     void FixedUpdate() {

        Vector3 vel = GetComponent<Rigidbody2D>().velocity;
         if(_rotate){

             //var rr=Quaternion.LookRotation(vel);
             
                  //transform.rotation = new Quaternion(0,0,rr.x,1);
            
	        
         }

    
             
           
    }
public void Launch()
{
    // source and target positions
    Vector3 pos = transform.position;
    //var ff=new Vector3(30,0,0);
    Vector3 target = transform.position-targetPos;

    // distance between target and source
    float dist = Vector3.Distance(pos, target);

    // rotate the object to face the target
    if(isParabolic)
        transform.LookAt(target);

    // calculate initival velocity required to land the cube on target using the formula (9)
    float Vi = Mathf.Sqrt(dist * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * _angle * 2)));
    float Vy, Vz;   // y,z components of the initial velocity

    Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * _angle);
    Vz = Vi * Mathf.Cos(Mathf.Deg2Rad * _angle);

    // create the velocity vector in local space
    Vector3 localVelocity = new Vector3(0f, Vy, Vz);
    
    // transform it to global vector
    Vector3 globalVelocity = transform.TransformVector(localVelocity);

    // launch the cube by setting its initial velocity
    GetComponent<Rigidbody2D>().velocity = globalVelocity;

    // after launch revert the switch
    targetReady = false;
} 

private void OnCollisionEnter2D(Collision2D other) {

    if(other.gameObject.name=="Player"){
            PlayUIManager.instance.onfailed();
        }

}

    
}
