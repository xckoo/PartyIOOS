using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bayilt_zemin : MonoBehaviour {
    public InfoManager infomanager;

    public bool sag = false;
    public float force;

    public Collider temp_other;
    public List<Collider> others = new List<Collider>();
    // Use this for initialization
    void Start () {
        infomanager = GameObject.Find("InfoManager").GetComponent<InfoManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void cadera_yoket() {




    }


    void OnTriggerEnter(Collider other){
        if (other.GetComponent<PlayerController_A> () && !other.GetComponent<PlayerController_A>().oldu) {
            other.GetComponent<PlayerController_A>().oldu=true;

            other.GetComponent<PlayerController_A>().stop_all_courotine();


            if (!other.GetComponent<PlayerController_A> ().dustu)
                other.GetComponent<PlayerController_A> ().Dusus ();
        //  other.transform.position = GameManager_A.gameManager.SelectedSpawnPoint ().transform.position;
            if (GameManager_A.gameManager.player != null) {
                if (other.GetComponent<PlayerController_A> ().transform == GameManager_A.gameManager.player.transform.GetChild (0)) {
                    GameManager_A.gameManager.dustum = true;
                }
            }
            if (GameManager_A.gameManager.allPlayers.Count == 7) {
                PlayerPrefs.SetInt ("yedinci", other.GetComponent<PlayerController_A> ().number);
                if(other.GetComponent<PlayerController_A> ().number>0)
                    PlayerPrefs.SetString ("yedinci_str", infomanager._infoTransforms [other.GetComponent<PlayerController_A> ().number-1].GetChild(1).GetComponent<TextMesh>().text);
                else
                    PlayerPrefs.SetString ("yedinci_str", PrefManager.GetUserName ());
            }
            if (GameManager_A.gameManager.allPlayers.Count == 6) {
                PlayerPrefs.SetInt ("altinci", other.GetComponent<PlayerController_A> ().number);
                if(other.GetComponent<PlayerController_A> ().number>0)
                    PlayerPrefs.SetString ("altinci_str", infomanager._infoTransforms [other.GetComponent<PlayerController_A> ().number-1].GetChild(1).GetComponent<TextMesh>().text);
                else
                    PlayerPrefs.SetString ("altinci_str", PrefManager.GetUserName ());
            }
            if (GameManager_A.gameManager.allPlayers.Count == 5) {
                PlayerPrefs.SetInt ("besinci", other.GetComponent<PlayerController_A> ().number);
                if(other.GetComponent<PlayerController_A> ().number>0)
                    PlayerPrefs.SetString ("besinci_str", infomanager._infoTransforms [other.GetComponent<PlayerController_A> ().number-1].GetChild(1).GetComponent<TextMesh>().text);
                else
                    PlayerPrefs.SetString ("besinci_str", PrefManager.GetUserName ());
            }
            if (GameManager_A.gameManager.allPlayers.Count == 4) {
                PlayerPrefs.SetInt ("dorduncu", other.GetComponent<PlayerController_A> ().number);
                if(other.GetComponent<PlayerController_A> ().number>0)
                    PlayerPrefs.SetString ("dorduncu_str", infomanager._infoTransforms [other.GetComponent<PlayerController_A> ().number-1].GetChild(1).GetComponent<TextMesh>().text);
                else
                    PlayerPrefs.SetString ("dorduncu_str", PrefManager.GetUserName ());
            }
            if (GameManager_A.gameManager.allPlayers.Count == 3) {
                PlayerPrefs.SetInt ("ucuncu", other.GetComponent<PlayerController_A> ().number);
                if(other.GetComponent<PlayerController_A> ().number>0)
                    PlayerPrefs.SetString ("ucuncu_str", infomanager._infoTransforms [other.GetComponent<PlayerController_A> ().number-1].GetChild(1).GetComponent<TextMesh>().text);
                else
                    PlayerPrefs.SetString ("ucuncu_str", PrefManager.GetUserName ());
            }
            if (GameManager_A.gameManager.allPlayers.Count == 2) {
                PlayerPrefs.SetInt ("ikinci", other.GetComponent<PlayerController_A> ().number);
                if (other.GetComponent<PlayerController_A> ().number > 0)
                    PlayerPrefs.SetString ("ikinci_str", infomanager._infoTransforms [other.GetComponent<PlayerController_A> ().number - 1].GetChild (1).GetComponent<TextMesh> ().text);
                else
                    PlayerPrefs.SetString ("ikinci_str", PrefManager.GetUserName ());
            }

            if(other.GetComponent<PlayerController_A> ().number>0)
            {   
                infomanager._infoTransforms [other.GetComponent<PlayerController_A> ().number - 1].gameObject.SetActive(false);
            }
            else
            {
                infomanager._myInfoTransform.gameObject.SetActive (false);
                
            }
            if(GameManager_A.gameManager.allPlayers.Count>1)
            GameManager_A.gameManager.allPlayers.Remove(other.GetComponent<PlayerController_A> ());

            PlayerPrefs.SetInt ("birinci", GameManager_A.gameManager.allPlayers[0].GetComponent<PlayerController_A> ().number);
            Debug.Log("briincis");
            if (GameManager_A.gameManager.allPlayers[0].GetComponent<PlayerController_A> ().number > 0)
                PlayerPrefs.SetString ("birinci_str", infomanager._infoTransforms [GameManager_A.gameManager.allPlayers[0].GetComponent<PlayerController_A> ().number - 1].GetChild (1).GetComponent<TextMesh> ().text);
            else
                PlayerPrefs.SetString ("birinci_str", PrefManager.GetUserName ());
            

           // Destroy (other.transform.parent.gameObject);
            

            if (other.GetComponent<PlayerController_A> ().enSonTutan)
                GameManager_A.gameManager.AtanaScoreVer (other.GetComponent<PlayerController_A> ().enSonTutan);
            other.GetComponent<PlayerController_A> ().enSonTutan = null;
            if (!other.GetComponent<PlayerController_A> ().isAI) {
                Camera.main.GetComponent<Kamera_A> ().KameraTeleport ();
            }
            foreach (var rb in other.transform.root.GetComponentsInChildren<Rigidbody>()) {
                rb.velocity = Vector3.zero;
            }

                
        } else if (other.GetComponent<ObjeTipi> ()) {
            //if (other.GetComponent<ObjeTipi> ().objetip == ObjeTipi.ObjeTip.Vurulabilir
              //  || other.GetComponent<ObjeTipi> ().objetip == ObjeTipi.ObjeTip.TekFirlatma)
                //other.transform.position = GameManager_A.gameManager.SelectedSpawnPoint ().transform.position;
        } else if (other.GetComponent<FracturedChunk> ()) {
            Destroy (other.gameObject);
        } else if (other.GetComponent<Parcala> ()) {
            Destroy (other.gameObject);
        }


        if (other.transform.GetComponent<Rigidbody>())
        {
            if (!sag)
                other.transform.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Force);
            else
                other.transform.GetComponent<Rigidbody>().AddForce(-transform.right * force, ForceMode.Force);

            if (!other.isTrigger)
                if (other.transform.root.childCount > 1)
                {
                    if (other.transform.root.GetChild(0).GetComponent<PlayerController_A>())
                    {
                        //if (!other.transform.root.GetChild (0).GetComponent<PlayerController_A> ().dustu) {

                        other.transform.root.GetChild(0).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;

                        other.transform.root.GetChild(0).GetComponent<PlayerController_A>().dustu = true;
                        other.transform.root.GetChild(0).GetComponent<PlayerController_A>().uyanis_sure = 200;
                        other.transform.root.GetChild(0).GetComponent<PlayerController_A>().kaydir = true;
                        //}
                    }
                }
        }

    }


   /* void OnTriggerStay(Collider other)
    {
        if (other.transform.GetComponent<Rigidbody>())
        {
            if (!sag)
                other.transform.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Force);
            else
                other.transform.GetComponent<Rigidbody>().AddForce(-transform.right * force, ForceMode.Force);

            if (!other.isTrigger)
                if (other.transform.root.childCount > 1)
                {
                    if (other.transform.root.GetChild(0).GetComponent<PlayerController_A>()) {
                    //if (!other.transform.root.GetChild (0).GetComponent<PlayerController_A> ().dustu) {

                    other.transform.root.GetChild(0).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;

                    other.transform.root.GetChild(0).GetComponent<PlayerController_A>().dustu = true;
                    other.transform.root.GetChild(0).GetComponent<PlayerController_A>().uyanis_sure = 200;
                    other.transform.root.GetChild(0).GetComponent<PlayerController_A>().kaydir = true;
                    //}
                    }
                }
        }
    }*/

}
