using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoFlagNames : MonoBehaviour {

    public string _country;

    public Material _flagMaterial;

	public string[] _names;

	List<string> _namesList = new List<string>();

	List<string> _usedNames = new List<string>();

	void Awake(){


		//_namesList.CopyTo (_names);

		foreach(string s in _names){
			_namesList.Add (s);
		}

//		Debug.Log ("_names " + transform.name + " " + _namesList.Count);

	}

    public string FindName()
    {
		
		if (_namesList.Count == 0) {
			return ("Player" + Random.Range (0000, 5000).ToString());
			//return null;
		}
			
		//Debug.Log ("count" + _namesList.Count + " " + transform.name);

		for(int i = 0; i< _namesList.Count;i++){
			
			int _r = Random.Range (0, _namesList.Count);

			if (!_usedNames.Contains (_namesList [_r])) {
				string _name = _namesList [_r];
				_usedNames.Add (_name);
				_namesList.Remove (_name);
				//Debug.Log ("dönen " + _name);
				return _name;
			}
		}

		//return ("Player" + Random.Range (5000, 9999).ToString());
		return null;
    }

	[ContextMenu("all used")]
	public void UsedAllNames(){
		//büütn isimler kullanıldıysa
		//mecbur eskilere geri döncez

		_namesList.CopyTo (_usedNames.ToArray());
		_usedNames.Clear ();
	}

	[ContextMenu("Used List")]
	void UsedNames(){

		foreach(string s in _usedNames){
			Debug.Log ("Used Names " + transform.name + " " + s);
		}
	}

}
