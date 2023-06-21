using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TekmeKararVerici : MonoBehaviour {

	PlayerController_A plc;
	void Start(){
		plc = transform.root.GetChild (0).GetComponent<PlayerController_A> ();
	}
	void OnTriggerEnter(Collider other){
		if (other.isTrigger)
			return;
		

	}


}
