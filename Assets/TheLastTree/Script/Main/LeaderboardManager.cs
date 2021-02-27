using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

class UserComparer: IComparer
{
    public int Compare(object x, object y)
    {
        return (new CaseInsensitiveComparer()).Compare(((User)y).userScore, ((User)x).userScore);
    }
}

public class LeaderboardManager : MonoBehaviour
{

    public GameObject ParentContent;
    public GameObject statsPrefab;

    public List<GameObject> statobs;
  

    void Awake()
    {
        statobs = new List<GameObject>();
        ///
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
 

 
       // LeaderboardHandler.instance.fetchScore(this);
      /*  for(int i = 0 ; i <  statobs.Count ;DestroyImmediate(statobs[i].gameObject), i++ );
        statobs.Clear();

        for(int i = 0 ; i <  10 ; i++ ){
          
        var go = Instantiate(statsPrefab, ParentContent.transform, false);
                 go.name =  "Player"+(i+1);
                 go.GetComponent<LeaderBoardui>().setStats((i+1)+"", "Player "+ Random.Range(100,500) , Random.Range(100,10000)+"");
                 statobs.Add(go);
               
        }  */    


        CI.HttpClient.HttpClient httpClient = new  CI.HttpClient.HttpClient();

            httpClient.Get(new Uri("https://tlsttree-b30cb.firebaseio.com/.json"),   CI.HttpClient.HttpCompletionOption.AllResponseContent, (r) => 
            {
                var users =  r.ReadAsString();
                JObject json1 = JObject.Parse(users);

                User []tempUsers =  new User[json1.Count];
                var indexCount  = 0;
               
                foreach (var x in json1) { // if 'obj' is a JObject

                    string name1 = x.Key;
                    JToken value1 = x.Value;
                    tempUsers[indexCount++]   = value1.ToObject<User>();
                }   

               Array.Sort(tempUsers,new UserComparer());    



                 for(int i = 0 ; i <  statobs.Count ;DestroyImmediate(statobs[i].gameObject), i++ );
                 statobs.Clear();

                 for(int i = 0 ; i <  tempUsers.Length ; i++ ){
          
                 var go = Instantiate(statsPrefab, ParentContent.transform, false);
                        go.name =  "Player"+(i+1);
                        go.GetComponent<LeaderBoardui>().setStats((i+1)+"", tempUsers[i].userName , tempUsers[i].userScore+"");
                        statobs.Add(go);
                    
                } 
               
                
            });  
        
    }
}
