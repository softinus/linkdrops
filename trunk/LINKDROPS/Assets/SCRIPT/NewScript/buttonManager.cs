using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class buttonManager : MonoBehaviour {

	static public GameObject manager;



	public void main_sceneLoad(){

		Application.LoadLevel (1);
	}

	public void retry_sceneLoad(){

		Application.LoadLevel (1);
	}

    public void Share()
    {
        Social.ShowLeaderboardUI();
        //Social.ShowLeaderboardUI();
    }

	void Awake(){
		manager = GameObject.Find ("Manager");
	}



		}



