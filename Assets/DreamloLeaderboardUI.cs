using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DreamloLeaderboardUI : MonoBehaviour
{
  

    private dreamloLeaderBoard dreamloRef;

    public GameObject ParentContent;
    public GameObject statsPrefab;

    public List<GameObject> statobs;

    void Awake()
    {
        statobs = new List<GameObject>();
        dreamloRef = dreamloLeaderBoard.Instance;
        ///
    }

    [System.Obsolete]
    private void OnEnable()
    {
              RefreshLeaderboard();
    }

    [System.Obsolete]
    public void RefreshLeaderboard()
    {

        dreamloRef.highScores = "";

        dreamloRef.LoadScores();

        for (int i = 0; i < statobs.Count; DestroyImmediate(statobs[i].gameObject), i++) ;
        statobs.Clear();


        StartCoroutine(DoLeaderboardUI());
    }

    IEnumerator DoLeaderboardUI()
    {
        float timer = 0.0f;

        while (timer < 5.0f && dreamloRef.highScores == "")
        {
            timer += Time.deltaTime;
            yield return null;
        }

        if (dreamloRef.highScores != "")
        {

            List<dreamloLeaderBoard.Score> scores = dreamloRef.ToListHighToLow();

            Debug.Log("tata mata dhghjghjfjh");

            for (int i = 0; i < scores.Count; ++i)
            {

                var go = Instantiate(statsPrefab, ParentContent.transform, false);
                go.name = "Player" + (i + 1);
                go.GetComponent<LeaderBoardui>().setStats((i + 1) + "", scores[i].playerName, scores[i].score.ToString() + "");
                statobs.Add(go);
            }

        }
       // StopCoroutine(DoLeaderboardUI());
    }
}
