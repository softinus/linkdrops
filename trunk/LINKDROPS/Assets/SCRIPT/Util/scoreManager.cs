using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class scoreManager : MonoBehaviour {

    public GameObject managerObj;
    public Text highscoreText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Global.s_nPlayMode == Global.TouchModes.E_TOUCH_MODE)
        {
            int highScore = Global.s_nScoreSlide;
            highscoreText.text = "" + highScore;
        }
        else
        {
            int highScore = Global.s_nScoreTlit;
            highscoreText.text = "" + highScore;
        }

		//this.gameObject.GetComponent<Text>().color.a = 10;


	}
}
