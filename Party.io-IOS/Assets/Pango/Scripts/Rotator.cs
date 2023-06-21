using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	public enum Axis{
		X,Y,Z
	}
	public Axis axis;
	public float speed;
	// Update is called once per frame
	void Update () {
		if (axis == Axis.X) {
			transform.Rotate (Time.deltaTime * speed,0,0);
		} else if (axis == Axis.Y) {
			transform.Rotate (0,Time.deltaTime * speed, 0);
		} else if (axis == Axis.Z) {
			transform.Rotate (0, 0,Time.deltaTime * speed);
		}
	}
}
