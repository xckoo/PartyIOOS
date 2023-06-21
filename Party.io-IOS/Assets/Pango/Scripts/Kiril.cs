using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kiril : MonoBehaviour {

	void OnTriggerEnter(Collider col)
	{
		if (!col.transform.GetComponent<Rigidbody> ())
			return;
		if (!GetComponent<Rigidbody> ().isKinematic)
			return;
		
		if (col.transform.GetComponent<Rigidbody>().velocity.magnitude >= 5f) {
			
			GetComponent<Rigidbody> ().isKinematic = false;
			GetComponent<Rigidbody> ().AddForce (Vector3.up * 10f + new Vector3(Random.value,0,Random.value)*5f, ForceMode.Impulse);

		}
	}
}
