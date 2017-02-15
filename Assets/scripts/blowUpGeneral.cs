using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//custom class for objects which scale up from small size to max size upon entry
//can also be used for scaling down
public class blowUpGeneral {
	public float velocity;	//initial scale speed
	public float acceleration;	//scale acceleration (should be negative for more natural scaling
	public float scale;		//current scale value. changes over time
//	public float scaleEnd;	//desired end scale value
//	public float time;
//	public float endVel;

	public blowUpGeneral (float vel, float acc, float scStart) 	{	//, float scEnd, float t, float endVel) {
		this.velocity = vel;
		this.acceleration = acc;
		this.scale = scStart;
//		this.scaleEnd = scEnd;
//		this.time = t;
//		this.endVel = endVel;
	}
/*
	float initVel() {
//		time, scale, finalVel, initVel
//		t,s,v,u
//		scale = 0.5*(initVel + finalVel)*time.deltaTime
		float initVel = 2.0f*(this.scaleEnd - this.scale)/this.time - this.endVel;
		return initVel;
	}

	float acc() {
//		t,s,v,a
//		s = vt - 0.5*a*t^2
		float accel = 2.0f*(this.endVel*this.time - (this.scaleEnd - this.scale))/(this.time*this.time);
		return accel;
	}
*/
	//call in update function
	public void updateVelocity() {
		//mechanics equation v = u + at
		this.velocity = this.velocity + this.acceleration * Time.deltaTime;
	}

	//call in update function
	public void updateScale() {
		//mechanics equation s = ut + 0.5at^2
		this.scale = this.scale + this.velocity * Time.deltaTime + 0.5f * this.acceleration * (Time.deltaTime) * (Time.deltaTime);
	}
}
