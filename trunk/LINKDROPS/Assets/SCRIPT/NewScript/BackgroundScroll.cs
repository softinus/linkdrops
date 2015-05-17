using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour {

    public float fVelocity = -1.0f;
    protected Vector3 vBeginPos;
	// Use this for initialization
	void Start ()
    {
        vBeginPos = GameObject.Find("bg_002").transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {

        this.gameObject.transform.Translate(new Vector3(0, fVelocity, 0));

        int nChildCount = this.gameObject.transform.childCount;

        for(int i=0; i<nChildCount; ++i)
        {
            Transform tChild = this.gameObject.transform.GetChild(i);   // get transform of child gameobject
            if (tChild.position.y < -1280)
            {
                tChild.position = vBeginPos;
            }
        }
	}
}
