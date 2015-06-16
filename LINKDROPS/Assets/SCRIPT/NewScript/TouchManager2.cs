using UnityEngine;
using System.Collections;

public class TouchManager2 : MonoBehaviour
{
    public Vector2 vTouchPos;
    private BlockManager2 bManager;
    private Vector2 vStartTouchPos;
    private Vector2 vGapBetweenTouchAndObj;
    protected bool bTouch = false;

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

        bManager = this.GetComponent<BlockManager2>();
        gStartBlock = GameObject.Find("main_block");
        gMainBlockStartPosition = GameObject.Find("MainBlockStartPosition").transform.position;

    } 

    // Update is called once per frame 
    void FixedUpdate () 
    {


        gStartBlock = GameObject.Find("main_block");

        if (Input.GetButtonDown("Fire1")) 
        { 
            _startMouse = Input.mousePosition; 
            _checkMouseIn = true;

//            if (bManager.BeginStart == false && bManager.bGameOver == false) // if game is not started yet
//                bManager.BeginStart = true;
            if (bManager.BeginStart == false && BlockManager2.retry == true) // when rety, if game is not started yet
            {
                bManager.BeginStart = true;
                BlockManager2.retry = false;
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
        //else 
        //{ 
        //    if (_rotBack != 0.0f)
        //    { 
        //        if (_rotBack > 0) 
        //        {
        //            if (_rotBack > fFactor)
        //            {
        //                _rotBack -= fFactor;
        //                gStartBlock.transform.Rotate(new Vector3(0.0f, 0.0f, -fFactor)); 
        //            }
        //            else
        //            { 
        //                gStartBlock.transform.eulerAngles = _angleBack; 
        //                _rotBack = 0.0f; 
        //            } 
        //        } 

        //    if (_rotBack < 0) 
        //    {
        //        if (_rotBack < -fFactor)
        //        {
        //            _rotBack += fFactor;
        //            gStartBlock.transform.Rotate(new Vector3(0.0f, 0.0f, fFactor)); 
        //        } else { 
        //            gStartBlock.transform.eulerAngles = _angleBack; 
        //            _rotBack = 0.0f; 
        //        } 
        //    } 
        //} 
        //}

        DragMove();
    } 

    void DragMove()
    {
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
    }

    void OnGUI () 
    {
        //GUI.color = Color.green;
        //GUI.skin.label.fontSize = 40;
        //GUILayout.Space(5);
        //////GUILayout.Label("current");
        ////GUILayout.Label(_rotBack + " : " + _rot, GUILayout.Width(800));
        //GUILayout.Label("" + (_rot - _rotNow) * -1, GUILayout.Width(800));
        ////GUI.Label(new Rect(10, 10, 320, 20), _rotBack + " : " + _rot); 
    }
}
