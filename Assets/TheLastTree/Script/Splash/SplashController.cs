using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SplashController : MonoBehaviour
{
    public TextMeshProUGUI curiosityTextmesh; 
    public GameObject[] splash1Objects;
    public GameObject splash2Object;
    public GameObject vdo;
    public Text curiosityText;

    public Image gameIcon;

    private bool isVideoReady;



    [Obsolete]
    void Start()
    {
       //PlayerPrefs.DeleteAll();
        
        splash1Objects[0].SetActive(false);
        splash1Objects[1].SetActive(false);
        splash1Objects[2].SetActive(false);
     
        splash2Object.SetActive(false);

        string[]  curiosityars = LoadJson();

    /*    curiosityText.text = curiosityars[UnityEngine.Random.Range(0,curiosityars.Length)];
        vdo.GetComponent<VideoPlayer> ().loopPointReached += CheckOver;
        vdo.GetComponent<VideoPlayer> ().Prepare();
        */
      
      
     Invoke("FireCoRoutine", 1f);
    
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!isVideoReady)
                if(vdo.GetComponent<VideoPlayer> ().isPrepared){
                    vdo.GetComponent<VideoPlayer> ().Play();
                    isVideoReady = true;
                }
    }

    void CheckOver(VideoPlayer vp){
      StartCoroutine("StartSparteffects");
    }

    [Obsolete]
    private string[] LoadJson ()
	{

		string FilePath = Application.streamingAssetsPath + "/" + "curiosity.json";
        var jsonString = "";

        #if UNITY_EDITOR || UNITY_IOS

		   jsonString = File.ReadAllText(FilePath);

	    #elif UNITY_ANDROID

           WWW reader = new WWW(FilePath);
		   while(!reader.isDone){}
		   jsonString = reader.text;
		
		#endif 

        return JsonUtility.FromJson<Curiosity>(jsonString).curiosity;     

	}


    private void FireCoRoutine(){

        StartCoroutine("StartSparteffects");
    }


    IEnumerator StartSparteffects(){

        
            
            SceneManager.LoadScene("Main");
            yield return null;

          /* yield return new WaitForSeconds(10);
        
      GameObject.DestroyImmediate(vdo);

        splash1Objects[0].SetActive(true);
        splash2Object.SetActive(false);

        yield return new WaitForSeconds(3f);
                
        splash1Objects[2].GetComponent<TextMeshProUGUI>().DOFade(0,2.6f);
        splash1Objects[1].GetComponent<Image>().DOFade(0,2.6f);
        splash1Objects[0].GetComponent<RawImage>().DOFade(0,2.8f);

        yield return new WaitForSeconds(1f);

       splash1Objects[0].SetActive(false);
        splash2Object.SetActive(true);


        
        yield return new WaitForSeconds(2);

            
        
        splash2Object.GetComponent<RawImage>().DOFade(0,1);

        yield return new WaitForSeconds(0.7f);

        SceneManager.LoadScene("Main");*/
            



    }


}
