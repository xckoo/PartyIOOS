using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaydirma : MonoBehaviour {
	public bool sag=false;
	public float force;
	/*void OnCollisionStay(Collision other){
		if (other.transform.GetComponent<Rigidbody> ()) {
			other.transform.GetComponent<Rigidbody> ().AddForce (transform.forward * force, ForceMode.Force);
			if(!other.collider.isTrigger)
			if (other.transform.root.childCount > 1) {
				if (!other.transform.root.GetChild (0).GetComponent<PlayerController_A> ().dustu) {
					other.transform.root.GetChild (0).GetComponent<PlayerController_A> ().Dusus ();
					other.transform.root.GetChild (0).GetComponent<PlayerController_A> ().kaydir = true;
				}
			}
		}
	}*/

	void OnTriggerStay(Collider other){
		if (other.transform.GetComponent<Rigidbody> ()) {
			if(!sag)
			other.transform.GetComponent<Rigidbody> ().AddForce (transform.forward * force, ForceMode.Force);
			else
			other.transform.GetComponent<Rigidbody> ().AddForce (-transform.right * force, ForceMode.Force);

			if(!other.isTrigger)
			if (other.transform.root.childCount > 1) {
                    if (other.transform.root.GetChild(0).GetComponent<PlayerController_A>())
                    {
                        //if (!other.transform.root.GetChild (0).GetComponent<PlayerController_A> ().dustu) {

                        other.transform.root.GetChild(0).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;

                        other.transform.root.GetChild(0).GetComponent<PlayerController_A>().dustu = true;
                        other.transform.root.GetChild(0).GetComponent<PlayerController_A>().uyanis_sure = 10;
                        other.transform.root.GetChild(0).GetComponent<PlayerController_A>().kaydir = true;
                        //}
                    }
			}
		}
	}
}
