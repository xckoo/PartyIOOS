using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kirilma : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<PlayerController_A>())
        {
            if (collision.transform.GetComponent<PlayerController_A>().dustu)
            {
                GetComponent<MeshCollider>().isTrigger = true;
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
                    transform.GetChild(i).GetComponent<Rigidbody>().AddForce(transform.GetChild(i).right * -0.5f, ForceMode.Impulse);
                }
            }
        }
    }
}
