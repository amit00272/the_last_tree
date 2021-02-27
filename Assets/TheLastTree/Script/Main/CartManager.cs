using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }


    public void PlantClicked(int i ){
        
        SoundManager.instance.PlayButtonSound();

        var seedval = PrefsManager.instance.getSeedVal();
        var randNum =  Random.Range(1,11);
        if(i == 1 && seedval >= 10){
                  seedval =  seedval - 10;
                 PrefsManager.instance.SetSeedVal(seedval);
                if(randNum < 7) PrefsManager.instance.SetCommonVal();
                if(randNum < 9) PrefsManager.instance.SetRareVal();
                else PrefsManager.instance.SetLegendaryVal();
        }
        else  if(i == 2 && seedval >= 20){

                 seedval =  seedval - 20;
                 PrefsManager.instance.SetSeedVal(seedval);
                if(randNum < 5) PrefsManager.instance.SetCommonVal();
                if(randNum < 8) PrefsManager.instance.SetRareVal();
                else PrefsManager.instance.SetLegendaryVal();
        }
        else  if(i == 3 && seedval >= 25){

                 seedval =  seedval - 25;
                 PrefsManager.instance.SetSeedVal(seedval);
                if(randNum < 5) PrefsManager.instance.SetCommonVal();
                if(randNum < 8) PrefsManager.instance.SetRareVal();
                else PrefsManager.instance.SetLegendaryVal();
        }

    }

    ///IAP TO bug seeds
    public void SeedClicked(int i){

        SoundManager.instance.PlayButtonSound();
        Debug.Log("Seed Clicked :   "+ i);
        IAPManager.Instance.BuyProduct(i);
    }

    public void CoinClicked(int i ){
        

        SoundManager.instance.PlayButtonSound();
        Debug.Log("Coin Clicked :   "+ i); 
        if(i == 1) BuyCoinForSeed(25, 1000);
        if(i == 2) BuyCoinForSeed(50, 2000);
        if(i == 3) BuyCoinForSeed(100, 4000);

    }

    private void BuyCoinForSeed(int seeds, int coins){

         var seedval = PrefsManager.instance.getSeedVal();

        if( seedval >= seeds){

            seedval =  seedval - seeds;
            PrefsManager.instance.SetSeedVal(seedval);
            PrefsManager.instance.IncreaseCoinVal(coins);
            Debug.Log("In game Purchased "+coins+" coins for "+seeds+" seeds");
            return;
        }

        Debug.Log("In game Purchase failed .You have "+seedval+" seeds");


        

    }
}
