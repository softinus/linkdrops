﻿using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {

    static public int s_nPlayCount = 0; // store play game count
    static public TouchModes s_nPlayMode = TouchModes.E_TILT_MODE;  // save last game mode

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