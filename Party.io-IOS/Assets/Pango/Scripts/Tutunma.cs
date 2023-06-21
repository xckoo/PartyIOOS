using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class Tutunma : MonoBehaviour {
	public float force = 4000;
	public bool sagKol;
	Rigidbody rb;
	//[HideInInspector]
	public bool tutundu = false;

	public PlayerController_A pc;

	//[HideInInspector]
	public GameObject tutulanObje;

    private void OnEnable()
    {
        tutundu = false;
    }

    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody> ();	
	}
	
	// Update is called once per frame
	void Update () {
		if (!pc.GetComponent<PlayerController_A> ().duvarda) {
			if (GetComponent<SpringJoint> () || GetComponent<FixedJoint> () || GetComponent<ConfigurableJoint> ()) {
				if (tutulanObje != null || tutulanObje.gameObject.activeSelf) {
					if (tutulanObje.GetComponent<PlayerController_A> ()) {
						if (!tutulanObje.GetComponent<PlayerController_A> ().dustu) {
							GerekliKoluBirak ();
						}
					} else if (tutulanObje.GetComponent<FirlatianObje> ()) {
						if (tutulanObje.GetComponent<FirlatianObje> ().kaldirildi &&
						   tutulanObje.GetComponent<FirlatianObje> ().ilkTutan != pc) {
							GerekliKoluBirak ();
						}
					}
				} else {
					GerekliKoluBirak ();
				} 

				if (!tutundu || pc.dustu) {
					GerekliKoluBirak ();
				}
			}
		}



        //ontriggerstay kodlari

        if(enter)
        {
            Collider col = temp_col;

            if (col.GetComponent<tutunma_trigger>() != null && !pc.GetComponent<PlayerController_A>().duvarda && !sagKol && gameObject.GetComponent<ConfigurableJoint>() == null)
            {
                pc.GetComponent<PlayerController_A>().duvarda = true;
                pc.GetComponent<PlayerController_A>().tutundugu_duvar = col.transform;

                //pc.GetComponent<PlayerController_A> ().sagKol.GetComponent<BoxCollider> ().isTrigger = true;
                //pc.GetComponent<PlayerController_A> ().solKol.GetComponent<BoxCollider> ().isTrigger = true;

                ConfigurableJointEkle_tutunma(col.GetComponent<Rigidbody>());
            }

            if (!sagKol)
                return;
            if (transform.childCount <= 0)
                return;
           /*if (!transform.GetChild(0).GetComponent<KolKontrol>().otherHandin)
            {
                //Debug.Log ("otherHandin-----");
                return;
            }*/

            if (tutundu)
            {

                return;
            }
            if (!col.transform.GetComponent<ObjeTipi>())
            {
                //Debug.Log ("ObjeTipi-----");
                return;
            }

            if (!pc.grab)
            {
                //Debug.Log ("ObjeTipi-----");
                return;
            }

            //if (!pc.isAI) 
            {
                if (col.transform.GetComponent<ObjeTipi>().objetip == ObjeTipi.ObjeTip.Kaldirilabilir)
                {

                    if (!col.transform.GetComponent<PlayerController_A>())
                        return;


                    //if ((col.transform.GetComponent<PlayerController_A> ().dustu) && (!col.transform.GetComponent<PlayerController_A> ().yatiyor))
                    //  return;

                    if (col.transform.GetComponent<PlayerController_A>() != pc && !col.transform.GetComponent<PlayerController_A>().kaldiririyor)
                    {
                        if (!col.transform.GetComponent<PlayerController_A>().dustu
                           && col.transform.GetComponent<PlayerController_A>() == pc.GetComponent<PlayerController_A>().onumdeki
                        //&& col.transform.GetComponent<PlayerController_A> ().arkamdaki == pc.GetComponent<PlayerController_A> ()
                        )
                        {
                            temp_col = col;
                            kafa = col.transform.GetComponent<PlayerController_A>().kafa.GetComponent<Rigidbody>();

                            pc.GetComponent<PlayerController_A>().sagOmuz.GetComponent<ConfigurableJoint>().targetRotation = new Quaternion(-0.5f, -1f, 0.5f, 1f);
                            pc.GetComponent<PlayerController_A>().solOmuz.GetComponent<ConfigurableJoint>().targetRotation = new Quaternion(0.5f, -1f, -0.5f, 1f);

                            col.transform.GetComponent<PlayerController_A>().durdu = true;
                            JointAt(col.transform.GetComponent<Rigidbody>());
                            col.transform.GetComponent<PlayerController_A>().uyanis = 0;
                            col.transform.GetComponent<PlayerController_A>().temp_uyanis = 2000;

                            col.transform.GetComponent<PlayerController_A>().yerde = false;
                            col.transform.GetComponent<PlayerController_A>().once_yer = true;



                            col.transform.GetComponent<PlayerController_A>().Dusus();
                        }
                        else if ((col.transform.GetComponent<PlayerController_A>().dustu)
                            && (col.transform.GetComponent<PlayerController_A>().yerde
                            && col.transform.GetComponent<PlayerController_A>() == pc.GetComponent<PlayerController_A>().yerdeki)
                            && col.transform.GetComponent<PlayerController_A>().yatiyor
                        )
                        {
                            
                            temp_col = col;
                            col.transform.GetComponent<PlayerController_A>().durdu = true;
                            kafa = col.transform.GetChild(0).GetComponent<Rigidbody>();//   col.transform.GetComponent<PlayerController_A> ().kafa.GetComponent<Rigidbody>();
                            JointAt(col.transform.GetComponent<Rigidbody>());

                            col.transform.GetComponent<PlayerController_A>().sagKol.GetComponent<BoxCollider>().isTrigger = true;
                            col.transform.GetComponent<PlayerController_A>().solKol.GetComponent<BoxCollider>().isTrigger = true;


                            col.transform.GetComponent<PlayerController_A>().uyanis = 0;
                            col.transform.GetComponent<PlayerController_A>().temp_uyanis = 2000;

                            col.transform.GetComponent<PlayerController_A>().yerde = false;
                            col.transform.GetComponent<PlayerController_A>().once_yer = true;

                            col.transform.GetComponent<PlayerController_A>().Dusus();

                        }


                    }




                }
                else if (col.transform.GetComponent<ObjeTipi>().objetip == ObjeTipi.ObjeTip.TekFirlatma)
                {
                    JointAt(col.transform.GetComponent<Rigidbody>());
                    TutulanObjeKaldir(col.transform);
                }
            }

        }


    }
   public  bool enter = false;
	Collider temp_col;
    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<PlayerController_A>() || col.GetComponent<tutunma_trigger>())
        {
            temp_col = col;
            enter = true;
        }



    }

    void OnTriggerExit(Collider col)
    {
        if (col.GetComponent<PlayerController_A>() || col.GetComponent<tutunma_trigger>())
        {
            enter = false;
        }
    }
    /*void OnTriggerStay(Collider col){
		

		if (col.GetComponent<tutunma_trigger> () != null && !pc.GetComponent<PlayerController_A> ().duvarda && !sagKol && gameObject.GetComponent<ConfigurableJoint>()==null) {
			pc.GetComponent<PlayerController_A> ().duvarda = true;
			pc.GetComponent<PlayerController_A> ().tutundugu_duvar = col.transform;

			//pc.GetComponent<PlayerController_A> ().sagKol.GetComponent<BoxCollider> ().isTrigger = true;
			//pc.GetComponent<PlayerController_A> ().solKol.GetComponent<BoxCollider> ().isTrigger = true;
			
			ConfigurableJointEkle_tutunma(col.GetComponent<Rigidbody>());
		}
       
        if (!sagKol)
			return;
		if (transform.childCount <= 0)
			return;
		if (!transform.GetChild (0).GetComponent<KolKontrol> ().otherHandin) {
			//Debug.Log ("otherHandin-----");
			return;
		}
			
		if (tutundu) {
			
			return;
		}
		if (!col.transform.GetComponent<ObjeTipi> ()) {
			//Debug.Log ("ObjeTipi-----");
			return;
		}

		if (!pc.grab) {
			//Debug.Log ("ObjeTipi-----");
			return;
		}

        //if (!pc.isAI) 
        {
			if (col.transform.GetComponent<ObjeTipi> ().objetip == ObjeTipi.ObjeTip.Kaldirilabilir) {
			
				if (!col.transform.GetComponent<PlayerController_A> ())
					return;


				//if ((col.transform.GetComponent<PlayerController_A> ().dustu) && (!col.transform.GetComponent<PlayerController_A> ().yatiyor))
				//	return;

				if (col.transform.GetComponent<PlayerController_A> () != pc && !col.transform.GetComponent<PlayerController_A> ().kaldiririyor) {
					if (!col.transform.GetComponent<PlayerController_A> ().dustu
						&& col.transform.GetComponent<PlayerController_A> () == pc.GetComponent<PlayerController_A> ().onumdeki
						//&& col.transform.GetComponent<PlayerController_A> ().arkamdaki == pc.GetComponent<PlayerController_A> ()
					) 
					{
						Debug.Log ("omuzrot" + col.transform.eulerAngles.y);
						temp_col = col;
						kafa =   col.transform.GetComponent<PlayerController_A> ().kafa.GetComponent<Rigidbody>();

						pc.GetComponent<PlayerController_A> ().sagOmuz.GetComponent<ConfigurableJoint> ().targetRotation = new Quaternion (-0.5f, -1f, 0.5f, 1f);
						pc.GetComponent<PlayerController_A> ().solOmuz.GetComponent<ConfigurableJoint> ().targetRotation =  new Quaternion (0.5f, -1f, -0.5f, 1f);

						col.transform.GetComponent<PlayerController_A> ().durdu = true;
						JointAt (col.transform.GetComponent<Rigidbody> ());
						col.transform.GetComponent<PlayerController_A> ().uyanis = 0;
						col.transform.GetComponent<PlayerController_A> ().temp_uyanis = 2000;

						col.transform.GetComponent<PlayerController_A> ().yerde = false;
						col.transform.GetComponent<PlayerController_A> ().once_yer = true;



						col.transform.GetComponent<PlayerController_A> ().Dusus ();
					} 
					else if ((col.transform.GetComponent<PlayerController_A> ().dustu) 
						&& (col.transform.GetComponent<PlayerController_A> ().yerde 
						&& col.transform.GetComponent<PlayerController_A> () == pc.GetComponent<PlayerController_A> ().yerdeki)
						&& col.transform.GetComponent<PlayerController_A> ().yatiyor
					) 
					{
						Debug.Log ("yerden_kaldirdi");
						Debug.Log ("col.transform.eulerAngles.y" + col.transform.eulerAngles.y);
						temp_col = col;
						col.transform.GetComponent<PlayerController_A> ().durdu = true;
						kafa = col.transform.GetChild (0).GetComponent<Rigidbody> ();//   col.transform.GetComponent<PlayerController_A> ().kafa.GetComponent<Rigidbody>();
						JointAt (col.transform.GetComponent<Rigidbody> ());

						col.transform.GetComponent<PlayerController_A> ().sagKol.GetComponent<BoxCollider> ().isTrigger = true;
						col.transform.GetComponent<PlayerController_A> ().solKol.GetComponent<BoxCollider> ().isTrigger = true;


						col.transform.GetComponent<PlayerController_A> ().uyanis = 0;
						col.transform.GetComponent<PlayerController_A> ().temp_uyanis = 2000;

						col.transform.GetComponent<PlayerController_A> ().yerde = false;
						col.transform.GetComponent<PlayerController_A> ().once_yer = true;

						col.transform.GetComponent<PlayerController_A> ().Dusus ();

					}


				}




			} else if (col.transform.GetComponent<ObjeTipi> ().objetip == ObjeTipi.ObjeTip.TekFirlatma) {
				JointAt (col.transform.GetComponent<Rigidbody> ());
				TutulanObjeKaldir (col.transform);
			}
		}
	}*/

    public Rigidbody kafa;
	public List<Transform> trnsform = new List<Transform> (); 
	public int layrr;
	private void JointAt(Rigidbody other_rb){
		
		if (other_rb.GetComponent<ObjeTipi> ().objetip == ObjeTipi.ObjeTip.Kaldirilabilir) {
			if (other_rb.GetComponent<ObjeTipi> ().GetSuanBeniTutan () != null) {
				if (other_rb.GetComponent<ObjeTipi> ().GetSuanBeniTutan () != pc) {
					return;
				}
			}

			if (other_rb.transform.root.GetChild(0).GetComponent<PlayerController_A> ().yatiyor) 
			{
				//Debug.Log ("Egildii-adam");
				pc.egil ();
				TutulanOyuncuKaldir2 (other_rb.transform);
				temp_rb = other_rb;
				tutundu = true;
				Invoke ("JointAt_kontrol", 0.3f);

			} 
			else 
			{	
				
				pc.egil ();
				//pc.dur ();
			//	Debug.Log ("normal_kaldirdi");
				TutulanOyuncuKaldir (other_rb.transform);

				// hepsinde olmali
				tutundu = true;
				tutulanObje = other_rb.gameObject;
				other_rb.constraints = RigidbodyConstraints.None;

				ConfigurableJointEkle(kafa.transform.root.GetChild(0).GetComponent<Rigidbody>());

				if (other_rb.GetComponent<ObjeTipi> ().objetip != ObjeTipi.ObjeTip.TekFirlatma) {
					if (sagKol && !pc.solKolTutucu.GetComponent<Tutunma> ().tutundu) {
						//pc.solKolTutucu.GetComponent<Tutunma> ().JointAt2 (kafa);
						pc.solKolTutucu.GetComponent<Tutunma> ().JointAt2 (kafa);

					}
					if(!pc.isAI)
                        MMVibrationManager.Haptic(HapticTypes.MediumImpact);



//					Debug.Log ("tuttum");
					Transform[] allChildren = other_rb.transform.root.GetComponentsInChildren<Transform>();
					layrr = other_rb.gameObject.layer;
					for (int i = 0; i < allChildren.Length; i++) {
						if (allChildren [i].gameObject.name != "KolChecker") {
							allChildren [i].gameObject.layer = 20; //transform.parent.gameObject.layer;
							trnsform.Add (allChildren [i].transform);
						}
						else {
							Debug.Log ("nameeee" + allChildren [i].gameObject.name);
						}
					}
				}
			}

		} else if (other_rb.GetComponent<ObjeTipi> ().objetip == ObjeTipi.ObjeTip.TekFirlatma) {
			TutulanObjeKaldir (other_rb.transform);
		}

	}

	private void JointAt2(Rigidbody other_rb){

		if (other_rb.GetComponent<ObjeTipi> ().objetip == ObjeTipi.ObjeTip.Kaldirilabilir) {
			if (other_rb.GetComponent<ObjeTipi> ().GetSuanBeniTutan () != null) {
				if (other_rb.GetComponent<ObjeTipi> ().GetSuanBeniTutan () != pc) {
					return;
				}
			}
			//TutulanOyuncuKaldir (other_rb.transform);
		} else if (other_rb.GetComponent<ObjeTipi> ().objetip == ObjeTipi.ObjeTip.TekFirlatma) {
			TutulanObjeKaldir (other_rb.transform);
		}
		tutundu = true;
		tutulanObje = other_rb.gameObject;
		other_rb.constraints = RigidbodyConstraints.None;

		//SpringJointEkle (other_rb);
		ConfigurableJointEkle(other_rb);

	}

	private void JointAt_kontrol(){
		Rigidbody other_rb = temp_rb;


		// hepsinde olmali
		tutundu = true;
		tutulanObje = other_rb.gameObject;
		other_rb.constraints = RigidbodyConstraints.None;

		ConfigurableJointEkle(kafa);

		if (other_rb.GetComponent<ObjeTipi> ().objetip != ObjeTipi.ObjeTip.TekFirlatma) {
			if (sagKol && !pc.solKolTutucu.GetComponent<Tutunma> ().tutundu) {
				//pc.solKolTutucu.GetComponent<Tutunma> ().JointAt2 (kafa);
				pc.solKolTutucu.GetComponent<Tutunma> ().JointAt2 (kafa);

			}
			if(!pc.isAI)
                MMVibrationManager.Haptic(HapticTypes.Warning);



			Debug.Log ("tuttum");
			Transform[] allChildren = other_rb.transform.root.GetComponentsInChildren<Transform>();
			layrr = other_rb.gameObject.layer;
			for (int i = 0; i < allChildren.Length; i++) {
				if (allChildren [i].gameObject.name != "KolChecker") {
					allChildren [i].gameObject.layer = 20; //transform.parent.gameObject.layer;
					trnsform.Add (allChildren [i].transform);
				}
				else {
					Debug.Log ("nameeee" + allChildren [i].gameObject.name);
				}
			}
		}

	}

	private void TutulanOyuncuKaldir(Transform tutulan){
//		Debug.Log ("TutulanOyuncuKaldir");
		if (tutulan.transform.GetComponent<ObjeTipi> ().objetip == ObjeTipi.ObjeTip.Kaldirilabilir) {
			Invoke ("Kaldir", 0.25f);
			 tutlanPlayer = tutulan.transform.root.GetChild (0).GetComponent<PlayerController_A> ();
			// Tutan kisi tutulan kisiye onu enson tutanın bu oldugunu soyler
			tutlanPlayer.enSonTutan = pc;
			tutlanPlayer.suanBeniTutan = pc;
		}
	}
	PlayerController_A tutlanPlayer;
	private void TutulanOyuncuKaldir2(Transform tutulan){
		Debug.Log ("TutulanOyuncuKaldir2");
		if (tutulan.transform.GetComponent<ObjeTipi> ().objetip == ObjeTipi.ObjeTip.Kaldirilabilir) {
			Invoke ("Kaldir_yatiyor", 0.8f);
			Invoke ("karsi_hareket", 1.2f);//Kaldirdiktan sonra karsıdakinin bizimle birlikte hareketi
			tutlanPlayer = tutulan.transform.root.GetChild (0).GetComponent<PlayerController_A> ();

			// Tutan kisi tutulan kisiye onu enson tutanın bu oldugunu soyler
			tutlanPlayer.enSonTutan = pc;
			tutlanPlayer.suanBeniTutan = pc;
		}
	}

	void karsi_hareket(){
		tutlanPlayer.yatiyor = false;
	}

	private void TutulanObjeKaldir(Transform tutulan){
		
		if (tutulan.GetComponent<FirlatianObje> ().tutuldu)
		if (tutulan.GetComponent<FirlatianObje> ().ilkTutan != pc)
			return;
		if (tutulan.GetComponent<FirlatianObje> ().kaldirildi)
			return;
		
		if (sagKol) {
			//if (pc.solKolTutucu.GetComponent<Tutunma>().tutundu) {
				Debug.Log ("Simdi Firlatabilir");

				Invoke ("Kaldir", 0.1f);
				Invoke ("TutulanObjeFirlat", 0.1f);

			//}
		} else {
			if (pc.sagKolTutucu.GetComponent<Tutunma>().tutundu) {
				Debug.Log ("Simdi Firlatabilir2");
				Invoke ("Kaldir", 0.1f);
				Invoke ("TutulanObjeFirlat", 0.1f);
			}
		}
	}

	private void Kaldir(){
		tutlanPlayer.transform.root.GetChild (0).GetComponent<PlayerController_A> ().durdu = false;
//			Debug.Log ("kaldirrrr111");
			pc.Kaldir ();

	}
	private void Kaldir_yatiyor(){
			Debug.Log ("kaldirrrr222");
			pc.Kaldir_yatiyor ();

	}
	private void TutulanObjeFirlat(){

		if (pc.kollarhavada) {
			pc.DisardanFirlat ();
			Debug.Log ("Simdi Firlatabilir");
		} else {
			Invoke ("TutulanObjeFirlat", 0.1f);
		}

	}
		
	void OnJointBreak(){
		tutundu = false;
		GerekliKoluBirak ();
	}

	private void GerekliKoluBirak(){
		pc.KollariSerbestBirak ();
	}


	#region EklenecekJointlerSecim
	// Kullanilmiyor
	private void SpringJointEkle(Rigidbody other_rb){
		SpringJoint sp = gameObject.AddComponent<SpringJoint> ();
		sp.connectedBody = other_rb;
		sp.autoConfigureConnectedAnchor = false;
		sp.anchor = Vector3.zero;
		sp.spring = 9999999;
		sp.damper = 9999999;
		sp.breakForce = force;
		sp.enablePreprocessing = false;
		sp.tolerance = 0;
	}

	// Kullanilmiyor
	private void FixedJointEkle(Rigidbody other_rb){
		FixedJoint fp = gameObject.AddComponent<FixedJoint> ();
		fp.connectedBody = other_rb;
		fp.autoConfigureConnectedAnchor = false;
		fp.anchor = Vector3.zero;
		fp.breakForce = force;
		fp.enablePreprocessing = false;
	}

	// Suan Kullanilan
	private void ConfigurableJointEkle(Rigidbody other_rb){
		ConfigurableJoint cp = gameObject.AddComponent<ConfigurableJoint> ();
		cp.connectedBody = other_rb;
		cp.autoConfigureConnectedAnchor = false;
		cp.anchor = Vector3.zero;
		cp.breakForce = Mathf.Infinity;
		cp.xMotion = ConfigurableJointMotion.Limited;
		cp.yMotion = ConfigurableJointMotion.Limited;
		cp.zMotion = ConfigurableJointMotion.Limited;
		cp.targetRotation = new Quaternion (cp.targetRotation.x, -0.82f, cp.targetRotation.z,1);
		SoftJointLimitSpring spr = new SoftJointLimitSpring ();
		spr.spring = 1;
		cp.linearLimitSpring = spr;

		JointDrive drv = new JointDrive ();
		drv.positionSpring = 1;
		//drv.maximumForce = 100;
		//drv.positionDamper = 10;
		cp.angularXDrive = drv;
		cp.angularYZDrive = drv;
	   	cp.connectedAnchor = new Vector3 (0, 0, 0);



		cp.enablePreprocessing = false;
	}
	Rigidbody temp_rb;
	// Suan Kullanilan
	private void ConfigurableJointEkle_tutunma(Rigidbody other_rb){
		ConfigurableJoint cp = gameObject.AddComponent<ConfigurableJoint> ();
		cp.connectedBody = other_rb;
		cp.autoConfigureConnectedAnchor = false;
		cp.anchor = Vector3.zero;
		cp.breakForce = Mathf.Infinity;
		cp.xMotion = ConfigurableJointMotion.Limited;
		cp.yMotion = ConfigurableJointMotion.Limited;
		cp.zMotion = ConfigurableJointMotion.Limited;
		//cp.targetRotation = new Quaternion (cp.targetRotation.x, -0.82f, cp.targetRotation.z,1);
		SoftJointLimitSpring spr = new SoftJointLimitSpring ();
		spr.spring = 1;
		cp.linearLimitSpring = spr;

		JointDrive drv = new JointDrive ();
		drv.positionSpring = 1;
		//drv.maximumForce = 100;
		//drv.positionDamper = 10;
		cp.angularXDrive = drv;
		cp.angularYZDrive = drv;
		cp.connectedAnchor = new Vector3 (0, 0, 0);


		cp.swapBodies = true;
		cp.enablePreprocessing = true;

		temp_rb = other_rb;
		//Invoke ("at2", 2);
		ConfigurableJointEkle_sagkol(other_rb);
	}
	void at2(){
		ConfigurableJointEkle_sagkol(temp_rb);
	}
	private void ConfigurableJointEkle_sagkol(Rigidbody other_rb){
		transform.root.GetChild (0).GetComponent<PlayerController_A> ().sagKol.transform.GetChild(1).GetChild (0).gameObject.SetActive (false);
		transform.root.GetChild (0).GetComponent<CapsuleCollider> ().isTrigger = true;
		transform.root.GetChild (0).GetComponent<BoxCollider> ().isTrigger = true;
		transform.root.GetChild (0).GetComponent<PlayerController_A> ().kafa.GetComponent<BoxCollider> ().isTrigger = true;

		ConfigurableJoint cp = transform.root.GetChild(0).GetComponent<PlayerController_A>().sagKol.transform.GetChild(1).gameObject.AddComponent<ConfigurableJoint> ();
		cp.connectedBody = other_rb.transform.GetChild(0).GetComponent<Rigidbody>();
		cp.autoConfigureConnectedAnchor = false;
		cp.anchor = Vector3.zero;
		cp.breakForce = Mathf.Infinity;
		cp.xMotion = ConfigurableJointMotion.Limited;
		cp.yMotion = ConfigurableJointMotion.Limited;
		cp.zMotion = ConfigurableJointMotion.Limited;
		//cp.targetRotation = new Quaternion (cp.targetRotation.x, -0.82f, cp.targetRotation.z,1);
		SoftJointLimitSpring spr = new SoftJointLimitSpring ();
		spr.spring = 1;
		cp.linearLimitSpring = spr;

		JointDrive drv = new JointDrive ();
		drv.positionSpring = 1;
		//drv.maximumForce = 100;
		//drv.positionDamper = 10;
		cp.angularXDrive = drv;
		cp.angularYZDrive = drv;
		cp.connectedAnchor = new Vector3 (0, 0, 0);


		//cp.swapBodies = true;
		//cp.enablePreprocessing = true;



		JointDrive spr2 = new JointDrive ();
		spr2.positionSpring =0;
		spr2.positionDamper = 0;
		spr2.maximumForce = Mathf.Infinity;
		transform.root.GetChild(0).GetComponent<PlayerController_A>().sagOmuz.GetComponent<ConfigurableJoint> ().angularXDrive=spr2;
		transform.root.GetChild(0).GetComponent<PlayerController_A>().sagOmuz.GetComponent<ConfigurableJoint> ().angularYZDrive=spr2;
		transform.root.GetChild(0).GetComponent<PlayerController_A>().solOmuz.GetComponent<ConfigurableJoint> ().angularXDrive=spr2;
		transform.root.GetChild(0).GetComponent<PlayerController_A>().solOmuz.GetComponent<ConfigurableJoint> ().angularYZDrive=spr2;



	//	pc.GetComponent<PlayerController_A> ().Dusus ();
	}



	#endregion
}
