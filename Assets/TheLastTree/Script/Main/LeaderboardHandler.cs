using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using Proyecto26;
using UnityEngine;

public class LeaderboardHandler : MonoBehaviour
{

    public static LeaderboardHandler instance = null;
    private dreamloLeaderBoard dreamloRef;
    // private LeaderboardDataRequest leaderboardDataRequest;
    //private LogEventRequest logEventRequest;


    void Awake ()
    {
        if (instance != null){

            Destroy(gameObject);
            return;
        }
        instance = this;
        dreamloRef = dreamloLeaderBoard.Instance;
        // leaderboardDataRequest =  new LeaderboardDataRequest();
        //logEventRequest =  new LogEventRequest();
        DontDestroyOnLoad(gameObject);     
    }

    void Start(){
         
        

    }

  

    public void SetScore(){

       
        var totaldtravel =  PrefsManager.instance.getDistanceTravelled();
        var newdist =  totaldtravel + DistanceController.distanceTravelled;
        if(newdist > 15500){

            var ty1 = 15500 - newdist;
            PrefsManager.instance.resetTravelledCards();
            PrefsManager.instance.SetDistanceTravelled(ty1);
        
        }else{
        
            PrefsManager.instance.SetDistanceTravelled(newdist);
        
        }
            PrefsManager.instance.totalDistance =  PrefsManager.instance.getDistanceTravelled();


       // dreamloRef.AddScore(PrefsManager.instance.getUserName(), DistanceController.distanceTravelled);


        //Debug.Log("new data on tavel distance :--===" + PrefsManager.instance.totalDistance);
       // if (DistanceController.distanceTravelled <= 0 || DistanceController.distanceTravelled <= PrefsManager.instance.getHighScore() ) return;
        
          

        var usr =  new User();
        usr.userName = PrefsManager.instance.getUserName();
        usr.userScore = DistanceController.distanceTravelled;
        usr.userid =  PrefsManager.instance.getUserID();
        PrefsManager.instance.SetHighScore(usr.userScore);

        dreamloRef.AddScore(PrefsManager.instance.getUserName(), DistanceController.distanceTravelled);



        RestClient.Post("https://tlsttree-b30cb.firebaseio.com/.json", usr);

    }

  
    public void getScore(){
        
        /* StartCoroutine(GetUserRoutine());
          RestClient.Get<string>("https://tlsttree-b30cb.firebaseio.com/.json").Then( response => {
                 Debug.Log(JsonUtility.ToJson(response) );
                
            }); */

            
    }

}
