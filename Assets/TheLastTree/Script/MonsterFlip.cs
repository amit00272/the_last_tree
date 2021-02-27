using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MonsterFlip : MonoBehaviour
{


    public AIPath monsterpath;
    private float scale=0.0f;

    
    
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.tag=="Bird"){

            scale=1.0f;

        }else{

            scale=0.3f;
        }
    }

    // Update is called once per frame
    void Update()
    {
       

           
            if(monsterpath.desiredVelocity.x>=0.01f){

                transform.localScale=new Vector3(scale,scale,1f);

            } else if(monsterpath.desiredVelocity.x <= - 0.01f){

                transform.localScale=new Vector3(-scale,scale,1f);
            }
        
    }

    

}
