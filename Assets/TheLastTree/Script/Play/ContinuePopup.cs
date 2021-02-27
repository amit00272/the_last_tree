using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinuePopup : MonoBehaviour
{
   public Image progress;

     void OnEnable(){

        progress.fillAmount =  1.0f;
        StartCoroutine("startTimerForReward");
        
    }

    IEnumerator startTimerForReward(){
            
        float tempFillAmount = 1.0f;

        for(var i = 0; i < 10 ; i++){
            yield return new WaitForSeconds(0.8f);
            progress.fillAmount = tempFillAmount;
            tempFillAmount -= 0.1f;
        }

        PlayUIManager.instance.showOnfieldUI();
         AdMobAdManager.instance.ShowInterstitial();     
        gameObject.SetActive(false);       
    }

    public void VideoButtonClicked(){
      


    #if UNITY_EDITOR

        gameObject.SetActive(false);
        PlayUIManager.instance.ResumeGame();

    #else
        AdMobAdManager.instance.ShowRewardVideo();
        gameObject.SetActive(false);  
    #endif
        

    }

    void OnDisable() {
        StopCoroutine("startTimerForReward");   
    }

}
