using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dolphinSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public float holdTime=5;

    private float holdTimeCounter;

    public GameObject dolphin;
    public float minY=-10.0f;
    private Vector3 initialPos;
    private Quaternion initialRotate;
    private FishController dolphinController;   

    void Start()
    {
        dolphinController=dolphin.GetComponent<FishController>();
        initialPos=dolphin.transform.position;
        initialRotate=dolphin.transform.rotation;
        holdTimeCounter=holdTime;

    }

    // Update is called once per frame

    private void FixedUpdate() {
       // Debug.Log(dolphin.transform.position.y);
    }
    void Update()
    {


      


        if(dolphin.transform.position.y<minY && !dolphinController.targetReady){

            
             dolphin.transform.position=initialPos;
             dolphin.transform.rotation=initialRotate;
             dolphin.GetComponent<Rigidbody2D>().velocity=new Vector3(0f,0f,0f); 
             dolphin.GetComponent<Rigidbody2D>().rotation=0.0f;
             //dolphinController.isDiving=false;
             //dolphinController.y_posTrkr=dolphin.transform.position.y;
             dolphin.gameObject.SetActive(false);
              StartCoroutine("LoseTime");

         }

          
        
    }

      IEnumerator LoseTime()
  {
    while (true) {
      yield return new WaitForSeconds (1);
      holdTimeCounter--; 
      if(holdTimeCounter==0){
          holdTimeCounter=holdTime;
          
         StopCoroutine("LoseTime");
         dolphin.transform.gameObject.SetActive(true);
         
         dolphinController.Launch();
         
      }
    }
  }
}
