using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YuruyenBandEfekti : MonoBehaviour {
	public float lastZPos;
	public float firstZPos;
	public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild (i).transform.position += transform.forward * Time.deltaTime * speed;
			if (transform.GetChild (i).localPosition.z > lastZPos) {
				transform.GetChild (i).localPosition = new Vector3 (transform.GetChild (i).localPosition.x,
					transform.GetChild (i).localPosition.y, firstZPos);
			}
		}

	}


}
