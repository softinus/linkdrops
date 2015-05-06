using UnityEngine;
using System.Collections;

public class drawline : MonoBehaviour {

	private Vector2 objectPosition;
	public float rotateFix;
	public float distanceFix;
	public float stetchSpeed;

	void Start ()
	{
		objectPosition = new Vector2 (transform.position.x,transform.position.y);
	
	}
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton (0)) {
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			float angle = Mathf.Atan2 (mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

			transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle + rotateFix));

		
			float distance = Vector2.Distance (transform.position, mousePosition) * distanceFix;

			if (transform.localScale.y <= distance) {

				transform.localScale = new Vector2 (transform.localScale.x, transform.localScale.y + stetchSpeed);

			}

			if ( distance < transform.localScale.y) {
				
				transform.localScale = new Vector2 (transform.localScale.x, transform.localScale.y - stetchSpeed);
				

			}

		}


	
	}



}
