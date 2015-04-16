using UnityEngine;
using System.Collections;

public class drawline : MonoBehaviour {


	
	void Start ()
	{
		// Set the sorting layer of the particle system.
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "BGround";
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = 3;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
