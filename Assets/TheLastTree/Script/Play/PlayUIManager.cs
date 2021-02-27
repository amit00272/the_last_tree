using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayUIManager : MonoBehaviour
{

    public Text CoinText;
    public GameObject pausepannel;
    public GameObject failedpannel;
    public GameObject gameOverPannel;
    public GameObject coinget;
    public bool gameOver;
    public GameObject monster;
    public GameObject astar;
    public GameObject player;
    public GameObject cameraMain;
    public int coinCount;

    public static PlayUIManager instance;

    public static int currentLevel;

    public GameObject continuePopup;

    public event Action OnPlayerResumed;

    public GameObject spawnPoints;
    public void PlayerResumedTrigger(){
                OnPlayerResumed?.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        instance =  this;
        gameOver = false;
        pausepannel.SetActive(false);
        gameOverPannel.SetActive(false);
        failedpannel.SetActive(false);
        CoinText.text = "0";
        coinCount = 0;
        setBGM();





    }

    // Update is called once per frame
    void Update()
    {
      //Debug.Log("Current Level:"+ currentLevel);
    }


    void FixedUpdate(){

         CoinText.text =  coinCount.ToString();//PrefsManager.instance.getCoinVal().ToString();

    }

    public void DisableGameObjects(){

       monster.SetActive(false);
       astar.SetActive(false);
       player.SetActive(false);

    }

    public void ResumeGame(){

        EnableGameObjects();
        player.GetComponent<PlayerMovement>().onResume();
        int children = spawnPoints.transform.childCount;

        int spawnObjectIndex = 0;
        for (int i = 0; i < children; i++)
        {
            var child = spawnPoints.transform.GetChild(i);
            var localScaleX = player.transform.localScale.x;

                if (child.position.x > player.transform.position.x)
                {
                    if (localScaleX > 0)
                    {
                        spawnObjectIndex = i - 1;
                        if (spawnObjectIndex < 0)
                            spawnObjectIndex = 0;
                    }else{
                        spawnObjectIndex = i;
                    }
                    break;
            }
        }

        // var lastPlayrPos = player.GetComponent<CharacterController2D>().lastGroundPos;
        var lastPlayrPos = spawnPoints.transform.GetChild(spawnObjectIndex).position;
        //lastPlayrPos.y += 3.0f;
        //lastPlayrPos.x -= 0.6f;
        monster.gameObject.transform.position = new Vector3(lastPlayrPos.x-6, 2.18f, monster.gameObject.transform.position.z);
        player.gameObject.transform.position = new Vector3(lastPlayrPos.x, lastPlayrPos.y, lastPlayrPos.z);
        //cameraMain.transform.position = new Vector3(player.gameObject.transform.position.x, 0.4f, -10);
        monster.GetComponent<MonsterAnimation>().isPlayerCatched = false;
        monster.GetComponent<MonsterFlip>().enabled = true;
        //Invoke("SetCameraOnResumeGame",1.0f);
         cameraMain.GetComponent<DeadzoneCamera>().moveCameraToTarget_OnResumeGame(new Vector3(lastPlayrPos.x,lastPlayrPos.y,lastPlayrPos.z));
        monster.GetComponent<CircleCollider2D>().enabled = true;
        monster.GetComponent<MonsterAnimation>().monsterAnimator.GetComponent<Animator>().Play("monstrIdle");

        PlayerResumedTrigger();
    }


    public void DisablePlayer(){
        player.SetActive(false);
    }


     public void EnableGameObjects(){

        monster.SetActive(true);
        astar.SetActive(true);
        player.SetActive(true);

    }

    public void GameOver(){

        LeaderboardHandler.instance.SetScore();
        gameOver = true;
        DisableGameObjects();
        gameOverPannel.SetActive(true);




    }


    public void onpause(){

        //LeaderboardHandler.instance.SetScore();
        // DisableGameObjects();
        Time.timeScale = 0;
        pausepannel.SetActive(true);
        SoundManager.instance.PlayButtonSound();

    }

    public void showOnfieldUI(){

        LeaderboardHandler.instance.SetScore();
        gameOver = true;
        DisableGameObjects();
        failedpannel.SetActive(true);
        SoundManager.instance.PlayGameOverSound();
    }

    public void onfailed(){

      //  LeaderboardHandler.instance.SetScore();
        DisablePlayer();
        if(AdMobAdManager.instance.IsRewardAvailable()){

            continuePopup.SetActive(true);


        }else{
            showOnfieldUI();
             AdMobAdManager.instance.ShowInterstitial();
        }


    }

    public void back(){
        LeaderboardHandler.instance.SetScore();
       LoadingManager.scenename =  "Main";
       SceneManager.LoadScene("Loading");
        SoundManager.instance.PlayButtonSound();
    }

    public void playCoingetAnimation(Vector3 p){

         var go = Instantiate(coinget, p, Quaternion.identity);
         Destroy(go, 0.1f);
         coinCount++;
         PrefsManager.instance.IncreaseCoinVal(1);
         CoinText.text =  coinCount.ToString();//PrefsManager.instance.getCoinVal().ToString();
    }

    private void setBGM(){

        var levelNumber =  SceneManager.GetActiveScene().name.Trim().Substring(4);
        MusicManager.instance.PlayLevelBG(Int32.Parse(levelNumber));
    }



}
