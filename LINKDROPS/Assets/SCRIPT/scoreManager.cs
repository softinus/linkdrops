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
		if (BlockMaking.GetComponent<BlockMaking> ().isGameOver == true) {

			int score = BlockMaking.GetComponent<BlockMaking> ().scrollCount;

			if(score > PlayerPrefs.GetInt("highscore")){
			PlayerPrefs.SetInt("highscore",score);
			}
		}

		currentScore.text = ""+BlockMaking.GetComponent<BlockMaking> ().scrollCount;
		highScore.text = ""+PlayerPrefs.GetInt ("highscore");


	}
}
