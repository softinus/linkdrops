using UnityEngine;
using System.Collections;

public class LinkedCheckMain : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {

	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        //if (GetComponent<BlockManager2>().GameStart == false)
        //  GetComponent<BlockManager2>().GameStart = true;
        //coll.gameObject.GetComponent<Animator>().enabled = false;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
	
	}
}
