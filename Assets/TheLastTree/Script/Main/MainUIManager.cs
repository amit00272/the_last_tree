using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour
{

    public GameObject popup;
    public Image soundButton;
    public Image musicButton;
    public Sprite onSprite;
    public Sprite offSprite;
    public Text coinValueText;
    public GameObject nameInputPannel;
    public InputField usernameinput;
    private dreamloLeaderBoard dreamloRef;

    // Start is called before the first frame update
    void Start()
    {

       //PlayerPrefs.DeleteAll();
       
        SetSoundButton();
        popup.SetActive(false);
        MusicManager.instance.PlayHomeBG();

        if(!PrefsManager.instance.isUserIDGenerated()){
            PrefsManager.instance.SetUserID(System.Guid.NewGuid().ToString());
        }


        dreamloRef = dreamloLeaderBoard.Instance;
        if (PrefsManager.instance.getUserName() == "Amit ji")
        {

            nameInputPannel.SetActive(true);
        }
        else {


            nameInputPannel.SetActive(false);
            dreamloRef.CheckNameFunction(PrefsManager.instance.getUserName());

        }

        


    }


    public void nameisValid() {

   
    }


    public void nameisNotValid()
    {



    }



    // Update is called once per frame
    void Update()
    {
        coinValueText.text =  PrefsManager.instance.getCoinVal().ToString();
    }

    public void UserNameEnterCicked(){

        if(usernameinput.text.Trim().Length > 1){

            PrefsManager.instance.SetUserName(usernameinput.text.Trim());
            dreamloRef.CheckNameFunction(PrefsManager.instance.getUserName());
            nameInputPannel.SetActive(false);

        }
    }

    public void PlayButtomPressed(){

        SoundManager.instance.PlayButtonSound();
        PlayUIManager.currentLevel = PrefsManager.instance.getUserLevel();
        Debug.Log("next sene number " + PlayUIManager.currentLevel);
        LoadingManager.scenename =  PlayUIManager.currentLevel == 9 ? "Main" : "test" + (PlayUIManager.currentLevel );
        SceneManager.LoadScene("Loading");

    }


    public void ProfilePressed(){

        OPCLICKED(0);
        Debug.Log("Profile  button pressed");
    }

    public void SettingPressed(){

        OPCLICKED(1);
        Debug.Log("Setting  button pressed");

    }

    public void LeaderboardPressed(){

        OPCLICKED(2);
        Debug.Log("Leaserboard  button pressed");

    }

    public void  ShopPressed(){

        OPCLICKED(3);
        Debug.Log("Shop  button pressed");

    }

    public void OPCLICKED(int op){

        SoundManager.instance.PlayButtonSound();
        popup.GetComponent<MenuPopupManager>().ButtonClicked(op);
        popup.SetActive(true);
    }


    public void SetSoundButton(){

        soundButton.sprite = SoundManager.instance.getSoundStatus() ? onSprite : offSprite;
        musicButton.sprite = MusicManager.instance.getMusicStatus() ? onSprite : offSprite;

    }


    public void SoundButtonClicked(){

        SoundManager.instance.PlayButtonSound();
        soundButton.sprite = SoundManager.instance.ToggleSound() ? onSprite : offSprite;

    }

    public void MusicButtonClicked(){

        SoundManager.instance.PlayButtonSound();
        musicButton.sprite = MusicManager.instance.ToggleMusic() ? onSprite : offSprite;

    }

    public void FacebookClicked(){

        FBManager.instance.FBLogin();
    }

}
