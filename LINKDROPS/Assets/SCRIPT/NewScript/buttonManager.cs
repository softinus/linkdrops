﻿using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using System.IO;
using Parse;
using System.Threading.Tasks;
using UnityEngine.UI;

public class buttonManager : MonoBehaviour {

    
	static public GameObject manager;

    //static private GameObject canvas;

    //bool bSlideMode = true;

    void OnGUI()
    {
    }




    public void ChangeMode()
    {
        if (Global.s_nPlayMode == Global.TouchModes.E_TOUCH_MODE)
        {
            Global.s_nPlayMode = Global.TouchModes.E_TILT_MODE;
            GameObject.Find("TXTmode").GetComponent<Text>().text = "TILT MODE";
            
        }
        else
        {
            Global.s_nPlayMode = Global.TouchModes.E_TOUCH_MODE;
            GameObject.Find("TXTmode").GetComponent<Text>().text = "SLIDE MODE";
        }


    }



	public void main_gameStart()
    {


		//manager.GetComponent<BlockManager2> ().BeginStart = true;
		//Application.LoadLevel (1);
        GameObject.Find("startCanvas").SetActive(false);
		if(manager.GetComponent<BlockManager2> ().bGameOver == false)
        {
            if (Global.s_nPlayMode == Global.TouchModes.E_TOUCH_MODE)
            {
                manager.GetComponent<TouchManager>().enabled = false;
                manager.GetComponent<TouchManager2>().enabled = true;
            }
            else
            {
                manager.GetComponent<TouchManager>().enabled = true;
                manager.GetComponent<TouchManager2>().enabled = false;
            }

		    manager.GetComponent<BlockManager2> ().BeginStart = true;
		}
	}

    public void GoHome()
    {
        Application.LoadLevel(0);
    }


    public void retry_sceneLoad()
    {
        BlockManager2.retry = true;
        Application.LoadLevel(0);
    }

    //    StartCoroutine(ShowCanvas());

    //}

    //IEnumerator ShowCanvas()
    //{
    //    yield return new WaitForSeconds(1);
    //    GameObject.Find("startCanvas").SetActive(false);        
    //}


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

	//change mode button controll
	public void modeChanger ()
	{


	}


    //public void Share()
    //{
    //    //GameObject g1= GameObject.Find("retryButton");
    //    //GameObject g2= GameObject.Find("retryButton 1");
    //    //g1.active = false;
    //    //g2.active = false;
    //    //Application.CaptureScreenshot("test.png");

    //    // save picture
    //    Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
    //    tex.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0, true);
    //    tex.Apply();
    //    //byte[] captureScreenShot = tex.EncodeToPNG();
    //    //DestroyImmediate(tex);
    //    //File.WriteAllBytes("/MyImage.png", captureScreenShot);

    //    // Save your image on designate path
    //    byte[] bytes = tex.EncodeToPNG();
    //    string path = Application.persistentDataPath + "/MyImage.png";
    //    File.WriteAllBytes(path, bytes);
 
    //    AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
    //    AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

    //    intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
    //    intentObject.Call<AndroidJavaObject>("setType", "image/*");
    //    intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "Media Sharing ");
    //    intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), "Media Sharing ");
    //    intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "Media Sharing Android Demo");
 
    //    AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
    //    AndroidJavaClass fileClass = new AndroidJavaClass("java.io.File");
 
    //    AndroidJavaObject fileObject = new AndroidJavaObject("java.io.File", path);// Set Image Path Here
    //    AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("fromFile", fileObject);
 
    //    ////	string uriPath =  uriObject.Call("getPath");
    //    //bool fileExist= fileObject.Call("exists");
    //    //Debug.Log("File exist : " + fileExist);
    //    // Attach image to intent
    //    //if (fileExist)
    //    intentObject.Call("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
    //    AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    //    AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");       
    //    currentActivity.Call("startActivity", intentObject);
    //    //- See more at: http://www.theappguruz.com/unity/general-sharing-in-android-ios-in-unity/#sthash.59kcY2Lz.dpuf

    //    //AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
    //    //AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
    //    //intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
    //    //intentObject.Call<AndroidJavaObject>("setType", "text/plain");
    //    //intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "SUBJECT");
    //    //intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "This is my text to send.");
    //    //AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    //    //AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
    //    //currentActivity.Call("startActivity", intentObject);
    //}

    private string destination = "";
    private bool isProcessing = false;
    public void Share2()
    {
        if (!isProcessing)
            StartCoroutine(ShareScreenshot());
    }

    public IEnumerator ShareScreenshot()
    {
        isProcessing = true;

        // wait for graphics to render
        yield return new WaitForEndOfFrame();
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
        // create the texture
        Texture2D screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
        // put buffer into texture
        screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0);
        // apply
        screenTexture.Apply();
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO

        byte[] dataToSave = screenTexture.EncodeToPNG();

        destination = Path.Combine(Application.persistentDataPath, System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");

        File.WriteAllBytes(destination, dataToSave);



        if (!Application.isEditor)
        {


            // block to open the file and share it ------------START
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
            //intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "testo");
            //intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "SUBJECT");
            intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

            // option one WITHOUT chooser:
            currentActivity.Call("startActivity", intentObject);

            // option two WITH chooser:
            //AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "YO BRO! WANNA SHARE?");
            //currentActivity.Call("startActivity", jChooser);

            // block to open the file and share it ------------END

        }
        isProcessing = false;
        //GetComponent<GUITexture>().enabled = true;
    }


	void Awake()
    {
		manager = GameObject.Find ("Manager");
        //canvas= GameObject.Find("startCanvas");

	}


    void Update()
    {
    }


}



