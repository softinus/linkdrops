using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour {

    GameObject gTutorialFinger = null;
    GameObject gTutorialAccelometer = null;

	// Use this for initialization
	void Start () 
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;


        gTutorialFinger = GameObject.Find("tuto_finger");
        gTutorialAccelometer = GameObject.Find("tuto_phone");
	}

	
	// Update is called once per frame
	void Update () 
    {
        if (Global.s_nPlayMode == Global.TouchModes.E_TILT_MODE)
        {
            gTutorialFinger.SetActive(false);
            gTutorialAccelometer.SetActive(true);

            int nHighScore = PlayerPrefs.GetInt("high_score_tilt");
            GameObject gHighScore = GameObject.Find("highscore_num");
            Text txtHighScore = gHighScore.GetComponent<Text>();
            txtHighScore.text = "" + nHighScore;
        }
        else if (Global.s_nPlayMode == Global.TouchModes.E_TOUCH_MODE)
        {
            gTutorialFinger.SetActive(true);
            gTutorialAccelometer.SetActive(false);

            int nHighScore = PlayerPrefs.GetInt("high_score_slide");
            GameObject gHighScore = GameObject.Find("highscore_num");
            Text txtHighScore = gHighScore.GetComponent<Text>();
            txtHighScore.text = "" + nHighScore;
        }
	}
}
