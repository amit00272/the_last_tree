using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class FailedPannel : MonoBehaviour
{

    public Text coinText;
    public GameObject[] buttons;
    public GameObject popup;
    private Tween tween;

    // Start is called before the first frame update
    void Start()
    {

    }


    void OnEnable(){

        MusicManager.instance.PauseMusic();
        popup.transform.DOLocalMoveY(0,0.5f).SetEase(Ease.InOutBack).OnComplete( ()=> {

            Sequence mySequence = DOTween.Sequence();
            mySequence.Join(buttons[0].transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.InOutBack));
            mySequence.Join(buttons[1].transform.DOScale(Vector3.one, 0.6f).SetEase(Ease.InOutBack));
            mySequence.Join(buttons[2].transform.DOScale(Vector3.one, 0.6f).SetEase(Ease.InOutBack));
            mySequence.OnComplete( () => {
                    tween =   buttons[1].transform.DOShakeRotation(2,20, 5, 20);
                    tween.SetLoops(-1);
                    AdMobAdManager.instance.ShowInterstitial();
            });

        });
    }

    void back(int actionNumber){

        MusicManager.instance.PlayMusic();
       if(tween != null){
            tween.SetLoops(0);
            tween.Complete();
            if (tween.target != null)
            {
                ((Transform)tween.target).rotation = Quaternion.identity;
                tween.Kill();
            }
        }
        Sequence mySequence = DOTween.Sequence();
         mySequence.Join(buttons[0].transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutBack));
         mySequence.Join(buttons[1].transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutBack));
         mySequence.Append(popup.transform.DOLocalMoveY(-1000,0.5f).SetEase(Ease.InOutBack));
         mySequence.OnComplete( () => {

                switch(actionNumber){

                    case 1:  LoadingManager.scenename =  "Main";
                             SceneManager.LoadScene("Loading");
                            break;

                    case 2: gameObject.SetActive(false);
                            LoadingManager.scenename = SceneManager.GetActiveScene().name;
                            LoadingManager.isRetry=true;
                             SceneManager.LoadScene("Loading");
                            break;
                    case 3: gameObject.SetActive(false);
                            PlayUIManager.instance.ResumeGame();
                            break;

                }
         });

    }


    // Update is called once per frame
    void Update()
    {
        coinText.text =  PlayUIManager.instance.coinCount.ToString();
    }
     public void HomeClicked(){

        SoundManager.instance.PlayButtonSound();
        back(1);
    }

    public void ResumeClicked(){
        SoundManager.instance.PlayButtonSound();
        back(3);
    }

    public void RetryClicked(){

       SoundManager.instance.PlayButtonSound();
       back(2);

    }
}
