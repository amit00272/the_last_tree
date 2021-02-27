using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardui : MonoBehaviour
{

    public Text playerserialno;
    public Text playerName;
    public Text playerScore;


    void Start(){}

    void Update(){}
    
    public void setStats(string sno, string pname,string scrore){

        setslno(sno);
        setplayername(pname);
        setPlayerscrore(scrore);

    }

    public void setslno(string sno){
        this.playerserialno.text =  sno;
    }

    
    public void setplayername(string pn){
        this.playerName.text =  pn;
    }

    
    public void setPlayerscrore(string sc){
        this.playerScore.text =  sc;
    }
}
