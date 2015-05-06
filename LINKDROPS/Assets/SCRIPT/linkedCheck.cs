using UnityEngine;
using System.Collections;

public class linkedCheck : MonoBehaviour
{

	public bool link = false;
	public GameObject Manager;


	void FixedUpdate ()
	{

		if (link == true) {

			gameObject.GetComponent<Animator>().enabled = true;


		}
	}
}

