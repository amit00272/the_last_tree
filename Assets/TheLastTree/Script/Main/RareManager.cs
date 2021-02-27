using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RareManager : MonoBehaviour
{

    public Image[] cards;
    public Slider[] progressSlider;
    // Start is called before the first frame update
    void Start()
    {
        setUpProgress();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


     private void setUpProgress(){


            for(int i = 0 ; i < cards.Length ; i++){

                var per =  PrefsManager.instance.getRareVal(i+1);
                var tempColor =  cards[i].color;
                    tempColor.a = per == 0 ? 0.5f :  0.5f + ((0.5f * per) /100);
                
                progressSlider[i].value =  per;
                cards[i].color =  tempColor;

            }

    }
}
