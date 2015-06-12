using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {

        int nHighScore = PlayerPrefs.GetInt("high_score");
        GameObject gHighScore= GameObject.Find("highscore_num");
        Text txtHighScore= gHighScore.GetComponent<Text>();
        txtHighScore.text = "" + nHighScore;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
