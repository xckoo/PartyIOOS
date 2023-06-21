using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uzulme : MonoBehaviour {
	PlayerController_A controller;
	// Use this for initialization
	void Start () {
		controller = GetComponent<PlayerController_A> ();
		Invoke ("wait", 1);

		controller.kaval1.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
		controller.kaval2.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
	}
	void wait(){
		StartCoroutine (KazananHareketi ());
	}

	// Update is called once per frame
	void Update () {

	}



	// Oyun sonunda kazanan kollarını havaya kaldırır
	IEnumerator KazananHareketi(){
		float timer = 0;
		//kaldiririyor = true;
		while(timer<=200f){
			timer += Time.deltaTime;
			float kaldirmaLerpSpeed = 5f;

			JointSpring jpsoe1_1 = controller.kafa.GetComponent<HingeJoint>().spring;
			jpsoe1_1.spring = 30;
			jpsoe1_1.damper = 0;
			jpsoe1_1.targetPosition = Mathf.Sin (Time.time * 3) * 30;
			controller.kafa.GetComponent<HingeJoint> ().spring = jpsoe1_1;


			yield return Time.deltaTime;
		}

	}
}
