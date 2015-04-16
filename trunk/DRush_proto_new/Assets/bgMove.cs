using UnityEngine;
using System.Collections;

public class bgMove : MonoBehaviour {

	public GameObject bg;
	public Vector2 moveSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		bg.transform.Translate (moveSpeed);
	


	}
}
