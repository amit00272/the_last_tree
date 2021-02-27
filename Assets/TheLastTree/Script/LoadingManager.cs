using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public Text lodingText;

    public Text loadingLbl;
    public static string scenename;

    public static bool isRetry=false;
    public GameObject[] dots;
    private int dotsCount;

    public float delay=0.01f;

    public string fullText;

    private string currentText= "";
    private List<string> levelIntrotext;

    public bool isOpenHome=false;

    // Start is called before the first frame update
    void Start()
    {
      MusicManager.instance.PauseMusic();
      //for(int i = 0; i < dots.Length; dots[i].SetActive(false), i++);
      //dotsCount = 0;
      //StartCoroutine(DotsRoutine());

      levelIntrotext=new List<string>();

        levelIntrotext.Add("Many years have passed since the fall of the humanity.\nPollution and climate change was blamed.\n"+
                            "Our last hope.... \"THE LAST TREE\" ... ");
        levelIntrotext.Add("The Icebergs are all but gone, the poles are now extremely \nfrigid.Ceaseless snowfall and fragile ice peaks are all that\nremains....");
        levelIntrotext.Add("Once a fertile land, now filled to the brim with garbage and industrial waste.\n"+
                            "The only things alive are dangerous rats ....");
        levelIntrotext.Add("The \"City of Dreams\" they called it.Crumbling buildings and toxic smoke are what we find in it's place....");
        levelIntrotext.Add("That remaining of humanity huddled together here in the LAST CITY. Now engulfed by fire.."+
                                "\nThere is no more....");

        levelIntrotext.Add("Here lies our hope, the last and probably the only place for the seeds of future can be planted..\n"+
                                "All else is doomed....");
        //levelIntrotext.Add("Loading");


        if(PlayUIManager.currentLevel<7)
        fullText=levelIntrotext[PlayUIManager.currentLevel-1];
                 Debug.Log(fullText);

         if(PlayUIManager.currentLevel<7 && LoadingManager.scenename!="Main" && !isRetry)
          Invoke("startIntro", 1.0f);
          else
          startLoading();

      //StartCoroutine()

      //Invoke("startload", 1f);
    }

    void startLoading(){

        if(LoadingManager.scenename=="Main" || isRetry){
                loadingLbl.gameObject.SetActive(true);
        }
        StartCoroutine(startload());
        isRetry=false;
    }

    IEnumerator startload(){

        for(int i=0;i<dots.Length;i++){
           dots[dotsCount].SetActive(true);

           if(++dotsCount == dots.Length){
               for(int j = 0; j < dots.Length; dots[i].SetActive(false), j++);
               dotsCount = 0;
           }
           yield return new WaitForSeconds(0.1f);
        }

          StartCoroutine(LoadYourAsyncScene());
    }

  void startIntro(){
      StartCoroutine(introRoutine());
  }

    IEnumerator introRoutine(){

        for(int i=0;i<fullText.Length;i++){

            //Debug.Log(currentText);
            if(i<fullText.Length-1){
                currentText=fullText.Substring(0,i);
            lodingText.text=currentText+" |";
            }
            else{
            lodingText.text=currentText+" ";
            }
            yield return new WaitForSeconds(delay);
        }

        StartCoroutine(startload());

    }

     IEnumerator LoadYourAsyncScene()
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scenename);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
           // lodingText.text = Convert.ToInt32(asyncLoad.progress).ToString()  +" %";
            yield return null;
        }

    }
}
