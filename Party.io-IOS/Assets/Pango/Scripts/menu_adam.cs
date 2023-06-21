using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_adam : MonoBehaviour {

	// Use this for initialization
	void Start () {

		InvokeRepeating ("wait", 2,4);
	}
	
	// Update is called once per frame
	void Update () {
        InvokeRepeating("wait", 2, 4);
    }
    void wait()
    {
        JointSpring sprng = new JointSpring();
        sprng.spring = Random.Range(80, 180);
        sprng.targetPosition = 0;
        GetComponent<HingeJoint>().spring = sprng;

    }
}
