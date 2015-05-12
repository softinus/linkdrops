using UnityEngine;
using System.Collections;

public class linkedCheck : MonoBehaviour
{

	public bool link = false;
	public BlockManager2 Manager;
    protected GameObject gMainBlock;

    void Start()
    {
        //selectedBlock = Manager.GetComponent<BlockManager2>()
        gMainBlock= GameObject.Find("main_block");
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //coll.gameObject.GetComponent<Animator>().enabled = true;

        if (this.gameObject.transform.tag == gMainBlock.transform.tag)
            this.link = true;
        else
        {
            //Manager.GetComponent<BlockManager2>().BeginStart = false;
            Manager.BeginStart = false;
        }
    }
    

	void FixedUpdate ()
	{
        if (link == true)
        {
            gameObject.GetComponent<Animator>().enabled = true;
        }
	}
}

