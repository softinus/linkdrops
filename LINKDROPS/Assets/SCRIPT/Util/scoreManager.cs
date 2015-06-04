using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class scoreManager : MonoBehaviour {

    public GameObject managerObj;
    public Text highscoreText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        int highScore = BlockManager2.s_nScore;

        highscoreText.text = "" + highScore;

		//this.gameObject.GetComponent<Text>().color.a = 10;


	}
}
