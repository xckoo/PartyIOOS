using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIController : MonoBehaviour {

	public NavMeshAgent agent;
	public Transform currentTarget;
	public PlayerController_A plc;
	public bool isSelected;//mesafe olcuyor
	public bool isSelected1;
	Vector3 direction;

	[Range(0,10f)]
	public float normalSpeed;
	[Range(0,10f)]
	public float dovusmeSpeed;

	public bool canDoSelection = false;
	void OnEnable () {
		Invoke ("WaitForSelection", 0.5f);
		plc.agent = agent.transform;

		//InvokeRepeating ("Repeat", 1, 8);
		//InvokeRepeating ("Target_null", 0, 7);

		Invoke ("Target_null", 0);
		Invoke ("Repeat", 1);

	}

	public void Target_null(){

		if (GameManager_A.gameManager.allPlayers.Count!=2) {
            int rnd = Random.Range(1, 5);
            if (rnd == 1)
            {
                isSelected1 = true;
            }
            else
            {
                isSelected1 = false;
            }
        } else {

			/*int rnd = Random.Range (0, 2);
			if (rnd == 1) {
				isSelected1 = false;
			} else {
				isSelected1 = true;
			}*/
			isSelected1 = false;
		}
	}


	public void Repeat(){
		
		canChooseTargetPlayer = true;
		canChooseThrowPoint = true;
		CancelInvoke ("Repeat");
	}

	private void WaitForSelection(){
		canDoSelection = true;
	}

	public void target_me(){
		currentTarget = transform;
		if (temp_target != null) {
			temp_target.GetComponent<null_point> ().secili = false;
		}
		if (GameManager_A.gameManager.beni_takip_edenler.Contains(GetComponent<PlayerController_A> ()))
		{
			GameManager_A.gameManager.beni_takip_edenler.Remove (GetComponent<PlayerController_A> ());
		}
	}
	public bool canChooseThrowPoint = true;
	public bool canChooseTargetPlayer = true;
	public float temp_speed;
	Transform temp_target,temp_target_beni;
	bool waiter=false;
	void Update () {

		if (currentTarget == null) {
		//	Debug.Log ("Repeaaattttttttttt------------");
			Repeat ();
		}
		else if (currentTarget.GetComponent<PlayerController_A>()!=null) {
			if (currentTarget.GetComponent<PlayerController_A> ().dustu) {
				Target_null ();
				Repeat ();
			}
			
			if (currentTarget.GetComponent<PlayerController_A> ().kaldiririyor) {
				Target_null ();
				Repeat ();
			}
		}

		if (canDoSelection) {
			if (plc.kaldiririyor) {
				if (canChooseThrowPoint) {
					canChooseThrowPoint = false;
					canChooseTargetPlayer = true;

					int rnd = Random.Range (5, 9);
					Invoke ("Target_null", rnd-1);
					Invoke ("Repeat", rnd);

					currentTarget = GameManager_A.gameManager.SelectedThrowPoint (plc).transform;
				
				}
			} else if (!plc.kaldiririyor) {
				if (canChooseTargetPlayer) {
					canChooseTargetPlayer = false;
					canChooseThrowPoint = true;

					int rnd = Random.Range (5, 9);
					Invoke ("Target_null", rnd-1);
					Invoke ("Repeat", rnd);

					if (!isSelected1) {
						currentTarget = GameManager_A.gameManager.SelectedTargetPlayer (plc).transform;

						if (GameManager_A.gameManager.beni_takip_edenler.Contains(GetComponent<PlayerController_A> ()))
						{
							GameManager_A.gameManager.beni_takip_edenler.Remove (GetComponent<PlayerController_A> ());
						}
						if(GameManager_A.gameManager.player!=null)
						if (currentTarget == GameManager_A.gameManager.player.transform.GetChild (0)) {
							GameManager_A.gameManager.beni_takip_edenler.Add (GetComponent<PlayerController_A> ());
						}

						if (GameManager_A.gameManager.beni_takip_edenler.Count > GameManager_A.gameManager.takip_edebilir_sayi) {
							isSelected1 = true;
							Repeat ();
							Target_null ();
						}

						if (temp_target != null) {
							temp_target.GetComponent<null_point> ().secili = false;
						}
						/*if(GameManager_A.gameManager.player!=null)
						if (currentTarget != GameManager_A.gameManager.player.transform.GetChild (0)) {
							if (!GameManager_A.gameManager.null_point.Contains (currentTarget.GetComponent<AIController> ().currentTarget)) {
								//hedefimizin bir bot hedefi varsa
								Debug.Log ("secim");
								isSelected1 = true;
								Repeat ();
								Target_null ();
							}
						}*/
							

					} else {
						currentTarget = GameManager_A.gameManager.SelectedTargetNull (plc.transform).transform;

						if (GameManager_A.gameManager.beni_takip_edenler.Contains(GetComponent<PlayerController_A> ()))
						{
							GameManager_A.gameManager.beni_takip_edenler.Remove (GetComponent<PlayerController_A> ());
						}

						if (temp_target != null) {
							temp_target.GetComponent<null_point> ().secili = false;
						}

						if (!currentTarget.GetComponent<null_point> ().secili) {
							currentTarget.GetComponent<null_point> ().secili = true;
							temp_target = currentTarget;
						} else if (currentTarget.GetComponent<null_point> ().secili) {
							Repeat ();
						}	
					}
				}
			}
		}


	if (agent.isOnNavMesh) {


        		 if (currentTarget && !plc.dustu && !plc.donuyor/*&&plc.GetComponent<Rigidbody>().velocity.magnitude<=agent.speed*/) {
        			if (plc.kaldiririyor) {
        				agent.speed = dovusmeSpeed;
        				temp_speed = dovusmeSpeed;
        				//Debug.Log ("dovusmeSpeed");
        			} else {
        				agent.speed = 0.6f; // normalSpeed;
        				temp_speed = 1;
        			}

        			agent.SetDestination (currentTarget.position);
        			//	agent.updateRotation = false;
        			transform.parent.GetChild (0).GetComponent<PlayerController_A> ().pos = agent.transform.position;
        			transform.parent.GetChild (0).GetComponent<PlayerController_A> ().currentTarget = currentTarget;
        		//	Debug.DrawLine (transform.position, currentTarget.position, Color.red);
               
        		}
        		else {
        				

        				agent.transform.position = plc.transform.position;
        				transform.parent.GetChild (0).GetComponent<PlayerController_A> ().pos = agent.transform.position;
        				transform.parent.GetChild (0).GetComponent<PlayerController_A> ().currentTarget = currentTarget;
        		//		Debug.DrawLine (transform.position,transform.forward*100,Color.green);

        		}
        }
	}
}
