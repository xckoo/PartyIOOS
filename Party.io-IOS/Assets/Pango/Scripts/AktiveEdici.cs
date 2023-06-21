using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AktiveEdici : MonoBehaviour {

	public HareketliObje ho;


	void OnTriggerEnter(){
		if (ho.hareketTipi == HareketliObje.HareketTipi.BoxEldiveni) {
			ho.BoxEldiveniFirlat ();
		} else if (ho.hareketTipi == HareketliObje.HareketTipi.Kapan) {
			ho.KapanCalis ();
		}

	}


}
