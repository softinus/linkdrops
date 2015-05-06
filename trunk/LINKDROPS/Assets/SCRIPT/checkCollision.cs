using UnityEngine;
using System.Collections;

public class checkCollision : MonoBehaviour
{

	public GameObject Manager;
	public SpriteRenderer blockSprit;
	public GameObject selectedBlock;
	public GameObject lineBase;

	void Start(){
	
		selectedBlock = Manager.GetComponent<BlockMaking> ().selectedBlock;	
	}

	void OnTriggerEnter2D (Collider2D coll)
	{


		if (coll.gameObject.tag == selectedBlock.tag) {

			Manager.GetComponent<BlockMaking> ().isOverlapPath = true;
			coll.gameObject.GetComponent<linkedCheck> ().link = true;


		}

	
		if (coll.gameObject.tag == selectedBlock.tag && coll.gameObject.tag != selectedBlock.tag) {

			Manager.GetComponent<BlockMaking> ().isOverlapPath = true;
			coll.gameObject.GetComponent<linkedCheck> ().link = true;
			
			
		}

		if (coll.gameObject.tag != selectedBlock.tag) {
			
			if (Manager.GetComponent<BlockMaking> ().isStarted == true) {
				
				//Manager.GetComponent<BlockMaking_j> ().Life -= 1;
				
				//if (Manager.GetComponent<BlockMaking_j> ().Life <= 0) {
					
					//Manager.GetComponent<BlockMaking_j> ().isOverlapPath = false;
				}
			}
		}
	}

//}

