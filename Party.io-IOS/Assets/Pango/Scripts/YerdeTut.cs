using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YerdeTut : MonoBehaviour {

	public LayerMask layermask;

	PlayerController_A plc;
	void OnEnable(){
		//gameObject.SetActive (false);
		plc = transform.root.GetChild(0).GetComponent<PlayerController_A> ();


	}
	private Vector3 hafifyukari = new Vector3 (0, 0.3f, 0);
	void FixedUpdate(){
		RaycastHit hit;
		if (Physics.Raycast (plc.hips.transform.position,	Vector3.down, out hit, 1f, layermask)) {
			//if (hit.transform.gameObject.CompareTag ("harita")) {
				transform.localPosition = new Vector3 (0.263f,0,0);   /// hit.point + hafifyukari;
			//}
            GetComponent<ParticleSystemRenderer>().renderMode = ParticleSystemRenderMode.Mesh;
            GetComponent<ParticleSystem> ().startLifetime = 1.2f;
			transform.GetChild(0).GetComponent<ParticleSystem> ().startLifetime = 1.2f;
            transform.GetChild(0).GetComponent<ParticleSystemRenderer>().renderMode = ParticleSystemRenderMode.Mesh;
        } else {
            GetComponent<ParticleSystemRenderer>().renderMode = ParticleSystemRenderMode.None;
            GetComponent<ParticleSystem> ().startLifetime = 0;
			transform.GetChild(0).GetComponent<ParticleSystem> ().startLifetime = 0;
            transform.GetChild(0).GetComponent<ParticleSystemRenderer>().renderMode = ParticleSystemRenderMode.None;
        }


        transform.rotation = Quaternion.Euler(new Vector3(90,0,0));
        transform.GetChild(0).rotation = Quaternion.Euler(new Vector3(90, 0, 0));
    }
}
