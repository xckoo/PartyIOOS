using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class Kamera_A : MonoBehaviour {
	public GameObject player;
	public Transform initialCameraPoint;
	public float speed;
	Vector3 offset,offset2;
	private bool shaking = false;
	private bool fovayarliyor = false;
	private bool teleporting = false;
	private bool kameraYaklasiyor = false;
	public GameObject kamera_ilk,kamera_son;
	void Start ()
	{
       // player = player.transform.roo.GetChild(1).gameObject;
		transform.position = initialCameraPoint.position;
		transform.rotation = initialCameraPoint.rotation;
		offset = transform.position - player.transform.position;
		offset2 = transform.position - kamera_ilk.transform.position;
	}
	bool oncet=true;
	void LateUpdate (){
	
		if (!GameManager_A.gameManager.score.transform.parent.gameObject.activeSelf && oncet) {
//			Debug.Log ("baslangicccccccccccccccc");
			transform.position = new Vector3(kamera_ilk.transform.position.x,kamera_ilk.transform.position.y+18,kamera_ilk.transform.position.z-25);
			/*GetComponent<Rigidbody> ().MovePosition (Vector3.Lerp (transform.position,
				new Vector3(kamera_ilk.transform.position.x,kamera_ilk.transform.position.y+10,kamera_ilk.transform.position.z-15),
				Time.unscaledDeltaTime * speed));
			*/
		}

	}

	void FixedUpdate ()
	{
		 if (son && GameManager_A.gameManager.listedPlayers[0]!=null  ){
			transform.position = Vector3.Lerp(transform.position,GameManager_A.gameManager.listedPlayers[0].transform.position + new Vector3 (0, 3f, -7.6f),Time.deltaTime*2);
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler (-3, 0, 0),Time.deltaTime*2);
		}
		
		if (!GameManager_A.gameManager.score.transform.parent.gameObject.activeSelf) {
			
		}

		else if (!shaking&&!teleporting&&!kameraYaklasiyor) {

           
                if (player != null && !player.GetComponent<PlayerController_A>().oldu)
                {
                    oncet = false;

                    if (!player.transform.root.gameObject.activeSelf)
                    {

                        GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(transform.position,
                            kamera_ilk.transform.position + new Vector3(0, 14, -22),
                            Time.unscaledDeltaTime * speed));

                    }
                    else
                    {
                        GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(transform.position,
                            player.GetComponent<Rigidbody>().position + new Vector3(0, 14, -22), //   + offset,
                            Time.unscaledDeltaTime * speed));

                    }
                }
                else
                {
                    if (!GameManager_A.gameManager.gameOver)
                    {
                        GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(transform.position,
                            kamera_son.transform.position + new Vector3(0, 14, -22),
                            Time.unscaledDeltaTime * speed / 5));
                    }


                }

		}
	
	}

	public void KameraTeleport(){
		teleporting = true;
		Invoke ("Teleport", 0.1f);
	}
	private void Teleport(){
		transform.position = transform.position + offset;
		teleporting = false;
	}
	private void TelePorting(){
		
		teleporting = false;
	}
	public void CameraShaker(){
		if(!shaking)
		StartCoroutine (CamShake ());
	}

	IEnumerator CamShake(){
		float timer = 0;
		shaking = true;
        MMVibrationManager.Haptic(HapticTypes.MediumImpact);

		while (timer <= 0.4f) {
			timer += Time.deltaTime;
			float rx = Random.Range(-1f,1f);
			float ry = Random.Range(-1f,1f);
			transform.position = Vector3.Lerp (transform.position,new Vector3(
				transform.position.x+rx,
				transform.position.y+ry,
				transform.position.z
			)
				,Time.deltaTime*20f);
			yield return Time.unscaledDeltaTime*4f;
		}
		shaking = false;
	}

	public void FovEfekt(){
		if (!fovayarliyor) {
			StartCoroutine (FovAyarlayici ());
		}
	}
	IEnumerator FovAyarlayici(){
		float timer = 0;
		float speed = 15f;
		fovayarliyor = true;
		float initialFov = GetComponent<CameraFit> ().verticalFOV;
		while (timer <= 0.5f) {
			timer += Time.deltaTime;
			GetComponent<CameraFit> ().verticalFOV = Mathf.Lerp (GetComponent<CameraFit> ().verticalFOV,70f,Time.deltaTime*speed);
			GetComponent<CameraFit> ().verticalFOV = Mathf.Clamp (GetComponent<CameraFit> ().verticalFOV, 60f, 70f);
			yield return Time.unscaledDeltaTime;
		}
		timer = 0;
		while (timer <= 0.5f) {
			timer += Time.deltaTime;
			GetComponent<CameraFit> ().verticalFOV = Mathf.Lerp (GetComponent<CameraFit> ().verticalFOV,initialFov,Time.deltaTime*speed);
			GetComponent<CameraFit> ().verticalFOV = Mathf.Clamp (GetComponent<CameraFit> ().verticalFOV, 60f, 70f);
			yield return Time.unscaledDeltaTime;
		}
		GetComponent<CameraFit> ().verticalFOV = initialFov;
		fovayarliyor = false;
	}
	public Vector3 temp_pos,temp_pos2;
	bool son=false;
	public void KazanmaKameraYaklastir(){
		kameraYaklasiyor = true;
		son = true;
		//StartCoroutine (KameraYaklas ());




		}

	IEnumerator KameraYaklas(){
		int yno = 0;
		while (yno <= 50) {



			yno++;
			yield return Time.unscaledDeltaTime;
		}
	}
}
