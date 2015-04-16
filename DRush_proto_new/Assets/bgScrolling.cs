using UnityEngine;
using System.Collections;

public class bgScrolling : MonoBehaviour {

	public Vector2 speed;
	
	void LateUpdate()
	{
		GetComponent<Renderer>().material.mainTextureOffset = speed * Time.time;
	}

}
