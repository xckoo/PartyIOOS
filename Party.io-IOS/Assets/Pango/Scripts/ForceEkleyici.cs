using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceEkleyici : MonoBehaviour {
	
	

	public PlayerController_A plc;
	public bool isEnabled = false;

    private bool tekrarForceUygulayabilir = true;
	void OnCollisionEnter(Collision col)
	{
        //return;
		
		if (!plc.dustu&&isEnabled) {
			
				if (col.transform.GetComponent<Rigidbody> ()&&tekrarForceUygulayabilir) {
                    tekrarForceUygulayabilir = false;
                    Invoke("TekrarForceUygulayabilir", 1f);
                    col.transform.GetComponent<Rigidbody> ().AddForce (
                                -col.contacts [0].normal*100,   //col.transform.GetComponent<Rigidbody>().mass*20f,
                                ForceMode.Impulse
                                );
				}

		}
	}

    private void TekrarForceUygulayabilir()
    {
        tekrarForceUygulayabilir = true;
    }

}
