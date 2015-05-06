using UnityEngine;
using System.Collections;

public class outChecker : MonoBehaviour
{

	public GameObject Manager;
	private GameObject selectedBlock;
	public bool checkedOut = false;

	void Start(){
		
		selectedBlock = Manager.GetComponent<BlockMaking> ().selectedBlock;	
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == selectedBlock.tag) {

			Debug.Log ("stop1");

			if (coll.gameObject.GetComponent<linkedCheck> ().link == false) {
		
				Debug.Log ("stop2");
				Manager.GetComponent<BlockMaking> ().isGameOver = true;

			}
		}

	
	}
}
