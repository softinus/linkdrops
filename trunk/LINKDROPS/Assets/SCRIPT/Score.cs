using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {
	
	Text scoreText;

	BlockMaking blockMakerScript = null;
	public GameObject blockMaker;
	public int score;
	public int currentScore;
	// Use this for initialization
	void Awake () {

		scoreText = GetComponent<Text>();
		score = 0;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (blockMakerScript == null)
			blockMakerScript = blockMaker.GetComponent<BlockMaking> ();
		score = blockMakerScript.scrollCount;
		scoreText.text = "" + score;
		/*
		if (score % 10 >= 0 && score % 10 <= 3) {
		

		
		} 
		*/
		}
}
