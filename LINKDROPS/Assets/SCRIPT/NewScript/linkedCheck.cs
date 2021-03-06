﻿using UnityEngine;
using System.Collections;

public class linkedCheck : MonoBehaviour
{
	public int nRowIdx= 0;
	public bool link = false;
	public BlockManager2 Manager;
    protected GameObject gMainBlock;
    private GameObject gGameOverLimitY;

    
    private void GameOver()
    {
        if (Manager.GetComponent<BlockManager2>().bGameOver)    // call one time.
            return;

        buttonManager.bPressStart = false;

        

        //this.gameObject.GetComponent<Animator>().enabled = true;

        Manager.GetComponent<BlockManager2>().BeginStart = false;
        Manager.GetComponent<BlockManager2>().bGameOver = true;
        BlockManager2.SaveData();
    }

    void Start()
    {
        //selectedBlock = Manager.GetComponent<BlockManager2>()
        gMainBlock= GameObject.Find("main_block");
        gGameOverLimitY = GameObject.Find("GameOverLimitY");

    }

    void AbsorbCount()
    {
        if( this.gameObject.transform.tag == "yellow")
            ++Global.nAbsorbBlockY;
        if (this.gameObject.transform.tag == "red")
            ++Global.nAbsorbBlockR;
        if (this.gameObject.transform.tag == "blue")
            ++Global.nAbsorbBlockB;
        if (this.gameObject.transform.tag == "green")
            ++Global.nAbsorbBlockG;
        if (this.gameObject.transform.tag == "purple")
            ++Global.nAbsorbBlockP;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        
        //coll.gameObject.GetComponent<Animator>().enabled = true;
		if (!link)
        if (gMainBlock)
        if (this.gameObject.transform.tag == gMainBlock.transform.tag)
        {   // when connect link
            AbsorbCount();

            this.link = true;
		    if (Manager)
                Manager.arrLinked.Add(this.gameObject); // add linked game objects

            // attach line to linked block
            GameObject gLinkLine = Instantiate((GameObject)Resources.Load("line/LinkLine"));
			gLinkLine.transform.Translate(gameObject.transform.position);
			gLinkLine.name= "LinkLine_"+nRowIdx;
            Drawline2 draw= gLinkLine.GetComponent<Drawline2>();
            draw.sourceObj = gLinkLine;
			draw.nRowIdx= nRowIdx;
            //draw.destiPosition = gMainBlock.transform.position;
            gLinkLine.transform.SetParent(this.gameObject.transform);

			gMainBlock.GetComponent<AudioSource>().Play ();


            if (Global.s_nPlayMode == Global.TouchModes.E_TOUCH_MODE)
            {
                ++Global.s_nScoreSlide;
                // manage pitch of SE
                gMainBlock.GetComponent<AudioSource>().pitch = 1.0f + ((float)Global.s_nScoreSlide / 100.0f);
            }
            else
            {
                ++Global.s_nScoreTlit;
                gMainBlock.GetComponent<AudioSource>().pitch = 1.0f + ((float)Global.s_nScoreTlit / 100.0f);
            }

            

			//block_particle_play
				this.transform.FindChild("blockParticle").GetComponent<ParticleSystem>().Play();

				//this.transform.FindChild("blockParticle").GetComponent<ParticleSystem>().Stop();


			
        }
        else
        {
            GameOver();
            
        }

		this.GetComponent<SpriteRenderer> ().enabled = false;
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
            if (this.gameObject.transform.position.y < gGameOverLimitY.transform.position.y)  // game over condition 2
            {
                
                GameOver();
            }
        }
	}
}

