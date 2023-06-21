using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fracturee : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.layer!=19)
		GetComponent<FracturedObject> ().enabled = true;

	}
	// Update is called once per frame
	void Update () {
		
	}
}
