using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SImpleToAndFrooAIMover : MonoBehaviour
{
    // Start is called before the first frame update

    public AIDestinationSetter destination;

    public Transform startPos;
    public Transform endPos;

    private bool isToDest=true;

    //Vector3 nextPos;
    void Start()
    {
        destination.target=endPos;
    }

    // Update is called once per frame
    void Update()
    {
        var heading = destination.target.position - transform.position;
        var distance = heading.magnitude;
                if(distance<0.5 && !isToDest ){

                    //nextPos=endPos.position;

                    destination.target=endPos;
                    isToDest=true;

                }else if(distance<0.5 && isToDest){

                    //nextPos=startPos.position;
                    destination.target=startPos;
                    isToDest=false;

                    //Debug.Log("AI destination reset");

                }
    }
}
