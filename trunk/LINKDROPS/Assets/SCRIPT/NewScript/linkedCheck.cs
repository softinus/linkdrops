using UnityEngine;
using System.Collections;

public class linkedCheck : MonoBehaviour
{
	public int nRowIdx= 0;
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

        if (!link)
        if (gMainBlock)
        if (this.gameObject.transform.tag == gMainBlock.transform.tag)
        {
            this.link = true;

            if (Manager)
                Manager.arrLinked.Add(this.gameObject); // add linked game objects

            // attach line to linked block
            GameObject gLinkLine = Instantiate((GameObject)Resources.Load("LinkLine"));
            gLinkLine.transform.Translate(gameObject.transform.position);
			gLinkLine.name= "LinkLine_"+nRowIdx;
            Drawline2 draw= gLinkLine.GetComponent<Drawline2>();
            draw.sourceObj = gLinkLine;
			draw.nRowIdx= nRowIdx;
            //draw.destiPosition = gMainBlock.transform.position;
            gLinkLine.transform.SetParent(this.gameObject.transform);        
        }
        else
        {
            //Manager.GetComponent<BlockManager2>().BeginStart = false;
            Manager.BeginStart = false;
        }
    }
    

	void FixedUpdate ()
	{
        //if (link == true)
        //{
        //    gameObject.GetComponent<Animator>().enabled = true;
        //}
	}
}

