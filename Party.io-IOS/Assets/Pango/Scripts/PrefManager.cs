using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class  PrefManager {
	public const string rank = "RANK";
	public const string userName = "USERNAME";
	public const string randomScene = "RSC";
	public static void SetRank(int _value){
		if (_value >= 24)
			_value = 24;
		PlayerPrefs.SetInt (rank, _value);
	}

	public static int GetRank(){
		return PlayerPrefs.GetInt (rank);
	}

	public static void SetUserName(string _name){
		PlayerPrefs.SetString (userName, _name);
	}

	public static string GetUserName(){
		return PlayerPrefs.GetString (userName);
	}


	public static void SetIsRandomScene(bool value){
		if (value) {
			PlayerPrefs.SetInt (randomScene, 1);
		} else {
			PlayerPrefs.SetInt (randomScene, 0);
		}
	}

	public static bool GetIsRandomScene(){
		if (PlayerPrefs.GetInt (randomScene) == 1) {
			return true;
		} 
		return false;
	}

}
