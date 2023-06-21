using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yakinlikveduvarkontrol : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.layer == 21 ) {
			Debug.Log ("duvar");
			Debug.Log(other.gameObject.name);
			transform.root.GetChild (0).GetComponent<PlayerController_A> ().duvar = true;
			//transform.root.GetChild (0).GetComponent<AIController> ().target_me ();
		}
		/*if (other.gameObject.layer >=8) {
		//	Debug.Log ("target" + other.transform.root.GetChild (0).transform);

			if (other.gameObject.layer != other.transform.root.GetChild (0).gameObject.layer) {
				Debug.Log ("target" + other.transform.root.GetChild (0).transform);
				transform.root.GetChild (0).GetComponent<AIController> ().currentTarget = other.transform.root.GetChild (0).transform;
			}
		}*/
	}
	void OnTriggerExit(Collider other){
		if (other.gameObject.layer == 21 ) {
			transform.root.GetChild (0).GetComponent<PlayerController_A> ().duvar = false;
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
