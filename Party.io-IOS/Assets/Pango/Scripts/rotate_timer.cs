using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_timer : MonoBehaviour {
	public bool rotate = false;
	[Range(0,30)]
	public float timer;

	public float speed;
	Vector3 temp_rot;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("don", timer, timer);
		temp_rot = transform.eulerAngles;
	}
	void don(){
		rotate = true;

	}

	// Update is called once per frame
	void Update () {
		
		if(rotate)
		transform.Rotate (0, 0,Time.deltaTime * speed);
	
		if ((int)transform.eulerAngles.z == (int)temp_rot.z) {
			rotate = false;
		}
	}
}
