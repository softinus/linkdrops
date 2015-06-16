using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;

using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GPGS : MonoBehaviour {

    bool bLogined = false;
    void OnGUI()
    {
        //GUILayout.Label("GPGS : " + bLogined);
    }

    void Awake()
    {
        //PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        //// enables saving game progress.
        //.EnableSavedGames()
        //// registers a callback to handle game invitations received while the game is not running.
        //.WithInvitationDelegate(<callback method>)
        //// registers a callback for turn based match notifications received while the
        //// game is not running.
        //.WithMatchDelegate(<callback method>)
        //.Build();

        //PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
    }


	// Use this for initialization
	void Start ()
    {
        LoginGPGS();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// GPGS를 로그인 합니다.
    /// </summary>
    public void LoginGPGS()
    {
        // 로그인이 안되어 있으면
        if (!Social.localUser.authenticated)
            Social.localUser.Authenticate(LoginCallBackGPGS);
    }

    /// <summary>
    /// GPGS Login Callback
    /// </summary>
    /// <param name="result"> 결과 </param>
    public void LoginCallBackGPGS(bool result)
    {
        bLogined = result;
    }
 
}
