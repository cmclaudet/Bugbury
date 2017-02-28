using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spriter2UnityDX;

//detects when player taps on caterpillars in title screen and destroys them
//places splatter in their place and triggers splat sound effect
public class splatOnTouch : MonoBehaviour {
	public AudioSource[] splats{get;set;}
	public Transform splatter;
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			//find touch position
			Vector3 touchPosScreen = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2(touchPosScreen.x, touchPosScreen.y);

			//if touch position is overlapping object box collider, object is set to inactive
			if (GetComponent<BoxCollider2D>() == Physics2D.OverlapPoint(touchPos))
			{
				//set splatter to background sorting layer so that it is below the caterpillars
				Transform newSplatter = Instantiate (splatter);
				newSplatter.GetComponent<EntityRenderer> ().SortingLayerName = "Background";
				newSplatter.GetComponent<EntityRenderer> ().SortingOrder = 1;	//set to 1 so it can be seen above the background
				newSplatter.transform.position = transform.position;

				//randomize sound effect and pitch
				int i = Random.Range (0, splats.Length);
				splats [i].pitch = Random.Range (0.8f, 1.2f);
				splats [i].Play ();
				gameObject.SetActive (false);
			}
		}
	}
}
