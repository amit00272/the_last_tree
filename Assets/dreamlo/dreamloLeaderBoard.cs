using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class dreamloLeaderBoard : MonoBehaviour
{

	string dreamloWebserviceURL = "http://dreamlo.com/lb/";

	public string privateCode =  "";
	public string publicCode  =  "";
	public string highScores  =  "";



	private static dreamloLeaderBoard instance;
	public static dreamloLeaderBoard Instance
	{
		get { return instance; }
	}

	void Awake()
	{
		if (instance != null)
		{

			Destroy(gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad(gameObject);
	}


	public struct Score
	{
		public string playerName;
		public int score;
		public int seconds;
		public string shortText;
		public string dateString;
	}

	void Start()
	{
		this.highScores = "";
	}

	public static dreamloLeaderBoard GetSceneDreamloLeaderboard()
	{
		GameObject go = GameObject.Find("dreamloPrefab");

		if (go == null)
		{
			Debug.LogError("Could not find dreamloPrefab in the scene.");
			return null;
		}
		return go.GetComponent<dreamloLeaderBoard>();
	}


	public static double DateDiffInSeconds(System.DateTime now, System.DateTime olderdate)
	{
		var difference = now.Subtract(olderdate);
		return difference.TotalSeconds;
	}

	System.DateTime _lastRequest = System.DateTime.Now;
	int _requestTotal = 0;




	bool TooManyRequests()
	{
		var now = System.DateTime.Now;

		if (DateDiffInSeconds(now, _lastRequest) <= 2)
		{
			_lastRequest = now;
			_requestTotal++;
			if (_requestTotal > 3)
			{
				Debug.LogError("DREAMLO Too Many Requests. Am I inside an update loop?");
				return true;
			}

		}
		else
		{
			_lastRequest = now;
			_requestTotal = 0;
		}

		return false;
	}

	[System.Obsolete]
	public void AddScore(string playerName, int totalScore)
	{
		if (TooManyRequests()) return;

		StartCoroutine(AddScoreWithPipe(playerName, totalScore));
	}

	[System.Obsolete]
	public void AddScore(string playerName, int totalScore, int totalSeconds)
	{
		if (TooManyRequests()) return;

		StartCoroutine(AddScoreWithPipe(playerName, totalScore, totalSeconds));
	}

	[System.Obsolete]
	public void AddScore(string playerName, int totalScore, int totalSeconds, string shortText)
	{
		if (TooManyRequests()) return;

		StartCoroutine(AddScoreWithPipe(playerName, totalScore, totalSeconds, shortText));
	}

	// This function saves a trip to the server. Adds the score and retrieves results in one trip.
	[System.Obsolete]
	IEnumerator AddScoreWithPipe(string playerName, int totalScore)
	{
		playerName = Clean(playerName);

		WWW www = new WWW(dreamloWebserviceURL + privateCode + "/add-pipe/" + WWW.EscapeURL(playerName) + "/" + totalScore.ToString());
		yield return www;
		highScores = www.text;
	}

	[System.Obsolete]
	IEnumerator AddScoreWithPipe(string playerName, int totalScore, int totalSeconds)
	{
		playerName = Clean(playerName);

		WWW www = new WWW(dreamloWebserviceURL + privateCode + "/add-pipe/" + WWW.EscapeURL(playerName) + "/" + totalScore.ToString() + "/" + totalSeconds.ToString());
		yield return www;
		highScores = www.text;
	}

	[System.Obsolete]
	IEnumerator AddScoreWithPipe(string playerName, int totalScore, int totalSeconds, string shortText)
	{
		playerName = Clean(playerName);
		shortText = Clean(shortText);

		WWW www = new WWW(dreamloWebserviceURL + privateCode + "/add-pipe/" + WWW.EscapeURL(playerName) + "/" + totalScore.ToString() + "/" + totalSeconds.ToString() + "/" + shortText);
		yield return www;
		highScores = www.text;
	}

	[System.Obsolete]
	IEnumerator GetScores()
	{
		highScores = "";
		WWW www = new WWW(dreamloWebserviceURL + publicCode + "/pipe");
		yield return www;
		highScores = www.text;
	}

	[System.Obsolete]
	public void CheckNameFunction(string _name)
	{
		StartCoroutine(GetSingleScore(_name, true));
	}

	[System.Obsolete]
	IEnumerator GetSingleScore(string playerName, bool doCallFunction = false)
	{
		highScores = "";
		WWW www = new WWW(dreamloWebserviceURL + publicCode + "/pipe-get/" + WWW.EscapeURL(playerName));
		yield return www;
		highScores = www.text;

		if (doCallFunction)
		{
			if (www.text != "")
			{
				Debug.Log(playerName+"  Name is not valid ramesh");

				//if(SceneManager.GetActiveScene().name == "main")
				//FindObjectOfType<MainUIManager>().nameisNotValid();

			}
			else if (www.text == "")
			{
				//Debug.Log(playerName + "  Name is  valid ramesh");
				//FindObjectOfType<MainUIManager>().nameisValid();
				//FindObjectOfType<InitialLoginWindowScript>().NameIsValid();
			}
		}
	}

	[System.Obsolete]
	public void LoadScores()
	{
		if (TooManyRequests()) return;
		StartCoroutine(GetScores());
	}


	public string[] ToStringArray()
	{
		if (this.highScores == null) return null;
		if (this.highScores == "") return null;

		string[] rows = this.highScores.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
		return rows;
	}

	public List<Score> ToListLowToHigh()
	{
		Score[] scoreList = this.ToScoreArray();

		if (scoreList == null) return new List<Score>();

		List<Score> genericList = new List<Score>(scoreList);

		genericList.Sort((x, y) => x.score.CompareTo(y.score));

		return genericList;
	}

	public List<Score> ToListHighToLow()
	{
		Score[] scoreList = this.ToScoreArray();

		if (scoreList == null) return new List<Score>();

		List<Score> genericList = new List<Score>(scoreList);

		genericList.Sort((x, y) => y.score.CompareTo(x.score));

		return genericList;
	}

	public Score[] ToScoreArray()
	{
		string[] rows = ToStringArray();
		if (rows == null) return null;

		int rowcount = rows.Length;

		if (rowcount <= 0) return null;

		Score[] scoreList = new Score[rowcount];

		for (int i = 0; i < rowcount; i++)
		{
			string[] values = rows[i].Split(new char[] { '|' }, System.StringSplitOptions.None);

			Score current = new Score();
			current.playerName = values[0];
			current.score = 0;
			current.seconds = 0;
			current.shortText = "";
			current.dateString = "";
			if (values.Length > 1) current.score = CheckInt(values[1]);
			if (values.Length > 2) current.seconds = CheckInt(values[2]);
			if (values.Length > 3) current.shortText = values[3];
			if (values.Length > 4) current.dateString = values[4];
			scoreList[i] = current;
		}

		return scoreList;
	}



	// Keep pipe and slash out of names

	public static string Clean(string s)
	{
		s = s.Replace("/", "");
		s = s.Replace("|", "");
		s = s.Replace("*", "");
		return s;

	}

	int CheckInt(string s)
	{
		int x = 0;

		int.TryParse(s, out x);
		return x;
	}

}
