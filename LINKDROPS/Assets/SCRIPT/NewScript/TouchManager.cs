using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour {

    private Vector2 vStartTouchPos;
    private Vector2 vGapBetweenTouchAndObj;
    public Vector2 vTouchPos;
	public float mainblockPosY = 300f;

    protected bool bTouch = false;
    public Vector2 vCharToward;
    public float vCharSpeed;
    public float vCharIncreseSpeed;

    public bool s_TouchMode = true;   // game mode

    GameObject gLeftWall ;
    GameObject gRightWall;

    void OnGUI()
    {
        GameObject gStartBlock = GameObject.Find("main_block");
                
        GUI.Label(new Rect(10, 75, 300, 20), "Touch :: X: " + vTouchPos.x + "   Y: " + vTouchPos.y);
        GUI.Label(new Rect(10, 100, 300, 20), "gMain :: X: " + gStartBlock.transform.position.x + "   Y: " + gStartBlock.transform.position.y);
        GUI.Label(new Rect(10, 125, 300, 20), "Gap :: X: " + vGapBetweenTouchAndObj.x + "   Y: " + vGapBetweenTouchAndObj.y);
        GUI.Label(new Rect(10, 150, 300, 20), "Screen.width : " + Screen.width);
        GUI.Label(new Rect(10, 175, 300, 20), "Screen.height : " + Screen.height);
        
    }

	// Use this for initialization
	void Start ()
    {
        vTouchPos = new Vector2(0, 0);
        vCharToward = new Vector2(0, 0);
        vCharSpeed = 5.0f;
        vCharIncreseSpeed = 0.00025f;

        gLeftWall = GameObject.Find("left_wall");
        gRightWall = GameObject.Find("right_wall");
	}
	
    void InWindows()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
        


        if (Input.GetMouseButtonUp(0))  // change direction alternately
        {
            if (vCharToward.x == 0)
            {
                vCharToward.x = 1;
            }
            else if (vCharToward.x == 1)
            {
                vCharToward.x = -1;
            }
            else if (vCharToward.x == -1)
            {
                vCharToward.x = 1;
            }

            if (this.GetComponent<BlockManager2>().BeginStart == false) // if game is not started yet
                this.GetComponent<BlockManager2>().BeginStart = true;

            bTouch = false;
            vTouchPos = pos;

            
        }
        else if (Input.GetMouseButtonDown(0))
        {
            vTouchPos = pos;
            bTouch = true;
            GameObject gStartBlock = GameObject.Find("main_block");
            vStartTouchPos = vTouchPos;
            vGapBetweenTouchAndObj.x = (gStartBlock.transform.position.x - vTouchPos.x);
            ////vGapBetweenTouchAndObj.x = gStartBlock.transform.position.x - (vTouchPos.x + fHalfScreen);
        }
        else if(Input.GetMouseButton(0))
        {
            vTouchPos = pos;    // input to the member variable.
        }
    }

    void InMobile()
    {
        if (Input.touchCount > 0)
        {
            Vector2 pos = Input.GetTouch(0).position;    // 터치한 위치
            Vector3 theTouch = new Vector3(pos.x, pos.y, 0.0f);    // 변환 안하고 바로 Vector3로 받아도 되겠지.

            Ray ray = Camera.main.ScreenPointToRay(theTouch);    // 터치한 좌표 레이로 바꾸엉
            RaycastHit hit;    // 정보 저장할 구조체 만들고
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))    // 레이저를 끝까지 쏴블자. 충돌 한넘이 있으면 return true다.
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)    // 딱 처음 터치 할때 발생한다
                {
                    vTouchPos = pos;
                    bTouch = true;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Moved)    // 터치하고 움직이믄 발생한다.
                {
                    vTouchPos = pos;
                    bTouch = true;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended)    // 터치 따악 떼면 발생한다.
                {
                    vTouchPos = pos;
                    bTouch = false;
                }
            }
        }

        int count = Input.touchCount;
        if (count == 0)
            return; // nothing to do

    }

    private void InJoySticks()
    {
        string[] strJoySticks= Input.GetJoystickNames();
        if (strJoySticks.Length != 0)
        {
            float f = Input.GetAxis("Horizontal");
           

            GameObject gStartBlock = GameObject.Find("main_block");
            Vector2 v = new Vector2(f, 0);
            gStartBlock.transform.Translate(v*10);
            
            if(f != 0.0f)
            {
                if (this.GetComponent<BlockManager2>().BeginStart == false) // if game is not started yet
                    this.GetComponent<BlockManager2>().BeginStart = true;
            }
            
            
        }
    }

	// Update is called once per frame
	void Update ()
    {

        InJoySticks();
        
        //Debug.Log(Application.platform);

        //if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            InWindows();
        }
        //else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        //{
        //    InMobile();
        //}

        if(s_TouchMode == true) // each modes are completely different
        {
            if (bTouch)
            {
                GameObject gStartBlock = GameObject.Find("main_block");

                float fMove = vTouchPos.x + vGapBetweenTouchAndObj.x;
                //if (100 < fMove && Screen.width-100 > fMove)
                if (gLeftWall.transform.position.x < fMove && gRightWall.transform.position.x > fMove)
                {
				    gStartBlock.transform.position = new Vector3( vTouchPos.x+vGapBetweenTouchAndObj.x, mainblockPosY, 100);
                }

            }
        }
        else
        {
            GameObject gStartBlock = GameObject.Find("main_block");
            gStartBlock.transform.Translate(vCharToward * vCharSpeed);
        }
        
       

        vCharSpeed += vCharIncreseSpeed;

	}
}

