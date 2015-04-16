using UnityEngine;
using System.Collections;

public class outChecker : MonoBehaviour
{

	public GameObject Manager;
	public bool checkedOut = false;

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "P_block") {

			Debug.Log ("stop1");

			if (coll.gameObject.GetComponent<linkedCheck> ().link == false) {
		
				Debug.Log ("stop2");
				Manager.GetComponent<BlockMaking_j> ().isGameOver = true;

			}
		}

	
	}
}
