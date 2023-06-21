using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BayiltmaBolgesi : MonoBehaviour {
	public bool kafa;
	public PlayerController_A plc;

	private float aivurusmuliplier;

	void OnCollisionEnter(Collision col)
	{
		//Instantiate (GameManager_A.gameManager.hitParticle, col.contacts [0].point, Quaternion.identity);
		/*
		//Rank Degerine gore yapay zeka vurus guclerini artırma
		if (GameManager_A.gameManager)
			aivurusmuliplier = GameManager_A.gameManager.oyunDegerler.AiVurusMultipliers [PrefManager.GetRank ()];
		else
			aivurusmuliplier = 1f;
		if (!plc.dustu && !plc.kaldiririyor && plc.tekmeAtabilir) {
			if (col.relativeVelocity.magnitude >= 3f) {
				if (kafa) {
					if (plc.kafaAtiyor)
						return;
					if (plc.isAI) {
						float r = Random.value;
						if (r >= 0.5f && r <= 0.8f) {
							if (!plc.kaldiririyor) {
								if (col.transform.GetComponent<ObjeTipi> ())
								  if (col.transform.GetComponent<ObjeTipi> ().objetip == ObjeTipi.ObjeTip.Kaldirilabilir) {
									if (!plc.GetComponent<AIController> ().isSelected) {
										plc.transform.GetComponent<AIController> ().currentTarget = col.transform;
									}
								
								}

							}
						}
					}
					if (col.transform.GetComponent<ObjeTipi> ()) {
						if (col.transform.GetComponent<ObjeTipi> ().objetip == ObjeTipi.ObjeTip.Kaldirilabilir) {
							Instantiate (GameManager_A.gameManager.hitParticle, col.contacts [0].point, Quaternion.identity);
							//TapticManager.Impact (ImpactFeedback.Medium);
						}
					} else if (col.gameObject.CompareTag ("hareketlivuran")) {
						Instantiate (GameManager_A.gameManager.buyukHitParticle, col.contacts [0].point + Vector3.up * 3f, Quaternion.identity);
						if (!plc.isAI)
							Camera.main.GetComponent<Kamera_A> ().CameraShaker ();
					}


					if (plc.isAI) {


						if (col.transform.root.childCount > 0) {
							if (col.transform.root.GetChild (0).GetComponent<PlayerController_A> ()) {
								if(!col.transform.root.GetChild (0).GetComponent<PlayerController_A> ().dustu
									&&	!col.transform.root.GetChild (0).GetComponent<PlayerController_A> ().yatiyor)
								{
									if (col.transform.root.GetChild (0).GetComponent<PlayerController_A> ().kafa.transform == col.transform) 
									{
										if (col.transform.root.GetChild (0).GetComponent<PlayerController_A> ().kafaAtiyor) 
										{
											
												Debug.Log ("Kafa vurdu");
												plc.bayilmaHealth -= 600;

										}
									} 
									else if (col.transform.root.GetChild (0).GetComponent<PlayerController_A> ().solKol.transform == col.transform
									           || col.transform.root.GetChild (0).GetComponent<PlayerController_A> ().sagKol.transform == col.transform) {
										 {
											{
												plc.bayilmaHealth -= 300;
												Debug.Log ("kolll vurdu" + transform.root + "   " + gameObject);

												if (col.transform.GetComponent<Tutunma> () != null) {

													if (col.transform.GetComponent<Tutunma> ().sagKol) {
														Debug.Log ("Sagkolll vurdu");
													} else {
														Debug.Log ("Solkolll vurdu");
													}
												}
											}
										}
									}
							 	}
							}
						}	
					} else {
						//bana vuruyorlar

						if (col.transform.root.childCount > 0) {
							if (col.transform.root.GetChild (0).GetComponent<AIController> ()) {
								if (!col.transform.root.GetChild (0).GetComponent<PlayerController_A> ().dustu
								    && !col.transform.root.GetChild (0).GetComponent<PlayerController_A> ().yatiyor) {
									//yapay zekalar birbirine vuruyor
									if (col.transform.root.GetChild (0).GetComponent<PlayerController_A> ().kafa.transform == col.transform) {
										if (col.transform.root.GetChild (0).GetComponent<PlayerController_A> ().kafaAtiyor) {
											Debug.Log ("Kafa vurdu");
											plc.bayilmaHealth -= 100;
										}
									} else if (col.transform.root.GetChild (0).GetComponent<PlayerController_A> ().solKol.transform == col.transform
									          || col.transform.root.GetChild (0).GetComponent<PlayerController_A> ().sagKol.transform == col.transform) {
										if (col.transform.root.GetChild (0).GetComponent<PlayerController_A> ().yumrukAtiyor) {
											{
												plc.bayilmaHealth -= 50;
												Debug.Log ("kolll vurdu" + transform.root + "   " + gameObject);
												if (col.transform.GetComponent<Tutunma> () != null) {

													if (col.transform.GetComponent<Tutunma> ().sagKol) {
														Debug.Log ("Sagkolll vurdu");
													} else {
														Debug.Log ("Solkolll vurdu");
													}
												}
											}
										}
									}

								}
							} else {
								
								if (col.transform.root.GetChild (0).GetComponent<PlayerController_A> ()) 
								{
									if (!col.transform.root.GetChild (0).GetComponent<PlayerController_A> ().dustu
									    &&	!col.transform.root.GetChild (0).GetComponent<PlayerController_A> ().yatiyor) 
									{
										if (col.transform.root.GetChild (0).GetComponent<PlayerController_A> ().kafa.transform == col.transform) {
											if (col.transform.root.GetChild (0).GetComponent<PlayerController_A> ().kafaAtiyor) {
												Debug.Log ("Kafa vurdu");
												TapticManager.Impact (ImpactFeedback.Medium);
												plc.bayilmaHealth -= 100;
											}
										} else if (col.transform.root.GetChild (0).GetComponent<PlayerController_A> ().solKol.transform == col.transform
										           || col.transform.root.GetChild (0).GetComponent<PlayerController_A> ().sagKol.transform == col.transform) {
											if (col.transform.root.GetChild (0).GetComponent<PlayerController_A> ().yumrukAtiyor) {
												{
													plc.bayilmaHealth -= 100;
													TapticManager.Impact (ImpactFeedback.Medium);
													Debug.Log ("kolll vurdu" + transform.root + "   " + gameObject);
													if (col.transform.GetComponent<Tutunma> () != null) {

														if (col.transform.GetComponent<Tutunma> ().sagKol) {
															Debug.Log ("Sagkolll vurdu");
														} else {
															Debug.Log ("Solkolll vurdu");
														}
													}
												}
											}
										}

									}
								}
							}
						} 
					}

					if (plc.bayilmaHealth <= 0) {
						if (col.transform.root.GetChild (0).GetComponent<AIController> ()) {
							col.transform.root.GetChild (0).GetComponent<AIController> ().currentTarget = plc.transform;
						}
						plc.Dusus_yumruk ();

						if (!plc.isAI) {
							Camera.main.GetComponent<Kamera_A> ().CameraShaker ();
						}
					}

				}
			} else if (plc.dustu) {
				if (col.relativeVelocity.magnitude >= 1f && col.gameObject.CompareTag ("harita")) {
					Instantiate (GameManager_A.gameManager.tozParticle, transform.position, Quaternion.identity);
				}
			}
		}  
		*/
	}

	void vurma(){


	}
	void Update(){
		if (Input.GetKeyDown (KeyCode.Z)) {
			plc.Dusus_yumruk ();
		}
	}


}
