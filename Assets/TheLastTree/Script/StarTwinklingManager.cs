using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTwinklingManager : MonoBehaviour
{

    public GameObject starPrefab;

    public GameObject bg;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("st", 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void st(){
        StartCoroutine(startTwinkle());
    }


    IEnumerator startTwinkle(){

        var canpos = new Vector3(417,234,0);

        for(int i = 0; i < 50 ; i++){

         
            var scale = Random.Range(0.2f, 0.4f);
            var ls = new Vector3(scale,scale,1);
            var changepos = new Vector3(Random.Range(-960, 961), Random.Range(-540, 541), 0);
             
            var go =  Instantiate(starPrefab, Vector3.zero, transform.rotation);
                go.transform.SetParent(bg.transform, false);
                go.GetComponent<RectTransform>().localPosition = changepos;
               // go.GetComponent<RectTransform>().localScale = ls; 


            yield return new WaitForSeconds(0.01f);
        
        }

        yield return null;
    }
}
