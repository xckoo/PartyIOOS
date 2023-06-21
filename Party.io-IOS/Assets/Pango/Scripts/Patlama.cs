using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patlama : MonoBehaviour {

	void OnEnable(){

		Collider[] cols = Physics.OverlapSphere (transform.position, 10f);
		foreach (var _collider in cols) {
			if (_collider.GetComponent<PlayerController_A> ()) {
				_collider.GetComponent<Rigidbody> ().AddForce (300f*(transform.position-_collider.transform.position).normalized,ForceMode.Impulse);
			}
		}
		Destroy (gameObject,2f);
	}


}
