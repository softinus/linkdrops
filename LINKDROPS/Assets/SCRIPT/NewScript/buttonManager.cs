using UnityEngine;
using System.Collections;

public class buttonManager : MonoBehaviour {

	static public GameObject manager;



	public void main_sceneLoad(){

		Application.LoadLevel (1);
	}

	public void retry_sceneLoad(){

		Application.LoadLevel (1);

	}

	void Awake(){
		manager = GameObject.Find ("Manager");
	}



		}



