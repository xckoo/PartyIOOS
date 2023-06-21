using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YeniKiyafetSecim : MonoBehaviour {

	public GameObject[] kiyafet;

	public Material adam_renk;
	public Material snowMan;
	public Material panda;
	public Material blackWhite;
	public Material jacket;
	public Material clown;
	public Material doctor;
	public Material basketball;
	public Material marshmallow;
	public Material skull;
	public Material ninja;
	public Material robot;
	public Material space;
	public Material blue;
	public Material green;
	public Material orange;
	public Material pink;
    bool isTextureScrolling = false;

    public GameObject eyes;
	public GameObject man;
    float scrollX = 0.02f;
    float scrollY = 0f;
    float offsetX;
    float offsetY;
    // Use this for initialization

    public List<Transform> trnsfrm = new List<Transform>();
	public List<Vector3> pos = new List<Vector3>();
	public List<Quaternion> rot = new List<Quaternion>();


	public List<ConfigurableJoint> confi = new List<ConfigurableJoint>();

	void Start()
	{
        Debug.Log(PlayerPrefs.GetInt("kiyafet_deg"));
        
       // kiyafet[PlayerPrefs.GetInt("unlocked")].gameObject.SetActive(true);
        
        
       // kiyafet[PlayerPrefs.GetInt("kiyafet_deg")].gameObject.SetActive(true);
        // for (int x = 0; x < kiyafet[PlayerPrefs.GetInt("unlocked")+1].GetComponent<acilacak_kiyafetler>().kiyafet.Length; x++)
        // {
        //     kiyafet[PlayerPrefs.GetInt("unlocked")].GetComponent<acilacak_kiyafetler>().kiyafet[x].SetActive(true);
        //     
        // }
        foreach (var t in transform.GetChild(0).GetComponentsInChildren<Transform>())
		{
			if (t.transform == transform.GetChild(0))
			{

			}
			else if (t.GetComponent<Transform>())
			{
				trnsfrm.Add(t.GetComponent<Transform>());
				pos.Add(t.GetComponent<Transform>().localPosition);
				rot.Add(t.GetComponent<Transform>().localRotation);
			}
		}

    
                
        }


	

}

