using System;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdMobAdManager : MonoBehaviour {
    
    public static AdMobAdManager instance = null;

    [Header (" Setting ")]
    public bool enableTestAds;

    [Header (" Android ")]
    public string appidandroid;
    public string bannerandroid;
    public string interstitialandroid;
    public string rewardAndroid;

    #region Admob
    [Header (" iOS ")]

    public string appidios;
    public string bannerios;
    public string interstitialios;
    public string rewardios;
   
   
    [Header ("Test IDS")]
    public string appidtest;
    public string bannertest;
    public string interstitialtest;
    public string rewardtest;


    #endregion


    private string app_id = "";
    private  string bannerid = "";
    private string interstitialID = "";
    private string rewardID = "";
    private BannerView smallbannerView;
    private InterstitialAd interstitial;
    private int gdprval;
    private RewardBasedVideoAd   rewardBasedVideo;
    private bool rewarded;


    void Awake ()
    {
        if (instance != null){

            Destroy(gameObject);
            return;
        }

        instance = this;
        gdprval = PlayerPrefs.GetInt("ngdpr",0);
        
        SetUpIDs();
        RequestAds();
        rewarded = false;
        DontDestroyOnLoad(gameObject);     
    }
   
    private void setupRemoteConfig(){

     
    }
    
    void Update() {

       
    }

    public void RequestAds()
    {
      // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(app_id);
        initAds(); 
        RequestSmallBanner();
        RequestInterstitial();
        RequestRewardVideo();
    }
    private void initAds(){

        smallbannerView = new BannerView(bannerid, AdSize.Banner, AdPosition.Bottom);
        smallbannerView.OnAdFailedToLoad += HandleSBOnAdFailedToLoad;
        this.interstitial = new InterstitialAd(interstitialID);  
        this.interstitial.OnAdFailedToLoad += HandleiOnAdFailedToLoad;
        this.interstitial.OnAdClosed += HandleiOnAdClosed;  
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication; 
    }
    private void UserRewardedThroughVideoAds(){
       
        //GameManager.continueGame = true;
        PlayUIManager.instance.ResumeGame();

        this.RequestRewardVideo();
    }


    public void RequestRewardVideo(){
     
        
       
        this.rewardBasedVideo.LoadAd(GetAdRequest(), rewardID);
        

    }

    private AdRequest GetAdRequest(){
        return new AdRequest.Builder()
        .AddExtra("npa", ""+gdprval)
        .Build();;
    }
    public bool IsRewardAvailable(){
        return this.rewardBasedVideo.IsLoaded();
    }
    public void ShowRewardVideo(){

            if (rewardBasedVideo.IsLoaded()) {
             
                    rewardBasedVideo.Show();
                
            }else{
                RequestRewardVideo();
            }
    }
    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
        
    }
    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        this.RequestRewardVideo();
        if(rewarded){
            UserRewardedThroughVideoAds();
            rewarded = false;
        }
    }
    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args){this.RequestRewardVideo();}
    public void HandleRewardBasedVideoRewarded(object sender, Reward args){rewarded = true; }
    public void RequestSmallBanner(){
        smallbannerView.LoadAd(GetAdRequest());
        //smallbannerView.Show();
    smallbannerView.Hide();
    }
    public void HandleSBOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) { RequestSmallBanner();}
    public void ShowSmallBanner(){  
        //smallbannerView.Show();
        }
    public void RemoveSmallBanner(){   
        if(smallbannerView != null)
           smallbannerView.Hide();
        
    }
    private void RequestInterstitial(){
        this.interstitial.LoadAd(GetAdRequest());
    }
    public void HandleiOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) {RequestInterstitial();}
    public void HandleiOnAdClosed(object sender, EventArgs args){RequestInterstitial(); }  
    public void ShowInterstitial(){
   
        if (this.interstitial.IsLoaded()) {
                this.interstitial.Show();
         }
    }
    public bool IsInterstitialAvailable(){
        return this.interstitial.IsLoaded();
    }

    private void SetUpIDs(){         

            #if UNITY_EDITOR
                    Debug.Log("Initialising admob for Editor");
            #else
                
                 if(enableTestAds){
                   
                  
                    app_id = appidtest;
                    bannerid = bannertest;
                    interstitialID =  interstitialtest;
                    rewardID =  rewardtest;
                   
            }else{

                    app_id = appidandroid;
                    bannerid = bannerandroid;
                    interstitialID =  interstitialandroid;
                    rewardID =  rewardAndroid;
              
            }
        #endif

    }

}


