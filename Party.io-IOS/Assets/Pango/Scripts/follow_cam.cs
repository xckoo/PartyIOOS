using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_cam : MonoBehaviour {
    public Transform player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Camera.main.transform.LookAt(player);

    }
}
