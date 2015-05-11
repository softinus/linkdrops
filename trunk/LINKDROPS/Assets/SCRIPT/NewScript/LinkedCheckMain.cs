using UnityEngine;
using System.Collections;

public class LinkedCheckMain : MonoBehaviour {

    public GameObject gManager= null;
	// Use this for initialization
	void Start ()
    {

	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (gManager)
        {
            //if (gManager.GetComponent<BlockManager2>().BeginStart == false)
            //    gManager.GetComponent<BlockManager2>().BeginStart = true;

            //GameObject gStartBlock= GameObject.Find("main_block");
            //gStartBlock.GetComponent<SpriteRenderer>().enabled = false;
            //gManager.GetComponent<Animator>().enabled = false;
        }
        
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
	
	}
}
