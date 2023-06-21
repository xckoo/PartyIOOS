using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uzulme2 : MonoBehaviour {

	PlayerController_A controller;

	// Use this for initialization
	void Start () {
		controller = GetComponent<PlayerController_A> ();
		//Invoke ("wait", 1);

		controller.kaval1.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
		controller.kaval2.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
