using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefsManager : MonoBehaviour
{

    public static PrefsManager instance = null;
    private const string SHOES_VAL = "shoesval";
    private const string T_SHIRT_VAL = "tshirtval";
    private const string TROUSERS_VAL = "trousersval";
    private const string COIN_VAL = "coinval";
    private const string SEED_VAL = "seedval";
    private const string COMMON_VAL = "commonval";
    private const string RARE_VAL = "rareval";
    private const string LEGENDARY_VAL = "legendaryval";

    public static int playerScore;

    public bool []cardOpen ;

    public int totalDistance;

    public GameObject cardOpenPrefab;

    void Awake()
    {


        if (instance != null){

            Destroy(gameObject);
            return;
        }
        
        initPrefsManager();
        instance = this;
        DontDestroyOnLoad(gameObject); 

    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>

   private void initPrefsManager(){

       this.cardOpen =  new bool[6];
       this.cardOpen[0] =  get500CardOpen();
       this.cardOpen[1] =  get1500CardOpen();
       this.cardOpen[2] =  get3500CardOpen();
       this.cardOpen[3] =  get6500CardOpen();
       this.cardOpen[4] =  get10500CardOpen();
       this.cardOpen[5] =  get15500CardOpen();
       this.totalDistance =  getDistanceTravelled();

       
   }

   public void resetTravelledCards(){

       PlayerPrefs.SetInt("500mOpen", 99);
       PlayerPrefs.SetInt("1500mOpen", 99);
       PlayerPrefs.SetInt("3500mOpen", 99);
       PlayerPrefs.SetInt("6500mOpen", 99);
       PlayerPrefs.SetInt("10500mOpen", 99);
       PlayerPrefs.SetInt("15500mOpen", 99);
       
       for (int i = 0; i  < 6 ;this.cardOpen[i] = false, i++);

      // PlayerPrefs.SetInt("rtotaltravelledDist", 0);

       PlayerPrefs.Save();
       

   }

    void Start(){}
    public void upgradeShoesVal() {
       PlayerPrefs.SetInt(SHOES_VAL, getShoesVal() + 1);
       PlayerPrefs.Save();
    }    
    public void SetShoesVal(int sv){
        PlayerPrefs.SetInt(SHOES_VAL, sv);
    }
    public int getShoesVal(){
        return PlayerPrefs.GetInt(SHOES_VAL, 0);
    }

    public void upgradeTShirtVal(){
       PlayerPrefs.SetInt(T_SHIRT_VAL, getTShirtVal() + 1);
       PlayerPrefs.Save();
    }    
    public void SetTShirtVal(int sv){
        PlayerPrefs.SetInt(T_SHIRT_VAL, sv);
    }
    public int getTShirtVal(){
        return PlayerPrefs.GetInt(T_SHIRT_VAL, 0);
    }

    public void upgradeTrouserVal(){
       PlayerPrefs.SetInt(TROUSERS_VAL, getTrouserVal() + 1);
       PlayerPrefs.Save();
    }   
    public void SetTrouserVal(int sv){
        PlayerPrefs.SetInt(TROUSERS_VAL, sv);
    }
    public int getTrouserVal(){
        return PlayerPrefs.GetInt(TROUSERS_VAL, 0);
    }

    public int coinValueofUpgrade(int u){

        return u == 0 ? 100 :
               u == 1 ? 200 :
               u == 2 ? 500 :
               u == 3 ? 1000 :
               u == 4 ? 2000 : 5000;
    }

    public void IncreaseCoinVal(int cv){
       PlayerPrefs.SetInt(COIN_VAL, getCoinVal() + cv);
       PlayerPrefs.Save();
    }   
    public void SetCoinVal(int sv){
        PlayerPrefs.SetInt(COIN_VAL, sv);
    }
    public int getCoinVal(){
        return PlayerPrefs.GetInt(COIN_VAL, 0);
    }


    public void IncreaseSeedVal(int cv){
       PlayerPrefs.SetInt(SEED_VAL, getSeedVal() + cv);
       PlayerPrefs.Save();
    }   
    public void SetSeedVal(int sv){
        PlayerPrefs.SetInt(SEED_VAL, sv);
    }
    public int getSeedVal(){
        return PlayerPrefs.GetInt(SEED_VAL, 0);
    }



    public void IncreaseCommonVal(int cvalue = 10){

        int currentCommonCount = getCommonValCount() + 1;   
        if(currentCommonCount > 13) return;
        var tata = getCommonVal(currentCommonCount) + cvalue;

       if(tata >= 100){
            IncreatCommonValCount();
            tata =  100;    
       }
        PlayerPrefs.SetInt(COMMON_VAL + currentCommonCount, tata);
        PlayerPrefs.Save();  
    }   
    public void SetCommonVal(){

        if(getCommonValCount() > 13) return;
        IncreatCommonValCount();
        PlayerPrefs.SetInt(COMMON_VAL + getCommonValCount(), 100);
        PlayerPrefs.Save();
    }
    public int getCommonVal(int commonSeq){
        return PlayerPrefs.GetInt(COMMON_VAL + commonSeq, 0);
    }
    public int getCommonValCount(){
        return PlayerPrefs.GetInt("cvalcount", 0);
    }
    public void IncreatCommonValCount(){
        PlayerPrefs.SetInt("cvalcount", getCommonValCount() + 1);
        PlayerPrefs.Save();

        var gp = Instantiate(cardOpenPrefab,GameObject.FindGameObjectWithTag("uicanvas").transform);
            gp.GetComponent<CardOpen>().unlockCommonCards(getCommonValCount());
    }
     public void SetCommonValCount(int commonSeq){
        PlayerPrefs.SetInt("cvalcount", commonSeq);
        PlayerPrefs.Save();
    }




    public void IncreaseRareVal(int rvalue = 10){

       int currentRareCount = getRareValCount() + 1;   
       if(currentRareCount > 9) return;
       var tata = getRareVal(currentRareCount) + rvalue;

       if(tata >= 100){
            increaseRareValCount();
            tata =  100;    
       }
        PlayerPrefs.SetInt(RARE_VAL + currentRareCount, tata);
        PlayerPrefs.Save();  
    }   
    public void SetRareVal(){
        if(getRareValCount() > 9) return;
        increaseRareValCount();
        PlayerPrefs.SetInt(RARE_VAL + getRareValCount(), 100);
        PlayerPrefs.Save();
    }
    public int getRareVal(int rareSeq){
        return PlayerPrefs.GetInt(RARE_VAL + rareSeq, 0);
    }
    public int getRareValCount(){
        return PlayerPrefs.GetInt("rarevalcount", 0);
    }
    public void increaseRareValCount(){
       PlayerPrefs.SetInt("rarevalcount", getRareValCount() + 1);
       PlayerPrefs.Save();

        var gp = Instantiate(cardOpenPrefab,GameObject.FindGameObjectWithTag("uicanvas").transform);
            gp.GetComponent<CardOpen>().unlockRareCards(getRareValCount());
    }
     public void SetRareValCount(int commonSeq){
       PlayerPrefs.SetInt("rarevalcount", commonSeq);
       PlayerPrefs.Save();
    }





     public void IncreaseLegendaryVal(int legendaryvalue = 10){ // to increase the percentage and open the card progressively

       int currentLegendryCount = getLegenderyValCount() + 1;   
       if(currentLegendryCount > 6) return;
       var tata = getLegendaryVal(currentLegendryCount) + legendaryvalue;

       if(tata >= 100){
            increaseLegenderyValCount();
            tata =  100;    
       }
        PlayerPrefs.SetInt(LEGENDARY_VAL + currentLegendryCount, tata);
        PlayerPrefs.Save();  
    }   

    public void SetLegendaryVal(){// open card directly
         
        if(getLegenderyValCount() > 6) return;
        increaseLegenderyValCount();
        PlayerPrefs.SetInt(LEGENDARY_VAL + getLegenderyValCount(), 100);
        PlayerPrefs.Save();

    }
    public int getLegendaryVal(int legendarySeq){
        return PlayerPrefs.GetInt(LEGENDARY_VAL + legendarySeq, 0);
    }

    public int getLegenderyValCount(){
        return PlayerPrefs.GetInt("legenderyvalcount", 0);
    }
    public void increaseLegenderyValCount(){
         PlayerPrefs.SetInt("legenderyvalcount", getLegenderyValCount() + 1);
         PlayerPrefs.Save();

        var gp = Instantiate(cardOpenPrefab,GameObject.FindGameObjectWithTag("uicanvas").transform);
            gp.GetComponent<CardOpen>().unlockLegenedryCards(getLegenderyValCount());
    }
     public void SetLegenderyValCount(int commonSeq){
        PlayerPrefs.SetInt("legenderyvalcount", commonSeq);
         PlayerPrefs.Save();
    }

    public string getUserName(){

        return PlayerPrefs.GetString("usernmaeis", "Amit ji");

    }
    public void SetUserName(string uni){

         PlayerPrefs.SetString("usernmaeis", uni);
         PlayerPrefs.Save();

    }


    public string getCard500(){

        return PlayerPrefs.GetString("usernmaeis", "");

    }
    public void SetCard500(string uni){

         PlayerPrefs.SetString("usernmaeis", uni);
         PlayerPrefs.Save();

    }


    public int getHighScore(){

        return PlayerPrefs.GetInt("userhighscore", 0);

    }

    public void SetHighScore(int sc){

         PlayerPrefs.SetInt("userhighscore", sc);
         PlayerPrefs.Save();

    }

    public int getUserLevel(){

        return PlayerPrefs.GetInt("usergamelevel", 1);
    }

    public void SetUserLevel(int lvl){

         PlayerPrefs.SetInt("usergamelevel", lvl);
         PlayerPrefs.Save();
    }

    public string getUserID(){

        return PlayerPrefs.GetString("thisuserid", "na");
    }

    public void SetUserID(string str){

         PlayerPrefs.SetString("thisuserid", str);
         PlayerPrefs.Save();
    }

    public bool isUserIDGenerated(){

        return getUserID() != "na" ;
    }

    public bool get500CardOpen(){

        return PlayerPrefs.GetInt("500mOpen", 99) == 1;
    }

    public void Set500CardOpen(){

        PlayerPrefs.SetInt("500mOpen", 1);
        PlayerPrefs.Save();
    }

    public bool get1500CardOpen(){

        return PlayerPrefs.GetInt("1500mOpen", 99) == 1;
    }

    public void Set1500CardOpen(){

        PlayerPrefs.SetInt("1500mOpen", 1);
        PlayerPrefs.Save();
    }

    public bool get3500CardOpen(){

        return PlayerPrefs.GetInt("3500mOpen", 99) == 1;
    }



    public void Set3500CardOpen(){

        PlayerPrefs.SetInt("3500mOpen", 1);
        PlayerPrefs.Save();
    }

    public bool get6500CardOpen(){

        return PlayerPrefs.GetInt("6500mOpen", 99) == 1;
    }

    public void Set6500CardOpen(){

        PlayerPrefs.SetInt("6500mOpen", 1);
        PlayerPrefs.Save();
    }

    public bool get10500CardOpen(){

        return PlayerPrefs.GetInt("10500mOpen", 99) == 1;
    }

    public void Set10500CardOpen(){

        PlayerPrefs.SetInt("10500mOpen", 1);
        PlayerPrefs.Save();
    }

    public bool get15500CardOpen(){

        return PlayerPrefs.GetInt("15500mOpen", 99) == 1;
    }

    public void Set15500CardOpen(){

        PlayerPrefs.SetInt("15500mOpen", 1);
        PlayerPrefs.Save();
        resetTravelledCards();
    }
 
    public int getDistanceTravelled(){

        return PlayerPrefs.GetInt("rtotaltravelledDist", 0);
    }

    public void  SetDistanceTravelled(int rdist){

        PlayerPrefs.SetInt("rtotaltravelledDist", rdist);
        PlayerPrefs.Save();
    }

    public void IncreseDistanceTravelled(int incVal){

        var dty =  incVal + getDistanceTravelled();
        PlayerPrefs.SetInt("rtotaltravelledDist", dty);
        PlayerPrefs.Save();
    }
}
