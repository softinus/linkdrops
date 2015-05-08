using UnityEngine;
using System.Collections;

public class linkedCheck : MonoBehaviour
{

	public bool link = false;
	public GameObject Manager;

    void OnTriggerEnter2D(Collider2D coll)
    {
        coll.gameObject.GetComponent<Animator>().enabled = true;
    }
    

	void FixedUpdate ()
	{
        
        //if (link == true)
        //{

        //    gameObject.GetComponent<Animator>().enabled = true;


        //}
	}
}

