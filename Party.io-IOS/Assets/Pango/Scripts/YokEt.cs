using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YokEt : MonoBehaviour {
	public float yokolmazaman = 0.75f;
	public bool IgnoreTimeScale;
	void OnEnable(){
		if (!IgnoreTimeScale)
			Destroy (gameObject, yokolmazaman);
		else
			StartCoroutine (_YokEt ());
	}

	IEnumerator _YokEt(){
		yield return new WaitForSecondsRealtime (yokolmazaman);
		Destroy (gameObject);
	}
}
