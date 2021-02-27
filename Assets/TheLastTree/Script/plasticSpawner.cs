using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plasticSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public float SpawnIntv;

    public GameObject prefab;
    private IEnumerator coroutine;

    public float moveSpeed;

    public bool enableRandomScaling = false;
    public bool isPlastic = false;
    
    private List<float> scales;
    public int direction;

    void Start()
    {
        scales = new List<float>();
        if (!isPlastic)
        {
            // scales.Add(1.0f);
            // scales.Add(0.8f);
            // scales.Add(0.9f);
            scales.Add(0.4f);
            scales.Add(0.5f);
            scales.Add(0.4f);
        }
        scales.Add(0.5f);
        scales.Add(0.4f);
       
        coroutine =LoseTime(SpawnIntv);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        IEnumerator LoseTime(float waitTime)
    {
            while (true) {
                yield return new WaitForSeconds (waitTime);
            
                GameObject tyre=Instantiate(prefab,transform.position,Quaternion.identity);
                
               
                float scalex=1;

                if(direction<0){
                    scalex=-1;
                }else{
                 scalex=1;   
                }


                tyre.transform.localScale=new Vector3(scalex,1,1);


            if (enableRandomScaling)
            {
                float scale=scales[Random.Range(0,scales.Count)];
                // direction=direction*-1;
                if (direction < 0)
                {
                    float scalx = scale * -1;
                    tyre.transform.localScale = new Vector3(scalx, scale, 1);
                }
                else
                {
                    tyre.transform.localScale = new Vector3(scale, scale, 1);
                }


            }

            var tyreObj=tyre.GetComponent<plasticMvmnt>();
            if (isPlastic)
            {
                float[] movSpeeds = new float[5] { 0.09f, 0.08f, 0.03f, 0.06f, 0.02f };
                moveSpeed= movSpeeds[Random.Range(0, movSpeeds.Length)];
            }
            if(tyreObj)
                tyreObj.speed=moveSpeed;
                
            }
    }
  
}
