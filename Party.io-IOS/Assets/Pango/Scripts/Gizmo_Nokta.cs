using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo_Nokta : MonoBehaviour {

	public Color color;
	void Start () {
		
	}
	
	void OnDrawGizmos(){
		Gizmos.color =color;
		Gizmos.DrawSphere (transform.position, 1f);

	}
}
