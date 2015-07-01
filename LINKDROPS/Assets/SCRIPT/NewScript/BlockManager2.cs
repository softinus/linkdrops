using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]  //  MonoBehaviour가 아닌 클래스에 대해 Inspector에 나타내기.
public class RowItems
{
    public GameObject[] items;
    public ArrayList arr1row = new ArrayList();

    protected int nWidth;    
    protected int nSelectTag;
    protected string strSelectTag;
    protected int nPrevPos = -1;
    protected int nFixedPos = -1;
    //protected ArrayList arrColors = new ArrayList();

    
   

    public int FixedPos
    {
        get { return nFixedPos; }
        set { nFixedPos = value; }
    }
    public int PrevPos
    {
        get { return nPrevPos; }
        set { nPrevPos = value; }
    }
    public int Width
    {
        get { return nWidth; }
        set { nWidth = value; }

    }
    public int SelectTag
    {
        get { return nSelectTag; }
        set
        {
            nSelectTag = value;
            if (nSelectTag == 0)
                strSelectTag = "green";
            else if (nSelectTag == 1)
                strSelectTag = "red";
            else if (nSelectTag == 2)
                strSelectTag = "purple";
            else if (nSelectTag == 3)
                strSelectTag = "yellow";
            else if (nSelectTag == 4)
                strSelectTag = "blue";
        }
    }
    public string Tag
    {
        get { return strSelectTag; }
    }

    //연두색: 187 249 98
    //빨간색: 255 86	86
    //노란색: 255 215 94
    //파란색: 0 168 255
    //보라색: 136 77 147
    public Color GetSelectColor()
    {
        Color res = new Color(255, 255, 255);
        if(this.strSelectTag == "green")
            res = new Color(187, 249, 98);
        else if(this.strSelectTag == "red")
            res = new Color(255, 86, 86);
        else if(this.strSelectTag == "yellow")
            res = new Color(255, 215, 94);
        else if(this.strSelectTag == "blue")
            res = new Color(0, 168, 255);
        else if(this.strSelectTag == "purple")
            res = new Color(136, 77, 147);
        return res;
    }

    // find pos of selected block
    public int GetSelectedBlockPos()
    {
        for (int i = 0; i < arr1row.Count; ++i)
        {
            GameObject GO = arr1row[i] as GameObject;
            if (GO.tag == this.Tag)
                return i;
        }
        return -1;  // if cannot find the block
    }


	public void Awake(){
	


	}

   

	public void ChangeSelectBlockTo()
    {
        if(nPrevPos == -1)  // hasn't previous position
        {
            int nCurrSel = this.GetSelectedBlockPos();  // get location of selected block
            if (nCurrSel == -1)  // if selected block is not located in current row, should make a new one.
            {
                int nNewPos = Random.Range(0, nWidth);
                arr1row[nNewPos] = items[nSelectTag];
            }
        }
        else    // if it has previous position
        {
            int nNewPos = -1;
            while(true) // find new proper position.
            {
                //if (nPrevPos == 0 || nPrevPos == nWidth-1)  // if previous position is
                //{
                //}

                nNewPos = Random.Range(nPrevPos - 1, nPrevPos + 2);
                if (nNewPos == -1)
                { }
                else if (nNewPos == nWidth)
                { }
                else
                    break;
            }

            if (arr1row.Count < nNewPos)
                nNewPos = 0;

            int nCurrSel = this.GetSelectedBlockPos();  // get location of selected block
            if(nCurrSel == -1)  // if selected block is not located in current row, should make a new one.
            {
                arr1row[nNewPos] = items[nSelectTag];
            }
            else // make sure to change position between selected block and normal block
            {
                if(nNewPos != nCurrSel) // if both are same 
                {   // change respectively 
                    GameObject tmpGO = arr1row[nNewPos] as GameObject;
                    arr1row[nNewPos] = arr1row[nCurrSel];
                    arr1row[nCurrSel] = tmpGO;
                }
            }
        }
    }

    
    public void Randomly(bool bInit)
    {
        if (bInit)
            arr1row.Clear();

        // look to make the random items.
        for (int i = 0; i < nWidth; ++i)
        {
            int nIdx = 0;
            bool bExist = true;
            while (bExist)
            {
                bool bCheck = false;
                nIdx = Random.Range(0, items.Length);
                foreach (GameObject GO in arr1row)
                {
                    if (GO == items[nIdx]) // if it has already existed in list
                    {
                        bCheck = true;
                        break;
                    }
                }

                bExist = bCheck;
            }
            arr1row.Add(items[nIdx]);
        }
    }


}

public class BlockManager2 : MonoBehaviour
{


	public int nWidth= 5;
    public int nHeight = 15;
	public float fSpeed= 115f;   // 
    public float fIncreseSpeed = 0.07f; // 
    public float fYdistance = 120f;
    public float fRadiusForAvoid;
    public float fRadiusForAbsorb;

    public ArrayList arrLinked = new ArrayList();
    public RowItems[] groups;

    protected float fDistance = 0.0f;
    protected int nSelectedGroup = 0;

    protected int nSelectedItem= 0;
    protected int nRowCount = 0;
    //protected GameObject gLatestGO;  // the latest game object
    public bool BeginStart = false;    // started?
    public bool bGameOver= false;    // is game over?
	static public bool retry = false;

    private int nNextStandardPos = -1;  // previous position moved by force

    public int SelectedGroup
    {
        get { return nSelectedGroup; }
        set { nSelectedGroup = value; }
    }
	//Queue arr= new Queue();
    //GameObject gMainBlock;
    
	Vector3 vRootPos;

    ArrayList arrXpos = new ArrayList();

    // make main block
    private void MakeTouchBlock(float _screenWidth)
    {
        GameObject gStartBlock = Instantiate((GameObject)Resources.Load("pacman/pacman_1"));
        //GameObject gStartBlock = Instantiate(groups[nSelectedGroup].items[nSelectedItem]);
        gStartBlock.name = "main_block";    // set name for controllable
        gStartBlock.tag = groups[nSelectedGroup].items[nSelectedItem].tag;  // set tag distinguish between the color of blocks
        
        gStartBlock.transform.position = GameObject.Find("MainBlockStartPosition").transform.position;

        LinkedCheckMain gLinkScriptMain= gStartBlock.AddComponent<LinkedCheckMain>();
        
    }


    private float CalcEachBlockXPos(int x, int i)    // row and each columns
    {
        int n = groups[nSelectedGroup].items.Length + 1;

        var ScreenHeight = 2 * Camera.main.orthographicSize;
        float w = ScreenHeight * Camera.main.aspect;
        float per20 = w * 0.2f;

        float currXpos = (n - x) * w / 10 + (per20 * i);
        return currXpos;
    }


    private void Make1Row(int y)
    {
        int nBoardX = 0;
        //if (Application.platform == RuntimePlatform.Android)
        //    nBoardX = -40;
        //else if(Application.platform == RuntimePlatform.IPhonePlayer)
        //    nBoardX = 0;
        //else if (Application.platform == RuntimePlatform.WindowsEditor)
        //    nBoardX = 0;
        //else
        //    nBoardX = 0;


        GameObject gBoard = new GameObject();
        if(y<nHeight)   // first gen
        {
            gBoard = Instantiate(gBoard);
            gBoard.name = "board" + y;
            gBoard.transform.tag = "board";
            gBoard.transform.position = new Vector3(nBoardX, 500 + (fYdistance * y), 40);
        }
        else
        {
            GameObject l= GameObject.Find("board" + (y-1));
            gBoard = Instantiate(gBoard);
            gBoard.name = "board" + y;
            gBoard.transform.tag = "board";
            gBoard.transform.position = new Vector3(nBoardX, l.transform.position.y + fYdistance, 40); 
        }

        // destroy blank objects
        GameObject GOforDestory = GameObject.Find("New Game Object");
        if (GOforDestory)
            Destroy(GOforDestory);  
        
        vRootPos = gBoard.transform.position;
        

        groups[nSelectedGroup].Randomly(true);  // sorting

        if (nNextStandardPos != -1)
        {
            groups[nSelectedGroup].PrevPos = nNextStandardPos;
            nNextStandardPos = -1;
        }

        groups[nSelectedGroup].ChangeSelectBlockTo();  //

        groups[nSelectedGroup].PrevPos = groups[nSelectedGroup].GetSelectedBlockPos();  // refresh

        for (int x = 0; x < groups[nSelectedGroup].arr1row.Count; ++x)
        {
            GameObject o = null;//arr[i] as GameObject;
            o = Instantiate(groups[nSelectedGroup].arr1row[x] as GameObject,
                //new Vector3(vRootPos.x + ((x + 1) * fDistance),
                new Vector3((float)arrXpos[x],
                    vRootPos.y,
                    100),
                    Quaternion.Euler(0, 0, 0)) as GameObject;
            o.transform.SetParent(gBoard.transform);

            //if (groups[nSelectedGroup].PrevPos == x) // if it's selected color
              //  o.transform.tag = "selected"; // if selected
            
            CircleCollider2D collider = o.AddComponent<CircleCollider2D>(); // add collider
            collider.isTrigger = true;

            if (groups[nSelectedGroup].PrevPos == x)    // adjust the radius of blocks
                collider.radius = fRadiusForAbsorb;
            else
                collider.radius = fRadiusForAvoid;                    

            linkedCheck linkedScript= o.GetComponent<linkedCheck>();
            linkedScript.Manager = this;
			linkedScript.nRowIdx= y;	// set row index
        }
    }


    void ChangeWidth(int _nWid)
    {
        foreach (RowItems BI in groups)
        {
            BI.Width = _nWid;
        }

        arrXpos.Clear();
        for (int i = 0; i < _nWid; ++i )
        {            
            float fXpos = CalcEachBlockXPos(_nWid, i);
            arrXpos.Add(fXpos);
        }
            
    }

    //void ChangeColor()
    //{
    //    nSelectedItem = Random.Range(0, nWidth);    // select color
    //    foreach (RowItems BI in groups)
    //    {came
    //        BI.SelectTag = nSelectedItem;
    //    }
    //    GameObject gMain= GameObject.Find("main_block");
    //    gMain.tag = groups[nSelectedGroup].items[nSelectedItem].tag;
    //}

    void OnGUI()
    {

        //GUILayout.Space(5);
        //GUILayout.Label("current");
        //GUILayout.Label("y : " + nAbsorbBlockY, GUILayout.Width(150));
        //GUILayout.Label("r : " + nAbsorbBlockR, GUILayout.Width(150));
        //GUILayout.Label("g : " + nAbsorbBlockG, GUILayout.Width(150));
        //GUILayout.Label("b : " + nAbsorbBlockB, GUILayout.Width(150));
        //GUILayout.Label("p : " + nAbsorbBlockP, GUILayout.Width(150));

        if (bGameOver)    // game over
        {
            //GUILayout.Space(20);
            //GUILayout.Label("all");
            //GUILayout.Label("y : " + PlayerPrefs.GetInt("yellow_block"), GUILayout.Width(150));
            //GUILayout.Label("r : " + PlayerPrefs.GetInt("red_block"), GUILayout.Width(150));
            //GUILayout.Label("g : " + PlayerPrefs.GetInt("green_block"), GUILayout.Width(150));
            //GUILayout.Label("b : " + PlayerPrefs.GetInt("blue_block"), GUILayout.Width(150));
            //GUILayout.Label("p : " + PlayerPrefs.GetInt("purple_block"), GUILayout.Width(150));

            ////
            //GUILayout.Space(10);
            //GUILayout.Label("score : " + BlockManager2.s_nScore, GUILayout.Width(150));

            //int nMyHighScore = PlayerPrefs.GetInt("high_score_slide");
            //GUILayout.Label("high_score_slide : " + nMyHighScore, GUILayout.Width(150));

			//pacman_died_animation_stop

        }
        

    }

    static public void SaveData()
    {
        PlayerPrefs.SetInt("blue_block",    PlayerPrefs.GetInt("blue_block")    + Global.nAbsorbBlockB);
        PlayerPrefs.SetInt("red_block",     PlayerPrefs.GetInt("red_block")     + Global.nAbsorbBlockR);
        PlayerPrefs.SetInt("yellow_block", PlayerPrefs.GetInt("yellow_block") + Global.nAbsorbBlockY);
        PlayerPrefs.SetInt("purple_block", PlayerPrefs.GetInt("purple_block") + Global.nAbsorbBlockP);
        PlayerPrefs.SetInt("green_block", PlayerPrefs.GetInt("green_block") + Global.nAbsorbBlockG);
        Global.nAbsorbBlockR = 0;
        Global.nAbsorbBlockB = 0;
        Global.nAbsorbBlockG = 0;
        Global.nAbsorbBlockY = 0;
        Global.nAbsorbBlockP = 0;

        if (PlayerPrefs.GetInt("blue_block") > 100)
        {
            Social.ReportProgress("CgkIuKTZ6sIaEAIQFw", 100.0f, (bool success) =>
            {
            });
        }
        if (PlayerPrefs.GetInt("blue_block") > 1000)
        {
            Social.ReportProgress("CgkIuKTZ6sIaEAIQGA", 100.0f, (bool success) =>
            {
            });
        }
        if (PlayerPrefs.GetInt("blue_block") > 2000)
        {
            Social.ReportProgress("CgkIuKTZ6sIaEAIQGQ", 100.0f, (bool success) =>
            {
            });
        }
        if (PlayerPrefs.GetInt("blue_block") > 5000)
        {
            Social.ReportProgress("CgkIuKTZ6sIaEAIQGg", 100.0f, (bool success) =>
            {
            });
        }

        if (PlayerPrefs.GetInt("green_block") > 100)
        {
            Social.ReportProgress("CgkIuKTZ6sIaEAIQFA", 100.0f, (bool success) =>
            {
            });
        }
        if (PlayerPrefs.GetInt("green_block") > 1000)
        {
            Social.ReportProgress("CgkIuKTZ6sIaEAIQGw", 100.0f, (bool success) =>
            {
            });
        }
        if (PlayerPrefs.GetInt("green_block") > 2000)
        {
            Social.ReportProgress("CgkIuKTZ6sIaEAIQFQ", 100.0f, (bool success) =>
            {
            });
        }
        if (PlayerPrefs.GetInt("green_block") > 5000)
        {
            Social.ReportProgress("CgkIuKTZ6sIaEAIQFg", 100.0f, (bool success) =>
            {
            });
        }


        if (PlayerPrefs.GetInt("purple_block") > 100)
        {
            Social.ReportProgress("CgkIuKTZ6sIaEAIQEA", 100.0f, (bool success) =>
            {
            });
        }
        if (PlayerPrefs.GetInt("purple_block") > 1000)
        {
            Social.ReportProgress("CgkIuKTZ6sIaEAIQEQ", 100.0f, (bool success) =>
            {
            });
        }
        if (PlayerPrefs.GetInt("purple_block") > 2000)
        {
            Social.ReportProgress("CgkIuKTZ6sIaEAIQEg", 100.0f, (bool success) =>
            {
            });
        }
        if (PlayerPrefs.GetInt("purple_block") > 5000)
        {
            Social.ReportProgress("CgkIuKTZ6sIaEAIQEw", 100.0f, (bool success) =>
            {
            });
        }


        if (PlayerPrefs.GetInt("yellow_block") > 100)
        {
            Social.ReportProgress("CgkIuKTZ6sIaEAIQDA", 100.0f, (bool success) =>
            {
            });
        }
        if (PlayerPrefs.GetInt("yellow_block") > 1000)
        {
            Social.ReportProgress("CgkIuKTZ6sIaEAIQDQ", 100.0f, (bool success) =>
            {
            });
        }
        if (PlayerPrefs.GetInt("yellow_block") > 2000)
        {
            Social.ReportProgress("CgkIuKTZ6sIaEAIQDg", 100.0f, (bool success) =>
            {
            });
        }
        if (PlayerPrefs.GetInt("yellow_block") > 5000)
        {
            Social.ReportProgress("CgkIuKTZ6sIaEAIQDw", 100.0f, (bool success) =>
            {
            });
        }

        if (PlayerPrefs.GetInt("red_block") > 100)
        {
            Social.ReportProgress("CgkIuKTZ6sIaEAIQCA", 100.0f, (bool success) =>
            {
            });
        }
        if (PlayerPrefs.GetInt("red_block") > 1000)
        {
            Social.ReportProgress("CgkIuKTZ6sIaEAIQCQ", 100.0f, (bool success) =>
            {
            });
        }
        if (PlayerPrefs.GetInt("red_block") > 2000)
        {
            Social.ReportProgress("CgkIuKTZ6sIaEAIQCg", 100.0f, (bool success) =>
            {
            });
        }
        if (PlayerPrefs.GetInt("red_block") > 5000)
        {
            Social.ReportProgress("CgkIuKTZ6sIaEAIQCw", 100.0f, (bool success) =>
            {
            });
        }


        

        

        //CgkIuKTZ6sIaEAIQAg(tilt), CgkIuKTZ6sIaEAIQAQ(slide)
        if (Global.s_nPlayMode == Global.TouchModes.E_TOUCH_MODE)
        {
            int nMyHighScore = PlayerPrefs.GetInt("high_score_slide");
            if (nMyHighScore < Global.s_nScoreSlide) // if it's higher than current high score renew the high score
            {
                PlayerPrefs.SetInt("high_score_slide", Global.s_nScoreSlide);
                Social.ReportScore(Global.s_nScoreSlide, "CgkIuKTZ6sIaEAIQAQ", (bool success) =>
                {
                    bool _success = success;
                });

                if (PlayerPrefs.GetInt("high_score_slide") > 100)
                {
                    Social.ReportProgress("CgkIuKTZ6sIaEAIQAw", 100.0f, (bool success) =>
                    {
                    });
                }
                if (PlayerPrefs.GetInt("high_score_slide") > 200)
                {
                    Social.ReportProgress("CgkIuKTZ6sIaEAIQBA", 100.0f, (bool success) =>
                    {
                    });
                }
                if (PlayerPrefs.GetInt("high_score_slide") > 300)
                {
                    Social.ReportProgress("CgkIuKTZ6sIaEAIQBQ", 100.0f, (bool success) =>
                    {
                    });
                }
                if (PlayerPrefs.GetInt("high_score_slide") > 500)
                {
                    Social.ReportProgress("CgkIuKTZ6sIaEAIQBg", 100.0f, (bool success) =>
                    {
                    });
                }
                if (PlayerPrefs.GetInt("high_score_slide") > 1000)
                {
                    Social.ReportProgress("CgkIuKTZ6sIaEAIQBw", 100.0f, (bool success) =>
                    {
                    });
                }
            }
        }
        else
        {
            int nMyHighScore = PlayerPrefs.GetInt("high_score_tilt");
            if (nMyHighScore < Global.s_nScoreTlit) // if it's higher than current high score renew the high score
            {
                PlayerPrefs.SetInt("high_score_tilt", Global.s_nScoreTlit);
                Social.ReportScore(Global.s_nScoreTlit, "CgkIuKTZ6sIaEAIQAg", (bool success) =>
                {
                    bool _success = success;
                });

                if (PlayerPrefs.GetInt("high_score_tilt") > 100)
                {
                    Social.ReportProgress("CgkIuKTZ6sIaEAIQHQ", 100.0f, (bool success) =>
                    {
                    });
                }
                if (PlayerPrefs.GetInt("high_score_tilt") > 200)
                {
                    Social.ReportProgress("CgkIuKTZ6sIaEAIQHg", 100.0f, (bool success) =>
                    {
                    });
                }
                if (PlayerPrefs.GetInt("high_score_tilt") > 300)
                {
                    Social.ReportProgress("CgkIuKTZ6sIaEAIQHw", 100.0f, (bool success) =>
                    {
                    });
                }
                if (PlayerPrefs.GetInt("high_score_tilt") > 500)
                {
                    Social.ReportProgress("CgkIuKTZ6sIaEAIQIA", 100.0f, (bool success) =>
                    {
                    });
                }
                if (PlayerPrefs.GetInt("high_score_tilt") > 1000)
                {
                    Social.ReportProgress("CgkIuKTZ6sIaEAIQIQ", 100.0f, (bool success) =>
                    {
                    });
                }
            }
        }


        
    }

	void Start ()
	{
        Global.s_nScoreSlide = 0;
        Global.s_nScoreTlit = 0;
        nRowCount = nHeight;
        nSelectedGroup = Random.Range(0, groups.Length);    // select shape
        nSelectedItem = Random.Range(0, nWidth);    // select color
		//vRootPos= GameObject.Find("Manager").transform.position;
        
        // if retry
         GameObject.Find("startCanvas").SetActive(!retry);

        foreach(RowItems BI in groups)
        {
            BI.Width = nWidth;
            BI.SelectTag = nSelectedItem;
            BI.Randomly(false);         
        }
        
		var ScreenHeight = 2 * Camera.main.orthographicSize;
        var ScreenWidth  = ScreenHeight * Camera.main.aspect;
        fDistance  = ScreenWidth / (nWidth + 1);

        
        //int nStartMode = Random.Range(0, 5);
        for(int y=0; y<nHeight; ++y)
        {
            if (y == 0)  // change position dynamically
                ChangeWidth(1);
            if (y == 5)
            {
                ChangeWidth(2);
                //int nNewStage = Random.Range(3, 6);
                //ChangeWidth(nNewStage);

                //if (nNewStage == 3)
                //    nNextStandardPos = 1;
                //else if (nNewStage == 4)
                //    nNextStandardPos = 2;
                //else
                //    nNextStandardPos = 2;
            }


            Make1Row(y);            
        }

        MakeTouchBlock(ScreenWidth);
	}


	
	// Update is called once per frame
	void Update ()
	{

        if(bGameOver)
        {
            GameObject.Find("main_block").transform.FindChild("pac_slice").GetComponent<Animator>().enabled = false;
            GameObject.Find("main_block").transform.FindChild("pac_right").GetComponent<Animator>().enabled = false;
        }

        if(BeginStart)
        {
            GameObject[] gBoards = GameObject.FindGameObjectsWithTag("board");
            foreach (GameObject GO in gBoards)
            {
                if (GO.transform.position.y < -150)
                {
                    Destroy(GO);

                    if (nRowCount == 50)
                        ChangeWidth(1);
                    else if (nRowCount == 55)
                        ChangeWidth(3);
                    else if (nRowCount == 130)
                        ChangeWidth(1);
                    else if (nRowCount == 135)
                        ChangeWidth(4);
                    else if (nRowCount == 260)
                        ChangeWidth(1);
                    else if (nRowCount == 265)
                        ChangeWidth(5);


                    Make1Row(nRowCount++);
                }
                GO.transform.Translate(0, fSpeed * Time.deltaTime * -1, 0);
            }
            fSpeed += fIncreseSpeed;
        }
        else 
        {
            if(bGameOver)    // game over
            {
                GameObject camMain= GameObject.Find("Main Camera");
                Camera camComponent = camMain.GetComponent<Camera>();
                camMain.transform.Rotate(new Vector3(0, 0, 0.5f));

                if (this.GetComponent<AudioSource>().pitch > 0f)
                    this.GetComponent<AudioSource>().pitch -= 0.12f * Time.deltaTime;

                GameObject gMainBlock = GameObject.Find("main_block");
				TransformExtensions.ZoomOrthoCamera(new Vector2(gMainBlock.transform.position.x,gMainBlock.transform.position.y), 2.0f, camComponent, 100, 640);
            }

            GameObject.Find("gameoverPanel").GetComponent<Image>().enabled = bGameOver;
            
            // update scores
            int nMyHighScoreSlide = PlayerPrefs.GetInt("high_score_slide");
            int nMyHighScoreTilt = PlayerPrefs.GetInt("high_score_tilt");

            if (Global.s_nPlayMode == Global.TouchModes.E_TOUCH_MODE)
            {
                GameObject.Find("currentScore_text").GetComponent<Text>().text = "" + Global.s_nScoreSlide;
                GameObject.Find("highScore_text").GetComponent<Text>().text = "" + nMyHighScoreSlide;
            }
            else
            {
                GameObject.Find("currentScore_text").GetComponent<Text>().text = "" + Global.s_nScoreTlit;
                GameObject.Find("highScore_text").GetComponent<Text>().text = "" + nMyHighScoreTilt;
            }

            GameObject.Find("scoreBoard").GetComponent<Image>().enabled = bGameOver;
            GameObject.Find("currentScore_text").GetComponent<Text>().enabled = bGameOver;
            GameObject.Find("highScore_text").GetComponent<Text>().enabled = bGameOver;

            GameObject.Find("homeButton").GetComponent<Button>().enabled = bGameOver;
            GameObject.Find("homeButton").GetComponent<Image>().enabled = bGameOver;

            GameObject.Find("rateButton").GetComponent<Button>().enabled = bGameOver;
            GameObject.Find("rateButton").GetComponent<Image>().enabled = bGameOver;

            GameObject.Find("leaderboardButton").GetComponent<Button>().enabled = bGameOver;
            GameObject.Find("leaderboardButton").GetComponent<Image>().enabled = bGameOver;
            
            GameObject.Find("retryButton").GetComponent<Image>().enabled = bGameOver;
            GameObject.Find("retryButton").GetComponent<Button>().enabled = bGameOver;
			GameObject.Find("retryButton").transform.FindChild("retryText").GetComponent<Text>().enabled = bGameOver;

			GameObject.Find("shareButton").GetComponent<Image>().enabled = bGameOver;
			GameObject.Find("shareButton").GetComponent<Button>().enabled = bGameOver;
			GameObject.Find("shareButton").transform.FindChild("shareText").GetComponent<Text>().enabled = bGameOver;


        }
	}
}
