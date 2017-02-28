using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//custom class for objects which scale up from one size to another on entry
//can be used for scaling down or up
public class blowUpGeneral {
	public float velocity;	//initial scale speed
	public float acceleration;	//scale acceleration (should be negative for more natural scaling
	public float scale;		//current scale value. changes over time

	public blowUpGeneral (float vel, float acc, float scStart) 	{	//, float scEnd, float t, float endVel) {
		this.velocity = vel;
		this.acceleration = acc;
		this.scale = scStart;
	}

	//call in update function
	public void updateVelocity() {
		//mechanics equation v = u + at
		this.velocity = this.velocity + this.acceleration * Time.fixedDeltaTime;
	}

	//call in update function
	public void updateScale() {
		//mechanics equation s = ut + 0.5at^2
		this.scale = this.scale + this.velocity * Time.fixedDeltaTime + 0.5f * this.acceleration * (Time.fixedDeltaTime) * (Time.fixedDeltaTime);
	}
}
