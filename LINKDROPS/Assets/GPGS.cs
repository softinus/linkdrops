using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public class GPGS : MonoBehaviour {

    void OnGUI()
    {
        GUILayout.Label("GPGS : " + bLogined);
    }

        bool bLogined = false;
    void Awake()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }
	// Use this for initialization
	void Start () {
        Social.localUser.Authenticate(
            (bool success) =>
            {
                bLogined = success;
            });
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
