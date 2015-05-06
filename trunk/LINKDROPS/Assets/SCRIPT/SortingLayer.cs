using UnityEngine;
using System.Collections;

public class SortingLayer : MonoBehaviour {

	public TrailRenderer trail;
	// Use this for initialization
	void Start () {

		trail.sortingLayerName = "tree";
		trail.sortingOrder = 2;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
