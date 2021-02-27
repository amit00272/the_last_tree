using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestcardOpen : MonoBehaviour
{

    public GameObject cardOpenPrefab;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow)){

               var gp = Instantiate(cardOpenPrefab,GameObject.FindGameObjectWithTag("uicanvas").transform);
                    gp.GetComponent<CardOpen>().unlockCommonCards(1);
        }
    }
}
