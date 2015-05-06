using UnityEngine;
using System.Collections;

public class Bg_scrolling : MonoBehaviour {


	public Transform scrollstartPos;
	public Transform scrollendPos;
	public GameObject blockmaking;
	public float scrollSpeed;
	
	
	void Start ()
	{
	}
	
	void Update ()
	{
		transform.Translate (0.0f, scrollSpeed, 0.0f);

		if (transform.position.y <= scrollendPos.position.y) {

			transform.position = scrollstartPos.position;
		}

		if (blockmaking.GetComponent<BlockMaking> ().isGameOver == true) {
		
			scrollSpeed = 0;
		
		}

	}
}
