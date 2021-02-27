using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuPopupManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject popupObject;
    public GameObject[] directionArrow;
    public GameObject[] contentsObj;
    public Image[] buttonSprite;
    private Color enabledColor;
    private Color disabledColor; 
    public Text coinValueTextOnPopup;

    public Text seedvalueTextPopup;

    void Start()
    {
        enabledColor = new Color(1,1,1,1f);
        disabledColor = new Color(1,1,1,0.35f);  
    }

    // Update is called once per frame
    void Update()
    {
        coinValueTextOnPopup.text =  PrefsManager.instance.getCoinVal().ToString();
        seedvalueTextPopup.text =  PrefsManager.instance.getSeedVal().ToString();
    }

    public void ButtonClicked(int i){

        for(int ir = 0 ; ir < 4 ; ir++){    

            directionArrow[ir].SetActive(ir == i);
            buttonSprite[ir].color =  ir == i ? enabledColor : disabledColor;
            contentsObj[ir].SetActive(ir == i);
        
        }
        SoundManager.instance.PlayButtonSound();

    }

    public void CloseButtonClicked(){

        SoundManager.instance.PlayButtonSound();
        popupObject.SetActive(false);
    }


}
