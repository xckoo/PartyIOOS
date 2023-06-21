using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kapatma : MonoBehaviour {
    [Range(0, 10)]
    public float speed;

	// Use this for initialization
	void OnEnable () {
        StartCoroutine(Close());
	}
	
    IEnumerator Close() {

        yield return new WaitForSecondsRealtime(speed);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
