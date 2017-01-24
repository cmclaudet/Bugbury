using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class blowUpGeneral {
	public float velocity;
	public float acceleration;
	public float scale;

	public blowUpGeneral (float vel, float acc, float sc) {
		this.velocity = vel;
		this.acceleration = acc;
		this.scale = sc;
	}

	public void updateVelocity() {
		this.velocity = this.velocity + this.acceleration * Time.deltaTime;
	}

	public void updateScale() {
		this.scale = this.scale + this.velocity * Time.deltaTime + 0.5f * this.acceleration * (Time.deltaTime) * (Time.deltaTime);
	}
}
