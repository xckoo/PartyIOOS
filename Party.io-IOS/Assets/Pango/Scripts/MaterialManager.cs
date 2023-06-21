using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour {

    [SerializeField]
    GameObject playerBas;
	// Use this for initialization
	void Start () {
        GetComponent<SkinnedMeshRenderer>().material = playerBas.GetComponent<SkinnedMeshRenderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
