using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {

    static public int s_nPlayCount = 0; // store play game count
    static public TouchModes s_nPlayMode = TouchModes.E_TILT_MODE;  // save last game mode

    static public int s_nScoreTlit = 0;
    static public int s_nScoreSlide = 0;

    static public int nAbsorbBlockR;
    static public int nAbsorbBlockB;
    static public int nAbsorbBlockG;
    static public int nAbsorbBlockY;
    static public int nAbsorbBlockP;


    public enum TouchModes
    {
        E_TOUCH_MODE= 0,
        E_TILT_MODE= 1
    }


	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
