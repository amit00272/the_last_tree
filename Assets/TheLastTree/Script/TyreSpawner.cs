using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyreSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public float SpawnIntv;

    public GameObject prefab;
    private IEnumerator coroutine;

    public float moveSpeed;

    //private List<int> directions;
    public int direction;
    void Start()
    {
        /*directions= new List<int>();
                directions.Add(-1);
                directions.Add(1);*/
        coroutine=LoseTime(SpawnIntv);
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
                
                //int direction=directions[Random.Range(0,directions.Count)];
               // direction=direction*-1;
                float scalex=1;

                if(direction<0){
                    scalex=-1;
                }else{
                 scalex=1;   
                }

                tyre.transform.localScale=new Vector3(scalex,1,1);

                var tyreObj=tyre.GetComponent<BurnigTyreMovmnt>();
                tyreObj.speed=moveSpeed;
                
            }
    }
  
}
