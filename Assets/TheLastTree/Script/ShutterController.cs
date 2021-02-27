using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShutterController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject monster;
    public GameObject controls; 
    public GameObject shutterTop; 
    public GameObject shutterBottom; 

    void Start()
    {

        monster.SetActive(false);
        controls.SetActive(false);
        Invoke("StartShutter", 1f);
        
    }

    void StartShutter(){


        Sequence mySequence = DOTween.Sequence();
            mySequence.Join(shutterTop.transform.DOLocalMoveY(850, 3f));
            mySequence.Join(shutterBottom.transform.DOLocalMoveY(-850, 3f));
            mySequence.OnComplete( () => { 

                controls.SetActive(true);
                monster.SetActive(true);
                DestroyImmediate(gameObject);
                        
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
