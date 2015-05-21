using UnityEngine;
using System.Collections;

public class Drawline2 : MonoBehaviour
{
	//private Vector2 objectPosition;
    public GameObject sourceObj;    // source
    //public Vector2 sourcePosition;  // source 
    Vector2 destiPosition;  // destination 

	//public GameObject insteadObj;
	public int nRowIdx;

	void Start ()
	{
		//nRowIdx= 0;
		//bLinkToMain= true;
		//objectPosition = new Vector2 (transform.position.x,transform.position.y);	
	}
	// Update is called once per frame
	void Update ()
    {
		GameObject obj= GameObject.Find("LinkLine_" + (nRowIdx+1));

		if(obj == null)
		{
			GameObject gMainBlock= GameObject.Find("main_block") as GameObject;	// find main block
			destiPosition = gMainBlock.transform.position;	// get destination of main block
			transform.LookAt2D(gMainBlock.transform, Vector3.up);	// rotate this line obj to target block such as main block.
			
			float distance = Vector2.Distance(sourceObj.transform.position, destiPosition);	// get distance from source object to destination object.
			transform.localScale = new Vector2(transform.localScale.x, distance/15);	// change scale
		}
		else
		{
			//obj.transform.parent;
			destiPosition = obj.transform.position;	// get destination of main block
			transform.LookAt2D(obj.transform, Vector3.up);	// rotate this line obj to target block such as main block.
			
			float distance = Vector2.Distance(sourceObj.transform.position, destiPosition);	// get distance from source object to destination object.
			transform.localScale = new Vector2(transform.localScale.x, distance/15);	// change scale
		}

        //transform.localScale = transform.parent.localScale;
        ///transform.localScale = new Vector2(transform.localScale.x, Mathf.Pow(distance, 0.55f));

        ////transform.Rotate(0, 0, 1, Space.Self);
        ////Vector3 v = destiPosition - sourcePosition;
        ////Vector3 lookPoint = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        ////float dx = Mathf.Abs(destiPosition.x - sourcePosition.x);
        ////float dy = Mathf.Abs(destiPosition.y - sourcePosition.y);
        //float angle = Mathf.Atan2(destiPosition.y, destiPosition.x) * Mathf.Rad2Deg;
        ////transform.LookAt2D(lookPoint);
        //transform.rotation = Quaternion.Euler(new Vector3(0,0,-angle));

        //Vector3 vN = Vector3.Normalize(v);
        //Quaternion.Euler(vN);
        //transform.rotation = Quaternion.Euler(vN);
        //transform.= new Vector2(transform.localScale.x, 5);

        //float dx= Mathf.Abs(destiPosition.x - sourcePosition.x);
        //float dy= Mathf.Abs(destiPosition.y - sourcePosition.y);
        //float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        //float distance = Vector2.Distance(sourcePosition, destiPosition);
        //Vector3.Normalize()
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y + distance);

        //if (Input.GetMouseButton(0))
        //{
        //Vector2 sourWorld = Camera.main.ScreenToWorldPoint(sourcePosition);
        //Vector2 destWorld = Camera.main.ScreenToWorldPoint(destiPosition);
        //float dx = Mathf.Abs(destWorld.x - sourWorld.x);
        //float dy = Mathf.Abs(destWorld.y - sourWorld.y);
        //Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        //float angle = Mathf.Atan2(destiPosition.y, destiPosition.x) * Mathf.Rad2Deg;
        ////angle = 0;
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + rotateFix));
        //Debug.Log(angle);

        //float distance = Vector2.Distance(transform.position, destiPosition) * distanceFix;
        //if (transform.localScale.y <= distance)
        //{
        //    transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y + stetchSpeed);
        //}

        //if (distance < transform.localScale.y)
        //{
        //    transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y - stetchSpeed);
        //}
        //}
	}

}
