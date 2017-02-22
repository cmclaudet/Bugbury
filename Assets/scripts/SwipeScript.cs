﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwipeScript : MonoBehaviour {

	public float minSwipeDist  = 50.0f;
	public float maxSwipeTime = 0.5f;

	public Button forwardButton;
	public Button backButton;

	private float fingerStartTime  = 0.0f;
	private Vector2 fingerStartPos = Vector2.zero;
	private bool isSwipe = false;

	// Update is called once per frame
	void Update () {

		if (Input.touchCount > 0){

			foreach (Touch touch in Input.touches)
			{
				switch (touch.phase)
				{
				case TouchPhase.Began :
					/* this is a new touch */
					isSwipe = true;
					fingerStartTime = Time.time;
					fingerStartPos = touch.position;
					break;

				case TouchPhase.Canceled :
					/* The touch is being canceled */
					isSwipe = false;
					break;

				case TouchPhase.Ended :

					float gestureTime = Time.time - fingerStartTime;
					float gestureDist = (touch.position - fingerStartPos).magnitude;

					if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist){
						Vector2 direction = touch.position - fingerStartPos;
						Vector2 swipeType = Vector2.zero;

						if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)){
							// the swipe is horizontal:
							swipeType = Vector2.right * Mathf.Sign(direction.x);
						}else{
							// the swipe is vertical - nothing happens
//							swipeType = Vector2.up * Mathf.Sign(direction.y);
							swipeType = Vector2.zero;
						}

						//swipe triggers button press functions if buttons are interactable
						if(swipeType.x != 0.0f){
							if(swipeType.x > 0.0f){
								panMenu2 panMenu2 = GetComponent<panMenu2> ();
								if (panMenu2 != null && backButton.interactable) {
									panMenu2.Press ();
								}
							}else{
								panMenu1 panMenu1 = GetComponent<panMenu1> ();
								if (panMenu1 != null && forwardButton.interactable) {
									panMenu1.Press ();
								}
							}
						}
						/*
						if(swipeType.y != 0.0f ){
							if(swipeType.y > 0.0f){
								// MOVE UP
							}else{
								// MOVE DOWN
							}
						}*/

					}

					break;
				}
			}
		}

	}
}