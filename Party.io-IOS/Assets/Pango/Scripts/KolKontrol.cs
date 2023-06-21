using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KolKontrol : MonoBehaviour {
	public bool otherHandin = false;
	/*void OnTriggerStay(Collider col){
		if (col.GetComponent<Tutunma> ()) {
			//Eger Kendi Kolum ise
			//Debug.Log ("col.GetComponent<Tutunma> ().pc"+col.GetComponent<Tutunma> ().pc);
			//Debug.Log ("transform.root.GetChild (0).GetComponent<PlayerController_A> ()"+transform.root.GetChild (0).GetComponent<PlayerController_A> ());

			if (col.GetComponent<Tutunma> ().pc == transform.root.GetChild (0).GetComponent<PlayerController_A> ()) {
				//Debug.Log ("Tuttttuu furuttu");
				otherHandin = true;
			}
		}
		
	}

	void OnTriggerExit(Collider col){
		if (col.GetComponent<Tutunma> ()) {
			//Eger Kendi Kolum ise
			if (col.GetComponent<Tutunma> ().pc == transform.root.GetChild (0).GetComponent<PlayerController_A> ()) {
			//	Debug.Log ("exit");
				otherHandin = false;
			}
		}
	}*/
}
