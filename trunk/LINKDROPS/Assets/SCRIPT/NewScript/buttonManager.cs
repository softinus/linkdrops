using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using System.IO;

public class buttonManager : MonoBehaviour {

	static public GameObject manager;



	public void main_sceneLoad(){

		Application.LoadLevel (1);
	}

	public void retry_sceneLoad(){

		Application.LoadLevel (1);
	}

    public void GoHome()
    {
        Application.LoadLevel(0);
    }


    public void GoStore()
    {
        //if(Platfo)
        Application.OpenURL("market://details?id=com.gabina.linkman");
    }

    public void ShowAchievement()
    {
        Social.ShowAchievementsUI();
    }

    public void ShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }

    public void Share()
    {
        //GameObject g1= GameObject.Find("retryButton");
        //GameObject g2= GameObject.Find("retryButton 1");
        //g1.active = false;
        //g2.active = false;
        //Application.CaptureScreenshot("test.png");

        // save picture
        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
        tex.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0, true);
        tex.Apply();
        //byte[] captureScreenShot = tex.EncodeToPNG();
        //DestroyImmediate(tex);
        //File.WriteAllBytes("/MyImage.png", captureScreenShot);

        // Save your image on designate path
        byte[] bytes = tex.EncodeToPNG();
        string path = Application.persistentDataPath + "/MyImage.png";
        File.WriteAllBytes(path, bytes);
 
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        intentObject.Call<AndroidJavaObject>("setType", "image/*");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "Media Sharing ");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), "Media Sharing ");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "Media Sharing Android Demo");
 
        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        AndroidJavaClass fileClass = new AndroidJavaClass("java.io.File");
 
        AndroidJavaObject fileObject = new AndroidJavaObject("java.io.File", path);// Set Image Path Here
        AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("fromFile", fileObject);
 
        ////	string uriPath =  uriObject.Call("getPath");
        //bool fileExist= fileObject.Call("exists");
        //Debug.Log("File exist : " + fileExist);
        // Attach image to intent
        //if (fileExist)
        intentObject.Call("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");       
        currentActivity.Call("startActivity", intentObject);
        //- See more at: http://www.theappguruz.com/unity/general-sharing-in-android-ios-in-unity/#sthash.59kcY2Lz.dpuf

        //AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        //AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
        //intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        //intentObject.Call<AndroidJavaObject>("setType", "text/plain");
        //intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "SUBJECT");
        //intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "This is my text to send.");
        //AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        //currentActivity.Call("startActivity", intentObject);
    }


	void Awake(){
		manager = GameObject.Find ("Manager");
	}



		}



