using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjeTipi : MonoBehaviour {
	public enum ObjeTip{
		Tutunulabilir,Kaldirilabilir,Vurulabilir,TekFirlatma
	};
	public ObjeTip objetip;

	[HideInInspector]
	public PlayerController_A my_PlayerController;

	void OnEnable(){
		if (objetip == ObjeTip.Kaldirilabilir) {
			my_PlayerController = transform.root.GetChild (0).GetComponent<PlayerController_A> ();
		}
	}


	public void SetSuanBeniTutan(PlayerController_A tutanKisi){
		if (objetip == ObjeTip.Kaldirilabilir) {
			my_PlayerController.suanBeniTutan = tutanKisi;
		}
	}

	public PlayerController_A GetSuanBeniTutan(){
		if (objetip == ObjeTip.Kaldirilabilir) {
			return my_PlayerController.suanBeniTutan;
		}

		return null;
	}
}
