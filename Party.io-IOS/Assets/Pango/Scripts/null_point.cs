using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class null_point : MonoBehaviour {
	public bool secili=false;
	// Use this for initialization
	void Start () {
		
	}
	void OnTriggerEnter(Collider other){
		if (other.GetComponent<AIController> () != null) {
			other.GetComponent<AIController> ().Target_null ();
			other.GetComponent<AIController> ().Repeat ();
		//	Debug.Log ("Triggerrrrrrrr");	
		}	
	}

	// Update is called once per frame
	void Update () {
		
	}
}
