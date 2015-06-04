﻿using UnityEngine;
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

			gMainBlock.GetComponent<AudioSource>().Play ();

			

            ++BlockManager2.s_nScore;

            gMainBlock.GetComponent<AudioSource>().pitch = 1.0f + ((float)BlockManager2.s_nScore / 100.0f);
            //if( BlockManager2.s_nScore >= 10 )
            //{		
            //    gMainBlock.GetComponent<AudioSource>().pitch = 1.1f;

            //    if(  BlockManager2.s_nScore >= 20)
            //    {
            //        gMainBlock.GetComponent<AudioSource>().pitch = 1.2f;
            //    }
            //}

			
        }
        else
        {
            Manager.GetComponent<BlockManager2>().BeginStart = false;
            Manager.GetComponent<BlockManager2>().bGameOver = true;
        }
    }
    

	void FixedUpdate ()
	{
        //if (link == true)
        //{
         //   gameObject.GetComponent<Animator>().enabled = true;
        //}

        if (this.gameObject.transform.tag == gMainBlock.transform.tag)
        {
            if (!this.gameObject.GetComponent<linkedCheck>().link)
            if (this.gameObject.transform.position.y < gMainBlock.transform.position.y)
            {
                Manager.GetComponent<BlockManager2>().BeginStart = false;
                Manager.GetComponent<BlockManager2>().bGameOver = true;
            }
        }
	}
}

