using UnityEngine;
using System.Collections;

public class checkCollision : MonoBehaviour
{

	public GameObject Manager;
	public SpriteRenderer blockSprit;

	void OnTriggerEnter2D (Collider2D coll)
	{


		if (coll.gameObject.tag == "P_block") {

			Manager.GetComponent<BlockMaking_j> ().isOverlapPath = true;
			coll.gameObject.GetComponent<linkedCheck> ().link = true;


		}

	
		if (coll.gameObject.tag == "P_block" && coll.gameObject.tag != "P_block") {

			Manager.GetComponent<BlockMaking_j> ().isOverlapPath = true;
			coll.gameObject.GetComponent<linkedCheck> ().link = true;
			
			
		}

		if (coll.gameObject.tag != "P_block") {
			
			if (Manager.GetComponent<BlockMaking_j> ().isStarted == true) {
				
				Manager.GetComponent<BlockMaking_j> ().Life -= 1;
				
				if (Manager.GetComponent<BlockMaking_j> ().Life <= 0) {
					
					Manager.GetComponent<BlockMaking_j> ().isOverlapPath = false;
				}
			}
		}
	}

}

