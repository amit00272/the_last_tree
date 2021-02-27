using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceController : MonoBehaviour
{

    public Slider slider;
    public Text TravelledText;
    private int travelledDistance;
    private float endPointPosition;

    private GameObject player;

    private int virDistance ;

    private float playerInitialPosition;

    public Text distanceToTravel;

    public static int distanceTravelled;







    // Start is called before the first frame update
    void Start()
    {
        InitSlider();
        distanceTravelled = 0;
        

        
    }

    // Update is called once per frame
    void FixedUpdate(){

        if( player.transform.position.x < -playerInitialPosition) return;
        var travelPer = (((player.transform.position.x+playerInitialPosition) * 100) / endPointPosition);
        var ct = Convert.ToInt32((virDistance * travelPer) / 100);
        travelledDistance =/* ((PlayUIManager.currentLevel - 1) * 1000)+ */ct;
        distanceTravelled = travelledDistance;
        TravelledText.text = travelledDistance.ToString() ;
        SetProgress();
        CheckAndOpenCards(travelledDistance);
    }

     void Update() {
         
       /*if (Input.GetKeyDown(KeyCode.Space))
        {
            print("space key was pressed");
             PrefsManager.instance.IncreaseCommonVal(50);
        }*/
       

    }



    private void InitSlider(){

        travelledDistance = 0;// (PlayUIManager.currentLevel - 1) * 1000;
        virDistance = PlayUIManager.currentLevel < 6 ? 1000 : 500;
        endPointPosition =  GameObject.Find("endPoint").transform.position.x;
        player = GameObject.FindGameObjectWithTag("Player");
        playerInitialPosition  = Math.Abs( player.transform.position.x);
        endPointPosition += playerInitialPosition;
        distanceToTravel.text =  "/"+virDistance+"m";

    }

    //5500
    private void SetProgress(){

          var total =   ( travelledDistance * 100 ) / 1000;
          //var total =   ( travelledDistance * 100 ) / 5500;
          slider.value = (total / 100.0f);


    }

    private void CheckAndOpenCards(int ct){


        if(PlayUIManager.instance.continuePopup.activeInHierarchy ||
           PlayUIManager.instance.pausepannel.activeInHierarchy ||
           PlayUIManager.instance.gameOverPannel.activeInHierarchy ||
           PlayUIManager.instance.failedpannel.activeInHierarchy
         ) return;


        var nd  = ct + PrefsManager.instance.totalDistance;

        if(nd > 500 && nd < 1500){

            if(PrefsManager.instance.cardOpen[0] == false){

                PrefsManager.instance.cardOpen[0] =  true;
                PrefsManager.instance.Set500CardOpen();
                this.OpenCard(500);

            }

        }else if(nd > 1500 && nd < 3500){

            if(PrefsManager.instance.cardOpen[1] == false){

                PrefsManager.instance.cardOpen[1] =  true;
                PrefsManager.instance.Set1500CardOpen();
                this.OpenCard(1500);

            }

        }else if(nd > 3500 && nd < 6500){

            if(PrefsManager.instance.cardOpen[2] == false){

                PrefsManager.instance.cardOpen[2] =  true;
                PrefsManager.instance.Set3500CardOpen();
                this.OpenCard(3500);

            }

        }else if(nd > 6500 && nd < 10500){

            if(PrefsManager.instance.cardOpen[3] == false){

                PrefsManager.instance.cardOpen[3] =  true;
                PrefsManager.instance.Set6500CardOpen();
                this.OpenCard(6500);

            }

        }else if(nd > 10500 && nd < 15500){

            if(PrefsManager.instance.cardOpen[4] == false){

                PrefsManager.instance.cardOpen[4] =  true;
                PrefsManager.instance.Set10500CardOpen();
                this.OpenCard(10500);

            }
        }else if(nd > 15500){

            if(PrefsManager.instance.cardOpen[5] == false){

                PrefsManager.instance.cardOpen[5] =  true;
                PrefsManager.instance.Set15500CardOpen();
                PrefsManager.instance.resetTravelledCards();
                var extraDistance =   nd - 15500;
                PrefsManager.instance.SetDistanceTravelled(extraDistance);
                this.OpenCard(15500);


            }
        }


       // Debug.Log("Travelled distance"+ nd);




          /* if(tata ){
                  seedval =  seedval - 10;
                 PrefsManager.instance.SetSeedVal(seedval);
                if(randNum < 7) PrefsManager.instance.SetCommonVal();
                if(randNum < 9) PrefsManager.instance.SetRareVal();
                else PrefsManager.instance.SetLegendaryVal();
        }*/


    }


    private void OpenCard(int tempDist){

        var oi = UnityEngine.Random.Range(1, 101);

        Debug.Log("card open called with value of ="+oi);
        
        switch (tempDist)
        {
            case 500:   openCardWithPosiibility(90, 9, 1, oi); break;
            case 1500:  openCardWithPosiibility(85, 12, 3, oi); break;
            case 3500:  openCardWithPosiibility(80, 16, 4, oi); break;
            case 6500:  openCardWithPosiibility(70, 25, 5, oi); break;
            case 10500: openCardWithPosiibility(55, 39, 6, oi); break;
            case 15500: openCardWithPosiibility(45, 47, 8, oi); break;
            
        }

    }


    private  void openCardWithPosiibility( int commonPer, int rarePer, int lengendryperm, int actualVal){

              Debug.Log("card open with possibility called");
              
              if(actualVal < commonPer)
                PrefsManager.instance.IncreaseCommonVal(50);
              else if(actualVal < (commonPer + rarePer))  
                PrefsManager.instance.IncreaseRareVal(50);
              else PrefsManager.instance.IncreaseLegendaryVal(50);
              
              
              
              
    }

}

    /*
500 m  card n1  90% common 9% rare 1% legendary
1500 m card n2  85 % common  12% rare 3% legendary
3500 m card n3 80% common  16% rare 4% legendary
6500 m card n4 70% common  25% rare 5% legendary
10500 m card n5 55% common  39% rare 6% legendary
15500 m card n6 45%  common 47% rare 8% legendary
    */
