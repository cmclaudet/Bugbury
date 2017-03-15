using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//sets gameobjects to singleton managers so they can be easily referenced by other scripts
public class setGameObjects : MonoBehaviour {

	public GameObject slingshotLeft;
	public GameObject slingshotRight;
	public GameObject springAnchor;
	public GameObject throwSound;
	public GameObject tinkSound;
	public GameObject splatSounds;
	public GameObject missedSound;

	void Awake() {
		rockManager.Instance.slingshotLeft = slingshotLeft;
		rockManager.Instance.slingshotRight = slingshotRight;
		rockManager.Instance.springAnchor = springAnchor;
		rockManager.Instance.throwSound = throwSound;
		rockManager.Instance.splatSounds = splatSounds;
		rockManager.Instance.tinkSound = tinkSound;
		rockManager.Instance.tinkSound = missedSound;

	}

}
