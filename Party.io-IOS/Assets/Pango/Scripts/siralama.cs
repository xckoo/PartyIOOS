using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class siralama : MonoBehaviour {
	public GameObject[] karakterler;
	public Transform[] zemin;
	bool zemin_deg=false;
	public GameObject confettis;
	// Use this for initialization
	void Start () {
		Invoke ("wait_zemin", 1);
	}

	void wait_zemin(){

		zemin_deg = true;				
		Invoke ("confetti", 1);
	}
	void confetti(){
		confettis.gameObject.SetActive (true);


	//	StartCoroutine (KazananHareketi ());
	//	StartCoroutine (kaybeden ());
	//	StartCoroutine (sinirli ());
	}

	// Update is called once per frame
	void Update () {
		if (zemin_deg) {
			zemin [0].transform.localScale = Vector3.Lerp (zemin [0].transform.localScale, new Vector3 (zemin [0].transform.localScale.x, 0.01f, zemin [0].transform.localScale.z), Time.deltaTime * 0.5f);
			zemin [1].transform.localScale = Vector3.Lerp (zemin [1].transform.localScale, new Vector3 (zemin [1].transform.localScale.x, 0.007f, zemin [1].transform.localScale.z), Time.deltaTime * 0.5f);
			zemin [2].transform.localScale = Vector3.Lerp (zemin [2].transform.localScale, new Vector3 (zemin [2].transform.localScale.x, 0.004f, zemin [2].transform.localScale.z), Time.deltaTime * 0.5f);
		}
	}

	#region OyunBittiHareketi

		

	// Oyun sonunda kazanan kollarını havaya kaldırır
	IEnumerator KazananHareketi(){
		float timer = 0;
		while(timer<=2f){
			timer += Time.deltaTime;
			float kaldirmaLerpSpeed = 5f;
			JointSpring jpsoe1_1 = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagOmuzEk.GetComponent<HingeJoint>().spring;
			jpsoe1_1.spring += karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagKolJointler [0].spring*0.025f;
			jpsoe1_1.damper = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagKolJointler [0].damper;
			jpsoe1_1.targetPosition = Mathf.Lerp(jpsoe1_1.targetPosition, karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagKolJointler[0].tp, Time.deltaTime * kaldirmaLerpSpeed);
			karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagOmuzEk.GetComponent<HingeJoint> ().spring = jpsoe1_1;

			JointLimits jpsoel1_1 = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagOmuzEk.GetComponent<HingeJoint>().limits;
			jpsoel1_1.max = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagKolJointler [0].maxLimit;
			jpsoel1_1.min = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagKolJointler [0].minLimit;
			karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagOmuzEk.GetComponent<HingeJoint> ().limits = jpsoel1_1;


			JointSpring jpsoe2_1 = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagOmuz.GetComponent<HingeJoint>().spring;
			jpsoe2_1.spring += karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagKolJointler [1].spring*0.025f;
			jpsoe2_1.damper = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagKolJointler [1].damper;
			jpsoe2_1.targetPosition = Mathf.Lerp(jpsoe2_1.targetPosition, karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagKolJointler[1].tp, Time.deltaTime * kaldirmaLerpSpeed);
			karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagOmuz.GetComponent<HingeJoint> ().spring = jpsoe2_1;

			JointLimits jpsoel2_1 = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagOmuz.GetComponent<HingeJoint>().limits;
			jpsoel2_1.max = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagKolJointler [1].maxLimit;
			jpsoel2_1.min = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagKolJointler [1].minLimit;
			karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagOmuz.GetComponent<HingeJoint> ().limits = jpsoel2_1;


			JointSpring jpsoe3_1 = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagKol.GetComponent<HingeJoint>().spring;
			jpsoe3_1.spring += karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagKolJointler [2].spring*0.025f;
			jpsoe3_1.damper = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagKolJointler [2].damper;
			jpsoe3_1.targetPosition = Mathf.Lerp(jpsoe3_1.targetPosition, karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagKolJointler[2].tp, Time.deltaTime * kaldirmaLerpSpeed);
			karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagKol.GetComponent<HingeJoint> ().spring = jpsoe3_1;

			JointLimits jpsoel3_1 = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagKol.GetComponent<HingeJoint>().limits;
			jpsoel3_1.max = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagKolJointler [2].maxLimit;
			jpsoel3_1.min = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagKolJointler [2].minLimit;
			karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagKol.GetComponent<HingeJoint> ().limits = jpsoel3_1;



			JointSpring jpsoe1_2 = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().solOmuzEk.GetComponent<HingeJoint>().spring;
			jpsoe1_2.spring += karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().solKolJointler [0].spring*0.025f;
			jpsoe1_2.damper = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().solKolJointler [0].damper;
			jpsoe1_2.targetPosition = Mathf.Lerp(jpsoe1_2.targetPosition, karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().solKolJointler[0].tp, Time.deltaTime * kaldirmaLerpSpeed);
			karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().solOmuzEk.GetComponent<HingeJoint> ().spring = jpsoe1_2;

			JointLimits jpsoel1_2 = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().solOmuzEk.GetComponent<HingeJoint>().limits;
			jpsoel1_2.max = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().solKolJointler [0].maxLimit;
			jpsoel1_2.min = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().solKolJointler [0].minLimit;
			karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().solOmuzEk.GetComponent<HingeJoint> ().limits = jpsoel1_2;


			JointSpring jpsoe2_2 = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().solOmuz.GetComponent<HingeJoint>().spring;
			jpsoe2_2.spring += karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().solKolJointler [1].spring*0.025f;
			jpsoe2_2.damper = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().solKolJointler [1].damper;
			jpsoe2_2.targetPosition = Mathf.Lerp(jpsoe2_2.targetPosition, karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().solKolJointler[1].tp, Time.deltaTime * kaldirmaLerpSpeed);
			karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().solOmuz.GetComponent<HingeJoint> ().spring = jpsoe2_2;

			JointLimits jpsoel2_2 = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().solOmuz.GetComponent<HingeJoint>().limits;
			jpsoel2_2.max = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().solKolJointler [1].maxLimit;
			jpsoel2_2.min = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().solKolJointler [1].minLimit;
			karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().solOmuz.GetComponent<HingeJoint> ().limits = jpsoel2_2;

			JointSpring jpsoe3_2 = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().solKol.GetComponent<HingeJoint>().spring;
			jpsoe3_2.spring += karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().solKolJointler [2].spring*0.025f;
			jpsoe3_2.damper = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().solKolJointler [2].damper;
			jpsoe3_2.targetPosition = Mathf.Lerp(jpsoe3_2.targetPosition, karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().sagKolJointler[2].tp, Time.deltaTime * kaldirmaLerpSpeed);
			karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().solKol.GetComponent<HingeJoint> ().spring = jpsoe3_2;

			JointLimits jpsoel3_2 = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().solKol.GetComponent<HingeJoint>().limits;
			jpsoel3_2.max = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().solKolJointler [2].maxLimit;
			jpsoel3_2.min = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().solKolJointler [2].minLimit;
			karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().solKol.GetComponent<HingeJoint> ().limits = jpsoel3_2;

			JointSpring jpsb = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().gogus.GetComponent<HingeJoint>().spring;
			jpsb.spring += karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().belJoint.spring*0.025f;
			jpsb.damper = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().belJoint.damper;
			jpsb.targetPosition = Mathf.Lerp(jpsb.targetPosition, karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().belJoint.tp, Time.deltaTime * kaldirmaLerpSpeed);
			karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().gogus.GetComponent<HingeJoint> ().spring = jpsb;

			JointLimits jpsbl = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().gogus.GetComponent<HingeJoint>().limits;
			jpsbl.max = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().belJoint.maxLimit;
			jpsbl.min = karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().belJoint.minLimit;
			karakterler[0].transform.GetChild(0).GetComponent<PlayerController_A>().gogus.GetComponent<HingeJoint> ().limits = jpsbl;

			yield return Time.deltaTime;
		}

	}
	IEnumerator kaybeden(){
		float timer = 0;
		while(timer<=2f){
			Debug.Log ("girdiii");
			timer += Time.deltaTime;
			float kaldirmaLerpSpeed = 5f;
			JointSpring jpsoe1_1 = karakterler[1].transform.GetChild(0).GetComponent<PlayerController_A>().kafa.GetComponent<HingeJoint>().spring;
			jpsoe1_1.spring = 0;
			//jpsoe1_1.damper = 0;
			//jpsoe1_1.targetPosition = 0;
			//karakterler [1].transform.GetChild (0).GetComponent<PlayerController_A> ().kafa.GetComponent<HingeJoint> ().axis = new Vector3 (0, 1, 0);
			karakterler[1].transform.GetChild(0).GetComponent<PlayerController_A>().kafa.GetComponent<HingeJoint> ().spring = jpsoe1_1;
			yield return Time.deltaTime;
		}

	}

	IEnumerator sinirli(){
		float timer = 0;
		while(timer<=20){
			Debug.Log ("girdiii2");
			timer += Time.deltaTime;
			float kaldirmaLerpSpeed = 5f;
			JointSpring jpsoe1_1 = karakterler[2].transform.GetChild(0).GetComponent<PlayerController_A>().kafa.GetComponent<HingeJoint>().spring;
			jpsoe1_1.spring = 200;
			jpsoe1_1.damper = 20;
			jpsoe1_1.targetPosition = Mathf.Sin(Time.time*3)*30;
			//karakterler [1].transform.GetChild (0).GetComponent<PlayerController_A> ().kafa.GetComponent<HingeJoint> ().axis = new Vector3 (0, 1, 0);
			karakterler[2].transform.GetChild(0).GetComponent<PlayerController_A>().kafa.GetComponent<HingeJoint> ().spring = jpsoe1_1;
			yield return Time.deltaTime;
		}

	}



	#endregion
}
