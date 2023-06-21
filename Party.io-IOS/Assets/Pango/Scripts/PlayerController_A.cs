using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class PlayerController_A : MonoBehaviour
{
    public bool isAI;
    public bool useKlavye;

    public bool grab = false, grab2 = false;
    [Header("Kemikler")]
    public GameObject sagOmuzEk;
    public GameObject solOmuzEk;
    public GameObject sagOmuz;
    public GameObject solOmuz;
    public GameObject sagKol;
    public GameObject solKol;
    public GameObject hips;
    public GameObject gogus;
    public GameObject kaval1;
    public GameObject kaval2;
    GameObject bacak1;
    GameObject bacak2;
    public GameObject kafa;

    [Header("Kollarin Hareketi Degerler")]
    public float kolKaldırmaForce;
    public Transform sagKolForcePoint;
    public Transform solKolForcePoint;
    public float yuruyuzOmuzForce;
    [Header("Oransal Degler")]
    public float ileriForce;
    public float speedOran;
    public float speedOran_Anim;
    public float maxAnimHiz;
    public float MaxHiz;
    public float minAnimHiz;
    public float minSpring;
    [Header("Donus Ani Degerleri")]
    public float DonusOmuzForce;
    public float rotationAnimHiz;
    public float rotationSpring;
    [Range(0, 0.01f)]
    public float donusTouchTreshold;
    [Header("Durma Ani Degerleri")]
    public float donusAngularVelocityTreshold;
    public float durusSpringDeger;


    [Header("Bacak Animasyon")]
    public Animator m_animator;
    public Animation m_animation;
    public piernasmov p1;
    public piernasmov p2;
    public piernasmov p3;
    public piernasmov p4;

    public piernasmov k1;
    public piernasmov k2;
    public piernasmov k3;
    public piernasmov k4;

    [Header("Bayilma Degerleri")]
    [Range(0, 2f)]
    public float kalkisForce;
    [Range(0, 10f)]
    public float kalkisSuresi;
    [Range(0, 500f)]
    public float bayilmaHealth;
    public GameObject sleepParticle;
    private float initialBayilmaHealth;
    //	[HideInInspector]
    public PlayerController_A suanBeniTutan;

    [Header("Tutup Kaldirma ve Firlatma")]
    public GameObject sagKolTutucu;
    public GameObject solKolTutucu;
    public bool kaldiririyor;
    public float kaldirma_kontrol_time;
    float kaldiriyortimer = 0;
    public bool kollarhavada;
    public bool tekbirak = true;

    [Header("Dovusme Degerleri")]
    public float yumrukYonlendirmeForce;
    [HideInInspector]
    public bool tekrarKafaAtabilir = true;
    [HideInInspector]
    public bool tekrarYumrukAtabilir = true;
    //[HideInInspector]
    public bool yumrukAtiyor = false;
    [HideInInspector]
    public bool kafaAtiyor = false;

    public Transform kameraKazanmaNoktasi;

    public GameObject yurumeParticle1;
    public GameObject yurumeParticle2;

    [Header("Isim ve Bayrak ve Tac")]
    public Transform takipEdecekObje;
    private Vector3 takipci_offset;
    public GameObject Tac;
    private Vector3 tac_offset;
    [Header("Raycast Layer Mask")]
    public LayerMask layermask;

    [HideInInspector]
    public float x;
    [HideInInspector]
    public float y;
    [HideInInspector]
    public float angle;
    //[HideInInspector]
    public bool dustu;
    public bool yatiyor;
    //[HideInInspector]
    public PlayerController_A enSonTutan;
    public PlayerController_A arkamdaki, onumdeki, yerdeki;
    //[HideInInspector]
    public int myScore;

    //[HideInInspector]
    public bool donuyor;
    Rigidbody rb;


    [HideInInspector]
    public Vector3 pos;
    [HideInInspector]
    public Transform currentTarget;
    [HideInInspector]
    public Transform agent;
    public bool yeniKalkti = false;

    public int number = 0;

    public bool firlatici_kontrol = true;

    int temp_layer2 = 0;

    [System.Serializable]
    public class SagKolJointDegerler
    {
        [HideInInspector]
        public float spring_i;
        [HideInInspector]
        public float damper_i;
        [HideInInspector]
        public float tp_i;
        [HideInInspector]
        public float maxLimit_i;
        [HideInInspector]
        public float minLimit_i;


        public float spring;
        public float damper;
        public float tp;

        public float maxLimit;
        public float minLimit;
    }

    public List<SagKolJointDegerler> sagKolJointler = new List<SagKolJointDegerler>();

    [System.Serializable]
    public class SolKolJointDegerler
    {

        [HideInInspector]
        public float spring_i;
        [HideInInspector]
        public float damper_i;
        [HideInInspector]
        public float tp_i;
        [HideInInspector]
        public float maxLimit_i;
        [HideInInspector]
        public float minLimit_i;

        public float spring;
        public float damper;
        public float tp;

        public float maxLimit;
        public float minLimit;
    }

    public List<SolKolJointDegerler> solKolJointler = new List<SolKolJointDegerler>();

    [System.Serializable]
    public class BelJointDegerler
    {

        [HideInInspector]
        public float spring_i;
        [HideInInspector]
        public float damper_i;
        [HideInInspector]
        public float tp_i;
        [HideInInspector]
        public float maxLimit_i;
        [HideInInspector]
        public float minLimit_i;

        public float spring;
        public float damper;
        public float tp;

        public float maxLimit;
        public float minLimit;
    }

    public BelJointDegerler belJoint;

    [Header("SagKolYumrukGerilmeDegerleri")]
    [Range(0, 50f)]
    public float saggerilmespringhiz;
    [Range(0, 50f)]
    public float saggerilmetargetposhiz;
    [Range(0, 1f)]
    public float saggerilmesure;

    [Header("SagKolYumrukVurusDegerleri")]
    [Range(0, 50f)]
    public float sagvuruspringhiz;
    [Range(0, 50f)]
    public float sagvurustargetposhiz;
    [Range(0, 1f)]
    public float sagvurussure;


    [Header("Tekme Vurus Degerleri")]
    [Range(0, 50f)]
    public float tekmeSpringHiz;
    [Range(0, 50f)]
    public float tekmeTargetPosHiz;
    [Range(0, 1f)]
    public float tekmeVurusSure;

    [Header("Tekme Toparlama Degerleri")]
    [Range(0, 50f)]
    public float tekmeToplaSpringHiz;
    [Range(0, 50f)]
    public float tekmeToplaTargetPosHiz;
    [Range(0, 1f)]
    public float tekmeToplaSure;

    float birak_spr = 500;


    [HideInInspector]
    public bool tekmeAtabilir = true;
    [HideInInspector]
    public bool tekme_havada = false;

    [System.Serializable]
    public class SagKolJointDegerlerY
    {
        [HideInInspector]
        public float spring_i;
        [HideInInspector]
        public float damper_i;
        [HideInInspector]
        public float tp_i;
        [HideInInspector]
        public float maxLimit_i;
        [HideInInspector]
        public float minLimit_i;


        public float spring;
        public float damper;
        public float tp;

        public float maxLimit;
        public float minLimit;
    }

    public List<SagKolJointDegerlerY> sagKolJointlerYumruk = new List<SagKolJointDegerlerY>();
    [Header("Sol Kol Yumruk Gerilme Degerleri")]
    [Range(0, 50f)]
    public float solgerilmespringhiz;
    [Range(0, 50f)]
    public float solgerilmetargetposhiz;
    [Range(0, 1f)]
    public float solgerilmesure;

    [Header("Sol Kol Yumruk Vurus Degerleri")]
    [Range(0, 50f)]
    public float solvuruspringhiz;
    [Range(0, 50f)]
    public float solvurustargetposhiz;
    [Range(0, 1f)]
    public float solvurussure;

    [Header("YENI DEGERLER")]
    [Range(0, 50f)]
    public float solyumruksure;
    [Range(0, 50f)]
    public float sagyumruksure;

    [Range(0, 10000f)]
    public float solyumrukspring;
    [Range(0, 10000f)]
    public float sagyumrukspring;
    [Header("--------")]

    [Range(-1, 1)]
    public float targetposx;
    [Range(-1, 1)]
    public float targetposy, targetposz, targetposw;

    [Header("--------")]
    [Range(-1, 1)]
    public float targetposx2;
    [Range(-1, 1)]
    public float targetposy2, targetposz2, targetposw2;
    [Header("--------")]

    [Range(0, 2)]
    public float yumruksure;

    [Header("--------")]
    [Range(-1000, 1000)]
    public float forcee;

    [System.Serializable]
    public class SolKolJointDegerlerY
    {
        [HideInInspector]
        public float spring_i;
        [HideInInspector]
        public float damper_i;
        [HideInInspector]
        public float tp_i;
        [HideInInspector]
        public float maxLimit_i;
        [HideInInspector]
        public float minLimit_i;


        public float spring;
        public float damper;
        public float tp;

        public float maxLimit;
        public float minLimit;
    }

    public List<SolKolJointDegerlerY> solKolJointlerYumruk = new List<SolKolJointDegerlerY>();

    [System.Serializable]
    public class BelJointDegerlerY
    {

        [HideInInspector]
        public float spring_i;
        [HideInInspector]
        public float damper_i;
        [HideInInspector]
        public float tp_i;
        [HideInInspector]
        public float maxLimit_i;
        [HideInInspector]
        public float minLimit_i;

        public float spring;
        public float damper;
        public float tp;

        public float maxLimit;
        public float minLimit;
    }

    public List<BelJointDegerlerY> belJointDegerlerYumruk = new List<BelJointDegerlerY>();

    [Header("Kafa Gerilme Degerleri")]
    [Range(0, 50f)]
    public float kafagerilmespringhiz;
    [Range(0, 50f)]
    public float kafagerilmetargetposhiz;
    [Range(0, 1f)]
    public float kafagerilmesure;

    [Header("Kafa Vurus Degerleri")]
    [Range(0, 50f)]
    public float kafavuruspringhiz;
    [Range(0, 50f)]
    public float kafavurustargetposhiz;
    [Range(0, 1f)]
    public float kafavurussure;

    private float capsuleHeight;

    public float uyanis = 0;

    public bool kaydir = false;

    int temp_layer = 0;

    public GameObject effect_kaldiririyor;
    public bool duvar = false;
    public bool duvarda = false;
    public bool egildi = false;
    public bool durdu = false;



    public bool oldu = false;
    private void KolInitializeDegerleriAyarla()
    {

        sagKolJointler[0].spring_i = sagOmuzEk.GetComponent<HingeJoint>().spring.spring;
        sagKolJointler[0].damper_i = sagOmuzEk.GetComponent<HingeJoint>().spring.damper;
        sagKolJointler[0].tp_i = sagOmuzEk.GetComponent<HingeJoint>().spring.targetPosition;
        sagKolJointler[0].maxLimit_i = sagOmuzEk.GetComponent<HingeJoint>().limits.max;
        sagKolJointler[0].minLimit_i = sagOmuzEk.GetComponent<HingeJoint>().limits.min;


        /*sagKolJointler [1].spring_i = sagOmuz.GetComponent<HingeJoint> ().spring.spring;
		sagKolJointler [1].damper_i = sagOmuz.GetComponent<HingeJoint> ().spring.damper;
		sagKolJointler [1].tp_i = sagOmuz.GetComponent<HingeJoint> ().spring.targetPosition;
		sagKolJointler [1].maxLimit_i = sagOmuz.GetComponent<HingeJoint> ().limits.max;
		sagKolJointler [1].minLimit_i = sagOmuz.GetComponent<HingeJoint> ().limits.min;*/

        sagKolJointler[2].spring_i = sagKol.GetComponent<HingeJoint>().spring.spring;
        sagKolJointler[2].damper_i = sagKol.GetComponent<HingeJoint>().spring.damper;
        sagKolJointler[2].tp_i = sagKol.GetComponent<HingeJoint>().spring.targetPosition;
        sagKolJointler[2].maxLimit_i = sagKol.GetComponent<HingeJoint>().limits.max;
        sagKolJointler[2].minLimit_i = sagKol.GetComponent<HingeJoint>().limits.min;


        solKolJointler[0].spring_i = solOmuzEk.GetComponent<HingeJoint>().spring.spring;
        solKolJointler[0].damper_i = solOmuzEk.GetComponent<HingeJoint>().spring.damper;
        solKolJointler[0].tp_i = solOmuzEk.GetComponent<HingeJoint>().spring.targetPosition;
        solKolJointler[0].maxLimit_i = solOmuzEk.GetComponent<HingeJoint>().limits.max;
        solKolJointler[0].minLimit_i = solOmuzEk.GetComponent<HingeJoint>().limits.min;


        /*solKolJointler [1].spring_i = solOmuz.GetComponent<HingeJoint> ().spring.spring;
		solKolJointler [1].damper_i = solOmuz.GetComponent<HingeJoint> ().spring.damper;
		solKolJointler [1].tp_i = solOmuz.GetComponent<HingeJoint> ().spring.targetPosition;
		solKolJointler [1].maxLimit_i = solOmuz.GetComponent<HingeJoint> ().limits.max;
		solKolJointler [1].minLimit_i = solOmuz.GetComponent<HingeJoint> ().limits.min;*/

        solKolJointler[2].spring_i = solKol.GetComponent<HingeJoint>().spring.spring;
        solKolJointler[2].damper_i = solKol.GetComponent<HingeJoint>().spring.damper;
        solKolJointler[2].tp_i = solKol.GetComponent<HingeJoint>().spring.targetPosition;
        solKolJointler[2].maxLimit_i = solKol.GetComponent<HingeJoint>().limits.max;
        solKolJointler[2].minLimit_i = solKol.GetComponent<HingeJoint>().limits.min;


        belJoint.spring_i = gogus.GetComponent<HingeJoint>().spring.spring;
        belJoint.damper_i = gogus.GetComponent<HingeJoint>().spring.damper;
        belJoint.tp_i = gogus.GetComponent<HingeJoint>().spring.targetPosition;
        belJoint.maxLimit_i = gogus.GetComponent<HingeJoint>().limits.max;
        belJoint.minLimit_i = gogus.GetComponent<HingeJoint>().limits.min;

    }
    private void ForceEkleyicileriInitializeEt()
    {
        kafa.GetComponent<ForceEkleyici>().isEnabled = false;
        sagKol.GetComponent<ForceEkleyici>().isEnabled = false;
        solKol.GetComponent<ForceEkleyici>().isEnabled = false;
    }

    void OnEnable()
    {
        p1.enabled = true;
        p2.enabled = true;
        p3.enabled = true;
        p4.enabled = true;

        p1.stop = false;
        p2.stop = false;
        p3.stop = false;
        p4.stop = false;


        JointSpring _gj = gogus.GetComponent<HingeJoint>().spring;
        _gj.spring = 400f;
        _gj.targetPosition = 0;
        gogus.GetComponent<HingeJoint>().spring = _gj;

        JointLimits gjl2 = gogus.GetComponent<HingeJoint>().limits;
        gjl2.max = 30;
        gjl2.min = -30;
        gogus.GetComponent<HingeJoint>().limits = gjl2;

        kaldiririyor = false;
        kollarhavada = false;
        enSonTutan = null;
        onumdeki = null;
        arkamdaki = null;

        durdu = false;
        egildi = false;

        degg = true;
        //Tac.SetActive (false);

    }

    private void AddPLayers()
    {
        GameManager_A.gameManager.allPlayers.Add(this);
        if (!isAI)
        {
            GameManager_A.gameManager.me = this;
        }
    }

    public void firlatici_kontroll()
    {

        firlatici_kontrol = true;
    }
    public void layer_control()
    {
        if (!dustu)
        {
            gameObject.layer = temp_layer2;
        }
    }

    void Start()
    {

        temp_layer2 = gameObject.layer;

        InvokeRepeating("layer_control", 3, 3);

        if (transform.parent.name == "PlayerAI_7")
        {
            number = 7;
        }
        if (transform.parent.name == "PlayerAI_8")
        {
            number = 8;
        }
        if (transform.parent.name == "PlayerAI_9")
        {
            number = 9;
        }

        if (transform.parent.name == "PlayerAI_10")
        {
            number = 10;
        }
        if (transform.parent.name == "PlayerAI_11")
        {
            number = 11;
        }
        if (transform.parent.name == "PlayerAI_12")
        {
            number = 12;
        }

        if (isAI)
            ileriForce = 4.5f;
        else
            ileriForce = 4.7f;

        Invoke("AddPLayers", 0.3f);
        tekmeAtabilir = true;

        InvokeRepeating("firlatici_kontroll", 3, 3);

        bacak1 = kaval1.transform.parent.gameObject;
        bacak2 = kaval2.transform.parent.gameObject;


        if (isAI)
        {





            if (PlayerPrefs.GetInt("Scorenew") < 6)
            {
                uyanis_sure = 30;

                int rnddx = Random.Range(1, 4);
                // int rndd2 = Random.Range(20, 30);

                grab_seviye = rnddx;//-PlayerPrefs.GetInt ("Scorenew");

                if (grab_seviye < 3)
                {
                    grab_seviye = 2;
                }


            }
            else if (PlayerPrefs.GetInt("Scorenew") >= 6 && PlayerPrefs.GetInt("Scorenew") < 16)
            {
                uyanis_sure = 25;

                int rnddx = Random.Range(1, 3);
                // int rndd2 = Random.Range(20, 30);

                grab_seviye = rnddx;//-PlayerPrefs.GetInt ("Scorenew");

                if (grab_seviye < 3)
                {
                    grab_seviye = 2;
                }

            }
            else
            {

                int rnddx = Random.Range(1, 3);
                // int rndd2 = Random.Range(20, 30);

                grab_seviye = rnddx;//-PlayerPrefs.GetInt ("Scorenew");

                if (grab_seviye < 3)
                {
                    grab_seviye = 2;
                }

                uyanis_sure = 20;
            }



            /*if (uyanis_sure < 20) {
				uyanis_sure = 19;
			}*/
        }
        else
        {

            if (PlayerPrefs.GetInt("Scorenew") < 6)
            {
                uyanis_sure = 4;


            }
            else if (PlayerPrefs.GetInt("Scorenew") >= 6 && PlayerPrefs.GetInt("Scorenew") < 16)
            {
                uyanis_sure = 5;


            }
            else
            {


                uyanis_sure = 6;
            }
        }




        capsuleHeight = GetComponent<CapsuleCollider>().height;
        takipEdecekObje.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z) + Vector3.up * 2f;
        takipci_offset = takipEdecekObje.transform.position - transform.position;
        tac_offset = Tac.transform.position - transform.position;
        //Tac.transform.parent = null;
        rb = GetComponent<Rigidbody>();
        dustu = false;
        if (isAI)
        {
            bayilmaHealth = GameManager_A.gameManager.oyunDegerler.YapayZeklarBayilmaDegerler[PrefManager.GetRank()];
        }
        else
        {
            kalkisSuresi = GameManager_A.gameManager.oyunDegerler.PlayerKalkisDegerier[PrefManager.GetRank()];
        }

        initialBayilmaHealth = bayilmaHealth;

        KolInitializeDegerleriAyarla();
        ForceEkleyicileriInitializeEt();
        donuyor = true;
        temp_layer = gameObject.layer;



        int rndd = Random.Range(0, 6);
        Invoke("once_el_kalk", rndd);
        Invoke("DonusInit", 0.2f);

    }

    void once_el_kalk()
    {
        el_kalk = true;

    }

    private void DonusInit()
    {
        donuyor = false;
    }

    private void waiting()
    {
        Kaldir2();

        Invoke("waiting2", 0.2f);
    }

    private void waiting2()
    {
        if (!kaldiririyor && gameObject.activeSelf)
        {
            StartCoroutine(_SagKolBirak2());
            StartCoroutine(_SolKolBirak2());
        }




        oncet = true;
    }
    private void kol_enable()
    {
        k1.enabled = false;
        k2.enabled = false;
        k3.enabled = false;
        k4.enabled = false;


        if (gameObject.activeSelf)
            StartCoroutine(diren());

        //waiting ();
        AI_kalkis = true;
    }
    public void diren_false_fonk()
    {

        StartCoroutine(diren_false());
    }

    private void hareket_false()
    {
        p1.stop = true;
        p2.stop = true;
        p3.stop = true;
        p4.stop = true;

        k1.stop = true;
        k2.stop = true;
        k3.stop = true;
        k4.stop = true;

        kol_enable();

    }

    private void AI_kalkis_fonk()
    {
        uyanis++;
        AI_kalkis = true;

    }

    private void el_kaldir()
    {
        el_kalk = true;
        if (!dustu)
            grab = false;
    }

    private bool oncet = true;
    [HideInInspector]
    public int temp_uyanis = 0;
    public int uyanis_sure = 0;

    bool AI_kalkis = true;
    bool el_kalk = false;
    public bool yerde = false;
    public bool once_yer = true;
    public Transform tutundugu_duvar;
    bool once_duvarda = true;
    bool deneme = true;
    bool oto_firlatma = true;
    float firlat_time = 1f;
    public LayerMask firlatici_bolge;
    IEnumerator oto_firlat()
    {
        GameManager_A.gameManager.firlatma.fillAmount = 1;
        GameManager_A.gameManager.firlatma.transform.parent.gameObject.SetActive(true);
        while (firlat_time > 0)
        {

            firlat_time -= 0.01f;
            if (firlatici_mesafe)
            {
                firlat_time -= (0.1f / mesafe_fir);
            }
            GameManager_A.gameManager.firlatma.fillAmount = Mathf.MoveTowards(GameManager_A.gameManager.firlatma.fillAmount, firlat_time / 1f, Time.deltaTime * 10);
            yield return new WaitForSecondsRealtime(0.01f);
        }

        BeldenFirlat();
        yield return new WaitForSecondsRealtime(1f);
        GameManager_A.gameManager.firlatma.transform.parent.gameObject.SetActive(false);
        firlat_time = 1f;
        yield return new WaitForSecondsRealtime(2f);
        oto_firlatma = true;
    }

    public void yumusama_kontroll()
    {

        yumusama_kontrol = false;
        StartCoroutine(yumusama2());
    }

    IEnumerator yumusama2()
    {
        yield return new WaitForSeconds(1f);
        tutma_yumusama = false;
        yumusama_kontrol = true;
    }


    bool firlatici_mesafe = false;
    float mesafe_fir = 10;
    bool tutma_yumusama = true, yumusama_kontrol = true;
    public float uyanis_duvar;
    public float duvar_timer = 0;
    Collider temp_col;

    void LateUpdate()
    {
        if (enter)
        {

            Collider other = temp_col;
            other = temp_col;
            if (other.GetComponent<PlayerController_A>())
            {
                if (!other.isTrigger)
                {
                    if (other.GetComponent<PlayerController_A>())
                    {

                        int rnd_grab = Random.Range(1, grab_seviye);
                        if (isAI && once && !GameManager_A.gameManager.gameOver)
                        {
                            if (rnd_grab == 1)
                            {
                                grab = true;
                            }

                            Invoke("grab_false", 1);
                            once = false;
                        }
                        if (!dustu && grab && !isAI)
                        {

                            TutmakIcinUzan(other.transform);
                        }
                        else if (!dustu && grab && isAI)
                        {
                            TutmakIcinUzan(other.transform);

                        }
                    }
                }
            }
            if (other.GetComponent<FirlatmaTrigger>() && isAI)
            {
                if (kaldiririyor)
                {

                    if (firlatici_kontrol)
                    {
                        DisardanFirlat();
                        firlatici_kontrol = false;
                    }
                }
            }
        }


        /*RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit,4.5f, firlatici_bolge)) {
			Debug.DrawRay (transform.position, transform.forward*4.5f, Color.red);
			mesafe_fir = Vector3.Distance (transform.position, hit.point);
			//Debug.Log ("mesafe_fir" + (1f / mesafe_fir));
			firlatici_mesafe = true;
		} 
		else{
			Debug.DrawRay (transform.position, transform.forward*4.5f, Color.blue);
			firlatici_mesafe=false;
		}*/


        /*if(!isAI)
		if (kollarhavada && oto_firlatma) {
			oto_firlatma = false;
			StartCoroutine (oto_firlat ());
		}*/



        bayilmaHealth = Mathf.MoveTowards(bayilmaHealth, 500, Time.deltaTime * 40);
        if (duvarda && !isAI)
        {
            GameManager_A.gameManager.firlatma.fillAmount = Mathf.MoveTowards(GameManager_A.gameManager.firlatma.fillAmount, uyanis_duvar / (uyanis_sure * 1f), Time.deltaTime * 2);
            if (uyanis_duvar > 0)
            {

                uyanis_duvar = (uyanis_duvar - (0.01f));
            }

            if (uyanis_duvar >= uyanis_sure)
            {
                if (once_duvarda)
                {
                    once_duvarda = false;
                    duvardan_firlat();
                    GameManager_A.gameManager.firlatma.fillAmount = 1;
                    duvar_timer = 0;
                }
            }
            if (duvar_timer < 500)
            {
                duvar_timer += 1;

            }
            if (duvar_timer >= 500)
            {
                if (once_duvarda)
                {
                    once_duvarda = false;
                    KolJointleriDestroyEt();
                    duvar_timer = 0;
                }
            }

            GameManager_A.gameManager.firlatma.transform.parent.gameObject.SetActive(true);

        }
        else
        {
            uyanis_duvar = 0;
        }
        if (duvarda && once_duvarda && isAI)
        {
            once_duvarda = false;

            Invoke("duvardan_firlat", 4);

        }
        /*if (duvarda && once_duvarda && !isAI) {
			if (grab) {
				once_duvarda = false;
				Invoke("duvardan_firlat",4);
			}
		}*/

        if (dustu)
        {
            grab = false;
            //GEri Acilacakkkk
            if (!yatiyor && enSonTutan != null)
            {
                //	Debug.Log ("donuyor bizimle");
                if (tutma_yumusama)
                {
                    if (yumusama_kontrol)
                        yumusama_kontroll();


                    transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, new Vector3(0f, enSonTutan.transform.eulerAngles.y, 0f), Time.deltaTime * 300);
                    //transform.eulerAngles =  new Vector3 (0f, enSonTutan.transform.eulerAngles.y, 0f);
                    rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;  //RigidbodyConstraints.None;

                }
                else
                {
                    transform.eulerAngles = new Vector3(0f, enSonTutan.transform.eulerAngles.y, 0f);
                    rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;  //RigidbodyConstraints.None;
                }
            }
            if (yatiyor)
                rb.constraints = RigidbodyConstraints.None;

            if (enSonTutan != null && enSonTutan.sagKolTutucu.GetComponent<ConfigurableJoint>() == null)
            {

                yatiyor = true;
                rb.constraints = RigidbodyConstraints.None;
                //				Debug.Log ("yatiyor"); 
            }
            else if (bayilmaHealth <= 0)
            {
                //yatiyor = true;
                //rb.constraints= RigidbodyConstraints.None;
                //Debug.Log ("yatiyor");
            }

            if (yatiyor && once_yer)
            {
                once_yer = false;
                yerde = false;
                Invoke("yere_dustu", 1f);
            }

            /*RaycastHit hit;
			LayerMask layerMask2 = temp_layer;
			//layerMask2 = ~layerMask2;
			if (Physics.Raycast (transform.position, Vector3.down, out hit, GetComponent<CapsuleCollider> ().height / 2 + 2.5f,layerMask2)) {

				Debug.DrawRay (transform.position, Vector3.down,Color.blue);
			}*/
            if (!isAI)
            {
                GameManager_A.gameManager.firlatma.fillAmount = Mathf.MoveTowards(GameManager_A.gameManager.firlatma.fillAmount, uyanis / (uyanis_sure * 1f), Time.deltaTime * 2);
                if (uyanis > 0)
                {

                    uyanis = (uyanis - (0.01f));
                }

                GameManager_A.gameManager.firlatma.transform.parent.gameObject.SetActive(true);
            }


            if ((int)uyanis != temp_uyanis)
            {
                temp_uyanis = (int)uyanis;
                /*p1.stop = false;
				p2.stop = false;
				p3.stop = false;
				p4.stop = false;

				k1.stop = false;
				k2.stop = false;
				k3.stop = false;
				k4.stop = false;*/

                //k1.enabled = true;
                //k2.enabled = true;
                //k3.enabled = true;
                //k4.enabled = true;
                Invoke("hareket_false", 0.2f);
                if (isAI)
                {
                    if (!kaydir)
                        if ((int)uyanis >= uyanis_sure)
                        {
                            Kalkis();
                        }
                }
                else
                {
                    if (!kaydir)
                        if ((int)uyanis >= uyanis_sure)
                        {
                            Kalkis();
                        }
                }
            }


            if (isAI && AI_kalkis)
            {
                AI_kalkis = false;
                Invoke("AI_kalkis_fonk", 0.4f);
            }
        }
        else if (!dustu)
        {
            tutma_yumusama = true;

            if (!isAI)
            {
                /*if (kaldiririyor && kollarhavada) {
					effect_kaldiririyor.SetActive (true);
				}else{
					effect_kaldiririyor.SetActive(false);
				}*/
                if (!duvarda)
                {
                    GameManager_A.gameManager.firlatma.fillAmount = 0;
                    GameManager_A.gameManager.firlatma.transform.parent.gameObject.SetActive(false);
                }
            }

            if (isAI && el_kalk)
            {
                el_kalk = false;
                int rnd = Random.Range(5, 15);
                grab = true;
                Invoke("grab_false", 0.1f);
                Invoke("el_kaldir", rnd);
            }
            if (sleepParticle.activeSelf)
            {
                sleepParticle.SetActive(false);
                Kalkis();
            }

            temp_uyanis = 2000;
            uyanis = 0;
        }

        /*if (Input.GetKeyUp (KeyCode.K)) {
				Kaldir ();
		}

		if (Input.GetKey (KeyCode.A)) 
		{
			Time.timeScale-=Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.D)) {
			Time.timeScale+=Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.S)) {
			Dusus ();
		}

		if (Input.GetKeyUp (KeyCode.B)) {
			if (deneme) {
				SagKolYumrukAtma2 (transform);
			} else {
				SolKolYumrukAtma2 (transform);
			}
			deneme = !deneme;
		}

		if (Input.GetKeyUp (KeyCode.C)) {
			
			crouch ();
		}


		if (Input.GetKeyUp (KeyCode.O) && !isAI) {
			duvardan_firlat ();
		}
		if (Input.GetKeyUp (KeyCode.Z)) {
			//Dusus ();
			StartCoroutine (_SagKolBirak2 ());
			StartCoroutine (_SolKolBirak2 ());

		}*/

        if (grab /*&& oncet*/)
        {
            oncet = false;
            if (!GameManager_A.gameManager.gameOver)
                Invoke("waiting", 0.01f);
        }

        takipEdecekObje.transform.position = transform.position + takipci_offset;
        //Tac.transform.position=new Vector3()  //= transform.position + tac_offset;

        if (!tekmeAtabilir)
            return;
        if (!dustu && tekmeAtabilir)
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, -90f);

        /*if (!isAI) {
			if (Input.GetKeyDown (KeyCode.E)) {
			egil ();
			}
			if (Input.GetKeyDown (KeyCode.V)) {
				Kalkis ();
			}

			if (Input.GetKeyDown (KeyCode.F)) {
				SolKolYumrukAtma (transform);
			}

			if (Input.GetKeyDown (KeyCode.G)) {
				SagKolYumrukAtma (transform);
			}

            if (Input.GetKeyDown(KeyCode.H))
            {
                //KafaAtmaGerilme();
            }

			if (Input.GetKeyDown (KeyCode.T)) {
				//TekmeAt ();
			}

			if (Input.GetKeyDown (KeyCode.B)) {
				//Dusus ();
			}

		if(Input.GetKeyDown(KeyCode.Q)){
				egil ();
				//KafaVeKollarBuyut();
			}

		}

		/*

		if (kaldiririyor) {
			kaldiriyortimer += Time.deltaTime;
			if (kaldiriyortimer >= 3f&&tekbirak) {
				tekbirak = false;
				DisardanFirlat ();
			}
		} else {
			tekbirak = true;
			kaldiriyortimer = 0;
		}
*/



    }

    void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            KafaVeKollarBuyut();

        }
        if (!tekmeAtabilir)
            return;
        if (!dustu)
        {
            //if (!duvarda)
            {
                if (!egildi)
                {
                    //if (!durdu) {
                    DonusVeYurume();
                    HareketeEkForcelar();

                    if (!yumrukAtiyor && !kaldiririyor)
                    {

                        /*	JointSpring jpsbx = transform.GetChild(0).GetComponent<HingeJoint>().spring;
                            jpsbx.spring = 200;// belJoint.spring*0.025f;
                            jpsbx.damper =5;// belJoint.damper;
                            jpsbx.targetPosition = 0;
                            transform.GetChild(0).GetComponent<HingeJoint> ().spring = jpsbx;
                        */

                        JointLimits jpsoel1_1x = transform.GetChild(0).GetComponent<HingeJoint>().limits;
                        jpsoel1_1x.max = 20;
                        jpsoel1_1x.min = -20;
                        transform.GetChild(0).GetComponent<HingeJoint>().limits = jpsoel1_1x;

                        if (!GameManager_A.gameManager.gameOver)
                            KollarHareket();
                    }
                    //} 

                }
            }

        }
        else
        {
            LimitAc();
        }
        if (durdu)
        {
            //GetComponent<Rigidbody> ().constraints=RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            //GetComponent<Rigidbody> ().constraints=RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
        OransalDegerler();

        if (!isAI)
        {

            /*hips.GetComponent<Rigidbody> ().velocity = new Vector3 (
				Mathf.Clamp (hips.GetComponent<Rigidbody> ().velocity.x, -MaxHiz, MaxHiz),

				hips.GetComponent<Rigidbody> ().velocity.y,

				Mathf.Clamp (hips.GetComponent<Rigidbody> ().velocity.z, -MaxHiz, MaxHiz)
			);*/
            /* RaycastHit hit2;
             LayerMask layerMask4 = LayerMask.GetMask(LayerMask.LayerToName(19));
             if (myScore < 2)
             {
                 if (Physics.Raycast(transform.position, Vector3.down, out hit2, (GetComponent<CapsuleCollider>().height / 2) + 0.6f, layerMask4))
                 {
                     force_izin = true;
                 }
                 else
                 {
                     force_izin = false;

                 }
             }


             else if (myScore < 4)
             {
                 if (Physics.Raycast(transform.position, Vector3.down, out hit2, (GetComponent<CapsuleCollider>().height / 2) + 0.9f, layerMask4))
                 {
                     force_izin = true;
                 }
                 else
                 {
                     force_izin = false;

                 }
             }
             else
             {
                 if (Physics.Raycast(transform.position, Vector3.down, out hit2, (GetComponent<CapsuleCollider>().height / 2) + 1.9f, layerMask4))
                 {
                     force_izin = true;
                 }
                 else
                 {
                     force_izin = false;

                 }

             }

         */
        }

        RaycastHit hit;
        /*if(Physics.Raycast(transform.position,Vector3.down,out hit,GetComponent<CapsuleCollider>().height/2+0.5f,layermask)){
			if (hit.transform.gameObject.CompareTag ("harita")) {
				if (!yurumeParticle1.activeSelf) {
					yurumeParticle1.SetActive (true);
					yurumeParticle2.SetActive (true);
				}
			} else {
				if (yurumeParticle1.activeSelf) {
					yurumeParticle1.SetActive (false);
					yurumeParticle2.SetActive (false);
				}
			}
		} else {
			if (yurumeParticle1.activeSelf) {
				yurumeParticle1.SetActive (false);
				yurumeParticle2.SetActive (false);
			}
		}*/

        if (dustu)
        {
            yurumeParticle1.SetActive(false);
            yurumeParticle2.SetActive(false);
        }
        else
        {
            yurumeParticle1.SetActive(true);
            yurumeParticle2.SetActive(true);
        }
        LayerMask layerMask2 = LayerMask.GetMask(LayerMask.LayerToName(19));
        //layerMask2 = ~layerMask2;



        int deg1 = gameObject.layer;
        int layerMask3 = LayerMask.GetMask(LayerMask.LayerToName(deg1)) | LayerMask.GetMask(LayerMask.LayerToName(0));
        layerMask3 = ~layerMask3;

        /*if (Physics.Raycast (transform.position, -transform.forward, out hit,1.5f, layerMask3)) {
                if ((hit.transform.gameObject.layer >= 8 && hit.transform.gameObject.layer <= 18) | hit.transform.gameObject.layer == 20) 
                {
                    if (hit.transform.root.childCount > 0) {
                        if (hit.transform.root.GetChild (0).GetComponent<PlayerController_A> () != null) {
                            //Debug.DrawRay (transform.position, -transform.forward * 20, Color.green);
                            arkamdaki = hit.transform.root.GetChild (0).GetComponent<PlayerController_A> ();
                        }
                        else 
                        {
                            //Debug.DrawRay (transform.position, -transform.forward * 20, Color.red);
                            arkamdaki = null;
                        }
                    } 
                    else 
                    {
                        //Debug.DrawRay (transform.position, -transform.forward * 20, Color.red);
                        arkamdaki = null;
                    }
                }
                else 
                {
                    //Debug.DrawRay (transform.position,-transform.forward*20, Color.blue);
                    arkamdaki = null;
                }
        }
        else {
            //Debug.DrawRay (transform.position,-transform.forward*20, Color.blue);
            arkamdaki = null;
        }*/

        if (Physics.Raycast(transform.position, transform.forward, out hit, 0.8f, layerMask3))
        {
            if ((hit.transform.gameObject.layer >= 8 && hit.transform.gameObject.layer <= 18) | hit.transform.gameObject.layer == 20 | hit.transform.gameObject.layer == 23 | hit.transform.gameObject.layer == 24)
            {
                if (hit.transform.root.childCount > 0)
                {
                    if (hit.transform.root.GetChild(0).GetComponent<PlayerController_A>() != null)
                    {
                        //Debug.DrawRay (transform.position, transform.forward * 20, Color.green);
                        onumdeki = hit.transform.root.GetChild(0).GetComponent<PlayerController_A>();
                    }
                    else
                    {
                        //Debug.DrawRay (transform.position, transform.forward * 20, Color.red);
                        onumdeki = null;
                    }
                }
                else
                {
                    //Debug.DrawRay (transform.position, transform.forward * 20, Color.red);
                    onumdeki = null;
                }
            }
            else
            {
                //Debug.DrawRay (transform.position,transform.forward*20, Color.blue);
                onumdeki = null;
            }
        }
        else
        {
            //Debug.DrawRay (transform.position,transform.forward*20, Color.blue);
            onumdeki = null;
        }

        /*if (Physics.Raycast (transform.position, (transform.forward+Vector3.down/2), out hit,1.5f, layerMask3)) {
            if ((hit.transform.gameObject.layer >= 8 && hit.transform.gameObject.layer <= 18) | hit.transform.gameObject.layer == 20) 
            {
                if (hit.transform.root.childCount > 0) {
                    if (hit.transform.root.GetChild (0).GetComponent<PlayerController_A> () != null) {
                        //Debug.DrawRay (transform.position, (transform.forward+Vector3.down/2) * 20, Color.green);
                        //onumdeki = hit.transform.root.GetChild (0).GetComponent<PlayerController_A> ();
                        yerdeki = hit.transform.root.GetChild (0).GetComponent<PlayerController_A> ();
                    }
                    else 
                    {
                        //Debug.DrawRay (transform.position, (transform.forward+Vector3.down/2) * 20, Color.red);
                        yerdeki = null;
                    }
                } 
                else 
                {
                    //Debug.DrawRay (transform.position, (transform.forward+Vector3.down/2) * 20, Color.red);
                    yerdeki = null;
                }
            }
            else 
            {
                //Debug.DrawRay (transform.position,(transform.forward+Vector3.down/2)*20, Color.blue);
                yerdeki = null;
            }
        }
        else {
            //Debug.DrawRay (transform.position,(transform.forward+Vector3.down/2)*20, Color.blue);
            yerdeki = null;
        }*/

        //	TekmeAttirici ();
    }
    public bool force_izin = true;

    void yere_dustu()
    {

        yerde = true;
    }

    void kaldir_sonrasi()
    {
        kaval1.transform.parent.GetComponent<piernasmov>().enabled = true;
        kaval2.transform.parent.GetComponent<piernasmov>().enabled = true;



        JointSpring jpsoe1_1x = kaval1.GetComponent<HingeJoint>().spring;
        jpsoe1_1x.targetPosition = 0;
        kaval1.GetComponent<HingeJoint>().spring = jpsoe1_1x;


        JointSpring jpsoe1_2x = kaval2.GetComponent<HingeJoint>().spring;
        jpsoe1_2x.targetPosition = 0;
        kaval2.GetComponent<HingeJoint>().spring = jpsoe1_2x;

        //GetComponent<CapsuleCollider> ().height = 1.03f;
    }
    public void crouch()
    {

        kaval1.transform.parent.GetComponent<piernasmov>().enabled = false;
        kaval2.transform.parent.GetComponent<piernasmov>().enabled = false;

        JointSpring jpsoe1_1 = kaval1.transform.parent.GetComponent<HingeJoint>().spring;
        jpsoe1_1.targetPosition = 70;
        kaval1.transform.parent.GetComponent<HingeJoint>().spring = jpsoe1_1;


        JointSpring jpsoe1_2 = kaval2.transform.parent.GetComponent<HingeJoint>().spring;
        jpsoe1_2.targetPosition = 70;
        kaval2.transform.parent.GetComponent<HingeJoint>().spring = jpsoe1_2;

        JointSpring jpsoe1_1x = kaval1.GetComponent<HingeJoint>().spring;
        jpsoe1_1x.targetPosition = -70;
        kaval1.GetComponent<HingeJoint>().spring = jpsoe1_1x;


        JointSpring jpsoe1_2x = kaval2.GetComponent<HingeJoint>().spring;
        jpsoe1_2x.targetPosition = -70;
        kaval2.GetComponent<HingeJoint>().spring = jpsoe1_2x;

        //GetComponent<CapsuleCollider> ().height = 0.76f;

        durdu = true;
    }

    private void TekmeAttirici()
    {
        RaycastHit hit;

        /*	if (Physics.Raycast (kafa.transform.position, hips.transform.forward, out hit, 4f, layermask)) {
                if (hit.transform.GetComponent<PlayerController_A> ()) {
                    if (!hit.transform.GetComponent<PlayerController_A> ().dustu) {
                        if (tekrarKafaAtabilir && tekrarYumrukAtabilir && tekmeAtabilir && !dustu) {
                            Vector3 fwhp = Vector3.ProjectOnPlane (hips.transform.forward, Vector3.up).normalized;
                            Vector3 fwthp = Vector3.ProjectOnPlane (hit.transform.forward, Vector3.up).normalized;
                            float DotP = Vector3.Dot (fwhp, fwthp);
                            if (DotP >= 0.7f) {
                                float d = Mathf.Abs (Vector3.Distance (hit.transform.position, transform.position));
                                if (d > 2.8f) {
                                    if (!solKolTutucu.GetComponent<Tutunma> ().tutundu && !sagKolTutucu.GetComponent<Tutunma> ().tutundu) {
                                        TekmeAt (hit.transform);
                                    }
                                }
                            }
                            }
                        }
                    }
                }*/
    }

    bool oncet2 = true;
    void rot_hesap()
    {
        oncet2 = true;
        temp_rot = transform.rotation.y;
    }
    float temp_rot = 0;
    public Transform trgt;
    [Range(0, 40)]
    public float ziplama_deg;

    private void DonusVeYurume()
    {
        if (!GameManager_A.gameManager.gameOver)
        {
            if (!isAI)
            {

                if (donuyor)
                {
                    rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                }
                else
                {
                    rb.constraints = RigidbodyConstraints.FreezeRotation;
                }

                Vector3 forwardDir = Vector3.ProjectOnPlane(hips.transform.forward * 1f, Vector3.up);


                GetComponent<CapsuleCollider>().height = 1.03f + Mathf.Sin(Time.time * ziplama_deg) * 0.03f;

                if (force_izin && hips.GetComponent<Rigidbody>().velocity.magnitude < 4.3f)
                {
                    {
                        hips.GetComponent<Rigidbody>().velocity = hips.transform.forward * ileriForce;
                    }
                }
                if (!force_izin)
                {
                    hips.GetComponent<Rigidbody>().AddForce(hips.transform.forward * 3 * Time.deltaTime, ForceMode.VelocityChange);
                }

                if (x == 0)
                {
                    m_animator.speed = minAnimHiz;
                }
                else
                {
                    m_animator.speed = rotationAnimHiz;
                }

                //			Debug.Log (transform.rotation.y);
                if (oncet2)
                {
                    oncet2 = false;
                    Invoke("rot_hesap", 0.01f);
                }



                if (Mathf.Abs(temp_rot - transform.rotation.y) > 0.004f && !kaldiririyor && !yumrukAtiyor)
                {
                    //	Debug.Log ("--------------------kol-----------");
                    //sagKol.GetComponent<Rigidbody> ().AddForceAtPosition (-sagKolForcePoint.right * DonusOmuzForce*(temp_rot-transform.rotation.y)*100, sagKolForcePoint.position, ForceMode.Force);
                    //solKol.GetComponent<Rigidbody> ().AddForceAtPosition (-solKolForcePoint.right * DonusOmuzForce*(temp_rot-transform.rotation.y)*100, solKolForcePoint.position, ForceMode.Force);

                    sagOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(sagOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(-0.57f, 0f, -0.56f, 0.93f), Time.deltaTime * 20);
                    solOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(solOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(0.57f, 0f, 0.56f, 0.93f), Time.deltaTime * 20);

                }
                else if (!dustu && !grab && !grab2 && !kaldiririyor && !yumrukAtiyor)
                {
                    sagOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(sagOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(-0.1f, 0f, 0.2f, 1), Time.deltaTime * 5);
                    solOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(solOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(0.1f, 0f, -0.2f, 1f), Time.deltaTime * 5);
                    //_SolKolBirak2 ();
                }

                JointSpring jps = new JointSpring();
                jps.spring = 100;
                jps.targetPosition = Mathf.Sin(Time.time * 5) * 30;
                kafa.GetComponent<HingeJoint>().spring = jps;


                if (donuyor)
                {
                    hips.transform.rotation = Quaternion.Lerp(hips.transform.rotation, Quaternion.Euler(
                        hips.transform.rotation.eulerAngles.x, angle, hips.transform.rotation.eulerAngles.z), Time.deltaTime * 15f);

                }
            }
            else
            {

                //  GetComponent<CapsuleCollider>().height = 1.03f + Mathf.Sin(Time.time * ziplama_deg) * 0.03f;

                if (currentTarget)
                {
                    hips.GetComponent<Rigidbody>().maxAngularVelocity = 60;
                    Vector3 forwardDir = Vector3.ProjectOnPlane(hips.transform.forward
                                         * 1f, Vector3.up);

                    Vector3 hipsN = Vector3.ProjectOnPlane(hips.transform.up, Vector3.up);
                    hipsN = hipsN.normalized;

                    Vector3 tDir = (hips.transform.position - (agent.position + agent.forward * 3f));
                    tDir = Vector3.ProjectOnPlane(tDir, Vector3.up);
                    tDir = tDir.normalized;
                    float dotP = Vector3.Dot(tDir, hipsN);

                    if (Mathf.Abs(dotP) <= 0.1f)
                    {
                        m_animator.speed = minAnimHiz;
                    }
                    else
                    {
                        m_animator.speed = rotationAnimHiz;
                    }

                    if (Mathf.Abs(dotP) > 0.3f)
                    {
                        donuyor = true;
                    }
                    else
                    {
                        donuyor = false;
                    }
                    if (!GameManager_A.gameManager.gameOver)
                        if (donuyor)
                        {
                            rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                        }
                        else
                        {
                            rb.constraints = RigidbodyConstraints.FreezeRotation;
                        }

                    JointSpring jps = new JointSpring();
                    jps.spring = 100;
                    jps.targetPosition = Mathf.Sin(Time.time * 5) * 30;
                    kafa.GetComponent<HingeJoint>().spring = jps;

                    if (oncet2)
                    {
                        oncet2 = false;
                        Invoke("rot_hesap", 0.01f);
                    }
                    if (Mathf.Abs(temp_rot - transform.rotation.y) > 0.004f && !kaldiririyor && !dustu)
                    {
                        sagOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(sagOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(-0.57f, 0f, -0.56f, 0.93f), Time.deltaTime * 20);
                        solOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(solOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(0.57f, 0f, 0.56f, 0.93f), Time.deltaTime * 20);

                    }
                    else if (!dustu && !grab && !grab2 && !kaldiririyor)
                    {
                        sagOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(sagOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(-0.1f, 0f, 0.2f, 1), Time.deltaTime * 5);
                        solOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(solOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(0.1f, 0f, -0.2f, 1f), Time.deltaTime * 5);
                        //_SolKolBirak2 ();
                    }

                    if (currentTarget.name == "point1" || currentTarget.name == "point2" || currentTarget.name == "point3" || currentTarget.name == "point4"
                        || currentTarget.name == "point5" || currentTarget.name == "point6" || currentTarget.name == "point7" || currentTarget.name == "point8"
                        || currentTarget.name == "point9")
                    {
                        var targetPosition = agent.GetComponent<NavMeshAgent>().pathEndPosition;
                        var targetPoint = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
                        var _direction = (targetPoint - transform.position).normalized;
                        var _lookRotation = Quaternion.LookRotation(_direction);

                        //if (!duvar)
                        //    agent.transform.rotation = Quaternion.RotateTowards(agent.transform.rotation, _lookRotation, 360);

                        hips.transform.rotation = Quaternion.Lerp(hips.transform.rotation, Quaternion.Euler(
                            hips.transform.rotation.eulerAngles.x, agent.transform.rotation.eulerAngles.y, hips.transform.rotation.eulerAngles.z), Time.deltaTime * 10);
                    }
                    else
                    {
                        var targetPosition = agent.GetComponent<NavMeshAgent>().pathEndPosition;
                        var targetPoint = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
                        var _direction = (targetPoint - transform.position).normalized;
                        var _lookRotation = Quaternion.LookRotation(_direction);
                        if (SceneManager.GetActiveScene().buildIndex != 13)
                        {
                            if (!duvar)
                                agent.transform.rotation = Quaternion.RotateTowards(agent.transform.rotation, _lookRotation, 360);
                        }
                        hips.transform.rotation = Quaternion.Lerp(hips.transform.rotation, Quaternion.Euler(
                            hips.transform.rotation.eulerAngles.x, agent.transform.rotation.eulerAngles.y, hips.transform.rotation.eulerAngles.z), Time.deltaTime * 10);

                    }
                    // if (Time.timeScale == 1)
                    {
                        if (!donuyor)
                            if (force_izin && hips.GetComponent<Rigidbody>().velocity.magnitude < 4.3f)
                                hips.GetComponent<Rigidbody>().velocity = hips.transform.forward * ileriForce;

                    }
                    // if (Time.timeScale != 1)
                    // {
                    //      GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity * 0.1f;
                    //  }

                }


                /* GetComponent<CapsuleCollider>().height=1.03f+ Mathf.Sin(Time.time*ziplama_deg)*0.03f;

                if (currentTarget) {
                    hips.GetComponent<Rigidbody> ().maxAngularVelocity = 60;
                    Vector3 forwardDir = Vector3.ProjectOnPlane (hips.transform.forward
                                         * 1f, Vector3.up);

                    Vector3 hipsN = Vector3.ProjectOnPlane (hips.transform.up, Vector3.up);
                    hipsN = hipsN.normalized;

                    Vector3 tDir = (hips.transform.position - (agent.position + agent.forward * 3f));
                    tDir = Vector3.ProjectOnPlane (tDir, Vector3.up);
                    tDir = tDir.normalized;
                    float dotP = Vector3.Dot (tDir, hipsN);

                    if (Mathf.Abs (dotP) <= 0.1f) {
                        m_animator.speed = minAnimHiz;
                    } else {
                        m_animator.speed = rotationAnimHiz;
                    }

                    if (Mathf.Abs (dotP) > 0.1f) {
                        donuyor = true;
                    } else {
                        donuyor = false;
                    }
                    if (!GameManager_A.gameManager.gameOver)
                    if (donuyor) {
                        rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                    } else {
                        rb.constraints = RigidbodyConstraints.FreezeRotation;
                    }

                    JointSpring jps = new JointSpring ();
                    jps.spring = 100;
                    jps.targetPosition = Mathf.Sin (Time.time * 5) * 30;
                    kafa.GetComponent<HingeJoint> ().spring = jps;

                    if (oncet2) {
                        oncet2 = false;
                        Invoke ("rot_hesap", 0.01f);
                    }
                    if (Mathf.Abs (temp_rot - transform.rotation.y) > 0.004f && !kaldiririyor && !dustu) {
                        sagOmuz.GetComponent<ConfigurableJoint> ().targetRotation = Quaternion.Lerp (sagOmuz.GetComponent<ConfigurableJoint> ().targetRotation, new Quaternion (-0.57f, 0f, -0.56f, 0.93f), Time.deltaTime * 20);
                        solOmuz.GetComponent<ConfigurableJoint> ().targetRotation = Quaternion.Lerp (solOmuz.GetComponent<ConfigurableJoint> ().targetRotation, new Quaternion (0.57f, 0f, 0.56f, 0.93f), Time.deltaTime * 20);

                    } else if (!dustu && !grab && !grab2 && !kaldiririyor) {
                        sagOmuz.GetComponent<ConfigurableJoint> ().targetRotation = Quaternion.Lerp (sagOmuz.GetComponent<ConfigurableJoint> ().targetRotation, new Quaternion (-0.1f, 0f, 0.2f, 1), Time.deltaTime * 5);
                        solOmuz.GetComponent<ConfigurableJoint> ().targetRotation = Quaternion.Lerp (solOmuz.GetComponent<ConfigurableJoint> ().targetRotation, new Quaternion (0.1f, 0f, -0.2f, 1f), Time.deltaTime * 5);
                    }


                    if (currentTarget.name == "point1" || currentTarget.name == "point2" || currentTarget.name == "point3" || currentTarget.name == "point4" 
                        || currentTarget.name == "point5" || currentTarget.name == "point6" || currentTarget.name == "point7"|| currentTarget.name == "point8"
                        || currentTarget.name == "point9" || currentTarget.name == "point9") {

                        var targetPosition = agent.GetComponent<NavMeshAgent>().pathEndPosition;
                        var targetPoint = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
                        var _direction = (targetPoint - transform.position).normalized;
                        var _lookRotation = Quaternion.LookRotation(_direction);

                        if (!duvar)
                            agent.transform.rotation = Quaternion.RotateTowards(agent.transform.rotation, _lookRotation, 180);

                        hips.transform.rotation = Quaternion.Lerp (hips.transform.rotation, Quaternion.Euler (
                            hips.transform.rotation.eulerAngles.x, agent.transform.rotation.eulerAngles.y, hips.transform.rotation.eulerAngles.z), Time.deltaTime * 5);

                    } else {
                        var targetPosition = agent.GetComponent<NavMeshAgent> ().pathEndPosition;
                        var targetPoint = new Vector3 (targetPosition.x, transform.position.y, targetPosition.z);
                        var _direction = (targetPoint - transform.position).normalized;
                        var _lookRotation = Quaternion.LookRotation (_direction);

                        if(!duvar)
                        agent.transform.rotation = Quaternion.RotateTowards (agent.transform.rotation, _lookRotation, 180);

                    hips.transform.rotation = Quaternion.Lerp (hips.transform.rotation, Quaternion.Euler (
                            hips.transform.rotation.eulerAngles.x, agent.transform.rotation.eulerAngles.y, hips.transform.rotation.eulerAngles.z), Time.deltaTime * 5);

                    }



                    if (Time.timeScale == 1) {
                        if (!donuyor)
                        {
                            if (force_izin && hips.GetComponent<Rigidbody>().velocity.magnitude < 4.3f)
                                hips.GetComponent<Rigidbody>().velocity = hips.transform.forward * ileriForce;
                        }
                        else
                        {
                            if (!duvar)
                            {
                                if (force_izin && hips.GetComponent<Rigidbody>().velocity.magnitude < 4.3f)
                                    hips.GetComponent<Rigidbody>().velocity = hips.transform.forward * ileriForce;
                            }
                            else if (!force_izin)
                            {
                                hips.GetComponent<Rigidbody>().AddForce(hips.transform.forward * 3 * Time.deltaTime, ForceMode.VelocityChange);


                            }
                            else {//deuvar kenari
                                if (kaldiririyor)
                                    hips.GetComponent<Rigidbody>().velocity = hips.transform.forward * ileriForce;
                                else if (force_izin && hips.GetComponent<Rigidbody>().velocity.magnitude < 4.3f)
                                    hips.GetComponent<Rigidbody>().velocity = hips.transform.forward * ileriForce ;
                            }

                        }

                        if (!force_izin) {
                                    hips.GetComponent<Rigidbody> ().AddForce (hips.transform.forward* 10 * Time.deltaTime, ForceMode.VelocityChange);
                        }
                    }

                }*/
            }

        }

    }

    private void HareketeEkForcelar()
    {

        if (p1.GetComponent<HingeJoint>().spring.targetPosition >= 25)
        {

            //	sagOmuz.GetComponent<Rigidbody> ().AddForceAtPosition (sagOmuz.transform.forward * yuruyuzOmuzForce,solOmuz.transform.position, ForceMode.Impulse);

        }
        if (p2.GetComponent<HingeJoint>().spring.targetPosition >= 25)
        {

            //	solOmuz.GetComponent<Rigidbody> ().AddForceAtPosition (solOmuz.transform.forward * yuruyuzOmuzForce,solOmuz.transform.position, ForceMode.Impulse);
        }

    }
    #region sagsolKolHareket

    float timerKol = -0.4f;
    float timerSag = 0;
    float timerSol = 0;

    private void KollarHareket()
    {
        //sagKola sol bacak sol kola sag bacak
        int donusM = 1;
        if (GetComponent<Rigidbody>().angularVelocity.magnitude > donusAngularVelocityTreshold)
        {
            donusM = 0;
        }

        timerKol += Time.deltaTime;
        if (timerKol >= 0.4f)
            timerKol = -0.4f;

        if (timerKol <= 0)
        {
            timerSol = 0;
            timerSag += Time.fixedDeltaTime;
            JointSpring sp1 = sagKol.GetComponent<HingeJoint>().spring;
            sp1.targetPosition = Mathf.Clamp(Mathf.Sin(timerSag * 10) * 60f, -10, 60);
            sp1.spring = Mathf.Clamp(p1.transform.GetComponent<HingeJoint>().spring.spring / 2 * donusM, 0, 100);
            sagKol.GetComponent<HingeJoint>().spring = sp1;

            sagOmuz.GetComponent<ConfigurableJoint>().targetRotation =
            Quaternion.Lerp(sagOmuz.GetComponent<ConfigurableJoint>().targetRotation
                    , new Quaternion(sagOmuz.GetComponent<ConfigurableJoint>().targetRotation.x, Mathf.Sin(timerSag * 10)
                        , sagOmuz.GetComponent<ConfigurableJoint>().targetRotation.z, 1), Time.deltaTime * 5);
            /*  ********  onemliii   ******************   

			JointSpring sp3 = sagOmuz.GetComponent<HingeJoint> ().spring;
			sagOmuz.GetComponent<HingeJoint> ().spring = sp3;*/
        }
        else
        {
            timerSol += Time.deltaTime;
            timerSag = 0;
            JointSpring sp2 = solKol.GetComponent<HingeJoint>().spring;
            sp2.targetPosition = Mathf.Clamp(Mathf.Sin(timerSol * 10) * 60f, -10, 60);
            sp2.spring = Mathf.Clamp(p2.transform.GetComponent<HingeJoint>().spring.spring / 2 * donusM, 0, 100);
            solKol.GetComponent<HingeJoint>().spring = sp2;

            solOmuz.GetComponent<ConfigurableJoint>().targetRotation =
            Quaternion.Lerp(solOmuz.GetComponent<ConfigurableJoint>().targetRotation
                , new Quaternion(solOmuz.GetComponent<ConfigurableJoint>().targetRotation.x, Mathf.Sin(timerSol * 10)
                                , solOmuz.GetComponent<ConfigurableJoint>().targetRotation.z, 1), Time.deltaTime * 10);


            /*  ********  onemliii   ******************   
			JointSpring sp4 = solOmuz.GetComponent<ConfigurableJoint> ().spring;
			solOmuz.GetComponent<ConfigurableJoint> ().spring = sp4; *////
        }
    }
    #endregion
    float temp = 1;
    Vector3 pos1;
    float dist;
    bool degg = true;

    IEnumerator sayac()
    {

        while (temp > 0.94f)
        {
            temp -= 0.03f;
            dist = Vector3.Distance(pos1, transform.position);
            yield return new WaitForSeconds(0.03f);
        }

        degg = true;
        temp = 1;
    }

    private void OransalDegerler()
    {
        if (degg)
        {
            pos1 = transform.position;
            degg = false;


            //          if(!isAI)
            //            Debug.Log("dist" + dist);



            JointSpring sp1 = kaval1.GetComponent<HingeJoint>().spring;
            sp1.spring = 600 / (1f / dist);
            sp1.damper = 20;
            kaval1.GetComponent<HingeJoint>().spring = sp1;

            JointSpring sp2 = kaval2.GetComponent<HingeJoint>().spring;
            sp2.spring = 600 / (1f / dist);
            sp2.damper = 20;
            kaval2.GetComponent<HingeJoint>().spring = sp2;

            JointSpring sp3 = kaval1.transform.parent.GetComponent<HingeJoint>().spring;
            sp3.spring = 600 / (1f / dist);
            sp3.damper = 20;
            kaval1.transform.parent.GetComponent<HingeJoint>().spring = sp3;

            JointSpring sp4 = kaval2.transform.parent.GetComponent<HingeJoint>().spring;
            sp4.spring = 600 / (1f / dist);
            sp4.damper = 20;
            kaval2.transform.parent.GetComponent<HingeJoint>().spring = sp4;


            StartCoroutine(sayac());

        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.relativeVelocity.magnitude > bayilmaHealth && !kaldiririyor)
        {
            //Dusus();
        }

        if (col.transform.root.childCount > 1)
        {

            if (col.transform.root.GetChild(0).GetComponent<PlayerController_A>())
            {
                if (col.transform.root.GetChild(0).GetComponent<PlayerController_A>().tekme_havada)
                {
                    if (!dustu)
                    {
                        if (col.relativeVelocity.magnitude >= 3f)
                        {
                            //	if (!isAI)
                            //		Camera.main.GetComponent<Kamera_A> ().CameraShaker ();
                            //	TekmeYemeDusus ();
                            //	gogus.GetComponent<Rigidbody> ().AddForce ((gogus.transform.position - col.transform.position) * 600f, ForceMode.Impulse);
                            enSonTutan = col.transform.root.GetChild(0).GetComponent<PlayerController_A>();
                        }
                    }
                }
            }
        }

    }

    #region TekmeYemeDusus
    private void TekmeYemeDusus()
    {
        /*if (!dustu) {
			dustu = true;
			rb.constraints = RigidbodyConstraints.None;
			p1.stop = true;
			p2.stop = true;
			p3.stop = true;
			p4.stop = true;
			Invoke ("TekmeKalkis", 1f);
			Invoke ("AgirlikHafiflet", 0.5f);
			KollariSerbestBirak ();
			ForceEkleyicileriInitializeEt ();
		}*/
    }

    private void TekmeKalkis()
    {
        StartCoroutine(_TekmeKalkisYavas());
    }

    IEnumerator _TekmeKalkisYavas()
    {
        float kalkisTime = 0;
        Vector3 lastPos = hips.transform.position;
        sleepParticle.SetActive(false);
        while (kalkisTime <= 1.5f && !GameManager_A.gameManager.gameOver)
        {
            kalkisTime += Time.deltaTime;
            if (GetComponent<Rigidbody>().velocity.y <= 1f)
                kafa.GetComponent<Rigidbody>().AddForce(Vector3.up * kafa.GetComponent<Rigidbody>().mass * 80f);
            lastPos = hips.transform.position;
            yield return Time.deltaTime;
        }
        if (!GameManager_A.gameManager.gameOver)
        {
            transform.position = lastPos + new Vector3(0, hips.GetComponent<CapsuleCollider>().height / 2f + 0.5f, 0);
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, -90f);
            rb.constraints = RigidbodyConstraints.FreezeRotationZ;
            if (GetComponent<Rigidbody>().mass < 80)
                TekrarEskiAgirliginaGetir(transform);
            ForceEkleyicileriInitializeEt();
            p1.stop = false;
            p2.stop = false;
            p3.stop = false;
            p4.stop = false;
            //dustu = false;
            if (isAI)
                yeniKalkti = false;
            JointLimits jpsbl = gogus.GetComponent<HingeJoint>().limits;
            jpsbl.max = belJoint.maxLimit_i;
            jpsbl.min = belJoint.minLimit_i;
            gogus.GetComponent<HingeJoint>().limits = jpsbl;
        }
    }
    #endregion

    #region Bayilma
    public void Dusus()
    {
        if (!dustu)
        {
            if (!isAI)
            {
                GameManager_A.gameManager.BayilmaEfektor();
            }
            dustu = true;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;// RigidbodyConstraints.None;

            /*p1.stop = true;
			p2.stop = true;
			p3.stop = true;
			p4.stop = true;*/

            //Invoke ("Kalkis", kalkisSuresi);

            Invoke("AgirlikHafiflet", 0.02f);
            //KollariSerbestBirak ();
            ForceEkleyicileriInitializeEt();
            sleepParticle.SetActive(true);
            SpringleriKapat();
            //GameManager_A.gameManager.BayginOyuncuEkle (this);
        }
    }

    public void Dusus_yumruk()
    {
        if (!dustu)
        {
            if (!isAI)
            {
                GameManager_A.gameManager.BayilmaEfektor();
            }
            dustu = true;


            rb.constraints = RigidbodyConstraints.None;

            /*p1.stop = true;
				p2.stop = true;
				p3.stop = true;
				p4.stop = true;*/

            //Invoke ("Kalkis", kalkisSuresi);

            Invoke("AgirlikHafiflet", 0.02f);
            //KollariSerbestBirak ();
            ForceEkleyicileriInitializeEt();
            sleepParticle.SetActive(true);
            //GameManager_A.gameManager.BayginOyuncuEkle (this);
            yatiyor = true;
        }
    }

    private void LimitAc()
    {
        JointLimits jpsbl = transform.GetChild(0).GetComponent<HingeJoint>().limits;
        jpsbl.max = 40;
        jpsbl.min = -40;
        transform.GetChild(0).GetComponent<HingeJoint>().limits = jpsbl;

        /*JointLimits jpsk = kafa.GetComponent<HingeJoint>().limits;
		jpsk.max = 70;
		jpsk.min = -70;
		kafa.GetComponent<HingeJoint> ().limits = jpsk;*/
    }

    void Kalkis()
    {
        StartCoroutine(_KalkisYavas());
    }
    public float kalkisTime = 0;

    IEnumerator _KalkisYavas()
    {
        kalkisTime = 0;
        Vector3 lastPos = hips.transform.position;

        if (!isAI)
            GameManager_A.gameManager.SpawnKurtuldu();

        sleepParticle.SetActive(false);
        while (kalkisTime <= 0.3f && !GameManager_A.gameManager.gameOver)
        {
            kalkisTime += Time.deltaTime;
            //if(GetComponent<Rigidbody>().velocity.y<=1f)
            //kafa.GetComponent<Rigidbody> ().AddForce (Vector3.up*kafa.GetComponent<Rigidbody>().mass*80f);
            lastPos = hips.transform.position;
            yield return Time.deltaTime;
        }
        if (!GameManager_A.gameManager.gameOver)
        {
            //transform.position = lastPos + new Vector3 (0, hips.GetComponent<CapsuleCollider> ().height / 2f, 0);
            //transform.localEulerAngles = new Vector3 (0, transform.localEulerAngles.y, -90f);
            rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            bayilmaHealth = initialBayilmaHealth;
            if (GetComponent<Rigidbody>().mass < 80)
                TekrarEskiAgirliginaGetir(transform);
            ForceEkleyicileriInitializeEt();
            p1.stop = false;
            p2.stop = false;
            p3.stop = false;
            p4.stop = false;
            dustu = false;
            if (isAI)
                yeniKalkti = false;
            JointLimits jpsbl = gogus.GetComponent<HingeJoint>().limits;
            jpsbl.max = belJoint.maxLimit_i;
            jpsbl.min = belJoint.minLimit_i;
            gogus.GetComponent<HingeJoint>().limits = jpsbl;
        }
        once_yer = true;
        yerde = false;
        yatiyor = false;
        GameManager_A.gameManager.BayginOyuncuCikar(this);
    }

    #endregion

    #region OyunBittiHareketi
    // Oyun bitisinde kazanan kalkar
    public void GameOverKalkis()
    {
        if (dustu)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + hips.GetComponent<CapsuleCollider>().height / 2f + 3f, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.parent.position.y + hips.GetComponent<CapsuleCollider>().height / 2f + 3f, transform.position.z);
        }
        //transform.localEulerAngles = new Vector3 (0, transform.localEulerAngles.y, -90f);
        transform.LookAt(Camera.main.transform);
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        kaldiririyor = false;
        //rb.isKinematic = true;
        bayilmaHealth = initialBayilmaHealth;
        //dustu = true;
        sleepParticle.SetActive(false);
        if (GetComponent<Rigidbody>().mass < 80)
            TekrarEskiAgirliginaGetir(transform);
        ForceEkleyicileriInitializeEt();
        p1.stop = true;
        p2.stop = true;
        p3.stop = true;
        p4.stop = true;

        if (isAI)
            yeniKalkti = false;
        JointLimits jpsbl = gogus.GetComponent<HingeJoint>().limits;
        jpsbl.max = belJoint.maxLimit_i;
        jpsbl.min = belJoint.minLimit_i;
        gogus.GetComponent<HingeJoint>().limits = jpsbl;
        StartCoroutine(KazananHareketi());
    }
    // Oyun sonunda kazanan kollarını havaya kaldırır
    IEnumerator KazananHareketi()
    {

        float timer = 0;
        kaldiririyor = true;
        while (timer <= 200f)
        {
            transform.LookAt(Camera.main.transform);
            timer += Time.deltaTime;
            float kaldirmaLerpSpeed = 5f;

            sagOmuz.GetComponent<ConfigurableJoint>().targetRotation =
            Quaternion.Lerp(sagOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(0, 0, 0.26f, 1), Time.deltaTime);

            solOmuz.GetComponent<ConfigurableJoint>().targetRotation =
                Quaternion.Lerp(solOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(0, 0, -0.26f, 1), Time.deltaTime);


            JointSpring jpsoe1_1 = sagOmuzEk.GetComponent<HingeJoint>().spring;
            jpsoe1_1.spring += sagKolJointler[0].spring * 0.025f;
            jpsoe1_1.damper = sagKolJointler[0].damper;
            jpsoe1_1.targetPosition = Mathf.Lerp(jpsoe1_1.targetPosition, sagKolJointler[0].tp, Time.deltaTime * kaldirmaLerpSpeed);
            sagOmuzEk.GetComponent<HingeJoint>().spring = jpsoe1_1;

            JointLimits jpsoel1_1 = sagOmuzEk.GetComponent<HingeJoint>().limits;
            jpsoel1_1.max = sagKolJointler[0].maxLimit;
            jpsoel1_1.min = sagKolJointler[0].minLimit;
            sagOmuzEk.GetComponent<HingeJoint>().limits = jpsoel1_1;


            /*JointSpring jpsoe2_1 = sagOmuz.GetComponent<HingeJoint>().spring;
			jpsoe2_1.spring += sagKolJointler [1].spring*0.025f;
			jpsoe2_1.damper = sagKolJointler [1].damper;
			jpsoe2_1.targetPosition = Mathf.Lerp(jpsoe2_1.targetPosition, sagKolJointler[1].tp, Time.deltaTime * kaldirmaLerpSpeed);
			sagOmuz.GetComponent<HingeJoint> ().spring = jpsoe2_1;

			JointLimits jpsoel2_1 = sagOmuz.GetComponent<HingeJoint>().limits;
			jpsoel2_1.max = sagKolJointler [1].maxLimit;
			jpsoel2_1.min = sagKolJointler [1].minLimit;
			sagOmuz.GetComponent<HingeJoint> ().limits = jpsoel2_1;*/


            JointSpring jpsoe3_1 = sagKol.GetComponent<HingeJoint>().spring;
            jpsoe3_1.spring += sagKolJointler[2].spring * 0.025f;
            jpsoe3_1.damper = sagKolJointler[2].damper;
            jpsoe3_1.targetPosition = Mathf.Lerp(jpsoe3_1.targetPosition, sagKolJointler[2].tp, Time.deltaTime * kaldirmaLerpSpeed);
            sagKol.GetComponent<HingeJoint>().spring = jpsoe3_1;

            JointLimits jpsoel3_1 = sagKol.GetComponent<HingeJoint>().limits;
            jpsoel3_1.max = sagKolJointler[2].maxLimit;
            jpsoel3_1.min = sagKolJointler[2].minLimit;
            sagKol.GetComponent<HingeJoint>().limits = jpsoel3_1;



            JointSpring jpsoe1_2 = solOmuzEk.GetComponent<HingeJoint>().spring;
            jpsoe1_2.spring += solKolJointler[0].spring * 0.025f;
            jpsoe1_2.damper = solKolJointler[0].damper;
            jpsoe1_2.targetPosition = Mathf.Lerp(jpsoe1_2.targetPosition, solKolJointler[0].tp, Time.deltaTime * kaldirmaLerpSpeed);
            solOmuzEk.GetComponent<HingeJoint>().spring = jpsoe1_2;

            JointLimits jpsoel1_2 = solOmuzEk.GetComponent<HingeJoint>().limits;
            jpsoel1_2.max = solKolJointler[0].maxLimit;
            jpsoel1_2.min = solKolJointler[0].minLimit;
            solOmuzEk.GetComponent<HingeJoint>().limits = jpsoel1_2;


            /*JointSpring jpsoe2_2 = solOmuz.GetComponent<HingeJoint>().spring;
			jpsoe2_2.spring += solKolJointler [1].spring*0.025f;
			jpsoe2_2.damper = solKolJointler [1].damper;
			jpsoe2_2.targetPosition = Mathf.Lerp(jpsoe2_2.targetPosition, solKolJointler[1].tp, Time.deltaTime * kaldirmaLerpSpeed);
			solOmuz.GetComponent<HingeJoint> ().spring = jpsoe2_2;

			JointLimits jpsoel2_2 = solOmuz.GetComponent<HingeJoint>().limits;
			jpsoel2_2.max = solKolJointler [1].maxLimit;
			jpsoel2_2.min = solKolJointler [1].minLimit;
			solOmuz.GetComponent<HingeJoint> ().limits = jpsoel2_2;*/

            JointSpring jpsoe3_2 = solKol.GetComponent<HingeJoint>().spring;
            jpsoe3_2.spring += solKolJointler[2].spring * 0.025f;
            jpsoe3_2.damper = solKolJointler[2].damper;
            jpsoe3_2.targetPosition = Mathf.Lerp(jpsoe3_2.targetPosition, sagKolJointler[2].tp, Time.deltaTime * kaldirmaLerpSpeed);
            solKol.GetComponent<HingeJoint>().spring = jpsoe3_2;

            JointLimits jpsoel3_2 = solKol.GetComponent<HingeJoint>().limits;
            jpsoel3_2.max = solKolJointler[2].maxLimit;
            jpsoel3_2.min = solKolJointler[2].minLimit;
            solKol.GetComponent<HingeJoint>().limits = jpsoel3_2;

            JointSpring jpsb = gogus.GetComponent<HingeJoint>().spring;
            jpsb.spring += belJoint.spring * 0.025f;
            jpsb.damper = belJoint.damper;
            jpsb.targetPosition = Mathf.Lerp(jpsb.targetPosition, belJoint.tp, Time.deltaTime * kaldirmaLerpSpeed);
            gogus.GetComponent<HingeJoint>().spring = jpsb;

            JointLimits jpsbl = gogus.GetComponent<HingeJoint>().limits;
            jpsbl.max = belJoint.maxLimit;
            jpsbl.min = belJoint.minLimit;
            gogus.GetComponent<HingeJoint>().limits = jpsbl;

            JointSpring jpsb1 = p1.GetComponent<HingeJoint>().spring;
            jpsb1.targetPosition = 0;
            p1.GetComponent<HingeJoint>().spring = jpsb1;

            JointSpring jpsb2 = p2.GetComponent<HingeJoint>().spring;
            jpsb2.targetPosition = 0;
            p2.GetComponent<HingeJoint>().spring = jpsb2;

            JointSpring jpsb3 = p3.GetComponent<HingeJoint>().spring;
            jpsb3.targetPosition = 0;
            p3.GetComponent<HingeJoint>().spring = jpsb3;

            JointSpring jpsb4 = p4.GetComponent<HingeJoint>().spring;
            jpsb4.targetPosition = 0;
            p4.GetComponent<HingeJoint>().spring = jpsb4;

            yield return Time.deltaTime;
        }

    }
    #endregion


    void grab_false()
    {
        once = true;
        grab = false;
    }

    public int grab_seviye = 4;
    bool once = true;
    int sag_yum = 0;

    bool yumruk = true;

    IEnumerator yumruk_say()
    {
        sag_yum++;
        yield return new WaitForSecondsRealtime(yumruksure);
        yumruk = true;
    }
    bool enter = false;
    /*void OnTriggerStay(Collider other){
        if (!other.isTrigger)
        {
            if (other.GetComponent<PlayerController_A>())
            {

                int rnd_grab = Random.Range(1, grab_seviye);
                if (isAI && once && !GameManager_A.gameManager.gameOver)
                {
                    if (rnd_grab == 1)
                    {
                        grab = true;
                    }

                    Invoke("grab_false", 1);
                    once = false;
                }
                if (!dustu && grab && !isAI)
                {
                    Debug.Log("uzaniyor");
                    TutmakIcinUzan(other.transform);
                }
                else if (!dustu && grab && isAI)
                {
                    TutmakIcinUzan(other.transform);

                }
            }
        } 
	}*/

    void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<PlayerController_A>() || other.GetComponent<FirlatmaTrigger>())
        {
            temp_col = other;
            enter = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController_A>())
        {
            // temp_col = null;
            enter = false;
        }

    }





    private void KafayaForceVer()
    {
        float r1 = Random.Range(-1f, 1f);
        float r2 = Random.Range(-1f, 1f);
        //	kafa.GetComponent<Rigidbody> ().AddForce (new Vector3 (0,r1,r2) * 500f, ForceMode.Impulse);
    }

    private void TutmakIcinUzan(Transform other)
    {
        if (!kaldiririyor)
        {
            if (!sagKolTutucu.GetComponent<Tutunma>().tutundu)
            {
                Vector3 dirSagKol = other.transform.position - sagKolForcePoint.position;
                //				Debug.Log ("SagKolTutundur");
                sagKol.GetComponent<Rigidbody>().AddForceAtPosition(dirSagKol * 200f, sagKolForcePoint.position);
            }

            if (!solKolTutucu.GetComponent<Tutunma>().tutundu)
            {
                //			Debug.Log ("SagKolTutundur");
                Vector3 dirSolKol = other.transform.position - solKolForcePoint.position;
                solKol.GetComponent<Rigidbody>().AddForceAtPosition(dirSolKol * 200f, solKolForcePoint.position);
            }
        }
    }

    #region KafaAtma
    private void KafaAtma(Vector3 targetPos)
    {
        //	if(tekrarKafaAtabilir)
        //	KafaAtmaGerilme();
    }

    private void KafaAtmaGerilme()
    {
        StartCoroutine(_KafaAtmaGerilme());
    }

    IEnumerator _KafaAtmaGerilme()
    {

        tekrarKafaAtabilir = false;
        kafaAtiyor = true;
        float kafagerilmesphiz = kafagerilmespringhiz;
        float kafagerilmetphiz = kafagerilmetargetposhiz;
        float timer = 0;
        JointLimits gjl = gogus.GetComponent<HingeJoint>().limits;
        gjl.max = 60;
        gjl.min = -60;
        gogus.GetComponent<HingeJoint>().limits = gjl;

        while (timer <= kafagerilmesure)
        {
            timer += Time.deltaTime;
            JointSpring gj = gogus.GetComponent<HingeJoint>().spring;
            gj.spring = Mathf.Lerp(gj.spring, 2000f, Time.deltaTime * kafagerilmesphiz);
            gj.targetPosition = Mathf.Lerp(gj.targetPosition, -30f, Time.deltaTime * kafagerilmetphiz);
            gogus.GetComponent<HingeJoint>().spring = gj;
            yield return Time.deltaTime;
        }
        Invoke("KafaAtmaVurus", 0.05f);
    }

    public void KafaAtmaVurus()
    {
        StartCoroutine(_KafaAtmaVurus());
    }

    IEnumerator _KafaAtmaVurus()
    {

        JointLimits gjl = gogus.GetComponent<HingeJoint>().limits;
        gjl.max = 60;
        gjl.min = -60;
        gogus.GetComponent<HingeJoint>().limits = gjl;

        float kafaatmasphiz = kafavuruspringhiz;
        float kafaatmatphiz = kafavurustargetposhiz;
        float timer = 0;
        while (timer <= kafavurussure)
        {
            timer += Time.deltaTime;
            JointSpring gj = gogus.GetComponent<HingeJoint>().spring;
            gj.spring = Mathf.Lerp(gj.spring, 2000f, Time.deltaTime * kafaatmasphiz);
            gj.targetPosition = Mathf.Lerp(gj.targetPosition, 90, Time.deltaTime * kafaatmatphiz);
            gogus.GetComponent<HingeJoint>().spring = gj;
            yield return Time.deltaTime;
        }
        kafa.GetComponent<ForceEkleyici>().isEnabled = true;
        Invoke("KafaAtmaToparlama", 0.05f);
    }

    public void KafaAtmaToparlama()
    {
        StartCoroutine(_KafaAtmaToparlama());
    }

    IEnumerator _KafaAtmaToparlama()
    {
        kafa.GetComponent<ForceEkleyici>().isEnabled = false;

        float timer = 0;
        float kafatoparlamasphiz = 10f;
        float kafatoparlamatphiz = 10f;
        while (timer <= 0.2f)
        {
            timer += Time.deltaTime;
            JointSpring gj = gogus.GetComponent<HingeJoint>().spring;
            gj.spring = Mathf.Lerp(gj.spring, 400f, Time.deltaTime * kafatoparlamasphiz);
            gj.targetPosition = Mathf.Lerp(gj.targetPosition, 0, Time.deltaTime * kafatoparlamatphiz);
            gogus.GetComponent<HingeJoint>().spring = gj;
            yield return Time.deltaTime;
        }

        JointSpring _gj = gogus.GetComponent<HingeJoint>().spring;
        _gj.spring = 400f;
        _gj.targetPosition = 0;
        gogus.GetComponent<HingeJoint>().spring = _gj;

        JointLimits gjl = gogus.GetComponent<HingeJoint>().limits;
        gjl.max = 30;
        gjl.min = -30;
        gogus.GetComponent<HingeJoint>().limits = gjl;
        sag_yum++;
        kafaAtiyor = false;
        Invoke("TekrarKafaAtabilir", 0.05f);
    }

    private void TekrarKafaAtabilir()
    {
        tekrarKafaAtabilir = true;
    }
    #endregion

    #region YumrukAtma
    private void SagKolYumrukAtma(Transform target)
    {
        if (tekrarYumrukAtabilir)
            SagKolYumrukGerilme(target);
    }

    private void SagKolYumrukGerilme(Transform target)
    {

        StartCoroutine(_SagKolYumrukGerilme(target));
    }

    #region YumrukAtma
    private void SagKolYumrukAtma2(Transform target)
    {
        //	if (tekrarYumrukAtabilir)
        SagKolYumrukGerilme2(target);
    }

    private void SagKolYumrukGerilme2(Transform target)
    {

        StartCoroutine(_SagKolYumrukGerilme2(target));
    }

    IEnumerator _SagKolYumrukGerilme2(Transform target)
    {
        tekrarYumrukAtabilir = false;
        yumrukAtiyor = true;
        float timer = 0;
        float gerilmespringhiz = saggerilmespringhiz;
        float gerilmetphiz = saggerilmetargetposhiz;

        if (kaldiririyor)
        {
            timer = sagvurussure + 1;
        }

        while (timer <= saggerilmesure)
        {
            timer += Time.deltaTime;

            JointSpring gjske = sagOmuzEk.GetComponent<HingeJoint>().spring;
            gjske.spring = Mathf.Lerp(gjske.spring, sagKolJointlerYumruk[0].spring, Time.deltaTime * gerilmespringhiz);
            gjske.targetPosition = Mathf.Lerp(gjske.targetPosition, sagKolJointlerYumruk[0].tp, Time.deltaTime * gerilmetphiz);
            gjske.damper = sagKolJointlerYumruk[0].damper;
            sagOmuzEk.GetComponent<HingeJoint>().spring = gjske;

            JointLimits gjskel = sagOmuzEk.GetComponent<HingeJoint>().limits;
            gjskel.max = sagKolJointlerYumruk[0].maxLimit;
            gjskel.min = sagKolJointlerYumruk[0].minLimit;
            sagOmuzEk.GetComponent<HingeJoint>().limits = gjskel;


            sagOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(sagOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(targetposx, targetposy, targetposz, targetposw)
                /*(-0.45f,-0.1f,0.36f,1)*/, Time.deltaTime * sagyumruksure);
            JointDrive spr = new JointDrive();
            spr.positionSpring = sagyumrukspring;
            spr.positionDamper = 10;
            spr.maximumForce = Mathf.Infinity;
            sagOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr;
            sagOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr;


            /*JointSpring gj = sagOmuz.GetComponent<HingeJoint>().spring;
			gj.spring = Mathf.Lerp(gj.spring, sagKolJointlerYumruk[1].spring, Time.deltaTime * gerilmespringhiz);
			gj.targetPosition = Mathf.Lerp(gj.targetPosition, sagKolJointlerYumruk[1].tp, Time.deltaTime * gerilmetphiz);
			gj.damper = sagKolJointlerYumruk[1].damper;
			sagOmuz.GetComponent<HingeJoint>().spring = gj;

			JointLimits gjl = sagOmuz.GetComponent<HingeJoint>().limits;
			gjl.max = sagKolJointlerYumruk[1].maxLimit;
			gjl.min = sagKolJointlerYumruk[1].minLimit;
			sagOmuz.GetComponent<HingeJoint>().limits = gjl;*/


            JointSpring gjsk = sagKol.GetComponent<HingeJoint>().spring;
            gjsk.spring = Mathf.Lerp(gjsk.spring, sagKolJointlerYumruk[2].spring, Time.deltaTime * gerilmetphiz);
            gjsk.targetPosition = Mathf.Lerp(gjsk.targetPosition, 68.4f, Time.deltaTime * 20);
            gjsk.damper = sagKolJointlerYumruk[2].damper;
            sagKol.GetComponent<HingeJoint>().spring = gjsk;

            /*JointLimits gjkl = sagOmuz.GetComponent<HingeJoint>().limits;
			gjkl.max = sagKolJointlerYumruk[2].maxLimit;
			gjkl.min = sagKolJointlerYumruk[2].minLimit;
			sagOmuz.GetComponent<HingeJoint>().limits = gjkl;*/

            JointSpring belj = gogus.GetComponent<HingeJoint>().spring;
            belj.spring = Mathf.Lerp(belj.spring, belJointDegerlerYumruk[0].spring, Time.deltaTime * gerilmespringhiz);
            belj.targetPosition = Mathf.Lerp(belj.targetPosition, belJointDegerlerYumruk[0].tp, Time.deltaTime * gerilmetphiz);
            belj.damper = belJointDegerlerYumruk[0].damper;
            gogus.GetComponent<HingeJoint>().spring = belj;

            JointSpring gjskx = transform.GetChild(0).GetComponent<HingeJoint>().spring;
            gjskx.spring = 20000;// Mathf.Lerp(gjsk.spring, sagKolJointlerYumruk[2].spring, Time.deltaTime * gerilmetphiz);
            gjskx.targetPosition = 0;// Mathf.Lerp(gjsk.targetPosition, sagKolJointlerYumruk[2].tp, Time.deltaTime * gerilmetphiz);
            gjskx.damper = 1;// sagKolJointlerYumruk[2].damper;
            transform.GetChild(0).GetComponent<HingeJoint>().spring = gjskx;

            yield return Time.deltaTime;
        }


        SagKolYumrukAtis2(target);
    }

    private void SagKolYumrukAtis2(Transform target)
    {
        StartCoroutine(_SagKolYumrukAtis2(target));
    }

    IEnumerator _SagKolYumrukAtis2(Transform target)
    {

        float atissphiz = sagvuruspringhiz;
        float atistphiz = sagvurustargetposhiz;
        float timer = 0;

        if (kaldiririyor)
        {
            timer = sagvurussure + 1;
        }


        while (timer <= sagvurussure)
        {
            timer += Time.deltaTime;

            JointSpring gjske = sagOmuzEk.GetComponent<HingeJoint>().spring;
            gjske.spring = Mathf.Lerp(gjske.spring, sagKolJointlerYumruk[3].spring, Time.deltaTime * atissphiz);
            gjske.targetPosition = Mathf.Lerp(gjske.targetPosition, sagKolJointlerYumruk[3].tp, Time.deltaTime * atistphiz);
            gjske.damper = sagKolJointlerYumruk[3].damper;
            sagOmuzEk.GetComponent<HingeJoint>().spring = gjske;

            JointLimits gjskel = sagOmuzEk.GetComponent<HingeJoint>().limits;
            gjskel.max = sagKolJointlerYumruk[3].maxLimit;
            gjskel.min = sagKolJointlerYumruk[3].minLimit;
            sagOmuzEk.GetComponent<HingeJoint>().limits = gjskel;



            /*JointSpring gj = sagOmuz.GetComponent<HingeJoint>().spring;
			gj.spring = Mathf.Lerp(gj.spring, sagKolJointlerYumruk[4].spring, Time.deltaTime * atissphiz);
			gj.targetPosition = Mathf.Lerp(gj.targetPosition, sagKolJointlerYumruk[4].tp, Time.deltaTime * atistphiz);
			gj.damper = sagKolJointlerYumruk[4].damper;
			sagOmuz.GetComponent<HingeJoint>().spring = gj;

			JointLimits gjl = sagOmuz.GetComponent<HingeJoint>().limits;
			gjl.max = sagKolJointlerYumruk[4].maxLimit;
			gjl.min = sagKolJointlerYumruk[4].minLimit;
			sagOmuz.GetComponent<HingeJoint>().limits = gjl;*/


            sagOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(sagOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(targetposx2, targetposy2, targetposz2, targetposw2)
                /*new Quaternion(-0.45f,-1f,0.36f,0.48f)*/, Time.deltaTime * sagyumruksure);
            //sagKol.GetComponent<Rigidbody> ().AddForce (sagKol.transform.forward * forcee, ForceMode.Impulse);

            JointDrive spr = new JointDrive();
            spr.positionSpring = sagyumrukspring;
            spr.positionDamper = 10;
            spr.maximumForce = Mathf.Infinity;
            sagOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr;
            sagOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr;



            JointSpring gjsk = sagKol.GetComponent<HingeJoint>().spring;
            gjsk.spring = 2000;// Mathf.Lerp(gjsk.spring, sagKolJointlerYumruk[5].spring, Time.deltaTime * atissphiz);
            gjsk.targetPosition = Mathf.Lerp(gjsk.targetPosition, 0, Time.deltaTime * 20);
            gjsk.damper = 10;//sagKolJointlerYumruk[5].damper;
            sagKol.GetComponent<HingeJoint>().spring = gjsk;

            JointLimits gjkl = sagKol.GetComponent<HingeJoint>().limits;
            gjkl.max = sagKolJointlerYumruk[5].maxLimit;
            gjkl.min = sagKolJointlerYumruk[5].minLimit;
            sagKol.GetComponent<HingeJoint>().limits = gjkl;

            JointSpring gjskx = transform.GetChild(0).GetComponent<HingeJoint>().spring;
            gjskx.spring = 20000;// Mathf.Lerp(gjsk.spring, sagKolJointlerYumruk[2].spring, Time.deltaTime * gerilmetphiz);
            gjskx.targetPosition = 0;// Mathf.Lerp(gjsk.targetPosition, sagKolJointlerYumruk[2].tp, Time.deltaTime * gerilmetphiz);
            gjskx.damper = 1;// sagKolJointlerYumruk[2].damper;
            transform.GetChild(0).GetComponent<HingeJoint>().spring = gjskx;


            JointSpring belj = gogus.GetComponent<HingeJoint>().spring;
            belj.spring = Mathf.Lerp(belj.spring, belJointDegerlerYumruk[1].spring, Time.deltaTime * atissphiz);
            belj.targetPosition = Mathf.Lerp(belj.targetPosition, belJointDegerlerYumruk[1].tp, Time.deltaTime * atistphiz);
            belj.damper = belJointDegerlerYumruk[1].damper;
            gogus.GetComponent<HingeJoint>().spring = belj;
            yield return Time.deltaTime;
        }

        //sagKol.GetComponent<ForceEkleyici> ().isEnabled = true;

        Invoke("SagKolToparlama", 0.1f);
        //SagKolToparlama();
    }


    private void SolKolYumrukAtma2(Transform target)
    {
        //if (tekrarYumrukAtabilir)
        SolKolYumrukGerilme2(target);
    }

    private void SolKolYumrukGerilme2(Transform target)
    {
        StartCoroutine(_SolKolYumrukGerilme2(target));
    }

    IEnumerator _SolKolYumrukGerilme2(Transform target)
    {

        tekrarYumrukAtabilir = false;
        yumrukAtiyor = true;

        float gerilmesphiz = solgerilmespringhiz;
        float gerilmetphiz = solgerilmetargetposhiz;
        float timer = 0;

        if (kaldiririyor)
        {
            timer = solvurussure + 1;
        }

        while (timer < solgerilmesure)
        {

            timer += Time.deltaTime;
            JointSpring gjske = solOmuzEk.GetComponent<HingeJoint>().spring;
            gjske.spring = Mathf.Lerp(gjske.spring, solKolJointlerYumruk[0].spring, Time.deltaTime * gerilmesphiz);
            gjske.targetPosition = Mathf.Lerp(gjske.targetPosition, solKolJointlerYumruk[0].tp, Time.deltaTime * gerilmetphiz);
            gjske.damper = solKolJointlerYumruk[0].damper;
            solOmuzEk.GetComponent<HingeJoint>().spring = gjske;

            JointLimits gjskel = solOmuzEk.GetComponent<HingeJoint>().limits;
            gjskel.max = solKolJointlerYumruk[0].maxLimit;
            gjskel.min = solKolJointlerYumruk[0].minLimit;
            solOmuzEk.GetComponent<HingeJoint>().limits = gjskel;



            solOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(solOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(-targetposx, targetposy, -targetposz, targetposw), Time.deltaTime * solyumruksure);
            JointDrive spr = new JointDrive();
            spr.positionSpring = solyumrukspring;
            spr.positionDamper = 10;
            spr.maximumForce = Mathf.Infinity;
            solOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr;
            solOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr;

            /*JointSpring gj = solOmuz.GetComponent<HingeJoint>().spring;
            gj.spring = Mathf.Lerp(gj.spring, solKolJointlerYumruk[1].spring, Time.deltaTime * gerilmesphiz);
            gj.targetPosition = Mathf.Lerp(gj.targetPosition, solKolJointlerYumruk[1].tp, Time.deltaTime * gerilmetphiz);
            gj.damper = solKolJointlerYumruk[1].damper;
            solOmuz.GetComponent<HingeJoint>().spring = gj;

            JointLimits gjl = solOmuz.GetComponent<HingeJoint>().limits;
            gjl.max = solKolJointlerYumruk[1].maxLimit;
            gjl.min = solKolJointlerYumruk[1].minLimit;
            solOmuz.GetComponent<HingeJoint>().limits = gjl;*/


            JointSpring gjsk = solKol.GetComponent<HingeJoint>().spring;
            gjsk.spring = Mathf.Lerp(gjsk.spring, solKolJointlerYumruk[2].spring, Time.deltaTime * gerilmesphiz);
            gjsk.targetPosition = Mathf.Lerp(gjsk.targetPosition, solKolJointlerYumruk[2].tp, Time.deltaTime * gerilmetphiz);
            gjsk.damper = solKolJointlerYumruk[2].damper;
            solKol.GetComponent<HingeJoint>().spring = gjsk;

            JointLimits gjkl = solKol.GetComponent<HingeJoint>().limits;
            gjkl.max = solKolJointlerYumruk[2].maxLimit;
            gjkl.min = solKolJointlerYumruk[2].minLimit;
            solKol.GetComponent<HingeJoint>().limits = gjkl;

            JointSpring belj = gogus.GetComponent<HingeJoint>().spring;
            belj.spring = Mathf.Lerp(belj.spring, belJointDegerlerYumruk[0].spring, Time.deltaTime * gerilmesphiz);
            belj.targetPosition = Mathf.Lerp(belj.targetPosition, belJointDegerlerYumruk[0].tp, Time.deltaTime * gerilmetphiz);
            belj.damper = belJointDegerlerYumruk[0].damper;
            gogus.GetComponent<HingeJoint>().spring = belj;


            JointSpring gjskx = transform.GetChild(0).GetComponent<HingeJoint>().spring;
            gjskx.spring = 20000;// Mathf.Lerp(gjsk.spring, sagKolJointlerYumruk[2].spring, Time.deltaTime * gerilmetphiz);
            gjskx.targetPosition = 0;// Mathf.Lerp(gjsk.targetPosition, sagKolJointlerYumruk[2].tp, Time.deltaTime * gerilmetphiz);
            gjskx.damper = 1;// sagKolJointlerYumruk[2].damper;
            transform.GetChild(0).GetComponent<HingeJoint>().spring = gjskx;


            yield return Time.deltaTime;
        }
        //Invoke ("SolKolYumrukAtis", 0.05f);
        SolKolYumrukAtis2(target);

    }

    private void SolKolYumrukAtis2(Transform target)
    {
        StartCoroutine(_SolKolYumrukAtis2(target));
    }

    IEnumerator _SolKolYumrukAtis2(Transform target)
    {
        float atissphiz = solvuruspringhiz;
        float atistphiz = solvurustargetposhiz;
        float timer = 0;

        if (kaldiririyor)
        {
            timer = solvurussure + 1;
        }
        while (timer <= solvurussure)
        {
            timer += Time.deltaTime;
            JointSpring gjske = solOmuzEk.GetComponent<HingeJoint>().spring;
            gjske.spring = Mathf.Lerp(gjske.spring, solKolJointlerYumruk[3].spring, Time.deltaTime * atissphiz);
            gjske.targetPosition = Mathf.Lerp(gjske.targetPosition, solKolJointlerYumruk[3].tp, Time.deltaTime * atistphiz);
            gjske.damper = solKolJointlerYumruk[3].damper;
            solOmuzEk.GetComponent<HingeJoint>().spring = gjske;

            JointLimits gjskel = solOmuzEk.GetComponent<HingeJoint>().limits;
            gjskel.max = solKolJointlerYumruk[3].maxLimit;
            gjskel.min = solKolJointlerYumruk[3].minLimit;
            solOmuzEk.GetComponent<HingeJoint>().limits = gjskel;


            solOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(solOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(-targetposx2, targetposy2, -targetposz2, targetposw2), Time.deltaTime * solyumruksure);
            //solKol.GetComponent<Rigidbody> ().AddForce (solKol.transform.forward * forcee, ForceMode.Impulse);

            JointDrive spr = new JointDrive();
            spr.positionSpring = solyumrukspring;
            spr.positionDamper = 10;
            spr.maximumForce = Mathf.Infinity;
            solOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr;
            solOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr;

            /*JointSpring gj = solOmuz.GetComponent<HingeJoint>().spring;
            gj.spring = Mathf.Lerp(gj.spring, solKolJointlerYumruk[4].spring, Time.deltaTime * atissphiz);
            gj.targetPosition = Mathf.Lerp(gj.targetPosition, solKolJointlerYumruk[4].tp, Time.deltaTime * atistphiz);
            gj.damper = solKolJointlerYumruk[4].damper;
            solOmuz.GetComponent<HingeJoint>().spring = gj;

            JointLimits gjl = solOmuz.GetComponent<HingeJoint>().limits;
            gjl.max = solKolJointlerYumruk[4].maxLimit;
            gjl.min = solKolJointlerYumruk[4].minLimit;
            solOmuz.GetComponent<HingeJoint>().limits = gjl;*/


            JointSpring gjsk = solKol.GetComponent<HingeJoint>().spring;
            gjsk.spring = Mathf.Lerp(gjsk.spring, solKolJointlerYumruk[5].spring, Time.deltaTime * atissphiz);
            gjsk.targetPosition = Mathf.Lerp(gjsk.targetPosition, solKolJointlerYumruk[5].tp, Time.deltaTime * atistphiz);
            gjsk.damper = solKolJointlerYumruk[5].damper;
            solKol.GetComponent<HingeJoint>().spring = gjsk;

            JointLimits gjkl = solKol.GetComponent<HingeJoint>().limits;
            gjkl.max = solKolJointlerYumruk[5].maxLimit;
            gjkl.min = solKolJointlerYumruk[5].minLimit;
            solKol.GetComponent<HingeJoint>().limits = gjkl;

            JointSpring belj = gogus.GetComponent<HingeJoint>().spring;
            belj.spring = Mathf.Lerp(belj.spring, belJointDegerlerYumruk[1].spring, Time.deltaTime * atissphiz);
            belj.targetPosition = Mathf.Lerp(belj.targetPosition, belJointDegerlerYumruk[1].tp, Time.deltaTime * atistphiz);
            belj.damper = belJointDegerlerYumruk[1].damper;
            gogus.GetComponent<HingeJoint>().spring = belj;


            JointSpring gjskx = transform.GetChild(0).GetComponent<HingeJoint>().spring;
            gjskx.spring = 20000;// Mathf.Lerp(gjsk.spring, sagKolJointlerYumruk[2].spring, Time.deltaTime * gerilmetphiz);
            gjskx.targetPosition = 0;// Mathf.Lerp(gjsk.targetPosition, sagKolJointlerYumruk[2].tp, Time.deltaTime * gerilmetphiz);
            gjskx.damper = 1;// sagKolJointlerYumruk[2].damper;
            transform.GetChild(0).GetComponent<HingeJoint>().spring = gjskx;


            yield return Time.deltaTime;

        }
        solKol.GetComponent<ForceEkleyici>().isEnabled = true;
        //SolKolToparlama ();
        Invoke("SolKolToparlama", 0.1f);
    }


    IEnumerator _SagKolYumrukGerilme(Transform target)
    {
        tekrarYumrukAtabilir = false;
        yumrukAtiyor = true;
        float timer = 0;
        float gerilmespringhiz = saggerilmespringhiz;
        float gerilmetphiz = saggerilmetargetposhiz;

        if (kaldiririyor)
        {
            timer = sagvurussure + 1;
        }

        while (timer <= saggerilmesure)
        {
            timer += Time.deltaTime;

            JointSpring gjske = sagOmuzEk.GetComponent<HingeJoint>().spring;
            gjske.spring = Mathf.Lerp(gjske.spring, sagKolJointlerYumruk[0].spring, Time.deltaTime * gerilmespringhiz);
            gjske.targetPosition = Mathf.Lerp(gjske.targetPosition, sagKolJointlerYumruk[0].tp, Time.deltaTime * gerilmetphiz);
            gjske.damper = sagKolJointlerYumruk[0].damper;
            sagOmuzEk.GetComponent<HingeJoint>().spring = gjske;

            JointLimits gjskel = sagOmuzEk.GetComponent<HingeJoint>().limits;
            gjskel.max = sagKolJointlerYumruk[0].maxLimit;
            gjskel.min = sagKolJointlerYumruk[0].minLimit;
            sagOmuzEk.GetComponent<HingeJoint>().limits = gjskel;


            sagOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(sagOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(targetposx, targetposy, targetposz, targetposw)
                /*(-0.45f,-0.1f,0.36f,1)*/, Time.deltaTime * sagyumruksure);
            JointDrive spr = new JointDrive();
            spr.positionSpring = sagyumrukspring;
            spr.positionDamper = 10;
            spr.maximumForce = Mathf.Infinity;
            sagOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr;
            sagOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr;


            /*JointSpring gj = sagOmuz.GetComponent<HingeJoint>().spring;
            gj.spring = Mathf.Lerp(gj.spring, sagKolJointlerYumruk[1].spring, Time.deltaTime * gerilmespringhiz);
            gj.targetPosition = Mathf.Lerp(gj.targetPosition, sagKolJointlerYumruk[1].tp, Time.deltaTime * gerilmetphiz);
            gj.damper = sagKolJointlerYumruk[1].damper;
            sagOmuz.GetComponent<HingeJoint>().spring = gj;

            JointLimits gjl = sagOmuz.GetComponent<HingeJoint>().limits;
            gjl.max = sagKolJointlerYumruk[1].maxLimit;
            gjl.min = sagKolJointlerYumruk[1].minLimit;
            sagOmuz.GetComponent<HingeJoint>().limits = gjl;*/


            JointSpring gjsk = sagKol.GetComponent<HingeJoint>().spring;
            gjsk.spring = Mathf.Lerp(gjsk.spring, sagKolJointlerYumruk[2].spring, Time.deltaTime * gerilmetphiz);
            gjsk.targetPosition = Mathf.Lerp(gjsk.targetPosition, 68.4f, Time.deltaTime * 20);
            gjsk.damper = sagKolJointlerYumruk[2].damper;
            sagKol.GetComponent<HingeJoint>().spring = gjsk;

            /*JointLimits gjkl = sagOmuz.GetComponent<HingeJoint>().limits;
            gjkl.max = sagKolJointlerYumruk[2].maxLimit;
            gjkl.min = sagKolJointlerYumruk[2].minLimit;
            sagOmuz.GetComponent<HingeJoint>().limits = gjkl;*/

            JointSpring belj = gogus.GetComponent<HingeJoint>().spring;
            belj.spring = Mathf.Lerp(belj.spring, belJointDegerlerYumruk[0].spring, Time.deltaTime * gerilmespringhiz);
            belj.targetPosition = Mathf.Lerp(belj.targetPosition, belJointDegerlerYumruk[0].tp, Time.deltaTime * gerilmetphiz);
            belj.damper = belJointDegerlerYumruk[0].damper;
            gogus.GetComponent<HingeJoint>().spring = belj;

            JointSpring gjskx = transform.GetChild(0).GetComponent<HingeJoint>().spring;
            gjskx.spring = 20000;// Mathf.Lerp(gjsk.spring, sagKolJointlerYumruk[2].spring, Time.deltaTime * gerilmetphiz);
            gjskx.targetPosition = 0;// Mathf.Lerp(gjsk.targetPosition, sagKolJointlerYumruk[2].tp, Time.deltaTime * gerilmetphiz);
            gjskx.damper = 1;// sagKolJointlerYumruk[2].damper;
            transform.GetChild(0).GetComponent<HingeJoint>().spring = gjskx;

            yield return Time.deltaTime;
        }


        SagKolYumrukAtis(target);
    }

    private void SagKolYumrukAtis(Transform target)
    {
        StartCoroutine(_SagKolYumrukAtis(target));
    }

    IEnumerator _SagKolYumrukAtis(Transform target)
    {

        float atissphiz = sagvuruspringhiz;
        float atistphiz = sagvurustargetposhiz;
        float timer = 0;

        if (kaldiririyor)
        {
            timer = sagvurussure + 1;
        }


        while (timer <= sagvurussure)
        {
            timer += Time.deltaTime;

            JointSpring gjske = sagOmuzEk.GetComponent<HingeJoint>().spring;
            gjske.spring = Mathf.Lerp(gjske.spring, sagKolJointlerYumruk[3].spring, Time.deltaTime * atissphiz);
            gjske.targetPosition = Mathf.Lerp(gjske.targetPosition, sagKolJointlerYumruk[3].tp, Time.deltaTime * atistphiz);
            gjske.damper = sagKolJointlerYumruk[3].damper;
            sagOmuzEk.GetComponent<HingeJoint>().spring = gjske;

            JointLimits gjskel = sagOmuzEk.GetComponent<HingeJoint>().limits;
            gjskel.max = sagKolJointlerYumruk[3].maxLimit;
            gjskel.min = sagKolJointlerYumruk[3].minLimit;
            sagOmuzEk.GetComponent<HingeJoint>().limits = gjskel;



            /*JointSpring gj = sagOmuz.GetComponent<HingeJoint>().spring;
            gj.spring = Mathf.Lerp(gj.spring, sagKolJointlerYumruk[4].spring, Time.deltaTime * atissphiz);
            gj.targetPosition = Mathf.Lerp(gj.targetPosition, sagKolJointlerYumruk[4].tp, Time.deltaTime * atistphiz);
            gj.damper = sagKolJointlerYumruk[4].damper;
            sagOmuz.GetComponent<HingeJoint>().spring = gj;

            JointLimits gjl = sagOmuz.GetComponent<HingeJoint>().limits;
            gjl.max = sagKolJointlerYumruk[4].maxLimit;
            gjl.min = sagKolJointlerYumruk[4].minLimit;
            sagOmuz.GetComponent<HingeJoint>().limits = gjl;*/


            sagOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(sagOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(targetposx2, targetposy2, targetposz2, targetposw2)
                /*new Quaternion(-0.45f,-1f,0.36f,0.48f)*/, Time.deltaTime * sagyumruksure);
            sagKol.GetComponent<Rigidbody>().AddForce(sagKol.transform.forward * forcee, ForceMode.Impulse);

            JointDrive spr = new JointDrive();
            spr.positionSpring = sagyumrukspring;
            spr.positionDamper = 10;
            spr.maximumForce = Mathf.Infinity;
            sagOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr;
            sagOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr;



            JointSpring gjsk = sagKol.GetComponent<HingeJoint>().spring;
            gjsk.spring = 2000;// Mathf.Lerp(gjsk.spring, sagKolJointlerYumruk[5].spring, Time.deltaTime * atissphiz);
            gjsk.targetPosition = Mathf.Lerp(gjsk.targetPosition, 0, Time.deltaTime * 20);
            gjsk.damper = 10;//sagKolJointlerYumruk[5].damper;
            sagKol.GetComponent<HingeJoint>().spring = gjsk;

            JointLimits gjkl = sagKol.GetComponent<HingeJoint>().limits;
            gjkl.max = sagKolJointlerYumruk[5].maxLimit;
            gjkl.min = sagKolJointlerYumruk[5].minLimit;
            sagKol.GetComponent<HingeJoint>().limits = gjkl;

            JointSpring gjskx = transform.GetChild(0).GetComponent<HingeJoint>().spring;
            gjskx.spring = 20000;// Mathf.Lerp(gjsk.spring, sagKolJointlerYumruk[2].spring, Time.deltaTime * gerilmetphiz);
            gjskx.targetPosition = 0;// Mathf.Lerp(gjsk.targetPosition, sagKolJointlerYumruk[2].tp, Time.deltaTime * gerilmetphiz);
            gjskx.damper = 1;// sagKolJointlerYumruk[2].damper;
            transform.GetChild(0).GetComponent<HingeJoint>().spring = gjskx;


            JointSpring belj = gogus.GetComponent<HingeJoint>().spring;
            belj.spring = Mathf.Lerp(belj.spring, belJointDegerlerYumruk[1].spring, Time.deltaTime * atissphiz);
            belj.targetPosition = Mathf.Lerp(belj.targetPosition, belJointDegerlerYumruk[1].tp, Time.deltaTime * atistphiz);
            belj.damper = belJointDegerlerYumruk[1].damper;
            gogus.GetComponent<HingeJoint>().spring = belj;
            yield return Time.deltaTime;
        }

        //sagKol.GetComponent<ForceEkleyici> ().isEnabled = true;

        Invoke("SagKolToparlama", 0.1f);
        //SagKolToparlama();
    }

    private void SagKolToparlama()
    {
        if (!kaldiririyor)
        {
            sagKol.GetComponent<ForceEkleyici>().isEnabled = false;
            yumrukAtiyor = false;

            /*JointSpring jpsoe1_1 = sagOmuzEk.GetComponent<HingeJoint>().spring;
	jpsoe1_1.spring = sagKolJointler [0].spring_i;
	jpsoe1_1.damper = sagKolJointler [0].damper_i;
	jpsoe1_1.targetPosition = sagKolJointler [0].tp_i;
	sagOmuzEk.GetComponent<HingeJoint> ().spring = jpsoe1_1;

	JointLimits jpsoel1_1 = sagOmuzEk.GetComponent<HingeJoint>().limits;
	jpsoel1_1.max = sagKolJointler [0].maxLimit_i;
	jpsoel1_1.min = sagKolJointler [0].minLimit_i;
	sagOmuzEk.GetComponent<HingeJoint> ().limits = jpsoel1_1;


	JointSpring jpsoe2_1 = sagOmuz.GetComponent<HingeJoint>().spring;
	jpsoe2_1.spring = sagKolJointler [1].spring_i;
	jpsoe2_1.damper = sagKolJointler [1].damper_i;
	jpsoe2_1.targetPosition = sagKolJointler [1].tp_i;
	sagOmuz.GetComponent<HingeJoint> ().spring = jpsoe2_1;

	JointLimits jpsoel2_1 = sagOmuz.GetComponent<HingeJoint>().limits;
	jpsoel2_1.max = sagKolJointler [1].maxLimit_i;
	jpsoel2_1.min = sagKolJointler [1].minLimit_i;
	sagOmuz.GetComponent<HingeJoint> ().limits = jpsoel2_1;
*/


            sagOmuz.GetComponent<ConfigurableJoint>().targetRotation = new Quaternion(0f, 0f, 0f, 1f);// Quaternion.Lerp(sagOmuz.GetComponent<ConfigurableJoint> ().targetRotation,new Quaternion(-0.45f,-1f,0.36f,0.48f),Time.deltaTime*20);
            JointDrive spr = new JointDrive();
            spr.positionSpring = sagyumrukspring;
            spr.positionDamper = 10;
            spr.maximumForce = Mathf.Infinity;
            sagOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr;
            sagOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr;


            JointSpring jpsoe3_1 = sagKol.GetComponent<HingeJoint>().spring;
            jpsoe3_1.spring = sagKolJointler[2].spring_i;
            jpsoe3_1.damper = sagKolJointler[2].damper_i;
            jpsoe3_1.targetPosition = sagKolJointler[2].tp_i;
            sagKol.GetComponent<HingeJoint>().spring = jpsoe3_1;

            JointLimits jpsoel3_1 = sagKol.GetComponent<HingeJoint>().limits;
            jpsoel3_1.max = sagKolJointler[2].maxLimit_i;
            jpsoel3_1.min = sagKolJointler[2].minLimit_i;
            sagKol.GetComponent<HingeJoint>().limits = jpsoel3_1;


            JointSpring belj = gogus.GetComponent<HingeJoint>().spring;
            belj.spring = 400;
            belj.targetPosition = 0;
            belj.damper = 5;
            gogus.GetComponent<HingeJoint>().spring = belj;

            JointLimits jpsbl = gogus.GetComponent<HingeJoint>().limits;
            jpsbl.max = belJoint.maxLimit_i;
            jpsbl.min = belJoint.minLimit_i;
            gogus.GetComponent<HingeJoint>().limits = jpsbl;
        }
        //sag_yum++;
        Invoke("TekrarYumrukAtabilir", 0.05f);
    }

    private void SolKolYumrukAtma(Transform target)
    {
        if (tekrarYumrukAtabilir)
            SolKolYumrukGerilme(target);
    }

    private void SolKolYumrukGerilme(Transform target)
    {
        StartCoroutine(_SolKolYumrukGerilme(target));
    }

    IEnumerator _SolKolYumrukGerilme(Transform target)
    {

        tekrarYumrukAtabilir = false;
        yumrukAtiyor = true;

        float gerilmesphiz = solgerilmespringhiz;
        float gerilmetphiz = solgerilmetargetposhiz;
        float timer = 0;

        if (kaldiririyor)
        {
            timer = solvurussure + 1;
        }

        while (timer < solgerilmesure)
        {

            timer += Time.deltaTime;
            JointSpring gjske = solOmuzEk.GetComponent<HingeJoint>().spring;
            gjske.spring = Mathf.Lerp(gjske.spring, solKolJointlerYumruk[0].spring, Time.deltaTime * gerilmesphiz);
            gjske.targetPosition = Mathf.Lerp(gjske.targetPosition, solKolJointlerYumruk[0].tp, Time.deltaTime * gerilmetphiz);
            gjske.damper = solKolJointlerYumruk[0].damper;
            solOmuzEk.GetComponent<HingeJoint>().spring = gjske;

            JointLimits gjskel = solOmuzEk.GetComponent<HingeJoint>().limits;
            gjskel.max = solKolJointlerYumruk[0].maxLimit;
            gjskel.min = solKolJointlerYumruk[0].minLimit;
            solOmuzEk.GetComponent<HingeJoint>().limits = gjskel;



            solOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(solOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(-targetposx, targetposy, -targetposz, targetposw), Time.deltaTime * solyumruksure);
            JointDrive spr = new JointDrive();
            spr.positionSpring = solyumrukspring;
            spr.positionDamper = 10;
            spr.maximumForce = Mathf.Infinity;
            solOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr;
            solOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr;

            /*JointSpring gj = solOmuz.GetComponent<HingeJoint>().spring;
            gj.spring = Mathf.Lerp(gj.spring, solKolJointlerYumruk[1].spring, Time.deltaTime * gerilmesphiz);
            gj.targetPosition = Mathf.Lerp(gj.targetPosition, solKolJointlerYumruk[1].tp, Time.deltaTime * gerilmetphiz);
            gj.damper = solKolJointlerYumruk[1].damper;
            solOmuz.GetComponent<HingeJoint>().spring = gj;

            JointLimits gjl = solOmuz.GetComponent<HingeJoint>().limits;
            gjl.max = solKolJointlerYumruk[1].maxLimit;
            gjl.min = solKolJointlerYumruk[1].minLimit;
            solOmuz.GetComponent<HingeJoint>().limits = gjl;*/


            JointSpring gjsk = solKol.GetComponent<HingeJoint>().spring;
            gjsk.spring = Mathf.Lerp(gjsk.spring, solKolJointlerYumruk[2].spring, Time.deltaTime * gerilmesphiz);
            gjsk.targetPosition = Mathf.Lerp(gjsk.targetPosition, solKolJointlerYumruk[2].tp, Time.deltaTime * gerilmetphiz);
            gjsk.damper = solKolJointlerYumruk[2].damper;
            solKol.GetComponent<HingeJoint>().spring = gjsk;

            JointLimits gjkl = solKol.GetComponent<HingeJoint>().limits;
            gjkl.max = solKolJointlerYumruk[2].maxLimit;
            gjkl.min = solKolJointlerYumruk[2].minLimit;
            solKol.GetComponent<HingeJoint>().limits = gjkl;

            JointSpring belj = gogus.GetComponent<HingeJoint>().spring;
            belj.spring = Mathf.Lerp(belj.spring, belJointDegerlerYumruk[0].spring, Time.deltaTime * gerilmesphiz);
            belj.targetPosition = Mathf.Lerp(belj.targetPosition, belJointDegerlerYumruk[0].tp, Time.deltaTime * gerilmetphiz);
            belj.damper = belJointDegerlerYumruk[0].damper;
            gogus.GetComponent<HingeJoint>().spring = belj;


            JointSpring gjskx = transform.GetChild(0).GetComponent<HingeJoint>().spring;
            gjskx.spring = 20000;// Mathf.Lerp(gjsk.spring, sagKolJointlerYumruk[2].spring, Time.deltaTime * gerilmetphiz);
            gjskx.targetPosition = 0;// Mathf.Lerp(gjsk.targetPosition, sagKolJointlerYumruk[2].tp, Time.deltaTime * gerilmetphiz);
            gjskx.damper = 1;// sagKolJointlerYumruk[2].damper;
            transform.GetChild(0).GetComponent<HingeJoint>().spring = gjskx;


            yield return Time.deltaTime;
        }
        //Invoke ("SolKolYumrukAtis", 0.05f);
        SolKolYumrukAtis(target);

    }

    private void SolKolYumrukAtis(Transform target)
    {
        StartCoroutine(_SolKolYumrukAtis(target));
    }

    IEnumerator _SolKolYumrukAtis(Transform target)
    {
        float atissphiz = solvuruspringhiz;
        float atistphiz = solvurustargetposhiz;
        float timer = 0;

        if (kaldiririyor)
        {
            timer = solvurussure + 1;
        }
        while (timer <= solvurussure)
        {
            timer += Time.deltaTime;
            JointSpring gjske = solOmuzEk.GetComponent<HingeJoint>().spring;
            gjske.spring = Mathf.Lerp(gjske.spring, solKolJointlerYumruk[3].spring, Time.deltaTime * atissphiz);
            gjske.targetPosition = Mathf.Lerp(gjske.targetPosition, solKolJointlerYumruk[3].tp, Time.deltaTime * atistphiz);
            gjske.damper = solKolJointlerYumruk[3].damper;
            solOmuzEk.GetComponent<HingeJoint>().spring = gjske;

            JointLimits gjskel = solOmuzEk.GetComponent<HingeJoint>().limits;
            gjskel.max = solKolJointlerYumruk[3].maxLimit;
            gjskel.min = solKolJointlerYumruk[3].minLimit;
            solOmuzEk.GetComponent<HingeJoint>().limits = gjskel;


            solOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(solOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(-targetposx2, targetposy2, -targetposz2, targetposw2), Time.deltaTime * solyumruksure);
            solKol.GetComponent<Rigidbody>().AddForce(solKol.transform.forward * forcee, ForceMode.Impulse);

            JointDrive spr = new JointDrive();
            spr.positionSpring = solyumrukspring;
            spr.positionDamper = 10;
            spr.maximumForce = Mathf.Infinity;
            solOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr;
            solOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr;

            /*JointSpring gj = solOmuz.GetComponent<HingeJoint>().spring;
            gj.spring = Mathf.Lerp(gj.spring, solKolJointlerYumruk[4].spring, Time.deltaTime * atissphiz);
            gj.targetPosition = Mathf.Lerp(gj.targetPosition, solKolJointlerYumruk[4].tp, Time.deltaTime * atistphiz);
            gj.damper = solKolJointlerYumruk[4].damper;
            solOmuz.GetComponent<HingeJoint>().spring = gj;

            JointLimits gjl = solOmuz.GetComponent<HingeJoint>().limits;
            gjl.max = solKolJointlerYumruk[4].maxLimit;
            gjl.min = solKolJointlerYumruk[4].minLimit;
            solOmuz.GetComponent<HingeJoint>().limits = gjl;*/


            JointSpring gjsk = solKol.GetComponent<HingeJoint>().spring;
            gjsk.spring = Mathf.Lerp(gjsk.spring, solKolJointlerYumruk[5].spring, Time.deltaTime * atissphiz);
            gjsk.targetPosition = Mathf.Lerp(gjsk.targetPosition, solKolJointlerYumruk[5].tp, Time.deltaTime * atistphiz);
            gjsk.damper = solKolJointlerYumruk[5].damper;
            solKol.GetComponent<HingeJoint>().spring = gjsk;

            JointLimits gjkl = solKol.GetComponent<HingeJoint>().limits;
            gjkl.max = solKolJointlerYumruk[5].maxLimit;
            gjkl.min = solKolJointlerYumruk[5].minLimit;
            solKol.GetComponent<HingeJoint>().limits = gjkl;

            JointSpring belj = gogus.GetComponent<HingeJoint>().spring;
            belj.spring = Mathf.Lerp(belj.spring, belJointDegerlerYumruk[1].spring, Time.deltaTime * atissphiz);
            belj.targetPosition = Mathf.Lerp(belj.targetPosition, belJointDegerlerYumruk[1].tp, Time.deltaTime * atistphiz);
            belj.damper = belJointDegerlerYumruk[1].damper;
            gogus.GetComponent<HingeJoint>().spring = belj;


            JointSpring gjskx = transform.GetChild(0).GetComponent<HingeJoint>().spring;
            gjskx.spring = 20000;// Mathf.Lerp(gjsk.spring, sagKolJointlerYumruk[2].spring, Time.deltaTime * gerilmetphiz);
            gjskx.targetPosition = 0;// Mathf.Lerp(gjsk.targetPosition, sagKolJointlerYumruk[2].tp, Time.deltaTime * gerilmetphiz);
            gjskx.damper = 1;// sagKolJointlerYumruk[2].damper;
            transform.GetChild(0).GetComponent<HingeJoint>().spring = gjskx;


            yield return Time.deltaTime;

        }
        solKol.GetComponent<ForceEkleyici>().isEnabled = true;
        //SolKolToparlama ();
        Invoke("SolKolToparlama", 0.1f);
    }

    private void SolKolToparlama()
    {
        if (!kaldiririyor)
        {
            solKol.GetComponent<ForceEkleyici>().isEnabled = false;
            JointSpring jpsoe1_1 = solOmuzEk.GetComponent<HingeJoint>().spring;
            jpsoe1_1.spring = solKolJointler[0].spring_i;
            jpsoe1_1.damper = solKolJointler[0].damper_i;
            jpsoe1_1.targetPosition = solKolJointler[0].tp_i;
            solOmuzEk.GetComponent<HingeJoint>().spring = jpsoe1_1;

            JointLimits jpsoel1_1 = solOmuzEk.GetComponent<HingeJoint>().limits;
            jpsoel1_1.max = solKolJointler[0].maxLimit_i;
            jpsoel1_1.min = solKolJointler[0].minLimit_i;
            solOmuzEk.GetComponent<HingeJoint>().limits = jpsoel1_1;


            /*JointSpring jpsoe2_1 = solOmuz.GetComponent<HingeJoint>().spring;
	jpsoe2_1.spring = solKolJointler [1].spring_i;
	jpsoe2_1.damper = solKolJointler [1].damper_i;
	jpsoe2_1.targetPosition = solKolJointler [1].tp_i;
	solOmuz.GetComponent<HingeJoint> ().spring = jpsoe2_1;

	JointLimits jpsoel2_1 = solOmuz.GetComponent<HingeJoint>().limits;
	jpsoel2_1.max = solKolJointler [1].maxLimit_i;
	jpsoel2_1.min = solKolJointler [1].minLimit_i;
	solOmuz.GetComponent<HingeJoint> ().limits = jpsoel2_1;*/

            solOmuz.GetComponent<ConfigurableJoint>().targetRotation = new Quaternion(0f, 0f, 0f, 1f);
            JointDrive spr = new JointDrive();
            spr.positionSpring = solyumrukspring;
            spr.positionDamper = 10;
            spr.maximumForce = Mathf.Infinity;
            solOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr;
            solOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr;

            JointSpring jpsoe3_1 = solKol.GetComponent<HingeJoint>().spring;
            jpsoe3_1.spring = solKolJointler[2].spring_i;
            jpsoe3_1.damper = solKolJointler[2].damper_i;
            jpsoe3_1.targetPosition = sagKolJointler[2].tp_i;
            solKol.GetComponent<HingeJoint>().spring = jpsoe3_1;

            JointLimits jpsoel3_1 = solKol.GetComponent<HingeJoint>().limits;
            jpsoel3_1.max = solKolJointler[2].maxLimit_i;
            jpsoel3_1.min = solKolJointler[2].minLimit_i;
            solKol.GetComponent<HingeJoint>().limits = jpsoel3_1;


            JointSpring belj = gogus.GetComponent<HingeJoint>().spring;
            belj.spring = 400;
            belj.targetPosition = 0;
            belj.damper = 5;
            gogus.GetComponent<HingeJoint>().spring = belj;

            JointLimits jpsbl = gogus.GetComponent<HingeJoint>().limits;
            jpsbl.max = belJoint.maxLimit_i;
            jpsbl.min = belJoint.minLimit_i;
            gogus.GetComponent<HingeJoint>().limits = jpsbl;

            JointSpring gjskx = transform.GetChild(0).GetComponent<HingeJoint>().spring;
            gjskx.spring = 20000;// Mathf.Lerp(gjsk.spring, sagKolJointlerYumruk[2].spring, Time.deltaTime * gerilmetphiz);
            gjskx.targetPosition = 0;// Mathf.Lerp(gjsk.targetPosition, sagKolJointlerYumruk[2].tp, Time.deltaTime * gerilmetphiz);
            gjskx.damper = 1;// sagKolJointlerYumruk[2].damper;
            transform.GetChild(0).GetComponent<HingeJoint>().spring = gjskx;
        }
        //sag_yum++;
        yumrukAtiyor = false;
        Invoke("TekrarYumrukAtabilir", 0.05f);
    }

    private void TekrarYumrukAtabilir()
    {
        tekrarYumrukAtabilir = true;
    }
    #endregion

    #region KollariKaldirma
    public void Kaldir()
    {
        //if(!kaldiririyor)
        StartCoroutine(_Kaldir());
    }
    public void Kaldir_yatiyor()
    {
        //if(!kaldiririyor)
        StartCoroutine(_Kaldir_yatiyor());
    }
    public void egil()
    {
        //Debug.Log ("egil"); 
        if (!kaldiririyor)
            StartCoroutine(_Egil());
    }
    public void dur()
    {
        Debug.Log("dur");
        //if (!kaldiririyor)
        //	durdu = true;
    }

    public void Kaldir2()
    {
        if (!kaldiririyor)
            StartCoroutine(_Kaldir2());
    }
    IEnumerator _Kaldir2()
    {
        float timer = 0;
        //kaldiririyor = true;
        while (timer <= 0.1f)
        {
            timer += Time.deltaTime;
            float kaldirmaLerpSpeed = 0.5f;

            JointSpring jpsoe1_1 = sagOmuzEk.GetComponent<HingeJoint>().spring;
            jpsoe1_1.spring += sagKolJointler[0].spring * 0.025f;
            jpsoe1_1.damper = sagKolJointler[0].damper;
            jpsoe1_1.targetPosition = Mathf.Lerp(jpsoe1_1.targetPosition, sagKolJointler[0].tp, Time.deltaTime * kaldirmaLerpSpeed);
            sagOmuzEk.GetComponent<HingeJoint>().spring = jpsoe1_1;

            JointLimits jpsoel1_1 = sagOmuzEk.GetComponent<HingeJoint>().limits;
            jpsoel1_1.max = sagKolJointler[0].maxLimit;
            jpsoel1_1.min = sagKolJointler[0].minLimit;
            sagOmuzEk.GetComponent<HingeJoint>().limits = jpsoel1_1;

            sagOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(sagOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(-0.7f, -0.97f, 0.53f, 0.83f), Time.deltaTime * 40);
            solOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(solOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(0.7f, -0.97f, -0.53f, 0.83f), Time.deltaTime * 40);

            JointDrive spr = new JointDrive();
            spr.positionSpring = 4000;
            spr.positionDamper = 10;
            spr.maximumForce = Mathf.Infinity;
            sagOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr;
            sagOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr;


            JointSpring jpsoe3_1 = sagKol.GetComponent<HingeJoint>().spring;
            jpsoe3_1.spring = 8000;
            jpsoe3_1.damper = 10;
            jpsoe3_1.targetPosition = 60;// Mathf.Lerp(jpsoe3_1.targetPosition, sagKolJointler[2].tp, Time.deltaTime * kaldirmaLerpSpeed);
            sagKol.GetComponent<HingeJoint>().spring = jpsoe3_1;

            JointLimits jpsoel3_1 = sagKol.GetComponent<HingeJoint>().limits;
            jpsoel3_1.max = sagKolJointler[2].maxLimit;
            jpsoel3_1.min = sagKolJointler[2].minLimit;
            sagKol.GetComponent<HingeJoint>().limits = jpsoel3_1;



            JointSpring jpsoe1_2 = solOmuzEk.GetComponent<HingeJoint>().spring;
            jpsoe1_2.spring += solKolJointler[0].spring * 0.025f;
            jpsoe1_2.damper = solKolJointler[0].damper;
            jpsoe1_2.targetPosition = Mathf.Lerp(jpsoe1_2.targetPosition, solKolJointler[0].tp, Time.deltaTime * kaldirmaLerpSpeed);
            solOmuzEk.GetComponent<HingeJoint>().spring = jpsoe1_2;

            JointLimits jpsoel1_2 = solOmuzEk.GetComponent<HingeJoint>().limits;
            jpsoel1_2.max = solKolJointler[0].maxLimit;
            jpsoel1_2.min = solKolJointler[0].minLimit;
            solOmuzEk.GetComponent<HingeJoint>().limits = jpsoel1_2;


            JointDrive spr2 = new JointDrive();
            spr2.positionSpring = 4000;
            spr2.positionDamper = 10;
            spr2.maximumForce = Mathf.Infinity;
            solOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr2;
            solOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr2;

            JointSpring jpsoe3_2 = solKol.GetComponent<HingeJoint>().spring;
            jpsoe3_2.spring = 8000;
            jpsoe3_2.damper = 10;
            jpsoe3_2.targetPosition = 60;
            solKol.GetComponent<HingeJoint>().spring = jpsoe3_2;

            JointLimits jpsoel3_2 = solKol.GetComponent<HingeJoint>().limits;
            jpsoel3_2.max = solKolJointler[2].maxLimit;
            jpsoel3_2.min = solKolJointler[2].minLimit;
            solKol.GetComponent<HingeJoint>().limits = jpsoel3_2;




            yield return Time.deltaTime;
        }
        //kollarhavada = true;
    }


    IEnumerator _Kaldir()
    {

        yatiyor = false;
        float timer = 0;
        if (!isAI)
            GameManager_A.gameManager.SpawnYakaladi();
        kaldiririyor = true;
        kaldirma_kontrol_time = Time.time;
        while (timer <= 0.3f)
        {
            timer += Time.deltaTime;
            float kaldirmaLerpSpeed = 0.5f;

            JointSpring jpsoe1_1 = sagOmuzEk.GetComponent<HingeJoint>().spring;
            jpsoe1_1.spring += sagKolJointler[0].spring * 0.025f;
            jpsoe1_1.damper = sagKolJointler[0].damper;
            jpsoe1_1.targetPosition = Mathf.Lerp(jpsoe1_1.targetPosition, 70, Time.deltaTime * kaldirmaLerpSpeed);
            sagOmuzEk.GetComponent<HingeJoint>().spring = jpsoe1_1;

            JointLimits jpsoel1_1 = sagOmuzEk.GetComponent<HingeJoint>().limits;
            jpsoel1_1.max = sagKolJointler[0].maxLimit;
            jpsoel1_1.min = sagKolJointler[0].minLimit;
            sagOmuzEk.GetComponent<HingeJoint>().limits = jpsoel1_1;

            //			Debug.Log ("kaldirttt-----");
            sagOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(sagOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(1f, 0.76f, 0.18f, -0.6f), Time.deltaTime * 10);
            solOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(solOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(-1f, 0.76f, -0.18f, -0.6f), Time.deltaTime * 10);

            JointDrive spr = new JointDrive();
            spr.positionSpring = 4000;
            spr.positionDamper = 10;
            spr.maximumForce = Mathf.Infinity;
            sagOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr;
            sagOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr;


            JointSpring jpsoe3_1 = sagKol.GetComponent<HingeJoint>().spring;
            jpsoe3_1.spring = 8000;
            jpsoe3_1.damper = 10;
            jpsoe3_1.targetPosition = 0;// Mathf.Lerp(jpsoe3_1.targetPosition, sagKolJointler[2].tp, Time.deltaTime * kaldirmaLerpSpeed);
            sagKol.GetComponent<HingeJoint>().spring = jpsoe3_1;

            JointLimits jpsoel3_1 = sagKol.GetComponent<HingeJoint>().limits;
            jpsoel3_1.max = sagKolJointler[2].maxLimit;
            jpsoel3_1.min = sagKolJointler[2].minLimit;
            sagKol.GetComponent<HingeJoint>().limits = jpsoel3_1;



            JointSpring jpsoe1_2 = solOmuzEk.GetComponent<HingeJoint>().spring;
            jpsoe1_2.spring += solKolJointler[0].spring * 0.025f;
            jpsoe1_2.damper = solKolJointler[0].damper;
            jpsoe1_2.targetPosition = Mathf.Lerp(jpsoe1_2.targetPosition, -70, Time.deltaTime * kaldirmaLerpSpeed);
            solOmuzEk.GetComponent<HingeJoint>().spring = jpsoe1_2;

            JointLimits jpsoel1_2 = solOmuzEk.GetComponent<HingeJoint>().limits;
            jpsoel1_2.max = solKolJointler[0].maxLimit;
            jpsoel1_2.min = solKolJointler[0].minLimit;
            solOmuzEk.GetComponent<HingeJoint>().limits = jpsoel1_2;


            JointDrive spr2 = new JointDrive();
            spr2.positionSpring = 4000;
            spr2.positionDamper = 10;
            spr2.maximumForce = Mathf.Infinity;
            solOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr2;
            solOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr2;

            JointSpring jpsoe3_2 = solKol.GetComponent<HingeJoint>().spring;
            jpsoe3_2.spring = 8000;
            jpsoe3_2.damper = 10;
            jpsoe3_2.targetPosition = 0;
            solKol.GetComponent<HingeJoint>().spring = jpsoe3_2;

            JointLimits jpsoel3_2 = solKol.GetComponent<HingeJoint>().limits;
            jpsoel3_2.max = solKolJointler[2].maxLimit;
            jpsoel3_2.min = solKolJointler[2].minLimit;
            solKol.GetComponent<HingeJoint>().limits = jpsoel3_2;


            JointSpring jpsbx = transform.GetChild(0).GetComponent<HingeJoint>().spring;
            jpsbx.spring = 2000;
            jpsbx.damper = 5;
            jpsbx.targetPosition = 0;// -40;
            transform.GetChild(0).GetComponent<HingeJoint>().spring = jpsbx;


            JointLimits jpsoel1_1x = transform.GetChild(0).GetComponent<HingeJoint>().limits;
            jpsoel1_1x.max = 40;
            jpsoel1_1x.min = -40;
            transform.GetChild(0).GetComponent<HingeJoint>().limits = jpsoel1_1x;



            JointSpring jpsb = gogus.GetComponent<HingeJoint>().spring;
            jpsb.spring += belJoint.spring * 0.025f;
            jpsb.damper = belJoint.damper;
            jpsb.targetPosition = Mathf.Lerp(jpsb.targetPosition, belJoint.tp, Time.deltaTime * kaldirmaLerpSpeed);
            gogus.GetComponent<HingeJoint>().spring = jpsb;

            JointLimits jpsbl = gogus.GetComponent<HingeJoint>().limits;
            jpsbl.max = belJoint.maxLimit;
            jpsbl.min = belJoint.minLimit;
            gogus.GetComponent<HingeJoint>().limits = jpsbl;


            //	kaval1.transform.parent.GetComponent<piernasmov> ().enabled = false;
            //kaval2.transform.parent.GetComponent<piernasmov> ().enabled = false;

            yield return Time.deltaTime;
        }

        p1.stop = false;
        p2.stop = false;
        p3.stop = false;
        p4.stop = false;

        //kaval1.transform.parent.GetComponent<piernasmov> ().enabled = true;
        //kaval2.transform.parent.GetComponent<piernasmov> ().enabled = true;
        kaldir_sonrasi();
        durdu = false;
        egildi = false;
        kollarhavada = true;
    }

    IEnumerator _Kaldir_yatiyor()
    {
        yatiyor = false;
        float timer = 0;
        if (!isAI)
            GameManager_A.gameManager.SpawnYakaladi();
        kaldiririyor = true;
        while (timer <= 0.3f)
        {
            timer += Time.deltaTime;
            float kaldirmaLerpSpeed = 0.5f;

            JointSpring jpsoe1_1 = sagOmuzEk.GetComponent<HingeJoint>().spring;
            jpsoe1_1.spring += sagKolJointler[0].spring * 0.025f;
            jpsoe1_1.damper = sagKolJointler[0].damper;
            jpsoe1_1.targetPosition = Mathf.Lerp(jpsoe1_1.targetPosition, sagKolJointler[0].tp, Time.deltaTime * kaldirmaLerpSpeed);
            sagOmuzEk.GetComponent<HingeJoint>().spring = jpsoe1_1;

            JointLimits jpsoel1_1 = sagOmuzEk.GetComponent<HingeJoint>().limits;
            jpsoel1_1.max = sagKolJointler[0].maxLimit;
            jpsoel1_1.min = sagKolJointler[0].minLimit;
            sagOmuzEk.GetComponent<HingeJoint>().limits = jpsoel1_1;

            sagOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(sagOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(-0.7f, -0.57f, 0.53f, 0.83f), Time.deltaTime * 10);
            solOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(solOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(0.7f, -0.57f, -0.53f, 0.83f), Time.deltaTime * 10);

            //ConfigurableJoint jpsoe2_1 = sagOmuz.GetComponent<ConfigurableJoint>();
            //jpsoe2_1.targetRotation.x = 0.7f;//+= sagKolJointler [1].spring*0.025f;
            //jpsoe2_1.targetRotation.y = -1f;
            //jpsoe2_1.targetRotation.x = 1f;
            //jpsoe2_1.damper = 10;//sagKolJointler [1].damper;
            //jpsoe2_1.targetPosition = Mathf.Lerp(jpsoe2_1.targetPosition, 180/*sagKolJointler[1].tp*/, Time.deltaTime * kaldirmaLerpSpeed);
            //sagOmuz.GetComponent<ConfigurableJoint> ().targetRotation = Quaternion.Lerp(sagOmuz.GetComponent<ConfigurableJoint> ().targetRotation,new Quaternion(-0.75f,-1f,0.5f,0),Time.deltaTime*20);
            JointDrive spr = new JointDrive();
            spr.positionSpring = 4000;
            spr.positionDamper = 10;
            spr.maximumForce = Mathf.Infinity;
            sagOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr;
            sagOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr;

            /*JointLimits jpsoel2_1 = sagOmuz.GetComponent<HingeJoint>().limits;
				jpsoel2_1.max = 180;//sagKolJointler [1].maxLimit;
				jpsoel2_1.min = -90;//sagKolJointler [1].minLimit;
				sagOmuz.GetComponent<HingeJoint> ().limits = jpsoel2_1;*/


            JointSpring jpsoe3_1 = sagKol.GetComponent<HingeJoint>().spring;
            jpsoe3_1.spring = 8000;// += sagKolJointler [2].spring*0.025f;
            jpsoe3_1.damper = 10;//sagKolJointler [2].damper;
            jpsoe3_1.targetPosition = 60;// Mathf.Lerp(jpsoe3_1.targetPosition, sagKolJointler[2].tp, Time.deltaTime * kaldirmaLerpSpeed);
            sagKol.GetComponent<HingeJoint>().spring = jpsoe3_1;

            JointLimits jpsoel3_1 = sagKol.GetComponent<HingeJoint>().limits;
            jpsoel3_1.max = sagKolJointler[2].maxLimit;
            jpsoel3_1.min = sagKolJointler[2].minLimit;
            sagKol.GetComponent<HingeJoint>().limits = jpsoel3_1;



            JointSpring jpsoe1_2 = solOmuzEk.GetComponent<HingeJoint>().spring;
            jpsoe1_2.spring += solKolJointler[0].spring * 0.025f;
            jpsoe1_2.damper = solKolJointler[0].damper;
            jpsoe1_2.targetPosition = Mathf.Lerp(jpsoe1_2.targetPosition, solKolJointler[0].tp, Time.deltaTime * kaldirmaLerpSpeed);
            solOmuzEk.GetComponent<HingeJoint>().spring = jpsoe1_2;

            JointLimits jpsoel1_2 = solOmuzEk.GetComponent<HingeJoint>().limits;
            jpsoel1_2.max = solKolJointler[0].maxLimit;
            jpsoel1_2.min = solKolJointler[0].minLimit;
            solOmuzEk.GetComponent<HingeJoint>().limits = jpsoel1_2;


            /*	JointSpring jpsoe2_2 = solOmuz.GetComponent<HingeJoint>().spring;
				jpsoe2_2.spring = 20000; //+= solKolJointler [1].spring*0.025f;
				jpsoe2_2.damper = 10;//solKolJointler [1].damper;
				jpsoe2_2.targetPosition = Mathf.Lerp(jpsoe2_2.targetPosition, 0/*solKolJointler[1].tp, Time.deltaTime * kaldirmaLerpSpeed);
				solOmuz.GetComponent<HingeJoint> ().spring = jpsoe2_2;

				JointLimits jpsoel2_2 = solOmuz.GetComponent<HingeJoint>().limits;
				jpsoel2_2.max = 180;//solKolJointler [1].maxLimit;
				jpsoel2_2.min = -180;//solKolJointler [1].minLimit;
				solOmuz.GetComponent<HingeJoint> ().limits = jpsoel2_2;*/

            //	solOmuz.GetComponent<ConfigurableJoint> ().targetRotation = Quaternion.Lerp(solOmuz.GetComponent<ConfigurableJoint> ().targetRotation,new Quaternion(0.75f,-1f,-0.5f,0),Time.deltaTime*20);
            JointDrive spr2 = new JointDrive();
            spr2.positionSpring = 4000;
            spr2.positionDamper = 10;
            spr2.maximumForce = Mathf.Infinity;
            solOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr2;
            solOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr2;

            JointSpring jpsoe3_2 = solKol.GetComponent<HingeJoint>().spring;
            jpsoe3_2.spring = 8000;// += solKolJointler [2].spring*0.025f;
            jpsoe3_2.damper = 10;//solKolJointler [2].damper;
            jpsoe3_2.targetPosition = 60;// Mathf.Lerp(jpsoe3_2.targetPosition, sagKolJointler[2].tp, Time.deltaTime * kaldirmaLerpSpeed);
            solKol.GetComponent<HingeJoint>().spring = jpsoe3_2;

            JointLimits jpsoel3_2 = solKol.GetComponent<HingeJoint>().limits;
            jpsoel3_2.max = solKolJointler[2].maxLimit;
            jpsoel3_2.min = solKolJointler[2].minLimit;
            solKol.GetComponent<HingeJoint>().limits = jpsoel3_2;

            /*JointSpring jpsbx = transform.GetChild(0).GetComponent<HingeJoint>().spring;
				jpsbx.spring = 60;// belJoint.spring*0.025f;
				jpsbx.damper =5;// belJoint.damper;
				//jpsb.targetPosition = Mathf.Lerp(jpsb.targetPosition, belJoint.tp, Time.deltaTime * kaldirmaLerpSpeed);
				transform.GetChild(0).GetComponent<HingeJoint> ().spring = jpsbx;
				*/

            JointSpring jpsbx = transform.GetChild(0).GetComponent<HingeJoint>().spring;
            jpsbx.spring = 2000;// belJoint.spring*0.025f;
            jpsbx.damper = 5;// belJoint.damper;
            jpsbx.targetPosition = -40;
            transform.GetChild(0).GetComponent<HingeJoint>().spring = jpsbx;


            JointLimits jpsoel1_1x = transform.GetChild(0).GetComponent<HingeJoint>().limits;
            jpsoel1_1x.max = 40;
            jpsoel1_1x.min = -40;
            transform.GetChild(0).GetComponent<HingeJoint>().limits = jpsoel1_1x;



            JointSpring jpsb = gogus.GetComponent<HingeJoint>().spring;
            jpsb.spring += belJoint.spring * 0.025f;
            jpsb.damper = belJoint.damper;
            jpsb.targetPosition = Mathf.Lerp(jpsb.targetPosition, belJoint.tp, Time.deltaTime * kaldirmaLerpSpeed);
            gogus.GetComponent<HingeJoint>().spring = jpsb;

            JointLimits jpsbl = gogus.GetComponent<HingeJoint>().limits;
            jpsbl.max = belJoint.maxLimit;
            jpsbl.min = belJoint.minLimit;
            gogus.GetComponent<HingeJoint>().limits = jpsbl;
            yield return Time.deltaTime;
        }
        kaldir_sonrasi();
        durdu = false;
        egildi = false;
        kollarhavada = true;
    }


    IEnumerator _Egil()
    {
        crouch();
        float timer = 0;
        egildi = true;
        kaldiririyor = true;
        kaldirma_kontrol_time = Time.time;
        while (timer <= 0.4f)
        {
            timer += Time.deltaTime;
            float kaldirmaLerpSpeed = 0.5f;


            JointSpring jpsb = gogus.GetComponent<HingeJoint>().spring;
            jpsb.spring = 2000;
            jpsb.damper = 5;
            jpsb.targetPosition = Mathf.Lerp(jpsb.targetPosition, 90, Time.deltaTime * 70);
            gogus.GetComponent<HingeJoint>().spring = jpsb;

            JointLimits jpsbl = gogus.GetComponent<HingeJoint>().limits;
            jpsbl.max = 90;
            jpsbl.min = -30;
            gogus.GetComponent<HingeJoint>().limits = jpsbl;

            sagOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(sagOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(-0.55f, -1f, 0.3f, 0.5f), Time.deltaTime * 20);
            solOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(solOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(0.55f, -1f, -0.3f, 0.5f), Time.deltaTime * 20);


            yield return Time.deltaTime;
        }

        JointSpring jpsbx = gogus.GetComponent<HingeJoint>().spring;
        jpsbx.spring = 2000;
        jpsbx.damper = 5;
        jpsbx.targetPosition = 0;
        gogus.GetComponent<HingeJoint>().spring = jpsbx;
    }
    public void DisardanFirlat()
    {
        if (kollarhavada)
        {
            BeldenFirlat();
        }
    }


    #endregion


    #region Firlatirken Bel Itme
    public float belden_firlat_sure = 0;
    public bool firlat_touch = true;
    public void BeldenFirlat()
    {
        //if(kaldiririyor)
        StartCoroutine(_BeldenFirlat());
    }

    /* IEnumerator _BeldenFirlat()
     {
         float kafagerilmesphiz = 20f;
         float kafagerilmetphiz = 10f;
         float timer = 0;
         JointLimits gjl = gogus.GetComponent<HingeJoint>().limits;
         gjl.max = 90;
         gjl.min = -90;
         gogus.GetComponent<HingeJoint>().limits = gjl;

         while (timer <= 0.8f)
         {
             timer += Time.deltaTime;
             JointSpring gj = gogus.GetComponent<HingeJoint>().spring;
             gj.spring = Mathf.Lerp(gj.spring, 500000f, Time.deltaTime * 1);
             gj.damper = 10;
             gj.targetPosition = Mathf.Lerp(gj.targetPosition, -70f, Time.deltaTime * 2000);
             gogus.GetComponent<HingeJoint>().spring = gj;

             JointLimits gjl1 = gogus.GetComponent<HingeJoint>().limits;
             gjl1.max = 90;
             gjl1.min = -70;
             gogus.GetComponent<HingeJoint>().limits = gjl1;

             yield return Time.deltaTime;
         }

         float kafaatmasphiz = 30f;
         float kafaatmatphiz = 20f;
         float timer2 = 0;
         while (timer2 <= 0.3f)
         {
             timer2 += Time.deltaTime;
             JointSpring gj = gogus.GetComponent<HingeJoint>().spring;
             gj.spring = Mathf.Lerp(gj.spring, 4000f, Time.deltaTime * 200);
             gj.targetPosition = Mathf.Lerp(gj.targetPosition, 10, Time.deltaTime * 200);
             gogus.GetComponent<HingeJoint>().spring = gj;

             yield return Time.deltaTime;
         }
         Debug.Log("Serbest_birak");

         if (myScore <= 1)
             KollariSerbestBirak();

         float timer3 = 0;
         float kafatoparlamasphiz = 10f;
         float kafatoparlamatphiz = 10f;
         while (timer3 <= 0.2f)
         {
             timer3 += Time.deltaTime;
             JointSpring gj = gogus.GetComponent<HingeJoint>().spring;
             gj.spring = Mathf.Lerp(gj.spring, 5000f, Time.deltaTime * 2000);
             gj.targetPosition = Mathf.Lerp(gj.targetPosition, 0, Time.deltaTime * kafatoparlamatphiz);
             gogus.GetComponent<HingeJoint>().spring = gj;
             yield return Time.deltaTime;
         }

         if (myScore > 1)
             KollariSerbestBirak();
         //if(!isAI)
         //  Camera.main.GetComponent<Kamera_A> ().CameraShaker ();


         JointSpring _gj = gogus.GetComponent<HingeJoint>().spring;
         _gj.spring = 400f;
         _gj.targetPosition = 0;
         gogus.GetComponent<HingeJoint>().spring = _gj;

         JointLimits gjl2 = gogus.GetComponent<HingeJoint>().limits;
         gjl2.max = 90;
         gjl2.min = -90;
         gogus.GetComponent<HingeJoint>().limits = gjl2;

         JointSpring jpsb = bacak1.GetComponent<HingeJoint>().spring;
         jpsb.spring = 500;
         jpsb.damper = 10;
         jpsb.targetPosition = 0;
         bacak1.GetComponent<HingeJoint>().spring = jpsb;

         JointSpring jpsb2 = bacak2.GetComponent<HingeJoint>().spring;
         jpsb2.spring = 500;
         jpsb2.damper = 10;
         jpsb2.targetPosition = 0;
         bacak2.GetComponent<HingeJoint>().spring = jpsb2;

         if (isAI)
             GetComponent<AIController>().Repeat();

         firlatici_kontrol = true;

         // KafaAtma(transform.position);
     }

     */
    IEnumerator _BeldenFirlat()
    {
        float kafagerilmesphiz = 20f;
        float kafagerilmetphiz = 10f;
        float timer = 0;
        JointLimits gjl = gogus.GetComponent<HingeJoint>().limits;
        gjl.max = 90;
        gjl.min = -90;
        gogus.GetComponent<HingeJoint>().limits = gjl;

        while (timer <= 0.8f)
        {
            timer += Time.deltaTime;
            JointSpring gj = gogus.GetComponent<HingeJoint>().spring;
            gj.spring = Mathf.Lerp(gj.spring, 500000f, Time.deltaTime * 1);
            gj.damper = 10;
            gj.targetPosition = Mathf.Lerp(gj.targetPosition, -70f, Time.deltaTime * 2000);
            gogus.GetComponent<HingeJoint>().spring = gj;

            JointLimits gjl1 = gogus.GetComponent<HingeJoint>().limits;
            gjl1.max = 90;
            gjl1.min = -70;
            gogus.GetComponent<HingeJoint>().limits = gjl1;


            JointDrive spr = new JointDrive();
            spr.positionSpring = 40000;
            spr.positionDamper = 10;
            spr.maximumForce = Mathf.Infinity;
            solOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr;
            solOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr;

            JointDrive spr2 = new JointDrive();
            spr2.positionSpring = 40000;
            spr2.positionDamper = 10;
            spr2.maximumForce = Mathf.Infinity;
            sagOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr2;
            sagOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr2;


            yield return Time.deltaTime;
        }
        StartCoroutine(_Serbest());
        StartCoroutine(_BeldenFirlat2());

    }
    IEnumerator _Serbest()
    {
        float timer2 = 0;

        while (timer2 <= 0.2f)
        {
            timer2 += Time.deltaTime;
            yield return Time.deltaTime;
        }
        KollariSerbestBirak();
    }

    IEnumerator _BeldenFirlat2()
    {
        float kafaatmasphiz = 30f;
        float kafaatmatphiz = 20f;
        float timer2 = 0;


        while (timer2 <= 0.3f)
        {
            timer2 += Time.deltaTime;
            JointSpring gj = gogus.GetComponent<HingeJoint>().spring;
            gj.spring = Mathf.Lerp(gj.spring, 400000f, Time.deltaTime * 200);
            gj.targetPosition = Mathf.Lerp(gj.targetPosition, 10, Time.deltaTime * 200);
            gogus.GetComponent<HingeJoint>().spring = gj;

            JointDrive spr = new JointDrive();
            spr.positionSpring = 40000;
            spr.positionDamper = 10;
            spr.maximumForce = Mathf.Infinity;
            solOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr;
            solOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr;

            JointDrive spr2 = new JointDrive();
            spr2.positionSpring = 40000;
            spr2.positionDamper = 10;
            spr2.maximumForce = Mathf.Infinity;
            sagOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr2;
            sagOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr2;

            yield return Time.deltaTime;
        }
        // if (myScore <= 1)


        StartCoroutine(_BeldenFirlat3());
    }


    IEnumerator _BeldenFirlat3()
    {
        float timer3 = 0;
        float kafatoparlamasphiz = 10f;
        float kafatoparlamatphiz = 10f;
        while (timer3 <= 0.2f)
        {
            timer3 += Time.deltaTime;
            JointSpring gj = gogus.GetComponent<HingeJoint>().spring;
            gj.spring = Mathf.Lerp(gj.spring, 5000000f, Time.deltaTime * 2000);
            gj.targetPosition = Mathf.Lerp(gj.targetPosition, 0, Time.deltaTime * kafatoparlamatphiz);
            gogus.GetComponent<HingeJoint>().spring = gj;


            JointDrive spr = new JointDrive();
            spr.positionSpring = 40000;
            spr.positionDamper = 10;
            spr.maximumForce = Mathf.Infinity;
            solOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr;
            solOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr;

            JointDrive spr2 = new JointDrive();
            spr2.positionSpring = 40000;
            spr2.positionDamper = 10;
            spr2.maximumForce = Mathf.Infinity;
            sagOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr2;
            sagOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr2;


            yield return Time.deltaTime;
        }
        // if (myScore > 1)
        //     KollariSerbestBirak();

        JointSpring _gj = gogus.GetComponent<HingeJoint>().spring;
        _gj.spring = 400f;
        _gj.targetPosition = 0;
        gogus.GetComponent<HingeJoint>().spring = _gj;

        JointLimits gjl2 = gogus.GetComponent<HingeJoint>().limits;
        gjl2.max = 90;
        gjl2.min = -90;
        gogus.GetComponent<HingeJoint>().limits = gjl2;

        JointSpring jpsb = bacak1.GetComponent<HingeJoint>().spring;
        jpsb.spring = 500;
        jpsb.damper = 10;
        jpsb.targetPosition = 0;
        bacak1.GetComponent<HingeJoint>().spring = jpsb;

        JointSpring jpsb2 = bacak2.GetComponent<HingeJoint>().spring;
        jpsb2.spring = 500;
        jpsb2.damper = 10;
        jpsb2.targetPosition = 0;
        bacak2.GetComponent<HingeJoint>().spring = jpsb2;

        if (isAI)
            GetComponent<AIController>().Repeat();

        firlatici_kontrol = true;
    }

    #endregion

    #region KollarBirakma

    public void KollariSerbestBirak()
    {

        BirakmaForce();

        KolJointleriDestroyEt();
        StartCoroutine(_SagKolBirak());
        StartCoroutine(_SolKolBirak());


        JointSpring jpsbx = transform.GetChild(0).GetComponent<HingeJoint>().spring;
        jpsbx.spring = 200;// belJoint.spring*0.025f;
        jpsbx.damper = 5;// belJoint.damper;
        jpsbx.targetPosition = 0;
        transform.GetChild(0).GetComponent<HingeJoint>().spring = jpsbx;


        JointLimits jpsoel1_1x = transform.GetChild(0).GetComponent<HingeJoint>().limits;
        jpsoel1_1x.max = 20;
        jpsoel1_1x.min = -20;
        transform.GetChild(0).GetComponent<HingeJoint>().limits = jpsoel1_1x;

        //	Debug.Log ("birakti");
    }

    public void KolJointleriDestroyEt()
    {

        if (sagKolTutucu.GetComponent<Tutunma>().tutulanObje)
        {
            GameObject tutulanObj = sagKolTutucu.GetComponent<Tutunma>().tutulanObje;
            if (tutulanObj.GetComponent<ObjeTipi>().objetip == ObjeTipi.ObjeTip.Kaldirilabilir)
            {
                tutulanObj.GetComponent<ObjeTipi>().SetSuanBeniTutan(null);
            }
        }
        else if (solKolTutucu.GetComponent<Tutunma>().tutulanObje)
        {
            GameObject tutulanObj = solKolTutucu.GetComponent<Tutunma>().tutulanObje;
            if (tutulanObj.GetComponent<ObjeTipi>().objetip == ObjeTipi.ObjeTip.Kaldirilabilir)
            {
                tutulanObj.GetComponent<ObjeTipi>().SetSuanBeniTutan(null);
            }
        }

        if (sagKolTutucu.GetComponent<SpringJoint>())
        {
            Destroy(sagKolTutucu.GetComponent<SpringJoint>());
        }
        else if (sagKolTutucu.GetComponent<FixedJoint>())
        {
            Destroy(sagKolTutucu.GetComponent<FixedJoint>());
        }
        else if (sagKolTutucu.GetComponent<ConfigurableJoint>())
        {
            Destroy(sagKolTutucu.GetComponent<ConfigurableJoint>());
        }

        if (solKolTutucu.GetComponent<SpringJoint>())
        {
            Destroy(solKolTutucu.GetComponent<SpringJoint>());
        }
        else if (solKolTutucu.GetComponent<FixedJoint>())
        {
            Destroy(solKolTutucu.GetComponent<FixedJoint>());
        }
        else if (solKolTutucu.GetComponent<ConfigurableJoint>())
        {
            Destroy(solKolTutucu.GetComponent<ConfigurableJoint>());
        }
    }

    IEnumerator _SagKolBirak()
    {
        float timer = 0;
        float sphiz = 20f;
        float tphiz = 5f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime;
            JointSpring jpsoe1_1_a = sagOmuzEk.GetComponent<HingeJoint>().spring;
            jpsoe1_1_a.spring = Mathf.Lerp(jpsoe1_1_a.spring, sagKolJointler[0].spring_i, Time.deltaTime * sphiz);
            jpsoe1_1_a.damper = Mathf.Lerp(jpsoe1_1_a.damper, sagKolJointler[0].damper_i, Time.deltaTime * sphiz);
            jpsoe1_1_a.targetPosition = Mathf.Lerp(jpsoe1_1_a.targetPosition, sagKolJointler[0].tp_i, Time.deltaTime * tphiz);
            sagOmuzEk.GetComponent<HingeJoint>().spring = jpsoe1_1_a;

            /*  JointSpring jpsoe2_1_a = sagOmuz.GetComponent<HingeJoint>().spring;
              jpsoe2_1_a.spring = Mathf.Lerp(jpsoe2_1_a.spring, sagKolJointler[1].spring_i, Time.deltaTime * sphiz);
              jpsoe2_1_a.damper = Mathf.Lerp(jpsoe2_1_a.damper, sagKolJointler[1].damper_i, Time.deltaTime * sphiz);
              jpsoe2_1_a.targetPosition = Mathf.Lerp(jpsoe2_1_a.targetPosition, sagKolJointler[1].tp_i, Time.deltaTime * tphiz);
              sagOmuz.GetComponent<HingeJoint>().spring = jpsoe2_1_a;
  */
            sagOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(sagOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(-0.1f, 0f, 0.2f, 1), Time.deltaTime * 20);



            JointSpring jpsoe3_1_a = sagKol.GetComponent<HingeJoint>().spring;
            jpsoe3_1_a.spring = Mathf.Lerp(jpsoe3_1_a.spring, sagKolJointler[2].spring_i, Time.deltaTime * sphiz);
            jpsoe3_1_a.damper = Mathf.Lerp(jpsoe3_1_a.damper, sagKolJointler[2].damper_i, Time.deltaTime * sphiz);
            jpsoe3_1_a.targetPosition = Mathf.Lerp(jpsoe3_1_a.targetPosition, sagKolJointler[2].tp_i, Time.deltaTime * tphiz);
            sagKol.GetComponent<HingeJoint>().spring = jpsoe3_1_a;


            yield return Time.deltaTime;
        }
        JointSpring jpsoe1_1 = sagOmuzEk.GetComponent<HingeJoint>().spring;
        jpsoe1_1.spring = sagKolJointler[0].spring_i;
        jpsoe1_1.damper = sagKolJointler[0].damper_i;
        jpsoe1_1.targetPosition = sagKolJointler[0].tp_i;
        sagOmuzEk.GetComponent<HingeJoint>().spring = jpsoe1_1;

        JointLimits jpsoel1_1 = sagOmuzEk.GetComponent<HingeJoint>().limits;
        jpsoel1_1.max = sagKolJointler[0].maxLimit_i;
        jpsoel1_1.min = sagKolJointler[0].minLimit_i;
        sagOmuzEk.GetComponent<HingeJoint>().limits = jpsoel1_1;


        /*JointSpring jpsoe2_1 = sagOmuz.GetComponent<HingeJoint>().spring;
        jpsoe2_1.spring = sagKolJointler[1].spring_i;
        jpsoe2_1.damper = sagKolJointler[1].damper_i;
        jpsoe2_1.targetPosition = sagKolJointler[1].tp_i;
        sagOmuz.GetComponent<HingeJoint>().spring = jpsoe2_1;

        JointLimits jpsoel2_1 = sagOmuz.GetComponent<HingeJoint>().limits;
        jpsoel2_1.max = sagKolJointler[1].maxLimit_i;
        jpsoel2_1.min = sagKolJointler[1].minLimit_i;
        sagOmuz.GetComponent<HingeJoint>().limits = jpsoel2_1;
*/

        JointSpring jpsoe3_1 = sagKol.GetComponent<HingeJoint>().spring;
        jpsoe3_1.spring = sagKolJointler[2].spring_i;
        jpsoe3_1.damper = sagKolJointler[2].damper_i;
        jpsoe3_1.targetPosition = sagKolJointler[2].tp_i;
        sagKol.GetComponent<HingeJoint>().spring = jpsoe3_1;

        JointLimits jpsoel3_1 = sagKol.GetComponent<HingeJoint>().limits;
        jpsoel3_1.max = sagKolJointler[2].maxLimit_i;
        jpsoel3_1.min = sagKolJointler[2].minLimit_i;
        sagKol.GetComponent<HingeJoint>().limits = jpsoel3_1;


        JointLimits jpsbl = gogus.GetComponent<HingeJoint>().limits;
        jpsbl.max = belJoint.maxLimit_i;
        jpsbl.min = belJoint.minLimit_i;
        gogus.GetComponent<HingeJoint>().limits = jpsbl;

        sagKolTutucu.GetComponent<Tutunma>().tutundu = false;
        if (!isAI)
            kaldiririyor = false;
        else
            Invoke("kaldiririyor_false", 1f);
        kollarhavada = false;

        JointDrive spr = new JointDrive();
        spr.positionSpring = birak_spr;
        spr.positionDamper = 10;
        spr.maximumForce = Mathf.Infinity;
        sagOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr;
        sagOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr;

        JointSpring jpsbx = transform.GetChild(0).GetComponent<HingeJoint>().spring;
        jpsbx.spring = 200;// belJoint.spring*0.025f;
        jpsbx.damper = 5;// belJoint.damper;
                         //jpsb.targetPosition = Mathf.Lerp(jpsb.targetPosition, belJoint.tp, Time.deltaTime * kaldirmaLerpSpeed);
        transform.GetChild(0).GetComponent<HingeJoint>().spring = jpsbx;
    }


    void kaldiririyor_false()
    {
        kaldiririyor = false;
    }

    IEnumerator _SolKolBirak()
    {
        float sphiz = 20f;
        float tphiz = 5f;
        float timer = 0;
        while (timer <= 1f)
        {
            timer += Time.deltaTime;
            JointSpring jpsoe1_2_a = solOmuzEk.GetComponent<HingeJoint>().spring;
            jpsoe1_2_a.spring = Mathf.Lerp(jpsoe1_2_a.spring, solKolJointler[0].spring_i, Time.deltaTime * sphiz);
            jpsoe1_2_a.damper = Mathf.Lerp(jpsoe1_2_a.damper, solKolJointler[0].damper_i, Time.deltaTime * sphiz);
            jpsoe1_2_a.targetPosition = Mathf.Lerp(jpsoe1_2_a.targetPosition, solKolJointler[0].tp_i, Time.deltaTime * tphiz);
            solOmuzEk.GetComponent<HingeJoint>().spring = jpsoe1_2_a;

            /*JointSpring jpsoe2_2_a = solOmuz.GetComponent<HingeJoint>().spring;
             jpsoe2_2_a.spring = Mathf.Lerp(jpsoe2_2_a.spring, solKolJointler[1].spring_i, Time.deltaTime * sphiz);
             jpsoe2_2_a.damper = Mathf.Lerp(jpsoe2_2_a.damper, solKolJointler[1].damper_i, Time.deltaTime * sphiz);
             jpsoe2_2_a.targetPosition = Mathf.Lerp(jpsoe2_2_a.targetPosition, solKolJointler[1].tp_i, Time.deltaTime * tphiz);
             solOmuz.GetComponent<HingeJoint>().spring = jpsoe2_2_a;
 */

            solOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(solOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(0.1f, 0f, -0.2f, 1f), Time.deltaTime * 20);



            JointSpring jpsoe3_2_a = solKol.GetComponent<HingeJoint>().spring;
            jpsoe3_2_a.spring = Mathf.Lerp(jpsoe3_2_a.spring, solKolJointler[2].spring_i, Time.deltaTime * sphiz);
            jpsoe3_2_a.damper = Mathf.Lerp(jpsoe3_2_a.damper, solKolJointler[2].damper_i, Time.deltaTime * sphiz);
            jpsoe3_2_a.targetPosition = Mathf.Lerp(jpsoe3_2_a.targetPosition, sagKolJointler[2].tp_i, Time.deltaTime * tphiz);
            solKol.GetComponent<HingeJoint>().spring = jpsoe3_2_a;

            JointSpring jpsb_a = gogus.GetComponent<HingeJoint>().spring;
            jpsb_a.spring = Mathf.Lerp(jpsb_a.spring, belJoint.spring_i, Time.deltaTime * sphiz);
            jpsb_a.damper = Mathf.Lerp(jpsb_a.damper, belJoint.damper_i, Time.deltaTime * sphiz);
            jpsb_a.targetPosition = Mathf.Lerp(jpsb_a.targetPosition, belJoint.tp_i, Time.deltaTime * tphiz);
            gogus.GetComponent<HingeJoint>().spring = jpsb_a;

            yield return Time.deltaTime;
        }
        JointSpring jpsoe1_2 = solOmuzEk.GetComponent<HingeJoint>().spring;
        jpsoe1_2.spring = solKolJointler[0].spring_i;
        jpsoe1_2.damper = solKolJointler[0].damper_i;
        jpsoe1_2.targetPosition = solKolJointler[0].tp_i;
        solOmuzEk.GetComponent<HingeJoint>().spring = jpsoe1_2;

        JointLimits jpsoel1_2 = solOmuzEk.GetComponent<HingeJoint>().limits;
        jpsoel1_2.max = solKolJointler[0].maxLimit_i;
        jpsoel1_2.min = solKolJointler[0].minLimit_i;
        solOmuzEk.GetComponent<HingeJoint>().limits = jpsoel1_2;


        /*JointSpring jpsoe2_2 = solOmuz.GetComponent<HingeJoint>().spring;
        jpsoe2_2.spring = solKolJointler[1].spring_i;
        jpsoe2_2.damper = solKolJointler[1].damper_i;
        jpsoe2_2.targetPosition = solKolJointler[1].tp_i;
        solOmuz.GetComponent<HingeJoint>().spring = jpsoe2_2;

        JointLimits jpsoel2_2 = solOmuz.GetComponent<HingeJoint>().limits;
        jpsoel2_2.max = solKolJointler[1].maxLimit_i;
        jpsoel2_2.min = solKolJointler[1].minLimit_i;
        solOmuz.GetComponent<HingeJoint>().limits = jpsoel2_2;
*/

        JointSpring jpsoe3_2 = solKol.GetComponent<HingeJoint>().spring;
        jpsoe3_2.spring = solKolJointler[2].spring_i;
        jpsoe3_2.damper = solKolJointler[2].damper_i;
        jpsoe3_2.targetPosition = sagKolJointler[2].tp_i;
        solKol.GetComponent<HingeJoint>().spring = jpsoe3_2;

        JointLimits jpsoel3_2 = solKol.GetComponent<HingeJoint>().limits;
        jpsoel3_2.max = solKolJointler[2].maxLimit_i;
        jpsoel3_2.min = solKolJointler[2].minLimit_i;
        solKol.GetComponent<HingeJoint>().limits = jpsoel3_2;

        JointSpring jpsb = gogus.GetComponent<HingeJoint>().spring;
        jpsb.spring = belJoint.spring_i;
        jpsb.damper = belJoint.damper_i;
        jpsb.targetPosition = belJoint.tp_i;
        gogus.GetComponent<HingeJoint>().spring = jpsb;

        solKolTutucu.GetComponent<Tutunma>().tutundu = false;

        kollarhavada = false;

        JointDrive spr = new JointDrive();
        spr.positionSpring = birak_spr;
        spr.positionDamper = 10;
        spr.maximumForce = Mathf.Infinity;
        solOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr;
        solOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr;
    }

    bool oncett2 = true;
    IEnumerator diren()
    {
        float timer = 0;
        float sphiz = 200f;
        float tphiz = 50f;
        while (timer <= 0.2f)
        {
            timer += Time.deltaTime;
            if (oncett2)
            {
                sagOmuz.GetComponent<ConfigurableJoint>().targetRotation = new Quaternion(-0.1f, 0.7f, -0.07f, 0.83f);// Quaternion.Lerp (sagOmuz.GetComponent<ConfigurableJoint> ().targetRotation, new Quaternion (-0.1f, 0.7f, -0.07f, 0.83f), Time.deltaTime * 300);

                JointDrive spr = new JointDrive();
                spr.positionSpring = 400;
                spr.positionDamper = 10;
                spr.maximumForce = Mathf.Infinity;
                sagOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr;
                sagOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr;

            }
            else
            {
                solOmuz.GetComponent<ConfigurableJoint>().targetRotation = new Quaternion(0.1f, 0.7f, 0.07f, 0.83f);//Quaternion.Lerp (solOmuz.GetComponent<ConfigurableJoint> ().targetRotation, new Quaternion (0.1f, 0.7f, 0.07f, 0.83f), Time.deltaTime * 300);

                JointDrive spr = new JointDrive();
                spr.positionSpring = 400;
                spr.positionDamper = 10;
                spr.maximumForce = Mathf.Infinity;
                solOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr;
                solOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr;
            }
            //sagOmuz.layer = temp_layer;
            //solOmuz.layer = temp_layer;
            //	sagKol.layer = temp_layer;
            //solKol.layer = temp_layer;
            //kafa.layer = temp_layer;
            //kaval1.layer = temp_layer;
            //kaval2.layer = temp_layer;


            /*JointSpring jpsb = bacak1.GetComponent<HingeJoint>().spring;
			jpsb.spring = 0;
			jpsb.damper = 0;
			jpsb.targetPosition = 0;
			bacak1.GetComponent<HingeJoint>().spring = jpsb;

			JointSpring jpsb2 = bacak2.GetComponent<HingeJoint>().spring;
			jpsb2.spring = 0;
			jpsb2.damper = 0;
			jpsb2.targetPosition = 0;
			bacak2.GetComponent<HingeJoint>().spring = jpsb2;
			*/
            yield return Time.deltaTime;
        }
        //if(oncett2)
        diren_false_fonk();

    }


    IEnumerator diren_false()
    {
        float timer = 0;
        float sphiz = 200f;
        float tphiz = 50f;
        while (timer <= 0.2f)
        {
            timer += Time.deltaTime;
            if (oncett2)
            {
                sagOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(sagOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(-0.1f, -0.7f, -0.07f, 0.83f), Time.deltaTime * 20);
            }
            else
            {
                solOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(solOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(0.1f, -0.7f, 0.07f, 0.83f), Time.deltaTime * 20);
            }
            yield return Time.deltaTime;
        }
        oncett2 = !oncett2;
    }

    IEnumerator _SagKolBirak2()
    {
        float timer = 0;
        float sphiz = 200f;
        float tphiz = 50f;
        while (timer <= 0.1f)
        {
            timer += Time.deltaTime;
            JointSpring jpsoe1_1_a = sagOmuzEk.GetComponent<HingeJoint>().spring;
            jpsoe1_1_a.spring = Mathf.Lerp(jpsoe1_1_a.spring, sagKolJointler[0].spring_i, Time.deltaTime * sphiz);
            jpsoe1_1_a.damper = Mathf.Lerp(jpsoe1_1_a.damper, sagKolJointler[0].damper_i, Time.deltaTime * sphiz);
            jpsoe1_1_a.targetPosition = Mathf.Lerp(jpsoe1_1_a.targetPosition, sagKolJointler[0].tp_i, Time.deltaTime * tphiz);
            sagOmuzEk.GetComponent<HingeJoint>().spring = jpsoe1_1_a;

            /*
			 * JointSpring jpsoe2_1_a = sagOmuz.GetComponent<HingeJoint>().spring;
			jpsoe2_1_a.spring = Mathf.Lerp(jpsoe2_1_a.spring, sagKolJointler[1].spring_i, Time.deltaTime * sphiz);
			jpsoe2_1_a.damper = Mathf.Lerp(jpsoe2_1_a.damper, sagKolJointler[1].damper_i, Time.deltaTime * sphiz);
			jpsoe2_1_a.targetPosition = Mathf.Lerp(jpsoe2_1_a.targetPosition, sagKolJointler[1].tp_i, Time.deltaTime * tphiz);
			sagOmuz.GetComponent<HingeJoint>().spring = jpsoe2_1_a;
			*/

            sagOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(sagOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(-0.1f, 0f, 0.2f, 1), Time.deltaTime * 20);
            JointDrive spr = new JointDrive();
            spr.positionSpring = birak_spr;
            spr.positionDamper = 10;
            spr.maximumForce = Mathf.Infinity;
            sagOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr;
            sagOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr;


            JointSpring jpsoe3_1_a = sagKol.GetComponent<HingeJoint>().spring;
            jpsoe3_1_a.spring = Mathf.Lerp(jpsoe3_1_a.spring, sagKolJointler[2].spring_i, Time.deltaTime * sphiz);
            jpsoe3_1_a.damper = Mathf.Lerp(jpsoe3_1_a.damper, sagKolJointler[2].damper_i, Time.deltaTime * sphiz);
            jpsoe3_1_a.targetPosition = Mathf.Lerp(jpsoe3_1_a.targetPosition, sagKolJointler[2].tp_i, Time.deltaTime * tphiz);
            sagKol.GetComponent<HingeJoint>().spring = jpsoe3_1_a;


            yield return Time.deltaTime;
        }


        JointSpring jpsoe1_1 = sagOmuzEk.GetComponent<HingeJoint>().spring;
        jpsoe1_1.spring = sagKolJointler[0].spring_i;
        jpsoe1_1.damper = sagKolJointler[0].damper_i;
        jpsoe1_1.targetPosition = sagKolJointler[0].tp_i;
        sagOmuzEk.GetComponent<HingeJoint>().spring = jpsoe1_1;

        JointLimits jpsoel1_1 = sagOmuzEk.GetComponent<HingeJoint>().limits;
        jpsoel1_1.max = sagKolJointler[0].maxLimit_i;
        jpsoel1_1.min = sagKolJointler[0].minLimit_i;
        sagOmuzEk.GetComponent<HingeJoint>().limits = jpsoel1_1;


        /*JointSpring jpsoe2_1 = sagOmuz.GetComponent<HingeJoint>().spring;
		jpsoe2_1.spring = sagKolJointler[1].spring_i;
		jpsoe2_1.damper = sagKolJointler[1].damper_i;
		jpsoe2_1.targetPosition = sagKolJointler[1].tp_i;
		sagOmuz.GetComponent<HingeJoint>().spring = jpsoe2_1;

		JointLimits jpsoel2_1 = sagOmuz.GetComponent<HingeJoint>().limits;
		jpsoel2_1.max = sagKolJointler[1].maxLimit_i;
		jpsoel2_1.min = sagKolJointler[1].minLimit_i;
		sagOmuz.GetComponent<HingeJoint>().limits = jpsoel2_1;*/


        JointSpring jpsoe3_1 = sagKol.GetComponent<HingeJoint>().spring;
        jpsoe3_1.spring = sagKolJointler[2].spring_i;
        jpsoe3_1.damper = sagKolJointler[2].damper_i;
        jpsoe3_1.targetPosition = sagKolJointler[2].tp_i;
        sagKol.GetComponent<HingeJoint>().spring = jpsoe3_1;

        JointLimits jpsoel3_1 = sagKol.GetComponent<HingeJoint>().limits;
        jpsoel3_1.max = sagKolJointler[2].maxLimit_i;
        jpsoel3_1.min = sagKolJointler[2].minLimit_i;
        sagKol.GetComponent<HingeJoint>().limits = jpsoel3_1;

        p1.stop = false;
        p2.stop = false;
        p3.stop = false;
        p4.stop = false;
        /*JointLimits jpsbl = gogus.GetComponent<HingeJoint>().limits;
		jpsbl.max = belJoint.maxLimit_i;
		jpsbl.min = belJoint.minLimit_i;
		gogus.GetComponent<HingeJoint>().limits = jpsbl;*/

        //sagKolTutucu.GetComponent<Tutunma> ().tutundu = false;
        //kaldiririyor = false;
        //kollarhavada = false;
    }

    IEnumerator _SolKolBirak2()
    {
        float sphiz = 200f;
        float tphiz = 50f;
        float timer = 0;
        while (timer <= 0.1f)
        {
            timer += Time.deltaTime;
            JointSpring jpsoe1_2_a = solOmuzEk.GetComponent<HingeJoint>().spring;
            jpsoe1_2_a.spring = Mathf.Lerp(jpsoe1_2_a.spring, solKolJointler[0].spring_i, Time.deltaTime * sphiz);
            jpsoe1_2_a.damper = Mathf.Lerp(jpsoe1_2_a.damper, solKolJointler[0].damper_i, Time.deltaTime * sphiz);
            jpsoe1_2_a.targetPosition = Mathf.Lerp(jpsoe1_2_a.targetPosition, solKolJointler[0].tp_i, Time.deltaTime * tphiz);
            solOmuzEk.GetComponent<HingeJoint>().spring = jpsoe1_2_a;

            /*
			JointSpring jpsoe2_2_a = solOmuz.GetComponent<HingeJoint>().spring;
			jpsoe2_2_a.spring = Mathf.Lerp(jpsoe2_2_a.spring, solKolJointler[1].spring_i, Time.deltaTime * sphiz);
			jpsoe2_2_a.damper = Mathf.Lerp(jpsoe2_2_a.damper, solKolJointler[1].damper_i, Time.deltaTime * sphiz);
			jpsoe2_2_a.targetPosition = Mathf.Lerp(jpsoe2_2_a.targetPosition, solKolJointler[1].tp_i, Time.deltaTime * tphiz);
			solOmuz.GetComponent<HingeJoint>().spring = jpsoe2_2_a;
			*/

            solOmuz.GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Lerp(solOmuz.GetComponent<ConfigurableJoint>().targetRotation, new Quaternion(0.1f, 0f, -0.2f, 1f), Time.deltaTime * 20);
            JointDrive spr = new JointDrive();
            spr.positionSpring = birak_spr;
            spr.positionDamper = 10;
            spr.maximumForce = Mathf.Infinity;
            solOmuz.GetComponent<ConfigurableJoint>().angularXDrive = spr;
            solOmuz.GetComponent<ConfigurableJoint>().angularYZDrive = spr;

            JointSpring jpsoe3_2_a = solKol.GetComponent<HingeJoint>().spring;
            jpsoe3_2_a.spring = Mathf.Lerp(jpsoe3_2_a.spring, solKolJointler[2].spring_i, Time.deltaTime * sphiz);
            jpsoe3_2_a.damper = Mathf.Lerp(jpsoe3_2_a.damper, solKolJointler[2].damper_i, Time.deltaTime * sphiz);
            jpsoe3_2_a.targetPosition = Mathf.Lerp(jpsoe3_2_a.targetPosition, sagKolJointler[2].tp_i, Time.deltaTime * tphiz);
            solKol.GetComponent<HingeJoint>().spring = jpsoe3_2_a;

            /*JointSpring jpsb_a = gogus.GetComponent<HingeJoint>().spring;
			jpsb_a.spring = Mathf.Lerp(jpsb_a.spring, belJoint.spring_i, Time.deltaTime * sphiz);
			jpsb_a.damper = Mathf.Lerp(jpsb_a.damper, belJoint.damper_i, Time.deltaTime * sphiz);
			jpsb_a.targetPosition = Mathf.Lerp(jpsb_a.targetPosition, belJoint.tp_i, Time.deltaTime * tphiz);
			gogus.GetComponent<HingeJoint>().spring = jpsb_a;
*/
            yield return Time.deltaTime;
        }
        JointSpring jpsoe1_2 = solOmuzEk.GetComponent<HingeJoint>().spring;
        jpsoe1_2.spring = solKolJointler[0].spring_i;
        jpsoe1_2.damper = solKolJointler[0].damper_i;
        jpsoe1_2.targetPosition = solKolJointler[0].tp_i;
        solOmuzEk.GetComponent<HingeJoint>().spring = jpsoe1_2;

        JointLimits jpsoel1_2 = solOmuzEk.GetComponent<HingeJoint>().limits;
        jpsoel1_2.max = solKolJointler[0].maxLimit_i;
        jpsoel1_2.min = solKolJointler[0].minLimit_i;
        solOmuzEk.GetComponent<HingeJoint>().limits = jpsoel1_2;


        /*JointSpring jpsoe2_2 = solOmuz.GetComponent<HingeJoint>().spring;
		jpsoe2_2.spring = solKolJointler[1].spring_i;
		jpsoe2_2.damper = solKolJointler[1].damper_i;
		jpsoe2_2.targetPosition = solKolJointler[1].tp_i;
		solOmuz.GetComponent<HingeJoint>().spring = jpsoe2_2;

		JointLimits jpsoel2_2 = solOmuz.GetComponent<HingeJoint>().limits;
		jpsoel2_2.max = solKolJointler[1].maxLimit_i;
		jpsoel2_2.min = solKolJointler[1].minLimit_i;
		solOmuz.GetComponent<HingeJoint>().limits = jpsoel2_2;
		*/

        JointSpring jpsoe3_2 = solKol.GetComponent<HingeJoint>().spring;
        jpsoe3_2.spring = solKolJointler[2].spring_i;
        jpsoe3_2.damper = solKolJointler[2].damper_i;
        jpsoe3_2.targetPosition = sagKolJointler[2].tp_i;
        solKol.GetComponent<HingeJoint>().spring = jpsoe3_2;

        JointLimits jpsoel3_2 = solKol.GetComponent<HingeJoint>().limits;
        jpsoel3_2.max = solKolJointler[2].maxLimit_i;
        jpsoel3_2.min = solKolJointler[2].minLimit_i;
        solKol.GetComponent<HingeJoint>().limits = jpsoel3_2;

        /*	JointSpring jpsb = gogus.GetComponent<HingeJoint>().spring;
            jpsb.spring = belJoint.spring_i;
            jpsb.damper = belJoint.damper_i;
            jpsb.targetPosition = belJoint.tp_i;
            gogus.GetComponent<HingeJoint>().spring = jpsb;
    */
        //solKolTutucu.GetComponent<Tutunma> ().tutundu = false;
        //kaldiririyor = false;
        //kollarhavada = false;
    }
    Rigidbody cnbsag;
    bool once_birakma = true;
    private void BirakmaForce()
    {

        if (once_birakma)
        {
            once_birakma = false;
            Invoke("once_birakma_kontrol", 2f);
            if (sagKolTutucu.GetComponent<Tutunma>().kafa && sagKolTutucu.GetComponent<Tutunma>().kafa.transform.root.GetChild(0).GetComponent<PlayerController_A>().dustu)
            {
                cnbsag = sagKolTutucu.GetComponent<Tutunma>().kafa.transform.root.GetChild(0).GetComponent<Rigidbody>();
                if (cnbsag.GetComponent<FirlatianObje>())
                {
                    cnbsag.AddForce(transform.forward * cnbsag.mass * 5f, ForceMode.Impulse);
                    cnbsag.AddForce(Vector3.up * cnbsag.mass * 2.5f, ForceMode.Impulse);
                    cnbsag.GetComponent<FirlatianObje>().dayaniklilikcarpani = 0.2f;
                }
                else
                {
                    transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    cnbsag.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    if (myScore == 0)
                    {
                        cnbsag.AddForce(transform.forward * 7, ForceMode.VelocityChange);
                        cnbsag.AddForce(Vector3.up * 2, ForceMode.VelocityChange);
                    }
                    else
                    {
                        cnbsag.AddForce(transform.forward * 5 * (myScore + 1), ForceMode.VelocityChange);
                        cnbsag.AddForce(Vector3.up * 2 * (myScore + 1), ForceMode.VelocityChange);
                    }
                }
                Debug.Log("Firlatildi" + transform.root);
            }
            else if (sagKolTutucu.GetComponent<Tutunma>().kafa)
            {

                cnbsag = sagKolTutucu.GetComponent<Tutunma>().kafa.transform.root.GetChild(0).GetComponent<Rigidbody>();
                if (cnbsag.GetComponent<FirlatianObje>())
                {
                    cnbsag.AddForce(transform.forward * cnbsag.mass * 5f, ForceMode.Impulse);
                    cnbsag.AddForce(Vector3.up * cnbsag.mass * 2.5f, ForceMode.Impulse);
                    cnbsag.GetComponent<FirlatianObje>().dayaniklilikcarpani = 0.2f;
                }
                else
                {
                    transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    cnbsag.GetComponent<Rigidbody>().velocity = Vector3.zero;

                    cnbsag.AddForce(transform.forward * 1, ForceMode.VelocityChange);
                    cnbsag.AddForce(Vector3.up * 1, ForceMode.VelocityChange);



                    Debug.Log("oooooppppp");

                }


            }

            Invoke("layer_deg", 0.21f);
        }
    }

    private void once_birakma_kontrol()
    {
        once_birakma = true;
    }

    private void layer_deg()
    {
        for (int i = 0; i < sagKolTutucu.GetComponent<Tutunma>().trnsform.Count; i++)
        {
            sagKolTutucu.GetComponent<Tutunma>().trnsform[i].gameObject.layer = sagKolTutucu.GetComponent<Tutunma>().layrr;
            //Debug.Log ("tutunmaaa-------------------");

        }
        for (int deg = 0; deg < sagKolTutucu.GetComponent<Tutunma>().trnsform.Count; deg++)
        {


            sagKolTutucu.GetComponent<Tutunma>().trnsform.Clear();
        }
        sagKolTutucu.GetComponent<Tutunma>().kafa = null;
    }

    #endregion

    #region TekmeAtis
    public void TekmeAt(Transform target)
    {
        if (!isAI)
        {
            Instantiate(GameManager_A.gameManager.ucanTekmeParticle, new Vector3(transform.position.x,
                    transform.position.y - GetComponent<CapsuleCollider>().height / 2f, transform.position.z), Quaternion.identity);
            GameManager_A.gameManager.DoSlowMo();
            Camera.main.GetComponent<Kamera_A>().FovEfekt();
        }
        tekme_havada = true;
        if (tekmeAtabilir)
        {
            TekmeZipla(target);
        }
    }

    IEnumerator TekmeYonlendir(Transform target)
    {
        TumHizlariSifirla();
        while (tekme_havada)
        {
            if (isAI)
            {
                Vector3 diff = transform.position - target.transform.position;
                diff = -Vector3.ProjectOnPlane(diff, Vector3.up).normalized;
                GetComponent<Rigidbody>().AddForce(diff * Time.deltaTime * 100f, ForceMode.VelocityChange);
                //GetComponent<Rigidbody> ().MovePosition (GetComponent<Rigidbody> ().position + diff * Time.deltaTime * 10f);
            }
            else
            {
                Vector3 diff = transform.position - target.transform.position;
                diff = -Vector3.ProjectOnPlane(diff, Vector3.up).normalized;
                //GetComponent<Rigidbody> ().MovePosition (GetComponent<Rigidbody> ().position + diff * Time.deltaTime * 20f);
                GetComponent<Rigidbody>().AddForce(diff * Time.deltaTime * 150f, ForceMode.VelocityChange);
            }
            yield return Time.deltaTime;
        }
    }

    private void TekmeZipla(Transform target)
    {
        tekmeAtabilir = false;
        StartCoroutine(TekmeYonlendir(target));
        p1.enabled = false;
        p2.enabled = false;
        p3.enabled = false;
        p4.enabled = false;
        GetComponent<Rigidbody>().AddForce(Vector3.up * 400f + hips.transform.forward * 100f, ForceMode.Impulse);
        Invoke("TekmeBasla", 0.05f);
    }

    private void TekmeBasla()
    {
        StartCoroutine(TekmeAtis());
    }

    IEnumerator TekmeAtis()
    {
        float timer = 0;
        JointLimits jpsoel1_2 = solOmuzEk.GetComponent<HingeJoint>().limits;
        jpsoel1_2.max = 180;
        jpsoel1_2.min = -180;
        solOmuzEk.GetComponent<HingeJoint>().limits = jpsoel1_2;

        JointLimits jpsoel1_1 = sagOmuzEk.GetComponent<HingeJoint>().limits;
        jpsoel1_1.max = 180;
        jpsoel1_1.min = -180;
        sagOmuzEk.GetComponent<HingeJoint>().limits = jpsoel1_1;

        while (timer <= tekmeVurusSure)
        {

            timer += Time.deltaTime;
            JointSpring jpba = p1.transform.GetChild(0).GetComponent<HingeJoint>().spring;
            jpba.spring = Mathf.Lerp(jpba.spring, 500000, Time.deltaTime * tekmeSpringHiz);
            jpba.targetPosition = Mathf.Lerp(jpba.targetPosition, 0, Time.deltaTime * tekmeTargetPosHiz);
            p1.transform.GetChild(0).GetComponent<HingeJoint>().spring = jpba;

            JointSpring jpba2 = p2.transform.GetChild(0).GetComponent<HingeJoint>().spring;
            jpba2.spring = Mathf.Lerp(jpba2.spring, 500000, Time.deltaTime * tekmeSpringHiz);
            jpba2.targetPosition = Mathf.Lerp(jpba2.targetPosition, 0, Time.deltaTime * tekmeTargetPosHiz);
            p2.transform.GetChild(0).GetComponent<HingeJoint>().spring = jpba2;


            JointSpring jpba3 = p1.GetComponent<HingeJoint>().spring;
            jpba3.spring = Mathf.Lerp(jpba3.spring, 500000, Time.deltaTime * tekmeSpringHiz);
            jpba3.targetPosition = Mathf.Lerp(jpba3.targetPosition, 90, Time.deltaTime * tekmeTargetPosHiz);
            p1.GetComponent<HingeJoint>().spring = jpba3;

            JointSpring jpba4 = p2.GetComponent<HingeJoint>().spring;
            jpba4.spring = Mathf.Lerp(jpba4.spring, 500000, Time.deltaTime * tekmeSpringHiz);
            jpba4.targetPosition = Mathf.Lerp(jpba4.targetPosition, 90, Time.deltaTime * tekmeTargetPosHiz);
            p2.GetComponent<HingeJoint>().spring = jpba4;


            JointSpring jpgo = gogus.GetComponent<HingeJoint>().spring;
            jpgo.targetPosition = Mathf.Lerp(jpgo.targetPosition, -10, Time.deltaTime * tekmeTargetPosHiz);
            gogus.GetComponent<HingeJoint>().spring = jpgo;

            yield return Time.deltaTime;
        }
        //if (!isAI) {
        //	Camera.main.GetComponent<Kamera_A> ().CameraShaker ();
        //}
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        timer = 0;
        Vector3 hpfw = hips.transform.forward;
        while (timer <= 0.3f)
        {
            timer += Time.deltaTime;
            JointSpring jpsoe1_1 = sagOmuzEk.GetComponent<HingeJoint>().spring;
            jpsoe1_1.spring = Mathf.Lerp(jpsoe1_1.spring, 500000, Time.deltaTime * tekmeSpringHiz);
            jpsoe1_1.targetPosition = Mathf.Lerp(jpsoe1_1.targetPosition, 180, Time.deltaTime * tekmeTargetPosHiz);
            sagOmuzEk.GetComponent<HingeJoint>().spring = jpsoe1_1;

            JointSpring jpsoe1_2 = solOmuzEk.GetComponent<HingeJoint>().spring;
            jpsoe1_2.spring = Mathf.Lerp(jpsoe1_2.spring, 500000, Time.deltaTime * tekmeSpringHiz);
            jpsoe1_2.targetPosition = Mathf.Lerp(jpsoe1_2.targetPosition, -180, Time.deltaTime * tekmeTargetPosHiz);
            solOmuzEk.GetComponent<HingeJoint>().spring = jpsoe1_2;

            kafa.GetComponent<Rigidbody>().AddForceAtPosition(-hips.transform.forward * 1000f, kafa.transform.position);
            p1.transform.GetChild(0).GetComponent<Rigidbody>().AddForceAtPosition(hpfw * 300f, p1.transform.GetChild(0).transform.position);
            p2.transform.GetChild(0).GetComponent<Rigidbody>().AddForceAtPosition(hpfw * 300f, p2.transform.GetChild(0).transform.position);

            yield return Time.deltaTime;
        }
        hips.GetComponent<Rigidbody>().AddForceAtPosition(Vector3.down * 9000f, hips.transform.position);
        Invoke("SpringleriKapat", 0.5f);
        Invoke("TekmeToparla", 1f);
    }

    private void TekmeToparla()
    {
        StartCoroutine(TekmeToparlayis());
    }

    IEnumerator TekmeToparlayis()
    {
        float timer = 0;
        JointLimits jpsoel1_1 = sagOmuzEk.GetComponent<HingeJoint>().limits;
        jpsoel1_1.max = sagKolJointler[0].maxLimit_i;
        jpsoel1_1.min = sagKolJointler[0].minLimit_i;
        sagOmuzEk.GetComponent<HingeJoint>().limits = jpsoel1_1;

        JointLimits jpsoel1_2 = solOmuzEk.GetComponent<HingeJoint>().limits;
        jpsoel1_2.max = solKolJointler[0].maxLimit_i;
        jpsoel1_2.min = solKolJointler[0].minLimit_i;
        solOmuzEk.GetComponent<HingeJoint>().limits = jpsoel1_2;

        while (timer <= tekmeToplaSure)
        {
            timer += Time.deltaTime;

            JointSpring jpsoer = solOmuzEk.GetComponent<HingeJoint>().spring;
            jpsoer.spring = Mathf.Lerp(jpsoer.spring, solKolJointler[0].spring_i, Time.deltaTime * tekmeToplaSpringHiz);
            jpsoer.damper = solKolJointler[0].damper_i;
            jpsoer.targetPosition = Mathf.Lerp(jpsoer.targetPosition, solKolJointler[0].tp_i, Time.deltaTime * tekmeToplaTargetPosHiz);
            solOmuzEk.GetComponent<HingeJoint>().spring = jpsoer;

            JointSpring jpsoel = sagOmuzEk.GetComponent<HingeJoint>().spring;
            jpsoel.spring = Mathf.Lerp(jpsoel.spring, sagKolJointler[0].spring_i, Time.deltaTime * tekmeToplaSpringHiz);
            jpsoel.damper = sagKolJointler[0].damper_i;
            jpsoel.targetPosition = Mathf.Lerp(jpsoel.targetPosition, sagKolJointler[0].tp_i, Time.deltaTime * tekmeTargetPosHiz);
            sagOmuzEk.GetComponent<HingeJoint>().spring = jpsoel;

            JointSpring jpba = p1.transform.GetChild(0).GetComponent<HingeJoint>().spring;
            jpba.spring = Mathf.Lerp(jpba.spring, 500, Time.deltaTime * tekmeSpringHiz);
            jpba.targetPosition = Mathf.Lerp(jpba.targetPosition, 0, Time.deltaTime * tekmeTargetPosHiz);
            p1.transform.GetChild(0).GetComponent<HingeJoint>().spring = jpba;

            JointSpring jpba2 = p2.transform.GetChild(0).GetComponent<HingeJoint>().spring;
            jpba2.spring = Mathf.Lerp(jpba2.spring, 500, Time.deltaTime * tekmeSpringHiz);
            jpba2.targetPosition = Mathf.Lerp(jpba2.targetPosition, 0, Time.deltaTime * tekmeTargetPosHiz);
            p2.transform.GetChild(0).GetComponent<HingeJoint>().spring = jpba2;


            JointSpring jpba3 = p1.GetComponent<HingeJoint>().spring;
            jpba3.spring = Mathf.Lerp(jpba3.spring, 5, Time.deltaTime * tekmeSpringHiz);
            jpba3.targetPosition = Mathf.Lerp(jpba3.targetPosition, 0, Time.deltaTime * tekmeTargetPosHiz);
            p1.GetComponent<HingeJoint>().spring = jpba3;

            JointSpring jpba4 = p2.GetComponent<HingeJoint>().spring;
            jpba4.spring = Mathf.Lerp(jpba4.spring, 5, Time.deltaTime * tekmeSpringHiz);
            jpba4.targetPosition = Mathf.Lerp(jpba4.targetPosition, 0, Time.deltaTime * tekmeTargetPosHiz);
            p2.GetComponent<HingeJoint>().spring = jpba4;

            yield return Time.deltaTime;
        }

        JointSpring jpsoer2 = solOmuzEk.GetComponent<HingeJoint>().spring;
        jpsoer2.spring = solKolJointler[0].spring_i;
        jpsoer2.damper = solKolJointler[0].damper_i;
        jpsoer2.targetPosition = solKolJointler[0].tp_i;
        solOmuzEk.GetComponent<HingeJoint>().spring = jpsoer2;

        JointSpring jpsoel2 = sagOmuzEk.GetComponent<HingeJoint>().spring;
        jpsoel2.spring = sagKolJointler[0].spring_i;
        jpsoel2.damper = sagKolJointler[0].damper_i;
        jpsoel2.targetPosition = sagKolJointler[0].tp_i;
        sagOmuzEk.GetComponent<HingeJoint>().spring = jpsoel2;

        JointSpring jpba21 = p3.GetComponent<HingeJoint>().spring;
        jpba21.spring = 500;
        jpba21.targetPosition = 0;
        p3.GetComponent<HingeJoint>().spring = jpba21;

        JointSpring jpba22 = p4.GetComponent<HingeJoint>().spring;
        jpba22.spring = 500;
        jpba22.targetPosition = 0;
        p4.GetComponent<HingeJoint>().spring = jpba22;


        JointSpring jpba32 = p1.GetComponent<HingeJoint>().spring;
        jpba32.spring = 5;
        jpba32.targetPosition = 0;
        p1.GetComponent<HingeJoint>().spring = jpba32;

        JointSpring jpba42 = p2.GetComponent<HingeJoint>().spring;
        jpba42.spring = 5;
        jpba42.targetPosition = 0;
        p2.GetComponent<HingeJoint>().spring = jpba42;

        JointSpring belj = gogus.GetComponent<HingeJoint>().spring;
        belj.spring = 400;
        belj.targetPosition = 0;
        belj.damper = 5;
        gogus.GetComponent<HingeJoint>().spring = belj;

        JointLimits jpsbl = gogus.GetComponent<HingeJoint>().limits;
        jpsbl.max = belJoint.maxLimit_i;
        jpsbl.min = belJoint.minLimit_i;
        gogus.GetComponent<HingeJoint>().limits = jpsbl;

        p1.enabled = true;
        p2.enabled = true;
        p3.enabled = true;
        p4.enabled = true;
        SpringleriAc();
        tekmeAtabilir = true;
        TumRbKinematikYap();
        Invoke("TumRbKinematikAc", 0.1f);
        transform.position = transform.position + new Vector3(0, hips.GetComponent<CapsuleCollider>().height / 2f + 1f, 0);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, -90f);

    }
    #endregion

    private void TumRbKinematikYap()
    {
        foreach (var rbs in GetComponentsInChildren<Rigidbody>())
        {
            rbs.isKinematic = true;
        }
    }

    private void TumRbKinematikAc()
    {
        foreach (var rbs in GetComponentsInChildren<Rigidbody>())
        {
            rbs.isKinematic = false;
        }
    }

    public void TekrarEskiAgirliginaGetir(Transform other)
    {
        foreach (var t in other.transform.root.GetComponentsInChildren<Transform>())
        {
            if (t.GetComponent<Rigidbody>())
                t.GetComponent<Rigidbody>().mass = t.GetComponent<Rigidbody>().mass * 5;
        }
        foreach (var t in other.transform.root.GetComponentsInChildren<Transform>())
        {
            if (t.GetComponent<HingeJoint>())
            {
                t.GetComponent<HingeJoint>().useSpring = true;
            }
        }
    }

    public void AgirlikHafiflet()
    {

        foreach (var t in transform.GetComponentsInChildren<Transform>())
        {
            if (t.GetComponent<Rigidbody>())
                t.GetComponent<Rigidbody>().mass = t.GetComponent<Rigidbody>().mass * 0.2f;
        }

        foreach (var t in transform.GetComponentsInChildren<Transform>())
        {
            if (t.GetComponent<HingeJoint>())
            {
                //t.GetComponent<HingeJoint> ().useSpring = false;
            }
        }
        kafa.GetComponent<HingeJoint>().useSpring = false;

        sagKol.GetComponent<HingeJoint>().useSpring = false;
        solKol.GetComponent<HingeJoint>().useSpring = false;

        kaval1.GetComponent<HingeJoint>().useSpring = false;
        kaval2.GetComponent<HingeJoint>().useSpring = false;
        gogus.GetComponent<HingeJoint>().useSpring = false;


    }

    private void SpringleriKapat()
    {
        foreach (var t in transform.GetComponentsInChildren<Transform>())
        {
            if (t.GetComponent<HingeJoint>())
            {
                t.GetComponent<HingeJoint>().useSpring = false;
            }
        }

        //ßtekme_havada = false;
    }

    private void SpringleriAc()
    {
        foreach (var t in transform.GetComponentsInChildren<Transform>())
        {
            if (t.GetComponent<HingeJoint>())
            {
                t.GetComponent<HingeJoint>().useSpring = true;
            }
        }
    }

    private void TumHizlariSifirla()
    {
        foreach (var _rb in transform.GetComponentsInChildren<Rigidbody>())
        {
            _rb.velocity = Vector3.zero;
        }
    }

    public void stop_all_courotine_death()
    {
        StopAllCoroutines();


    }

    public void stop_all_courotine()
    {
        Invoke("deathh", 10);

    }
    public void deathh()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    public void KafaVeKollarBuyut()
    {
        Invoke("buyutu", 0.01f);
        Invoke("wait_active", 0.09f);
        StopAllCoroutines();

    }
    void buyutu()
    {
        transform.parent.GetComponent<kiyafet_secim_ai>().buyut();

    }
    public void wait_active()
    {

        if (transform.localScale.x <= 6.7f)
        {
            StartCoroutine(KafaVeKolBounceBuyut());
        }
    }

    IEnumerator KafaVeKolBounceBuyut()
    {


        float timer = 0;
        Vector3 introScale = transform.localScale;
        Vector3 introSagKolScale = sagKol.transform.localScale;
        Vector3 introSolKolScale = solKol.transform.localScale;
        float lengthone = 0.1f;

        for (int i = 0; i < 2; i++)
        {
            if (i % 2 == 0)
            {
                timer = 0;
                while (timer <= lengthone)
                {
                    timer += Time.unscaledDeltaTime;
                    transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
                    //sagKol.transform.localScale += new Vector3 (0.1f, 0.1f, 0.1f);
                    //solKol.transform.localScale += new Vector3 (0.1f, 0.1f, 0.1f);
                    yield return Time.unscaledDeltaTime;
                }
            }
            else
            {
                timer = 0;
                while (timer <= lengthone)
                {
                    timer += Time.unscaledDeltaTime;
                    transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
                    //sagKol.transform.localScale -= new Vector3 (0.1f, 0.1f, 0.1f);
                    //solKol.transform.localScale -= new Vector3 (0.1f, 0.1f, 0.1f);
                    yield return Time.unscaledDeltaTime;
                }
            }
        }

        /* kafa.transform.localScale = new Vector3 (
             introScale.x + 0.1f,
             introScale.y + 0.1f,
             introScale.z + 0.1f);*/


    }

    public void duvardan_firlat()
    {
        //if(kaldiririyor)
        if (!oldu)
        {
            StartCoroutine(_duvardan_firlat());
        }
        else
        {
            Destroy(solKolTutucu.GetComponent<ConfigurableJoint>());
            Destroy(sagKolTutucu.GetComponent<ConfigurableJoint>());

        }
    }
    public float kuvvet_duvar, kuvvet_duvar_for;
    IEnumerator _duvardan_firlat()
    {
        dustu = true;
        transform.GetComponent<Rigidbody>().maxAngularVelocity = 100;
        float kafagerilmesphiz = 20f;
        float kafagerilmetphiz = 10f;
        float timer = 0;
        JointLimits gjl = gogus.GetComponent<HingeJoint>().limits;
        gjl.max = 60;
        gjl.min = -60;
        gogus.GetComponent<HingeJoint>().limits = gjl;

        Destroy(solKolTutucu.GetComponent<ConfigurableJoint>());
        Destroy(sagKolTutucu.GetComponent<ConfigurableJoint>());

        BoxColliderKapat();



        while (timer <= 0.25f)
        {
            timer += Time.deltaTime;

            /*sagKol.GetComponent<HingeJoint> ().useSpring = true;
			JointSpring gjsk = sagKol.GetComponent<HingeJoint>().spring;
			gjsk.spring = 200;// Mathf.Lerp(gjsk.spring, sagKolJointlerYumruk[2].spring, Time.deltaTime * gerilmetphiz);
			gjsk.targetPosition = Mathf.Lerp(gjsk.targetPosition, 70f, Time.deltaTime * 20);
			gjsk.damper = 10;//sagKolJointlerYumruk[2].damper;
			sagKol.GetComponent<HingeJoint>().spring = gjsk;
			
			solKol.GetComponent<HingeJoint> ().useSpring = true;
			JointSpring gjsk1 = solKol.GetComponent<HingeJoint>().spring;
			gjsk1.spring = 200;// Mathf.Lerp(gjsk.spring, sagKolJointlerYumruk[2].spring, Time.deltaTime * gerilmetphiz);
			gjsk1.targetPosition = Mathf.Lerp(gjsk1.targetPosition, 70f, Time.deltaTime * 20);
			gjsk1.damper = 10;//sagKolJointlerYumruk[2].damper;
			solKol.GetComponent<HingeJoint>().spring = gjsk1;
*/
            transform.GetComponent<Rigidbody>().AddForce(Vector3.up * 80, ForceMode.Acceleration);



            yield return Time.deltaTime;
        }
        //sagKol.GetComponent<HingeJoint> ().useSpring = false;
        //solKol.GetComponent<HingeJoint> ().useSpring = false;

        Invoke("gogusten_firlat", 0.15f);
        Invoke("duvardan_kurtuldu", 1.0f);


        //Invoke ("SpringleriKapat", 1f);
        //transform.root.GetChild (0).GetComponent<PlayerController_A> ().duvarda = false;

    }
    void gogusten_firlat()
    {
        transform.GetComponent<Rigidbody>().AddForce(tutundugu_duvar.forward * kuvvet_duvar_for * 20, ForceMode.Acceleration);
        //transform.GetComponent<Rigidbody> ().AddTorque (new Vector3(tutundugu_duvar.position.x,0,0) * kuvvet_duvar_for * 100, ForceMode.Acceleration);


        //SpringleriKapat ();

    }

    void duvardan_kurtuldu()
    {
        Kalkis();
        duvarda = false;
        tutundugu_duvar = null;
        transform.GetComponent<PlayerController_A>().sagKol.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);

        transform.GetComponent<PlayerController_A>().sagKol.GetComponent<BoxCollider>().isTrigger = false;
        transform.GetComponent<PlayerController_A>().solKol.GetComponent<BoxCollider>().isTrigger = false;

        transform.GetComponent<CapsuleCollider>().isTrigger = false;
        transform.GetComponent<BoxCollider>().isTrigger = false;
        kafa.GetComponent<BoxCollider>().isTrigger = false;

        BoxColliderAc();

        once_duvarda = true;
    }

    public List<BoxCollider> boxes = new List<BoxCollider>();
    private void BoxColliderKapat()
    {
        foreach (var t in transform.GetComponentsInChildren<BoxCollider>())
        {
            if (t.GetComponent<BoxCollider>())
            {
                if (t.GetComponent<BoxCollider>().isTrigger == false)
                {
                    t.GetComponent<BoxCollider>().isTrigger = true;
                    boxes.Add(t.GetComponent<BoxCollider>());
                }
            }
        }
    }

    private void BoxColliderAc()
    {
        for (int i = 0; i < boxes.Count; i++)
        {
            boxes[i].isTrigger = false;
        }
        boxes.Clear();

    }
    #endregion
}
