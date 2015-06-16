using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        if(Global.s_nPlayMode == Global.TouchModes.E_TILT_MODE)
        {
            Global.s_nPlayMode = Global.TouchModes.E_TILT_MODE;
            GameObject.Find("TXTmode").GetComponent<Text>().text = "TILT MODE";
        }
        else if (Global.s_nPlayMode == Global.TouchModes.E_TOUCH_MODE)
        {
            Global.s_nPlayMode = Global.TouchModes.E_TOUCH_MODE;
            GameObject.Find("TXTmode").GetComponent<Text>().text = "SLIDE MODE";
        }

        int nHighScore = PlayerPrefs.GetInt("high_score");
        GameObject gHighScore= GameObject.Find("highscore_num");
        Text txtHighScore= gHighScore.GetComponent<Text>();
        txtHighScore.text = "" + nHighScore;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
