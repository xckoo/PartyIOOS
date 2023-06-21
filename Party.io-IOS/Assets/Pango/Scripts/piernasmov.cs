using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piernasmov : MonoBehaviour {

    //public HingeJoint hj;
    //public Transform objetivo;

    
    public bool invertido;
	[HideInInspector]
	public float targetRotMultiplier;
	//[HideInInspector]
	public bool stop;

    HingeJoint hj;

    Transform objetivo;
    // Suscribe Edsonxn Channel On Youtube!!
    //for more tutorials

	public bool kol=false;
	public bool sag=false;

    // Use this for initialization
    void Start () {
        objetivo = GameObject.FindGameObjectWithTag("ayakHareket").transform;
       
		if (kol) {
			stop = true;
			//cj = GetComponent<ConfigurableJoint> ();
			hj = GetComponent<HingeJoint> ();
		} else {
			hj = GetComponent<HingeJoint> ();
		}
    }
	
	// Update is called once per frame
	void Update () {
		

		JointSpring js = new JointSpring ();

		if (kol) {
			if (invertido) {
				if (stop) {
					js.spring = 0;
					hj.spring = js;
					return;
				}
				
			
				js.targetPosition = objetivo.localEulerAngles.x;

				if (js.targetPosition > 180)
					js.targetPosition = js.targetPosition - 360;

				js.targetPosition = Mathf.Clamp (js.targetPosition, hj.limits.min + 20, hj.limits.max - 20);
				if (invertido) {
					js.targetPosition = js.targetPosition * -1;
				}

				js.spring = 2000; 
				hj.spring = js;
			} 
			else {
				if (sag) {
					if (stop) {
						GetComponent<ConfigurableJoint> ().targetRotation = Quaternion.Lerp(GetComponent<ConfigurableJoint> ().targetRotation, new Quaternion(-0.1f,0f,0.2f,1),Time.deltaTime*20);
						return;
					}

					GetComponent<ConfigurableJoint> ().targetRotation = Quaternion.Lerp (GetComponent<ConfigurableJoint> ().targetRotation, new Quaternion (-0.75f, -1f, 0.5f, 0), Time.deltaTime * 20);
				}
				if (!sag) {

					if (stop) {
						GetComponent<ConfigurableJoint> ().targetRotation =Quaternion.Lerp(GetComponent<ConfigurableJoint> ().targetRotation, new Quaternion(0.1f,0f,-0.2f,1f),Time.deltaTime*20);
						return;
					}

					GetComponent<ConfigurableJoint> ().targetRotation = Quaternion.Lerp(GetComponent<ConfigurableJoint> ().targetRotation,new Quaternion(0.75f,-1f,-0.5f,0),Time.deltaTime*20);

				
				}
			}


		} else {
			if (stop)
				return;
			
		 	js = hj.spring;
		
	        js.targetPosition = objetivo.localEulerAngles.x;

	        if (js.targetPosition > 180)
	            js.targetPosition = js.targetPosition - 360;
			
	        js.targetPosition = Mathf.Clamp(js.targetPosition, hj.limits.min + 5, hj.limits.max - 5);
	        if (invertido)
	        {
	            js.targetPosition = js.targetPosition * -1;
	        }

			//js.spring = 500;// targetRotMultiplier; 
			hj.spring = js;
		}
       
	}
}
