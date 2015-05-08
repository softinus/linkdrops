using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        int count = Input.touchCount;
        if (count == 0)
            return; // nothing to do

        Touch touch = Input.GetTouch(0);
        Vector2 pos = touch.position;

        // more detail
        if (touch.phase == TouchPhase.Began)
        {
            Debug.Log("시작점 : (0) : x = " + pos.x + ", y = " + pos.y);
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            Debug.Log("끝점 : (0) : x = " + pos.x + ", y = " + pos.y);
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            Debug.Log("이동중 : (0) : x = " + pos.x + ", y = " + pos.y);
        }


	}
}
