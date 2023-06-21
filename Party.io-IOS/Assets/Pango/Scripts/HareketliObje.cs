using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HareketliObje : MonoBehaviour {
	public enum HareketTipi{
		BoxEldiveni,Kapan
	};
	public HareketTipi hareketTipi;
	public enum Axis{
		X,Y,Z
	};
	public Axis axis;
	[Range(0,5f)]
	public float speed;
	private Vector3 ilkPos;
	private Quaternion ilkRot;


	private bool tekrarKullanabilir = true;
	void Start(){
		ilkPos = transform.position;
		ilkRot = transform.rotation;
	}

	public void BoxEldiveniFirlat(){
		if(tekrarKullanabilir)
		StartCoroutine (BoxEldiveniFirlayis());
	}

	IEnumerator BoxEldiveniFirlayis(){
		tekrarKullanabilir = false;
		float timer = 0;
		while (timer <= 0.1f) {
			timer += Time.deltaTime;
			if(axis == Axis.X)
			GetComponent<Rigidbody> ().MovePosition (Vector3.Lerp (GetComponent<Rigidbody> ().position,GetComponent<Rigidbody> ().position+transform.right,Time.deltaTime*50f));
			else if(axis == Axis.Y)
				GetComponent<Rigidbody> ().MovePosition (Vector3.Lerp (GetComponent<Rigidbody> ().position,GetComponent<Rigidbody> ().position+transform.up,Time.deltaTime*50f));
			else if(axis == Axis.Z)
				GetComponent<Rigidbody> ().MovePosition (Vector3.Lerp (GetComponent<Rigidbody> ().position,GetComponent<Rigidbody> ().position+transform.forward,Time.deltaTime*50f));
			yield return Time.deltaTime;
		}
		Invoke ("BoxEldiveniCek", 1f);
	}

	private void BoxEldiveniCek(){
		StartCoroutine (BoxEldiveniGeriCekilme());
	}

	IEnumerator BoxEldiveniGeriCekilme(){
		float timer = 0;
		while (timer <= 0.4f) {
			timer += Time.deltaTime;
			GetComponent<Rigidbody> ().MovePosition (Vector3.Lerp (GetComponent<Rigidbody> ().position,ilkPos,Time.deltaTime*50f*speed));
			yield return Time.deltaTime;
		}
		GetComponent<Rigidbody> ().MovePosition (ilkPos);
		tekrarKullanabilir = true;
	}


	public void KapanCalis(){
		if (tekrarKullanabilir)
			StartCoroutine (KapanCalisma ());
	}

	IEnumerator KapanCalisma(){
		tekrarKullanabilir = false;
		if (axis == Axis.X) {
			while (transform.localEulerAngles.x <= ilkRot.eulerAngles.x + 70f) {
				GetComponent<Rigidbody> ().MoveRotation (Quaternion.Euler (transform.localEulerAngles.x + 200f * Time.deltaTime*speed,
					transform.localEulerAngles.y,
					transform.localEulerAngles.z));
				yield return Time.deltaTime;
			}
		}else if (axis == Axis.Y) {
			while (transform.localEulerAngles.y <= ilkRot.eulerAngles.y + 70f) {
				GetComponent<Rigidbody> ().MoveRotation (Quaternion.Euler (transform.localEulerAngles.x,
					transform.localEulerAngles.y + 200f * Time.deltaTime*speed,
					transform.localEulerAngles.z));
				yield return Time.deltaTime;
			}
		}else if (axis == Axis.Z) {
			while (transform.localEulerAngles.z <= ilkRot.eulerAngles.z + 70f) {
				GetComponent<Rigidbody> ().MoveRotation (Quaternion.Euler (transform.localEulerAngles.x,
					transform.localEulerAngles.y,
					transform.localEulerAngles.z + 200f * Time.deltaTime*speed));
				yield return Time.deltaTime;
			}
		}
		Invoke ("KapanReset", 1f);
	}

	private void KapanReset(){
		StartCoroutine (KapanResetle ());

	}

	IEnumerator KapanResetle(){
		if (axis == Axis.X) {
			while (transform.localEulerAngles.x >= ilkRot.eulerAngles.x) {
				GetComponent<Rigidbody> ().MoveRotation (Quaternion.Euler (transform.localEulerAngles.x - 200f * Time.deltaTime*speed,
					transform.localEulerAngles.y,
					transform.localEulerAngles.z));
				
				yield return Time.deltaTime;
			}
		}else if (axis == Axis.Y) {
			while (transform.localEulerAngles.y >= ilkRot.eulerAngles.y) {
				GetComponent<Rigidbody> ().MoveRotation (Quaternion.Euler (transform.localEulerAngles.x,
					transform.localEulerAngles.y - 200f * Time.deltaTime*speed,
					transform.localEulerAngles.z));
				yield return Time.deltaTime;
			}
		}else if (axis == Axis.Z) {
			while (transform.localEulerAngles.z >= ilkRot.eulerAngles.z) {
				GetComponent<Rigidbody> ().MoveRotation (Quaternion.Euler (transform.localEulerAngles.x,
					transform.localEulerAngles.y,
					transform.localEulerAngles.z - 200f * Time.deltaTime*speed));
				Debug.Log ("GeriGeliyot");
				yield return Time.deltaTime;
			}
		}
		GetComponent<Rigidbody> ().MoveRotation (ilkRot);
		tekrarKullanabilir = true;
	}


}
