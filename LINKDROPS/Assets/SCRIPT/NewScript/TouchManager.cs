using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour {

    public Vector2 vTouchPos;
    protected bool bCollide = false;
    public Vector2 vCharToward;
    public float vCharSpeed;
    public float vCharIncreseSpeed;

    public bool s_TouchMode = true;   // game mode

	// Use this for initialization
	void Start ()
    {
        vTouchPos = new Vector2(0, 0);
        vCharToward = new Vector2(0, 0);
        vCharSpeed = 5.0f;
        vCharIncreseSpeed = 0.00025f;
	}
	
    void InWindows()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
        vTouchPos = pos;    // input to the member variable.


        if (Input.GetMouseButtonUp(0))
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

            if (this.GetComponent<BlockManager2>().BeginStart == false)
                this.GetComponent<BlockManager2>().BeginStart = true;

            if (hit.collider != null)
            {
                bCollide = true;
                //GetComponent<BlockManager2>().MakeStart = true;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            bCollide = false;
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
                    bCollide = true;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Moved)    // 터치하고 움직이믄 발생한다.
                {
                    vTouchPos = pos;
                    bCollide = true;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended)    // 터치 따악 떼면 발생한다.
                {
                    vTouchPos = pos;
                    bCollide = false;
                }
            }
        }

        int count = Input.touchCount;
        if (count == 0)
            return; // nothing to do

    }

	// Update is called once per frame
	void Update ()
    {
        //Debug.Log(Application.platform);

        //if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            InWindows();
        }
        //else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        //{
        //    InMobile();
        //}

        if(s_TouchMode == true)
        {
            if (bCollide)
            {
                GameObject gStartBlock = GameObject.Find("main_block");
                gStartBlock.transform.position = new Vector3(vTouchPos.x, 400, 100);
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
