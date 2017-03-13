using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getPointerVertices : MonoBehaviour {
	private float radius;
	private GameObject springAnchor;

	private bool horizontalReflection;
	private Vector3 rayDirection;
	private List<Vector3> pointerVertices;
	private RaycastHit2D[] collidersFound;
	private RaycastHit2D[] topCollidersFound;
	private RaycastHit2D[] botCollidersFound;
	private Vector2 rayOffsets;

	private RaycastHit2D nextCollider;

	private Vector3 nextColliderPoint;
	private Vector2 rockOffsets;
	// Use this for initialization
	void Start () {
		radius = GetComponent<projectileShoot> ().radius;
		springAnchor = rockManager.Instance.springAnchor;
		pointerVertices = new List<Vector3> ();
		horizontalReflection = true;	//initialise horizontal reflection. Is true if ray reflects in x direction. Assume true to start with.
	}

	/* Draws pointer from rock up to top of the screen showing player where their shot will go
	 * Creates raycast to find next collidable object. Adds this object point to the pointer vertices.
	 * If rock trajectory will go downwards due to the shot no pointer is cast past this point. Communicates to player their shot will not work.
	 * offset between contact point of ray and collider and the rock position on contact is calculated and used when casting the next ray
	 * If offset means rock will actually not land within the collider when the ray says it will, horizontal reflection is set to falseso no pointer is cast
	 * Once there are no longer any objects found ray is drawn between last object and the top of the screen where the rock will go through.
	*/
	public void updatePointer() {
		//find direction between rock and spring anchor (pointer direction)
		rayDirection = springAnchor.transform.position - transform.position;
		rayOffsets = getRayOffsets ();

		//find colliders that ray intersects with and add initial vertex
		collidersFound = Physics2D.RaycastAll (transform.position, rayDirection);
		pointerVertices.Add(collidersFound [0].point);	//add point at rock itself
		horizontalReflection = true;

		raycastEdges (transform.position);

		if (collidersFound.Length == 1) {
			if (botCollidersFound.Length == 1 && topCollidersFound.Length == 1) {
				if (topCollidersFound [0].point.y < botCollidersFound [0].point.y) {
					endPointerFromEdgeRays (topCollidersFound [0], transform.position);
				} else {
					endPointerFromEdgeRays (botCollidersFound [0], transform.position);
				}
			} else if (botCollidersFound.Length == 1) {
				endPointerFromEdgeRays (botCollidersFound [0], transform.position);
			} else if (topCollidersFound.Length == 1) {
				endPointerFromEdgeRays (topCollidersFound [0], transform.position);
			}
		}
/*
		if ((topCollidersFound.Length == 1 && collidersFound.Length == 1) || (botCollidersFound.Length == 1 && collidersFound.Length == 1)) {
			if (topCollidersFound [0].point.y < botCollidersFound [0].point.y) {
				endPointerFromEdgeRays (topCollidersFound [0], transform.position);
			} else {
				endPointerFromEdgeRays (botCollidersFound [0], transform.position);
			}
		}
		*/
//		if (botCollidersFound.Length == 1 && collidersFound.Length == 1) {
			//causes line to end prematurely sometimes. Should implement offset checker for this.
//			endPointerFromEdgeRays (botCollidersFound [0], transform.position);
//		}
		//if ray finds another collider besides initial rock a second ray must be cast
		if (collidersFound.Length > 1) {
//			Debug.Log (collidersFound.Length);
//			Debug.Log (topCollidersFound.Length);
//			Debug.Log (botCollidersFound.Length);
			if (topCollidersFound.Length == collidersFound.Length) {
				if (topCollidersFound [0].collider != collidersFound[1].collider) {
					endPointerFromEdgeRays (topCollidersFound [0], transform.position);
				}
			}
			if (botCollidersFound.Length == collidersFound.Length) {
				if (botCollidersFound [0].collider != collidersFound[1].collider) {
					endPointerFromEdgeRays (botCollidersFound [0], transform.position);
				}
			}
//new stuff
//			Debug.Log(collidersFound[1].point);

			if (topCollidersFound.Length > 0 && botCollidersFound.Length > 0) {
				if (collidersFound [1].collider != topCollidersFound [0].collider || collidersFound [1].collider != botCollidersFound [0].collider) {
					if (botCollidersFound [0].point.y < topCollidersFound [0].point.y) {
						if (botCollidersFound [0].point.y < collidersFound [1].point.y) {
							horizontalReflection = false;
							endPointerFromEdgeRays (botCollidersFound [0], transform.position);
						}
					} else {
						if (topCollidersFound [0].point.y < collidersFound [1].point.y) {
							horizontalReflection = false;
							endPointerFromEdgeRays (topCollidersFound [0], transform.position);
						}
					}
				}
			} else if (topCollidersFound.Length > 0) {
				if (collidersFound [1].collider != topCollidersFound [0].collider && topCollidersFound [0].point.y < collidersFound [1].point.y) {
					horizontalReflection = false;
					endPointerFromEdgeRays (topCollidersFound [0], transform.position);
				}
			} else if (botCollidersFound.Length > 0) {
				if (collidersFound [1].collider != botCollidersFound [0].collider && botCollidersFound [0].point.y < collidersFound [1].point.y) {
					horizontalReflection = false;
					endPointerFromEdgeRays (botCollidersFound [0], transform.position);
				}
			}
/*
			if (collidersFound [1].collider != topCollidersFound [0].collider || collidersFound [1].collider != botCollidersFound [0].collider) {
				if (botCollidersFound [0].point.y < topCollidersFound [0].point.y) {
					if (botCollidersFound [0].point.y < collidersFound [1].point.y) {
						horizontalReflection = false;
						endPointerFromEdgeRays (botCollidersFound [0], transform.position);
					}
				} else {
					if (topCollidersFound [0].point.y < collidersFound [1].point.y) {
						horizontalReflection = false;
						endPointerFromEdgeRays (topCollidersFound [0], transform.position);
					}
				}
			}*/
//
			if (horizontalReflection) {
				rockOffsets = pointerOffsets (rayDirection, reflectsInX(collidersFound[1]));	//find offsets of pointer vertex due to rock thickness
				//add point with offsets taken into account
				nextColliderPoint = new Vector3 (collidersFound [1].point.x + rockOffsets.x, collidersFound [1].point.y + rockOffsets.y);
				pointerVertices.Add (nextColliderPoint);
			}
			//if y offset takes rock below the minimum y co-ordinate on the collider the rock will not have a horizontal reflection
			if (collidersFound [1].collider.bounds.min.y > nextColliderPoint.y) {
				horizontalReflection = false;
			}
		//	if (!reflectsInX (collidersFound [1])) {
		//		horizontalReflection = false;
		//	}
			//find new colliders from new ray direction

			setNextCollider ();
			//if next collider is default raycasthit2D then it does not exist
			while (nextCollider != default(RaycastHit2D) && horizontalReflection) {
				//add next collider to pointer vertices
				pointerVertices.Add (nextColliderPoint);
				setNextCollider ();
			}
		}
		//only need to add final point at top of the screen if ray reflects horizontally
		if (horizontalReflection) {
			float endXPos = pointerXpos (pointerVertices [pointerVertices.Count - 1], rayDirection, ScreenVariables.worldHeight);
			pointerVertices.Add (new Vector3 (endXPos, ScreenVariables.worldHeight));
		}
		drawPointerLine (pointerVertices);
		pointerVertices.Clear ();
	}

	void raycastEdges(Vector3 midRayPoint) {
		if (rayDirection.x > 0) {
			topCollidersFound = Physics2D.RaycastAll (new Vector3 (midRayPoint.x - rayOffsets.x, midRayPoint.y + rayOffsets.y), rayDirection);
			botCollidersFound = Physics2D.RaycastAll (new Vector3 (midRayPoint.x + rayOffsets.x, midRayPoint.y - rayOffsets.y), rayDirection);
		} else {
			topCollidersFound = Physics2D.RaycastAll (new Vector3 (midRayPoint.x + rayOffsets.x, midRayPoint.y + rayOffsets.y), rayDirection);
			botCollidersFound = Physics2D.RaycastAll (new Vector3 (midRayPoint.x - rayOffsets.x, midRayPoint.y - rayOffsets.y), rayDirection);
		}
		List<RaycastHit2D> topColliderList = new List<RaycastHit2D> ();
		List<RaycastHit2D> botColliderList = new List<RaycastHit2D> ();

		foreach (RaycastHit2D collider in topCollidersFound) {
			if (!collider.collider.gameObject.CompareTag ("rock")) {
				topColliderList.Add (collider);
			}
		}
		foreach (RaycastHit2D collider in botCollidersFound) {
			if (!collider.collider.gameObject.CompareTag ("rock")) {
				botColliderList.Add (collider);
			}
		}
		topCollidersFound = topColliderList.ToArray ();
		botCollidersFound = botColliderList.ToArray ();
	}



	Vector2 getRayOffsets() {
		float sinAngle = Mathf.Abs(rayDirection.y) / Mathf.Pow (Mathf.Pow(rayDirection.y, 2.0f) + Mathf.Pow(rayDirection.x, 2.0f), 0.5f);
		float cosAngle = Mathf.Abs(rayDirection.x) / Mathf.Pow (Mathf.Pow(rayDirection.y, 2.0f) + Mathf.Pow(rayDirection.x, 2.0f), 0.5f);

		float deltaX = radius * sinAngle;
		float deltaY = radius * cosAngle;
		return new Vector2 (deltaX, deltaY);
	}

	void endPointerFromEdgeRays(RaycastHit2D collider, Vector3 midRayPoint) {
		horizontalReflection = false;
		rockOffsets = pointerOffsets (rayDirection, horizontalReflection);	//find offsets of pointer vertex due to rock thickness
		float colliderXpos = pointerXpos(midRayPoint, rayDirection, collider.point.y);
		//add point with offsets taken into account
		nextColliderPoint = new Vector3 (colliderXpos + rockOffsets.x, collider.point.y + rockOffsets.y);
		pointerVertices.Add (nextColliderPoint);

	}

	//first point after initial rock
	void addFirstColliderPoint() {
		horizontalReflection = reflectsInX (collidersFound [1]);	//find out if this is a horizontal reflection
		rockOffsets = pointerOffsets (rayDirection, horizontalReflection);	//find offsets of pointer vertex due to rock thickness

		//add point with offsets taken into account
		nextColliderPoint = new Vector3 (collidersFound [1].point.x + rockOffsets.x, collidersFound [1].point.y + rockOffsets.y);
		pointerVertices.Add (nextColliderPoint);
	}

	//find whether ray is reflected by a collider in x or y direction
	bool reflectsInX(RaycastHit2D collider) {
		if (collider.normal.y == 0) {
			return true;
		} else {
			return false;
		}
	}

	//find x and y offsets of vertex at collision due to thickness of rock. Must be shifted so that the next ray cast goes to correct place.
	Vector2 pointerOffsets(Vector3 rayDirection, bool reflectsInX) {
		float deltax = radius;
		float deltay = -radius;
		float tanAngle = Mathf.Abs (rayDirection.x) / rayDirection.y;

		switch (reflectsInX) {
		case true:
			if (rayDirection.x > 0) {
				deltax = -radius;
			}
			if (rayDirection.y > 0) {
				deltay = -radius / tanAngle;
			} else {
				deltay = radius / tanAngle;
			}
			break;
		case false:
			tanAngle = rayDirection.y / Mathf.Abs (rayDirection.x);
			deltax = radius / tanAngle;
			if (rayDirection.x > 0) {
				deltax = -radius / tanAngle;
			}
			if (rayDirection.y < 0) {
				deltay = radius;
			}
			break;
		}
		return new Vector2 (deltax, deltay);
	}

	//update last collider point, next collider and next collider point
	void setNextCollider() {
//		Debug.Log ("test");
		setRayDirection();
		//Debug.Log (rayDirection);
		rayOffsets = getRayOffsets ();
		raycastEdges (nextColliderPoint);
		collidersFound = Physics2D.RaycastAll (nextColliderPoint, new Vector3 (rayDirection.x, rayDirection.y));
//		Debug.Log (horizontalReflection);

//		Debug.Log (collidersFound.Length);
//		Debug.Log (topCollidersFound.Length);
//		Debug.Log (botCollidersFound.Length);

		if (topCollidersFound.Length > 0) {
			if (collidersFound.Length > 0) {
				if (topCollidersFound [0].collider != collidersFound [0].collider) {
					if (topCollidersFound [0].point.y < collidersFound [0].point.y) {
						endPointerFromEdgeRays (topCollidersFound [0], nextColliderPoint);
					}
				}
			} else {
				endPointerFromEdgeRays (topCollidersFound [0], nextColliderPoint);
			}
		}

		if (horizontalReflection) {
			nextCollider = getNextCollider (collidersFound, nextColliderPoint, rayDirection);
			nextColliderPoint = getNextColliderPoint (nextCollider, rayDirection, nextColliderPoint);
			checkRockIsWithinColliderBounds (nextCollider, nextColliderPoint);
		}

		if (horizontalReflection) {
			if (botCollidersFound.Length > 0) {
				if (collidersFound.Length > 0) {
					if (botCollidersFound [0].collider != collidersFound [0].collider) {
						if (botCollidersFound [0].point.y < collidersFound [0].point.y) {
							endPointerFromEdgeRays (botCollidersFound [0], nextColliderPoint);
						}
					}
				} else {
					endPointerFromEdgeRays (botCollidersFound [0], nextColliderPoint);
				}
			}
		}

		if (horizontalReflection) {
			nextCollider = getNextCollider (collidersFound, nextColliderPoint, rayDirection);
			nextColliderPoint = getNextColliderPoint (nextCollider, rayDirection, nextColliderPoint);
			checkRockIsWithinColliderBounds (nextCollider, nextColliderPoint);
		}
	}

	void setRayDirection() {
		if (horizontalReflection) {
			rayDirection = new Vector3 (-rayDirection.x, rayDirection.y);
		} else {
			rayDirection = new Vector3 (rayDirection.x, -rayDirection.y);
		}
	}

	//find if there is another collider
	RaycastHit2D getNextCollider(RaycastHit2D[] colliders, Vector3 lastColliderPoint, Vector3 rayDirection) {
		RaycastHit2D nextCollider;
		switch (colliders.Length) {
		//if there is no collider next collider does not exist
		case 0:
			nextCollider = default(RaycastHit2D);
			break;
		//if there is a collider next one should be the first one ray contacts
		default:
			nextCollider = colliders [0];
			break;
		}
		return nextCollider;
	}



	//next collider point is point at which ray hit next collider plus offsets from the thickness of the rock
	Vector3 getNextColliderPoint(RaycastHit2D collider, Vector3 rayDirection, Vector3 thisColliderPoint) {
		Vector3 nextColliderPoint = thisColliderPoint;
		if (collider != default(RaycastHit2D)) {
			//here horizontal reflection is only used to calculate offsets. Not meant to update global for finding if rock goes downwards
			bool horizontalReflection = reflectsInX (collider);
			Vector2 offsets = pointerOffsets (rayDirection, horizontalReflection);
			nextColliderPoint = collider.point;
			nextColliderPoint = new Vector3 (nextColliderPoint.x + offsets.x, nextColliderPoint.y + offsets.y);
		}
		return nextColliderPoint;
	}

	//if there is a valid collider (not default collider as this means there is no collider) and rock is not within collider bounds there cannot be a horizontal reflection
	//point at the collider is then added but no further points will be as the ray must stop at this point
	void checkRockIsWithinColliderBounds (RaycastHit2D nextCollider, Vector3 nextColliderPoint) {
		if (nextCollider != default(RaycastHit2D) && nextCollider.collider.bounds.min.y > nextColliderPoint.y) {
			horizontalReflection = false;
			pointerVertices.Add (nextColliderPoint);
		}
	}

	//finds x position at certain y value given the equation of a line
	float pointerXpos(Vector3 point, Vector3 dir, float yPos) {
		Vector3 p1 = point;
		Vector3 p2 = point + dir;
		float m = (p2.y - p1.y) / (p2.x - p1.x);
		float c = p1.y - m * p1.x;
		float XPos = (yPos - c) / m;
		return XPos;
	}

	//adds points on pointer to line renderer so they can be drawn out
	void drawPointerLine(List<Vector3> pointerVertices) {
		//set pointer indices equal to points at which ray intersects walls
		LineRenderer[] allRenderers = GetComponentsInChildren<LineRenderer> ();
		LineRenderer pointer;
		foreach (LineRenderer renderer in allRenderers) {
			if (renderer.gameObject != this.gameObject) {
				pointer = renderer;
				pointer.numPositions = pointerVertices.Count;
				pointer.SetPositions (pointerVertices.ToArray());

			}
		}
	}
}
