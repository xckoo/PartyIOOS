using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {
	public Transform[] Level;
	public bool ileri=true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (ileri) {
			if (transform.position.z >= Level [0].transform.GetChild (1).position.z + 50) {
				Level [0].transform.position = new Vector3 (Level [0].transform.position.x, Level [0].transform.position.y, Level [1].transform.GetChild (1).position.z);
			}
			if (transform.position.z >= Level [1].transform.GetChild (1).position.z + 50) {
				Level [1].transform.position = new Vector3 (Level [1].transform.position.x, Level [1].transform.position.y, Level [0].transform.GetChild (1).position.z);
			}
		}
		else {
			if (transform.position.z < Level [0].transform.position.z) {
				Level [0].transform.position = new Vector3 (Level [0].transform.position.x, Level [0].transform.position.y, Level [1].transform.GetChild (1).position.z);
			}
			if (transform.position.z < Level [1].transform.position.z) {
				Level [1].transform.position = new Vector3 (Level [1].transform.position.x, Level [1].transform.position.y, Level [0].transform.GetChild (1).position.z);
			}
		
		}
	}
}
