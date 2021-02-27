using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{

    public AudioSource musicSource;
    public static MusicManager instance;
    private bool musicStatus;

    public AudioClip homebgclip;
    public AudioClip[] levelbgclip;

    void Awake()
    {

     
        if (instance == null){
			instance = this;
            musicStatus = true;
        }    
		else
		Destroy (gameObject);

        
        DontDestroyOnLoad (gameObject);
    }

    void Update()
    {
        
    }

    public bool getMusicStatus(){

        return musicStatus;
    }
    public bool ToggleMusic(){
        musicStatus =  !musicStatus;

        musicSource.volume = musicStatus ?  1 : 0;
        return musicStatus;
    }


    public void PlayHomeBG(){
        
        musicSource.clip =  homebgclip;
        musicSource.Play();
    }

    public void PlayLevelBG(int level){
        
  
        musicSource.clip =  levelbgclip[level-1];
        musicSource.Play();
        musicSource.pitch = level == 1 ? 2 : 1; 
    }

    public void PauseMusic(){
        musicSource.Pause();
    }

    
    public void PlayMusic(){
        musicSource.Play();
    }

    
}
