﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TouchManager2 : MonoBehaviour
{
    public Vector2 vTouchPos;
    private BlockManager2 bManager;
    private Vector2 vStartTouchPos;
    private Vector2 vGapBetweenTouchAndObj;
    protected bool bTouch = false;

    private Vector2 vTouchDeltaPos;

    Vector2 _startMouse = Vector2.zero;
    float _rot = 0.0f;
    float _rotNow = 0.0f;
    float _rotBack = 0.0f;
    int _dir = 0;

    bool _checkMouseIn = false;

    Vector3 _angleBack = Vector3.zero;

    GameObject gStartBlock;
    Vector3 gMainBlockStartPosition;
    public float fSpeed = 3.8F;
    public float fFactor = 0.2F;

    GameObject gLeftWall;
    GameObject gRightWall;
    GameObject gTutorialFinger, gTutotiralAccelometer;

    public float vCharSpeed;
    public float vCharIncreseSpeed;

    // Use this for initialization 
    void Start () 
    {
        vTouchPos = new Vector2(0, 0);
        vCharSpeed = 5.0f;
        vCharIncreseSpeed = 0.00025f;

        //gStartBlock = GameObject.Find("main_block");
        //_angleBack = gStartBlock.transform.eulerAngles;
        _angleBack = new Vector3(0, 0, 0);

        gLeftWall = GameObject.Find("left_wall");
        gRightWall = GameObject.Find("right_wall");
        gTutorialFinger = GameObject.Find("tuto_finger");
        gTutotiralAccelometer = GameObject.Find("tuto_phone");

        bManager = this.GetComponent<BlockManager2>();
        gStartBlock = GameObject.Find("main_block");
        gMainBlockStartPosition = GameObject.Find("MainBlockStartPosition").transform.position;

        if(BlockManager2.retry)
        {
            gTutorialFinger.SetActive(false);
            gTutotiralAccelometer.SetActive(false);
        }

    } 

    // Update is called once per frame 
    void FixedUpdate () 
    {
        Text txt = GameObject.Find("currentScore").GetComponent<Text>();
        if (Global.SW_forStart.ElapsedMilliseconds < 1000)
        {
            txt.color = Color.white;
            txt.text = "3";
        }
        else if (Global.SW_forStart.ElapsedMilliseconds < 2000)
        {
            txt.text = "2";
        }
        else if (Global.SW_forStart.ElapsedMilliseconds < 3000)
        {
            txt.text = "1";
        }
        else if (Global.SW_forStart.ElapsedMilliseconds < 4000)
        {
            txt.fontSize = 100;
            txt.text = "YAMU!!";
        }
        else if (Global.SW_forStart.ElapsedMilliseconds < 5000)
        {
            bManager.BeginStart = true;
            txt.fontSize = 185;
            txt.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);

            if (gTutorialFinger != null)
                gTutorialFinger.SetActive(false);
        }


        gStartBlock = GameObject.Find("main_block");

        if (Input.GetButtonDown("Fire1")) 
        { 
            _startMouse = Input.mousePosition; 
            _checkMouseIn = true;

//            if (bManager.BeginStart == false && bManager.bGameOver == false) // if game is not started yet
//                bManager.BeginStart = true;
            if (bManager.BeginStart == false && BlockManager2.retry == true) // when rety, if game is not started yet
            {
                //gTutorialFinger.SetActive(false);
                //bManager.BeginStart = true;
                BlockManager2.retry = false;

                //if (Global.s_nPlayMode == Global.TouchModes.E_TOUCH_MODE)
                //{
                //    bManager.GetComponent<TouchManager>().enabled = false;
                //    bManager.GetComponent<TouchManager2>().enabled = true;
                //}
                //else
                //{
                //    bManager.GetComponent<TouchManager>().enabled = true;
                //    bManager.GetComponent<TouchManager2>().enabled = false;
                //}
            }
            else if (bManager.BeginStart == false && buttonManager.bPressStart && !bManager.bGameOver) // when rety, if game is not started yet
            {
                //bManager.BeginStart = true;
                //gTutorialFinger.SetActive(false);
            }
        }

        if (Input.GetButtonUp("Fire1")) 
        {

            _startMouse = Vector2.zero; 
            _rotBack = _rot + _rotBack; 
            _rot = 0; 
            _rotNow = 0; 
            _checkMouseIn = false; 
        } 

        if (_checkMouseIn) 
        { 
            float _tmpDis = 0.0f; 

            if (Input.mousePosition.x > _startMouse.x) 
            { 
                _dir = 1; 
                _tmpDis = Mathf.Sqrt((Input.mousePosition.x * Input.mousePosition.x) - (_startMouse.x * _startMouse.x)); 
            } else if (Input.mousePosition.x < _startMouse.x) { 
                _dir = 2; 
                _tmpDis = Mathf.Sqrt((_startMouse.x * _startMouse.x) - (Input.mousePosition.x * Input.mousePosition.x)); 
            } else { 
                _dir = 0; 
        } 

        float _tmpRot = (360.0f * _tmpDis) / Screen.width; 

        switch (_dir)
        { 
            case 1: 
                _rotNow = _rot; 
                _rot = _tmpRot; 
            break; 
            case 2: 
                _rotNow = _rot; 
                _rot = _tmpRot * -1; 
            break; 
            default: 
            break; 
        }


        //if (gStartBlock.transform.eulerAngles.z < 45.0f)
        //if (gStartBlock.transform.eulerAngles.z > -45.0f)
        float fRotate = Mathf.Clamp((_rot - _rotNow) * -1, -5f, 5f);

        if (fRotate > 0)    // right rotate
        {
            bool bCondition = false;
            if (gStartBlock.transform.eulerAngles.z < 55.0f)
                if (gStartBlock.transform.eulerAngles.z > 45.0f)
                    bCondition = true;

            if (!bCondition)
                gStartBlock.transform.Rotate(new Vector3(0.0f, 0.0f, fRotate)); 
        }
        else
        {
            bool bCondition = false;
            if (gStartBlock.transform.eulerAngles.z < 315.0f)
                if (gStartBlock.transform.eulerAngles.z > 305.0f)
                    bCondition = true;

            if (!bCondition)
                gStartBlock.transform.Rotate(new Vector3(0.0f, 0.0f, fRotate)); 
        }

        //gStartBlock.transform.Rotate(new Vector3(0.0f, 0.0f, (_rot - _rotNow) * -1));

        }

        if (buttonManager.bPressStart)
            DragMove();
    } 

    void DragMove()
    {
        if (Input.touchCount != 1)
            return;

        if (Input.touchCount > 0)
            vTouchDeltaPos = Input.GetTouch(0).deltaPosition;
        //Camera camera2 = GameObject.Find("Camera").GetComponent<Camera>();
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        if (Input.GetMouseButtonUp(0))  // change direction alternately
        {
            bTouch = false;
            vTouchPos = pos;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            vTouchPos = pos;
            bTouch = true;
            vStartTouchPos = vTouchPos;
            vGapBetweenTouchAndObj.x = (gStartBlock.transform.position.x - vTouchPos.x);
            Debug.Log(hit);
            ////vGapBetweenTouchAndObj.x = gStartBlock.transform.position.x - (vTouchPos.x + fHalfScreen);


        }
        else if (Input.GetMouseButton(0))
        {
            vTouchPos = pos;    // input to the member variable.
        }

        // move
        if (bTouch && !this.GetComponent<BlockManager2>().bGameOver)
        {
            //gStartBlock.transform.Translate(vTouchDeltaPos);
            float fMove = vTouchPos.x + vGapBetweenTouchAndObj.x;
            if (gLeftWall.transform.position.x < fMove && gRightWall.transform.position.x > fMove)
            {
                gStartBlock.transform.position = new Vector3(vTouchPos.x + vGapBetweenTouchAndObj.x, gMainBlockStartPosition.y, 100);
            }

        }

        if(Mathf.Abs(vTouchDeltaPos.x) < 0.1f)
        {
            if (gStartBlock.transform.eulerAngles.z < 359.0f)
            if (gStartBlock.transform.eulerAngles.z > 310.0f)
                gStartBlock.transform.Rotate(Vector3.back, -2.45f, Space.World);

            if (gStartBlock.transform.eulerAngles.z < 50.0f)
            if (gStartBlock.transform.eulerAngles.z > 1.0f)
                gStartBlock.transform.Rotate(Vector3.back, 2.45f, Space.World);
        }

    }

    void OnGUI () 
    {
        //GUI.color = Color.green;
        //GUI.skin.label.fontSize = 85;
        //////GUI.Label(new Rect(150, 275, 300, 20), "a");
        //////GUI.Label(new Rect(150, 275, 300, 20), "delta : " + Input.GetTouch(0).deltaPosition.x);
        //GUILayout.Space(5);
        //////GUILayout.Label("current");
        ////GUILayout.Label("delta : " + Input.GetTouch(0).deltaPosition.x, GUILayout.Width(800));
        //GUILayout.Label("delta : " + vTouchDeltaPos.x, GUILayout.Width(800));
        ////GUILayout.Label("acc(x) : " + Input.acceleration.x, GUILayout.Width(800));
        ////GUILayout.Label("acc(y) : " + Input.acceleration.y, GUILayout.Width(800));
        ////GUILayout.Label("acc(z) : " + Input.acceleration.z, GUILayout.Width(800));


        //GUI.color = Color.green;
        //GUI.skin.label.fontSize = 40;
        //GUILayout.Space(5);
        //////GUILayout.Label("current");
        ////GUILayout.Label(_rotBack + " : " + _rot, GUILayout.Width(800));
        //GUILayout.Label("Delta : " + vTouchDeltaPos.x, GUILayout.Width(800));
        //GUILayout.Label("Euler : " + gStartBlock.transform.eulerAngles.z, GUILayout.Width(800));
        ////GUI.Label(new Rect(10, 10, 320, 20), _rotBack + " : " + _rot); 
    }

}
