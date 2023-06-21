using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kirilma_surekli : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
       
            
                GetComponent<BoxCollider>().isTrigger = true;
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
                    transform.GetChild(i).GetComponent<Rigidbody>().AddForce(transform.GetChild(i).right * -0.5f, ForceMode.Impulse);
                }
            
        }

}
