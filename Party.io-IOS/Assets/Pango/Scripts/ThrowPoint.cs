using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPoint : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
		Invoke ("AddTrhowPoints", 0.5f);
	}
	private void AddTrhowPoints(){
		GameManager_A.gameManager.throwPoints.Add (this);
	}


}
