using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirlatianObje : MonoBehaviour {
	public GameObject[] icObjeler;

	[HideInInspector]
	public PlayerController_A ilkTutan;
	[HideInInspector]
	public bool tutuldu;
	[HideInInspector]
	public bool kaldirildi;
	[HideInInspector]
	public float dayaniklilikcarpani = 1f;

	void OnCollisionEnter(Collision col){
		
		if (col.relativeVelocity.magnitude <= 20f*dayaniklilikcarpani)
			return;
		
		GetComponent<MeshRenderer> ().enabled = false;
		GetComponent<Collider> ().enabled = false;
		for (int i = 0; i < icObjeler.Length; i++) {
			icObjeler [i].SetActive (true);
		}
	}

}
