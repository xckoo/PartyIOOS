using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sevinme : MonoBehaviour {
	PlayerController_A controller;
	// Use this for initialization
	void Start () {
		controller = GetComponent<PlayerController_A> ();
		Invoke ("wait", 2);

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
			JointSpring jpsoe1_1 = controller.sagOmuzEk.GetComponent<HingeJoint>().spring;
			jpsoe1_1.spring += controller.sagKolJointler [0].spring*0.025f;
			jpsoe1_1.damper = controller.sagKolJointler [0].damper;
			jpsoe1_1.targetPosition = Mathf.Lerp(jpsoe1_1.targetPosition, controller.sagKolJointler[0].tp, Time.deltaTime * kaldirmaLerpSpeed);
			controller.sagOmuzEk.GetComponent<HingeJoint> ().spring = jpsoe1_1;

			JointLimits jpsoel1_1 = controller.sagOmuzEk.GetComponent<HingeJoint>().limits;
			jpsoel1_1.max = controller.sagKolJointler [0].maxLimit;
			jpsoel1_1.min = controller.sagKolJointler [0].minLimit;
			controller.sagOmuzEk.GetComponent<HingeJoint> ().limits = jpsoel1_1;


		/*JointSpring jpsoe2_1 = sagOmuz.GetComponent<HingeJoint>().spring;
			jpsoe2_1.spring += sagKolJointler [1].spring*0.025f;
			jpsoe2_1.damper = sagKolJointler [1].damper;
			jpsoe2_1.targetPosition = Mathf.Lerp(jpsoe2_1.targetPosition, sagKolJointler[1].tp, Time.deltaTime * kaldirmaLerpSpeed);
			sagOmuz.GetComponent<HingeJoint> ().spring = jpsoe2_1;

			JointLimits jpsoel2_1 = sagOmuz.GetComponent<HingeJoint>().limits;
			jpsoel2_1.max = sagKolJointler [1].maxLimit;
			jpsoel2_1.min = sagKolJointler [1].minLimit;
			sagOmuz.GetComponent<HingeJoint> ().limits = jpsoel2_1;*/


			JointSpring jpsoe3_1 = controller.sagKol.GetComponent<HingeJoint>().spring;
			jpsoe3_1.spring += controller.sagKolJointler [2].spring*0.025f;
			jpsoe3_1.damper = controller.sagKolJointler [2].damper;
			jpsoe3_1.targetPosition = Mathf.Lerp(jpsoe3_1.targetPosition, controller.sagKolJointler[2].tp, Time.deltaTime * kaldirmaLerpSpeed);
			controller.sagKol.GetComponent<HingeJoint> ().spring = jpsoe3_1;

			JointLimits jpsoel3_1 = controller.sagKol.GetComponent<HingeJoint>().limits;
			jpsoel3_1.max = controller.sagKolJointler [2].maxLimit;
			jpsoel3_1.min = controller.sagKolJointler [2].minLimit;
			controller.sagKol.GetComponent<HingeJoint> ().limits = jpsoel3_1;



			JointSpring jpsoe1_2 = controller.solOmuzEk.GetComponent<HingeJoint>().spring;
			jpsoe1_2.spring += controller.solKolJointler [0].spring*0.025f;
			jpsoe1_2.damper = controller.solKolJointler [0].damper;
			jpsoe1_2.targetPosition = Mathf.Lerp(jpsoe1_2.targetPosition, controller.solKolJointler[0].tp, Time.deltaTime * kaldirmaLerpSpeed);
			controller.solOmuzEk.GetComponent<HingeJoint> ().spring = jpsoe1_2;

			JointLimits jpsoel1_2 = controller.solOmuzEk.GetComponent<HingeJoint>().limits;
			jpsoel1_2.max = controller.solKolJointler [0].maxLimit;
			jpsoel1_2.min = controller.solKolJointler [0].minLimit;
			controller.solOmuzEk.GetComponent<HingeJoint> ().limits = jpsoel1_2;


		/*JointSpring jpsoe2_2 = solOmuz.GetComponent<HingeJoint>().spring;
			jpsoe2_2.spring += solKolJointler [1].spring*0.025f;
			jpsoe2_2.damper = solKolJointler [1].damper;
			jpsoe2_2.targetPosition = Mathf.Lerp(jpsoe2_2.targetPosition, solKolJointler[1].tp, Time.deltaTime * kaldirmaLerpSpeed);
			solOmuz.GetComponent<HingeJoint> ().spring = jpsoe2_2;

			JointLimits jpsoel2_2 = solOmuz.GetComponent<HingeJoint>().limits;
			jpsoel2_2.max = solKolJointler [1].maxLimit;
			jpsoel2_2.min = solKolJointler [1].minLimit;
			solOmuz.GetComponent<HingeJoint> ().limits = jpsoel2_2;*/

			JointSpring jpsoe3_2 = controller.solKol.GetComponent<HingeJoint>().spring;
			jpsoe3_2.spring += controller.solKolJointler [2].spring*0.025f;
			jpsoe3_2.damper = controller.solKolJointler [2].damper;
			jpsoe3_2.targetPosition = Mathf.Lerp(jpsoe3_2.targetPosition, controller.sagKolJointler[2].tp, Time.deltaTime * kaldirmaLerpSpeed);
			controller.solKol.GetComponent<HingeJoint> ().spring = jpsoe3_2;

			JointLimits jpsoel3_2 = controller.solKol.GetComponent<HingeJoint>().limits;
			jpsoel3_2.max = controller.solKolJointler [2].maxLimit;
			jpsoel3_2.min = controller.solKolJointler [2].minLimit;
			controller.solKol.GetComponent<HingeJoint> ().limits = jpsoel3_2;

			JointSpring jpsb = controller.gogus.GetComponent<HingeJoint>().spring;
			jpsb.spring += controller.belJoint.spring*0.025f;
			jpsb.damper = controller.belJoint.damper;
			jpsb.targetPosition = Mathf.Lerp(jpsb.targetPosition, controller.belJoint.tp, Time.deltaTime * kaldirmaLerpSpeed);
			controller.gogus.GetComponent<HingeJoint> ().spring = jpsb;

			JointLimits jpsbl = controller.gogus.GetComponent<HingeJoint>().limits;
			jpsbl.max = controller.belJoint.maxLimit;
			jpsbl.min = controller.belJoint.minLimit;
			controller.gogus.GetComponent<HingeJoint> ().limits = jpsbl;

			JointSpring jpsb1 = controller.p1.GetComponent<HingeJoint>().spring;
		jpsb1.targetPosition = 0;
			controller.p1.GetComponent<HingeJoint> ().spring = jpsb1;

			JointSpring jpsb2 = controller.p2.GetComponent<HingeJoint>().spring;
		jpsb2.targetPosition = 0;
			controller.p2.GetComponent<HingeJoint> ().spring = jpsb2;

			JointSpring jpsb3 = controller.p3.GetComponent<HingeJoint>().spring;
		jpsb3.targetPosition = 0;
			controller.p3.GetComponent<HingeJoint> ().spring = jpsb3;

			JointSpring jpsb4= controller.p4.GetComponent<HingeJoint>().spring;
		jpsb4.targetPosition = 0;
			controller.p4.GetComponent<HingeJoint> ().spring = jpsb4;

		yield return Time.deltaTime;
	}

}
}
