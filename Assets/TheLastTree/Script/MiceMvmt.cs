using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiceMvmt : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform startPos;
    public Transform endPos;
    public float speed;
    Vector3 nextPos;
    void Start()
    {
        nextPos=startPos.position;
    }

    // Update is called once per frame
    void Update()
    {
                if(transform.position==startPos.position){

                    nextPos=endPos.position;
                    flip();

                }else if(transform.position==endPos.position){

                    nextPos=startPos.position;
                    flip();

                }
                transform.position=Vector3.MoveTowards(transform.position,nextPos,speed*Time.deltaTime);
    }

    void flip(){

        Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
    }
}
