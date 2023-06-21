using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class kiyafet_secim_ai : MonoBehaviour {
	public GameObject[] kiyafet;
	public Material mat;
    // Use this for initialization

    public List<Transform> trnsfrm = new List<Transform>();
    public List<Vector3> pos = new List<Vector3>();
    public List<Quaternion> rot = new List<Quaternion>();


    public List<ConfigurableJoint> confi = new List<ConfigurableJoint>();

    void Start () {

      
           
        foreach (var t in transform.GetChild(0).GetComponentsInChildren<Transform>())
        {
            if(t.transform==transform.GetChild(0))
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
	
	// Update is called once per frame
	void Update () {
        if (!transform.GetChild(0).GetComponent<PlayerController_A>().isAI)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                buyut();
                transform.GetChild(0).GetComponent<PlayerController_A>().myScore++;
            }

        }
    }

    void Scale_Up() {
        if (transform.GetChild(0).GetComponent<PlayerController_A>().myScore < 6)
        {
            transform.GetChild(0).localScale += new Vector3(0.3f, 0.3f, 0.3f);
        }

        transform.GetChild(0).gameObject.SetActive(true);


    }

    public void buyut() {

        for (int i = 0; i < trnsfrm.Count; i++)
        {

            trnsfrm[i].localPosition = pos[i];
            trnsfrm[i].localRotation = rot[i];

        }
        transform.GetChild(0).GetComponent<PlayerController_A>().StopAllCoroutines();

        transform.GetChild(0).gameObject.SetActive(false);
        Invoke("Scale_Up", 0.01f);
    }

}
