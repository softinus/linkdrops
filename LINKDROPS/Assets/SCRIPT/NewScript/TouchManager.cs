using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour
{
    private BlockManager2 bManager;
    private Vector2 vStartTouchPos;
    private Vector2 vGapBetweenTouchAndObj;
    public Vector2 vTouchPos;
    public float mainblockPosY = 300f;
    
    GameObject gStartBlock;
    GameObject gMainBlockStartPosition;
    public float fSpeed = 3.8F;

    private Vector2 vTouchDeltaPos;


    protected bool bTouch = false;
    public Vector2 vCharToward;
    public float vCharSpeed;
    public float vCharIncreseSpeed;

    public bool s_TouchMode = true;   // game mode

    GameObject gLeftWall;
    GameObject gRightWall;

    GUIStyle smallFont;
    GUIStyle largeFont;

    void OnGUI()
    {
        //        GameObject gStartBlock = GameObject.Find("main_block");

        ////        GUI.Label(new Rect(10, 75, 300, 20), "Touch :: X: " + vTouchPos.x + "   Y: " + vTouchPos.y);
        ////        GUI.Label(new Rect(10, 100, 300, 20), "gMain :: X: " + gStartBlock.transform.position.x + "   Y: " + gStartBlock.transform.position.y);
        ////        GUI.Label(new Rect(10, 125, 300, 20), "Gap :: X: " + vGapBetweenTouchAndObj.x + "   Y: " + vGapBetweenTouchAndObj.y);
        ////        GUI.Label(new Rect(10, 150, 300, 20), "Screen.width : " + Screen.width);
        ////        GUI.Label(new Rect(10, 175, 300, 20), "Screen.height : " + Screen.height);
        //GUI.Label(UnityEngine.Touch)     
        //GUIStyle myStyle = new GUIStyle();
        //myStyle.fontSize = 100;
        //GUI.Label(new Rect(150, 275, 300, 20), "delta : " + Input.GetTouch(0).deltaPosition, myStyle);

        
        //largeFont = new GUIStyle();
        //largeFont.fontSize = 80;
        
        

        GUI.color = Color.green;
        GUI.skin.label.fontSize = 85;
        ////GUI.Label(new Rect(150, 275, 300, 20), "a");
        ////GUI.Label(new Rect(150, 275, 300, 20), "delta : " + Input.GetTouch(0).deltaPosition.x);
        GUILayout.Space(5);
        ////GUILayout.Label("current");
        GUILayout.Label("delta : " + Input.GetTouch(0).deltaPosition.x, GUILayout.Width(800));
        //GUILayout.Label("acc(x) : " + Input.acceleration.x, GUILayout.Width(800));
        //GUILayout.Label("acc(y) : " + Input.acceleration.y, GUILayout.Width(800));
        //GUILayout.Label("acc(z) : " + Input.acceleration.z, GUILayout.Width(800));
        

        //GUI.Label(new Rect(150, 275, 300, 20), "<color=green><size=40>Lose</size></color>");
    }

    // Use this for initialization
    void Start()
    {
        vTouchPos = new Vector2(0, 0);
        vCharToward = new Vector2(0, 0);
        vCharSpeed = 5.0f;
        vCharIncreseSpeed = 0.00025f;

        gLeftWall = GameObject.Find("left_wall");
        gRightWall = GameObject.Find("right_wall");

        bManager = this.GetComponent<BlockManager2>();
        gStartBlock = GameObject.Find("main_block");
        gMainBlockStartPosition = GameObject.Find("MainBlockStartPosition");
    }

    void InWindows()
    {
        //Camera camera2 = GameObject.Find("Camera").GetComponent<Camera>();
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

            bTouch = false;
            vTouchPos = pos;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            vTouchPos = pos;
            bTouch = true;
            vStartTouchPos = vTouchPos;
            vGapBetweenTouchAndObj.x = (gStartBlock.transform.position.x - vTouchPos.x);
			Debug.Log (hit);
            ////vGapBetweenTouchAndObj.x = gStartBlock.transform.position.x - (vTouchPos.x + fHalfScreen);

            if (bManager.BeginStart == false && bManager.bGameOver == false) // if game is not started yet
                bManager.BeginStart = true;
        }
        else if (Input.GetMouseButton(0))
        {
            vTouchPos = pos;    // input to the member variable.
        }
    }

    void InMobile()
    {
        if (Input.touchCount > 0)
        {
            vTouchDeltaPos = Input.GetTouch(0).deltaPosition;

            if (Input.GetTouch(0).phase == TouchPhase.Began)    // 딱 처음 터치 할때 발생한다
            {
                bTouch = true;
                if (bManager.BeginStart == false && bManager.bGameOver == false) // if game is not started yet
                    bManager.BeginStart = true;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)    // 터치하고 움직이믄 발생한다.
            {
                bTouch = true;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)    // 터치 따악 떼면 발생한다.
            {
                bTouch = false;
            }
        }

        int count = Input.touchCount;
        if (count == 0)
            return; // nothing to do

    }

    private void InJoySticks()
    {
        string[] strJoySticks = Input.GetJoystickNames();
        if (strJoySticks.Length != 0)
        {
            float f = Input.GetAxis("Horizontal");

            Vector2 v = new Vector2(f, 0);
            gStartBlock.transform.Translate(v * 10);

            if (f != 0.0f)
            {
                if (this.GetComponent<BlockManager2>().BeginStart == false) // if game is not started yet
                    this.GetComponent<BlockManager2>().BeginStart = true;
            }


        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
	
        InJoySticks();

        //Debug.Log(Application.platform);

        if (Application.isEditor)
        {
            InWindows();

            if (s_TouchMode == true) // each modes are completely different
            {
                if (bTouch && !this.GetComponent<BlockManager2>().bGameOver)
                {
                    gStartBlock.transform.Translate(vTouchDeltaPos);
                    float fMove = vTouchPos.x + vGapBetweenTouchAndObj.x;
                    //if (100 < fMove && Screen.width-100 > fMove)
                    if (gLeftWall.transform.position.x < fMove && gRightWall.transform.position.x > fMove)
                    {
                        gStartBlock.transform.position = new Vector3(vTouchPos.x + vGapBetweenTouchAndObj.x, mainblockPosY, 100);
                    }

                }
            }
            else
            {
                gStartBlock.transform.Translate(vCharToward * vCharSpeed);
            }
        }
        else
        {
            InMobile();

            if (s_TouchMode == true) // each modes are completely different
            {
                if (!this.GetComponent<BlockManager2>().bGameOver)
                {
                    gStartBlock.transform.Translate(new Vector3(Input.acceleration.x * 25.5f, 0, 0));
                    gStartBlock.transform.rotation = Quaternion.Euler(0, 0, Input.acceleration.x * -135.5f);
                }

                //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                //{
                //    // Get movement of the finger since last frame
                //    Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                //    // Move object across XY plane
                //    gStartBlock.transform.Translate(touchDeltaPosition.x * fSpeed, 0, 0);
                //    gStartBlock.transform.rotation = Quaternion.Euler(0, 0, vTouchDeltaPos.x * 0.3f);
                //}

                //if (bTouch && !this.GetComponent<BlockManager2>().bGameOver)
                //{
                //    gStartBlock.transform.Translate(new Vector3(vTouchDeltaPos.x * 3.8f, 0, 0));
                //}
                //gStartBlock.transform.rotation = Quaternion.Euler(0, 0, vTouchDeltaPos.x * -20.5f);



                //if (vTouchDeltaPos == null || vTouchDeltaPos.x == 0)
                //{
                //    gStartBlock.transform.rotation = Quaternion.Euler(0, 0, 0);
                //}
                
                //if (vTouchDeltaPos.x > 0)
                //{
                //    gStartBlock.transform.rotation = Quaternion.Euler(0, 0, -45);
                //}
                //else
                //{
                //    gStartBlock.transform.rotation = Quaternion.Euler(0, 0, 45);
                //}
                
                // fixed y-position
                gStartBlock.transform.position = new Vector3(gStartBlock.transform.position.x, gMainBlockStartPosition.transform.position.y, gStartBlock.transform.position.z);
            }
            else
            {
                gStartBlock.transform.Translate(vCharToward * vCharSpeed);
            }
        }





        vCharSpeed += vCharIncreseSpeed;

    }
}

