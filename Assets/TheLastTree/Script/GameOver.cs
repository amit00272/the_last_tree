using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameOver : MonoBehaviour
{

    public GameObject[] winchars;
    public Text coinText;
    public GameObject[] buttons;
    public GameObject popup;
    public GameObject uiCoinPrefab;

    public GameObject coinSrc;
    public GameObject coinDest;
    private List<GameObject> genCoins;

    private int coinCounter;

    private int totalCoins;

    private Vector3 srcpos;

    private Tween tween;

    // Start is called before the first frame update
    void Start()
    {
        genCoins =  new List<GameObject>();
        coinCounter = 0;
        srcpos =  coinSrc.transform.position;
        srcpos.x -= 20;
    }


    void OnEnable(){
        Debug.Log("on ENable called GAmeover");
        MusicManager.instance.PauseMusic();

            popup.transform.DOScale(Vector3.one, 1.5f).SetEase(Ease.InOutElastic).OnComplete( () =>{
                 Debug.Log("GAmeove pop scaled...r");
                    winchars[0].SetActive(true);
                    winchars[1].SetActive(false);
                    Sequence mySequence = DOTween.Sequence();
                   totalCoins =  Random.Range(20, 30);
                    for(int i = 0 ; i < totalCoins; i++){

                            var go = Instantiate(uiCoinPrefab, srcpos, Quaternion.identity,popup.transform);
                                     mySequence.Append(go.transform.DOLocalMove(new Vector3(184, 160),0.1f).OnComplete(()=>{

                                         if(coinCounter < PlayUIManager.instance.coinCount )
                                          coinCounter++;
                                         coinText.text =  coinCounter.ToString();
                                     }));



                                genCoins.Add(go);

                    }
                     mySequence.OnComplete( () => {

                            coinCounter = PlayUIManager.instance.coinCount;
                            coinText.text =  coinCounter.ToString();
                            Sequence mySequence1 = DOTween.Sequence();
                                     mySequence1.Join(buttons[0].transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.InOutBack));
                                     mySequence1.Join(buttons[1].transform.DOScale(Vector3.one, 0.6f).SetEase(Ease.InOutBack));
                                     mySequence1.Join(buttons[2].transform.DOScale(Vector3.one, 0.8f).SetEase(Ease.InOutBack));
                                     mySequence1.OnComplete( () => {
                                            winchars[0].SetActive(false);
                                            winchars[1].SetActive(true);
                                        foreach(var go in genCoins) DestroyImmediate(go);

                                            tween =   buttons[2].transform.DOShakeRotation(2,20, 5, 20);
                                                      tween.SetLoops(-1);
                                    });

                                AdMobAdManager.instance.ShowInterstitial();

                     });


            });





    }


    void DisablePopup(int actionNumber){

        MusicManager.instance.PlayMusic();
        tween.SetLoops(0);
         tween.Complete();
       ( (Transform) tween.target).rotation =  Quaternion.identity;
         tween.Kill();

         Sequence mySequence1 = DOTween.Sequence();
                  mySequence1.Join(buttons[0].transform.DOScale(Vector3.zero, 0.8f).SetEase(Ease.InOutBack));
                  mySequence1.Join(buttons[1].transform.DOScale(Vector3.zero, 0.6f).SetEase(Ease.InOutBack));
                  mySequence1.Join(buttons[2].transform.DOScale(Vector3.zero, 0.4f).SetEase(Ease.InOutBack));
                  mySequence1.OnComplete( () => {

                       winchars[0].SetActive(false);
                       winchars[1].SetActive(false);
                       popup.transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InOutElastic).OnComplete(() => {

                             switch(actionNumber){

                                case 1: gameObject.SetActive(false);

                                        PrefsManager.instance.SetUserLevel((SceneManager.GetActiveScene().buildIndex+1) - 2);
                                        //PlayUIManager.currentLevel = nextSceneNumber - 2;
                                        LoadingManager.scenename =  "Main";
                                        SceneManager.LoadScene("Loading");
                                        break;

                                case 2:
                                        gameObject.SetActive(false);
                                        LoadingManager.scenename = SceneManager.GetActiveScene().name;
                                        LoadingManager.isRetry=true;
                                        SceneManager.LoadScene("Loading");

                                        break;

                                case 3:
                                        gameObject.SetActive(false);

                                        var nextSceneNumber1 = SceneManager.GetActiveScene().buildIndex+1;
                                        Debug.Log("next scene index"+nextSceneNumber1);
                                        PrefsManager.instance.SetUserLevel(nextSceneNumber1 - 2);
                                   PlayUIManager.currentLevel = PrefsManager.instance.getUserLevel();
                                   LoadingManager.scenename = nextSceneNumber1 == 9 ? "Main" : "test" + (PlayUIManager.currentLevel);


                                        SceneManager.LoadScene("Loading");

                                        break;
                }

                       });

                  });


    }




    // Update is called once per frame
    void FixedUpdate()
    {

    }
     public void HomeClicked(){

        SoundManager.instance.PlayButtonSound();
        DisablePopup(1);
    }

    public void RetryClicked(){
        SoundManager.instance.PlayButtonSound();
        DisablePopup(2);




    }

    public void NextClicked(){
       SoundManager.instance.PlayButtonSound();
       DisablePopup(3);

    }
}
