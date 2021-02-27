using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{

    public PathCreation.PathCreator pathCreator;
    public float speed=6;

    float distanceTravelled;

    private bool flipped=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distanceTravelled += speed*Time.deltaTime;
        transform.position=pathCreator.path.GetPointAtDistance(distanceTravelled);
        Quaternion rotate=pathCreator.path.GetRotationAtDistance(distanceTravelled);
        transform.rotation=Quaternion.Euler(rotate.x,rotate.y,rotate.z);

        if(rotate.x<0 && !flipped){

            transform.localScale=new Vector3(transform.localScale.x*-1,transform.localScale.y,transform.localScale.z);
            flipped=true;

        }else if(rotate.x>0 && flipped){
            flipped=false;
            transform.localScale=new Vector3(transform.localScale.x*-1,transform.localScale.y,transform.localScale.z);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        
        if(other.gameObject.name=="Player"){
            PlayUIManager.instance.onfailed();
        }
    }
}
