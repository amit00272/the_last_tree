using System.Collections;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource soundSource;
    public AudioClip[] gameSounds;
    public static SoundManager instance;
    private bool soundStatus;

    void Awake()
    {

        if (instance == null){
			instance = this;
            soundStatus = true;
        }    
		else
		Destroy (gameObject);
        
		DontDestroyOnLoad (gameObject);
       
       
    }

    void Start(){}
    void Update(){}

    public void PlayButtonSound()
    {
        Play(gameSounds[0]);
    }

    public void PlayGameOverSound()
    {
        Play(gameSounds[1]);
    }

   
    public void Play(AudioClip clip)
    {
        soundSource.clip = clip;
        soundSource.mute = !soundStatus;
        soundSource.Play();
    }


    public bool getSoundStatus(){
        return soundStatus;
    }

    public bool ToggleSound(){
        soundStatus =  !soundStatus;
        return soundStatus;
    }
}
