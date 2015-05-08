using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour {

    public Vector2 vTouchPos;
	// Use this for initialization
	void Start ()
    {
        vTouchPos = new Vector2(0, 0);
	}
	
    void InWindows()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        if (Input.GetMouseButton (0))
        {
            if (hit.collider != null)
            {
                GameObject gStartBlock = GameObject.Find("main_block");
                gStartBlock.transform.position = new Vector3(pos.x, pos.y, 100);
                //GetComponent<BlockManager2>().MakeStart = true;
            }
        }
    }

    void InMobile()
    {
        int count = Input.touchCount;
        if (count == 0)
            return; // nothing to do

        Touch touch = Input.GetTouch(0);
        Vector2 pos = touch.position;

        // more detail
        if (touch.phase == TouchPhase.Began)
        {
            Debug.Log("start : (0) : x = " + pos.x + ", y = " + pos.y);
            vTouchPos = pos;
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            Debug.Log("end : (0) : x = " + pos.x + ", y = " + pos.y);
            vTouchPos = pos;
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            Debug.Log("move : (0) : x = " + pos.x + ", y = " + pos.y);
            vTouchPos = pos;
        }
    }

	// Update is called once per frame
	void Update ()
    {
        //Debug.Log(Application.platform);

        if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            InWindows();
        }
        else
        {
            InMobile();
        }
        

	}
}
