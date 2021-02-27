using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User
{
    public String userName;
    public int userScore;
    public string userid;
    // Start is called before the first frame update

    public User(){

        userName =  PrefsManager.instance.getUserName();
        userScore =  PrefsManager.playerScore;
    }

}
