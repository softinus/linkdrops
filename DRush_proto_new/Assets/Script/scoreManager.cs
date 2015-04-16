using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class scoreManager : MonoBehaviour {

	public GameObject BlockMaking;
	public Text currentScore;
	public Text highScore;


	// Use this for initialization
	void Awake () {




	}
	
	// Update is called once per frame
	void Update () {
		if (BlockMaking.GetComponent<BlockMaking_j> ().isGameOver == true) {

			int score = BlockMaking.GetComponent<BlockMaking_j> ().scrollCount;

			if(score > PlayerPrefs.GetInt("highscore")){
			PlayerPrefs.SetInt("highscore",score);
			}
		}

		currentScore.text = ""+BlockMaking.GetComponent<BlockMaking_j> ().scrollCount;
		highScore.text = ""+PlayerPrefs.GetInt ("highscore");


	}
}
