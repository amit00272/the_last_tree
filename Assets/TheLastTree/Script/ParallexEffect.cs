﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallexEffect : MonoBehaviour
{

    private float startPos;
    public GameObject cam;
    public float parallextEffect;
    // Start is called before the first frame update
    void Start()
    {
        startPos=transform.position.x;
    }

    // Update is called once per frame
    private void FixedUpdate() {
        
        float dist = (cam.transform.position.x*parallextEffect);
        transform.position=new Vector3(startPos-dist,transform.position.y,transform.position.z);
    }
   
}
