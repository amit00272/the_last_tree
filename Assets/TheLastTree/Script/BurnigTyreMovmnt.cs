using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnigTyreMovmnt : MonoBehaviour
{
    // Start is called before the first frame update
   
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update() {
        
        if(transform.position.y<-5.0f){

                Object.Destroy(gameObject);
        }
    }

    // Update is called once per frame
    private void FixedUpdate() {
        
        float dist = (speed*transform.localScale.x*-1);
        transform.position=new Vector3(transform.position.x+dist,transform.position.y,transform.position.z);
    }
}
