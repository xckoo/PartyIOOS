using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InfoManager : MonoBehaviour {

	/*french Austin
	brazil Bernardino
	italy Edgardo
	chinese Syaoran
	german Bastein
	turkey ahmet
	english george
	spanish Lukas
	finnish Kalle*/

	public List<int> _chose;

	public Transform _myInfoTransform;

	//public Material[] _flagMaterials;
	//public string[] _names;

	public Transform[] _infoTransforms;

	void Awake(){

		_chose = new List<int> ();
		for(int i = 0; i < transform.childCount; i++){
			_chose.Add (i);
		}

        SetInfos();
        SetMyInfo();

    }


	public void SetInfos(){

		for(int i = 0; i < _infoTransforms.Length; i++){
			
			int a = Random.Range(0, _chose.Count);

			a = _chose [a];

			a = i;

			string _name = transform.GetChild (a).GetComponent<InfoFlagNames> ().FindName ();

			if (!string.IsNullOrEmpty(_name)) {
				
				_infoTransforms [i].transform.Find ("flag").GetComponent<MeshRenderer> ().material = transform.GetChild (a).GetComponent<InfoFlagNames> ()._flagMaterial;
				_infoTransforms [i].transform.Find ("flag").GetComponent<MeshRenderer> ().material.color = new Color (1f, 1f, 1f, 1f);

				_infoTransforms [i].transform.Find ("nick").GetComponent<TextMesh> ().text = _name;
				_infoTransforms [i].transform.Find ("nick").GetComponent<TextMesh> ().color = new Color (1f, 1f, 1f, 1f);

				_chose.Remove (a);

			} else {
				_infoTransforms [i].transform.Find ("flag").GetComponent<MeshRenderer> ().material = transform.GetChild (a).GetComponent<InfoFlagNames> ()._flagMaterial;
				_infoTransforms [i].transform.Find ("flag").GetComponent<MeshRenderer> ().material.color = new Color (1f, 1f, 1f, 1f);

				_infoTransforms [i].transform.Find ("nick").GetComponent<TextMesh> ().text = "Player " + Random.Range(0000, 9999);
				_infoTransforms [i].transform.Find ("nick").GetComponent<TextMesh> ().color = new Color (1f, 1f, 1f, 1f);
			}
		}


        /*int t = 0;
        while (t < _infoTransforms.Length)
        {
			int a = Random.Range(0, _chose.Count);

			a = _chose [a];

			string _name = transform.GetChild (a).GetComponent<InfoFlagNames> ().FindName ();

			if (!string.IsNullOrEmpty(_name)) {
				t++;
				_chose.Remove (a);

				_infoTransforms [t].transform.Find ("flag").GetComponent<MeshRenderer> ().material = transform.GetChild (a).GetComponent<InfoFlagNames> ()._flagMaterial;
				_infoTransforms [t].transform.Find ("flag").GetComponent<MeshRenderer> ().material.color = new Color (1f, 1f, 1f, 0.5f);

				_infoTransforms [t].transform.Find ("nick").GetComponent<TextMesh> ().text = _name;
				_infoTransforms [t].transform.Find ("nick").GetComponent<TextMesh> ().color = new Color (1f, 1f, 1f, 0.5f);

			} else {
				
				Debug.Log ("Hata");
			}
			// chose count 0 ise restle tekrdan isim atsın
			// bu kısım uzunca süre lazım olmıcak amaa çalışma ihitmali var
			//isimler biterse 
			//düzeltilecek
        }*/


	}

	public void SetMyInfo(){

		string lang = Application.systemLanguage.ToString ();
		lang = "French";

        //burası düzeltilcek
		_myInfoTransform.transform.Find("flag").GetComponent<MeshRenderer>().material = transform.Find(lang).GetComponent<InfoFlagNames>()._flagMaterial;
		_myInfoTransform.transform.Find ("flag").GetComponent<MeshRenderer> ().material.color = new Color (1f, 1f, 1f, 1f);
        _myInfoTransform.transform.Find("nick").GetComponent<TextMesh>().text = PlayerPrefs.GetString("username");
		_myInfoTransform.transform.Find ("nick").GetComponent<TextMesh> ().color = new Color (1f, 1f, 1f, 1f);
	}
}
