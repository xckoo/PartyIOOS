using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parcala : MonoBehaviour {
	void OnCollisionEnter(Collision col){
		
		if (col.relativeVelocity.magnitude >= 3f) {
			GetComponent<Rigidbody> ().isKinematic = false;
			GetComponent<Rigidbody> ().AddForce (new Vector3 (Random.value, Random.value, Random.value), ForceMode.Impulse);
		}
		Destroy (this, 0.1f);
	}
}
