using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class UnityAdsManager : MonoBehaviour, IUnityAdsListener
{

    public static UnityAdsManager instance = null;
    public string gameId = "3286598";
    public bool testMode = true;

    private string bannerID = "banner";
    private string interstitialID = "interstitial";
    private string rewardID = "rewardedVideo";


    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null){

            Destroy(gameObject);
            return;
        }

        instance = this;

        setUpAds();
        DontDestroyOnLoad(gameObject);  
    }

    private void setUpAds(){
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId,testMode); 
        Advertisement.Banner.SetPosition (BannerPosition.BOTTOM_CENTER);
    }

    public bool isInterstitialAvailble(){
        return Advertisement.IsReady(interstitialID);
    }
    public void ShowInterstitial(){

        if(isInterstitialAvailble()) 
            Advertisement.Show(interstitialID);
    }

    public bool isRewardAvailble(){
        return Advertisement.IsReady(rewardID);
    }
    public void ShowRewardedVideo(){

        if(isRewardAvailble()) 
            Advertisement.Show(rewardID);
    }

    IEnumerator ShowBannerWhenReady () {
        while (!Advertisement.IsReady (bannerID)) {
            yield return new WaitForSeconds (0.5f);
        }
        Advertisement.Banner.Show (bannerID);
    }

    public void showBanner(){
        StartCoroutine(ShowBannerWhenReady());
    }

    public void removeBanner(){
        Advertisement.Banner.Hide();
    }

    

     public void OnUnityAdsDidFinish (string placementId, ShowResult showResult) {

         if(placementId == rewardID)
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished) {
            
             
        } else if (showResult == ShowResult.Skipped) {

            // Do not reward the user for skipping the ad.
        } else if (showResult == ShowResult.Failed) {
           // Debug.LogWarning (“The ad did not finish due to an error.);
        }
    }

    public void OnUnityAdsReady (string placementId) {
        // If the ready Placement is rewarded, show the ad:
       // if (placementId == myPlacementId) {
         //   Advertisement.Show (myPlacementId);
        //}
    }

    public void OnUnityAdsDidError (string message) {
        // Log the error.
    }

    public void OnUnityAdsDidStart (string placementId) {
        // Optional actions to take when the end-users triggers an ad.
    }
}
