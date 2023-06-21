using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reklam_hollow : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("Screen height: "+ Screen.height);
        Debug.Log("Screen width : "+ Screen.width);
        Debug.Log("Screen : " + (Screen.height*1f / Screen.width*1f));
	    if((Screen.height*1f/Screen.width*1f)<1.4f) {
            Debug.Log("sized");
            GetComponent<RectTransform>().sizeDelta = new Vector2(316, 475);
            transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(316, 475);
        }
    }




    public void Link_hollow() { 
    
    Application.OpenURL("https://itunes.apple.com/tr/app/hollow/id1448479413");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
