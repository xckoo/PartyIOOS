using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

	void OnEnable(){
		Invoke ("SpawnPointGonder", 0.1f);
	}
	private void SpawnPointGonder(){
		GameManager_A.gameManager.spawnPoints.Add (this);
	}
}
