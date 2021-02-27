using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTestManager : MonoBehaviour
{
    // Start is called before the first frame update
   


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Level1(){

         PlayUIManager.currentLevel = 1;
        LoadingManager.scenename =  "test1";
        SceneManager.LoadScene("Loading");
    }


    
    public void Level2(){

         PlayUIManager.currentLevel = 2;
         LoadingManager.scenename =  "test2";
        SceneManager.LoadScene("Loading");
    }


    
    public void Level3(){

         PlayUIManager.currentLevel = 3;
         LoadingManager.scenename =  "test3";
        SceneManager.LoadScene("Loading");
    }

    
    public void Level4(){

         PlayUIManager.currentLevel = 4;
         LoadingManager.scenename =  "test4";
        SceneManager.LoadScene("Loading");
    }


    
    public void Level5(){

         PlayUIManager.currentLevel = 5;
         LoadingManager.scenename =  "test5";
        SceneManager.LoadScene("Loading");
    }

    
    public void Level6(){

         PlayUIManager.currentLevel = 6;
         LoadingManager.scenename =  "test6";
        SceneManager.LoadScene("Loading");
    }

    public void ENdGAmeVdo(){

         PlayUIManager.currentLevel = 7;
         LoadingManager.scenename =  "EndSene";
        SceneManager.LoadScene("Loading");
    }
}
