using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class geri_git : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z-(100f*Time.deltaTime));
	}
}
