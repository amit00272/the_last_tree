using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeHandler : MonoBehaviour
{

    public Image shoeUpgrade;
    public Image tshirtupgrade;
    public Image trouserupgrade;

    public Sprite[] upgradeShoes;
    public Sprite[] upgradeTshirts;
    public Sprite[] upgradeTrousers;

    // Start is called before the first frame update
    void Start()
    {
        setUpgradeImages();
        LogUpgrades();
    }
    void OnEnable()
    {
         setUpgradeImages();
         LogUpgrades();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void LogUpgrades(){

      /*  Debug.Log("------------Player Lavel Log Start-------------");
        Debug.Log("Shoe : "+  PrefsManager.instance.getShoesVal());
        Debug.Log("Tshirt : "+  PrefsManager.instance.getTShirtVal());
        Debug.Log("Trouser : "+  PrefsManager.instance.getTrouserVal());
        Debug.Log("------------Player Lavel Log End-------------");
        */
        
    }


    public void ShoesClicked(){
       var ix = PrefsManager.instance.getShoesVal(); 
       SoundManager.instance.PlayButtonSound();
       if(ix == 5) return;

       var sc = PrefsManager.instance.coinValueofUpgrade( ix);
       var cv = PrefsManager.instance.getCoinVal();

       if( sc <= cv){

           PrefsManager.instance.IncreaseCoinVal( -sc);
           PrefsManager.instance.upgradeShoesVal();
           setUpgradeImages();
       }
       LogUpgrades();
    }

    public void TShirtClicked(){

       var ix = PrefsManager.instance.getTShirtVal();
       SoundManager.instance.PlayButtonSound();
       if(ix == 5) return;

       var sc = PrefsManager.instance.coinValueofUpgrade( ix);
       var cv = PrefsManager.instance.getCoinVal();

       if( sc <= cv){

           PrefsManager.instance.IncreaseCoinVal( -sc);
           PrefsManager.instance.upgradeTShirtVal();
           setUpgradeImages();
       }
         LogUpgrades();
    }

    public void TrouserClicked(){

       var ix = PrefsManager.instance.getTrouserVal();
        SoundManager.instance.PlayButtonSound();
       if(ix == 5) return;

       var sc = PrefsManager.instance.coinValueofUpgrade( ix );
       var cv = PrefsManager.instance.getCoinVal();

       if( sc <= cv){

           PrefsManager.instance.IncreaseCoinVal( -sc);
           PrefsManager.instance.upgradeTrouserVal();
           setUpgradeImages();
       }
     LogUpgrades();
    }

    private void setUpgradeImages() {

        shoeUpgrade.sprite = upgradeShoes[PrefsManager.instance.getShoesVal()];
        tshirtupgrade.sprite = upgradeTshirts[PrefsManager.instance.getTShirtVal()];
        trouserupgrade.sprite = upgradeTrousers[PrefsManager.instance.getTrouserVal()];
    
    }
}
