using UnityEngine;
using System.Collections;

public class vibirationEgemen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (Fire());
		//bekle = false;
		//StartCoroutine (wait());
	}
	IEnumerator Fire()
	{
		yield return new WaitForSeconds (Random.Range (0f,3f));
		bekle = true;
	}
	public bool bekle=false;
	public float zaman;
	// Update is called once per frame
	void Update () 
	{		
		if (bekle)
		{
			zaman += Time.deltaTime;
			if (zaman > 1) 
			{
				zaman = 0f;
				bekle = false;
				StartCoroutine (wait());
			}
			transform.eulerAngles = new Vector3 (0, 0, 15 * Mathf.Sin (zaman * 30f));
		}

	}
	IEnumerator wait()
	{
		
		yield return new WaitForSeconds (Random.Range (1f,5f));
		bekle = true;
		//StartCoroutine (wait());
	}
}
