using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spriter2UnityDX;

public class splatOnTouch : MonoBehaviour {
	public AudioSource[] splats{get;set;}
	public Transform splatter;
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			Vector3 touchPosScreen = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2(touchPosScreen.x, touchPosScreen.y);
			if (GetComponent<BoxCollider2D>() == Physics2D.OverlapPoint(touchPos))
			{
				Transform newSplatter = Instantiate (splatter);
				newSplatter.GetComponent<EntityRenderer> ().SortingLayerName = "Background";
				newSplatter.GetComponent<EntityRenderer> ().SortingOrder = 1;
				newSplatter.transform.position = transform.position;

				int i = Random.Range (0, 2);
				splats [i].pitch = Random.Range (0.8f, 1.2f);
				splats [i].Play ();
				gameObject.SetActive (false);
			}
		}
	}
}
