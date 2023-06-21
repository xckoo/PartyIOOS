using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeManager : MonoBehaviour {
        public GameObject eyes;

	// Use this for initialization
	void Start () {

		if (eyes.gameObject != enabled)
		{
			GameObject currentEye = GameObject.FindGameObjectWithTag("eyes");
			currentEye.SetActive(false);

		}
		else
		{

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
