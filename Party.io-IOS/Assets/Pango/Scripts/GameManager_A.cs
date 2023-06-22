using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;
using Facebook.Unity;
using System;
using MoreMountains.NiceVibrations;

public class GameManager_A : MonoBehaviour
{
    public static GameManager_A gameManager;
    [Header("Particle Prefablari")] public GameObject hitParticle;
    public GameObject buyukHitParticle;
    public GameObject explParticle;
    public GameObject tozParticle;
    public GameObject ucanTekmeParticle;
    public GameObject kafaBuyumeParticle;
    float temp_ses;
    [Header("Screen Flash")] public Image screenFlash;
    [Header("Bayilma Efekt")] public Image bayilmaEfekt;
    [Header("SlowMo Efekt")] public Image slowMoEfect;

    [Header("Perfect Yazilar")] public GameObject[] perfectYazilar;
    public GameObject[] kurtulduyazilar;
    public GameObject[] yakalandiyazilar;

    public GameObject puan_yazi;

    public List<PlayerController_A> allPlayers = new List<PlayerController_A>();
    public List<PlayerController_A> allPlayers2 = new List<PlayerController_A>();
    public List<Transform> null_point = new List<Transform>();
    public List<PlayerController_A> listedPlayers = new List<PlayerController_A>();
    public List<ThrowPoint> throwPoints = new List<ThrowPoint>();
    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    public List<PlayerController_A> bayginoyuncular = new List<PlayerController_A>();
    public List<PlayerController_A> yerlesmisoyuncular = new List<PlayerController_A>();
    public List<PlayerController_A> beni_takip_edenler = new List<PlayerController_A>();
    [HideInInspector] public PlayerController_A me;

    public Text zaman;
    public Text score;
    [HideInInspector] public bool gameOver = false;

    [Header("Slow Motion")] public float slowdownfactor = 0.05f;
    public float slowdownLength = 2f;

    [Header("Oyun Bitis Panel")] public GameObject oyunBitisPanel;
    public GameObject VictoryYazi;
    public Text matchWinner;
    public Text killScore;
    public Text matchRank;
    int add_score_deg;

    public Text add_score;
    public Text Level_score;
    public Image oyunSonuRankBar;

    [Header("Oyun a Baslama")] public GameObject baslangicPanel;
    public GameObject baslangicKamera;
    public Camera oyunCamera;
    public GameObject infos;
    bool gameStarted = false;

    [Header("Baslangic Panel")] public string[] rankisimler;
    public GameObject[] rankResimler;
    public Text rankText;
    public Image rankBar;
    public Text isimText;
    public Text placeHolderText;
    public Text your_level_bas;
    [Header("Oyun Degerler Prefab")] public OyunZorlasmaDegerleri oyunDegerler;

    [Header("Tutorial")] public GameObject tutorial;
    [Header("Oyun Baslama")] public GameObject[] digerOyuncular;
    PlayerController_A selectedPlayer;
    [Header("BaslangicTimer")] public Text baslangicTimer;
    public GameObject baslangicWaitingPanel;
    public Text baslangicUcNokta;

    public GameObject player, player_bas;
    public int winCoin = 0;
    public Image kalkis_bar;

    public bool dustum = false;
    public Transform kamera_ilk;
    private bool inSlowMo = false;
    bool oncet = true;
    bool endGameCoin;

    private Color colorx,
        colorx1,
        colorx2,
        colorx3,
        colorx4,
        colorx5,
        colorx6,
        colorx7,
        colorx8,
        colorx9,
        colorx10,
        colorx11,
        colorx12,
        colorx13,
        colorx14,
        colorx15,
        colorx16,
        colorx17,
        colorx18,
        colorx19,
        colorx20,
        colorx21,
        colorx22,
        colorx23,
        colorx24,
        colorx25,
        colorx26,
        colorx27,
        colorx28,
        colorx29,
        colorx30,
        colorx31,
        colorx32,
        colorx33,
        colorx34,
        colorx35;

    static bool firstt = true;
    public int takip_edebilir_sayi = 2;

    public Image firlatma;

    public bool level_yuvarlak = false;

    static bool ads_timer = true;
    private bool _isIntDisplayed;

    CoinManager coinManager;

    void Awake()
    {
        MaxSdk.SetSdkKey("cHgLnnoKECUB17AIy93T68opMr-9z0yZIdv3OhOdutpw_-Dx8GUFPG2m61v32sdKTXNtDmV6dJ0cHETQV1fvIi");
        MaxSdk.InitializeSdk();

        // PlayerPrefs.SetInt("kiyafet_deg",0);
        // PlayerPrefs.SetInt("unlocked",0);


        PlayerPrefs.SetInt("DailySpinPopUp", 1);
        //PlayerPrefs.DeleteAll();
        gameManager = this;

        coinManager = GetComponent<CoinManager>();

        menuMusic.Play();

        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
        {
            // AppLovin SDK is initialized, start loading ads
        };

        if (ads_timer)
        {
            ads_timer = false;
            PlayerPrefs.SetString("ads_timer", System.DateTime.Now.ToBinary().ToString());
        }


        if (PlayerPrefs.GetInt("unlocked") < 1)
        {
            PlayerPrefs.SetInt("unlocked", 1);
        }

        takip_edebilir_sayi = 2;

        kalkis_bar.fillAmount = 0;

        if (PlayerPrefs.GetInt("Score_once") == 0 && PlayerPrefs.GetInt("Level_score") == 0)
        {
            PlayerPrefs.SetInt("Level_score", 100);
            PlayerPrefs.SetInt("Score_once", 1);
        }

        if (PlayerPrefs.GetInt("oncet") == 0)
        {
            PlayerPrefs.SetInt("oncet", 1);
            PlayerPrefs.SetInt("unlocked", 1);
        }

        your_level_bas.text = PlayerPrefs.GetInt("Level_score").ToString();
        if (PlayerPrefs.GetInt("XP") == 0)
        {
            PlayerPrefs.SetInt("XP", 1);
        }
        else
        {
        }

        if (firstt)
        {
            firstt = false;


            if (PlayerPrefs.GetInt("Level") < 14)
                SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
            else
            {
                int rnd2 = UnityEngine.Random.Range(0, 14);
                PlayerPrefs.SetInt("Level", rnd2);
            }
        }

        ColorUtility.TryParseHtmlString("#87A323FF", out colorx);
        ColorUtility.TryParseHtmlString("#FF2294FF", out colorx1);
        ColorUtility.TryParseHtmlString("#D23D11FF", out colorx2);
        ColorUtility.TryParseHtmlString("#1F4AC9FF", out colorx3);
        ColorUtility.TryParseHtmlString("#CF6FF9", out colorx4);
        ColorUtility.TryParseHtmlString("#7F12F0FF", out colorx5);
        ColorUtility.TryParseHtmlString("#A3845EFF", out colorx6);
        ColorUtility.TryParseHtmlString("#000000", out colorx7);
        ColorUtility.TryParseHtmlString("#D40F0BFF", out colorx8);
        ColorUtility.TryParseHtmlString("#206EC9FF", out colorx9);
        ColorUtility.TryParseHtmlString("#4113D2FF", out colorx10);
        ColorUtility.TryParseHtmlString("#B572BC", out colorx11);
        ColorUtility.TryParseHtmlString("#14AD17", out colorx12);
        ColorUtility.TryParseHtmlString("#2067E7", out colorx13);
        ColorUtility.TryParseHtmlString("#2E86B5", out colorx14);
        ColorUtility.TryParseHtmlString("#7DC51DFF", out colorx15);
        ColorUtility.TryParseHtmlString("#7DC51DFF", out colorx16);
        ColorUtility.TryParseHtmlString("#931E89", out colorx18);
        ColorUtility.TryParseHtmlString("#FB00C0FF", out colorx19);
        ColorUtility.TryParseHtmlString("#7DC51DFF", out colorx21);
        ColorUtility.TryParseHtmlString("#BB782C", out colorx22);
        ColorUtility.TryParseHtmlString("#762075", out colorx25);
        ColorUtility.TryParseHtmlString("#A6651A", out colorx26);
        ColorUtility.TryParseHtmlString("#FF8F18FF", out colorx27);
        ColorUtility.TryParseHtmlString("#7DC51DFF", out colorx28);
        ColorUtility.TryParseHtmlString("#FB00C0FF", out colorx29);
        ColorUtility.TryParseHtmlString("#018AC5", out colorx30);
        ColorUtility.TryParseHtmlString("#DEC772", out colorx31);
        ColorUtility.TryParseHtmlString("#FB00C0FF", out colorx32);
        ColorUtility.TryParseHtmlString("#FF8F18FF", out colorx33);
        ColorUtility.TryParseHtmlString("#7DC51DFF", out colorx34);
        ColorUtility.TryParseHtmlString("#6BFFF3FF", out colorx35);

        renk[0] = colorx;
        renk[1] = colorx1;
        renk[2] = colorx2;
        renk[3] = colorx3;
        renk[4] = colorx4;
        renk[5] = colorx5;
        renk[6] = colorx6;
        renk[7] = colorx7;
        renk[8] = colorx8;
        renk[9] = colorx9;
        renk[10] = colorx10;
        renk[11] = colorx11;
        renk[12] = colorx12;
        renk[13] = colorx13;
        renk[14] = colorx14;
        renk[15] = colorx15;
        renk[16] = colorx16;
        renk[17] = colorx17;
        renk[18] = colorx18;
        renk[19] = colorx19;
        renk[20] = colorx20;
        renk[21] = colorx21;
        renk[22] = colorx22;
        renk[23] = colorx23;
        renk[24] = colorx24;
        renk[25] = colorx25;
        renk[26] = colorx26;
        renk[27] = colorx27;
        renk[28] = colorx28;
        renk[29] = colorx29;
        renk[30] = colorx30;
        renk[31] = colorx31;
        renk[32] = colorx32;
        renk[33] = colorx33;
        renk[34] = colorx34;
        renk[35] = colorx35;


        InitializeRewardedAds();
        InitializeInterstitialAds();
        InitializeBannerAds();


        GameAnalytics.Initialize();
        //PlayerPrefs.DeleteAll ();
        BaslangicPanelDegerleri();

        score.transform.parent.gameObject.SetActive(false);
        oyunCamera.enabled = false;
        baslangicKamera.SetActive(true);

        int rank = PrefManager.GetRank();
        if (rank >= 3 && rank <= 5)
        {
            AdjustSelectedPlayers(2);
        }
        else if (rank >= 6 && rank <= 9)
        {
            AdjustSelectedPlayers(3);
        }
        else if (rank >= 10 && rank <= 15)
        {
            AdjustSelectedPlayers(4);
        }
        else if (rank >= 16 && rank <= 19)
        {
            AdjustSelectedPlayers(5);
        }
        else
        {
            AdjustSelectedPlayers(6);
        }

        baslangicTimer.gameObject.SetActive(false);
        baslangicWaitingPanel.SetActive(false);

        baslangic_adam();

        oyunculari_giydir();
    }

    public void oyunculari_giydir()
    {
        for (int i = 0; i < digerOyuncular.Length; i++)
        {
            int rnd = UnityEngine.Random.Range(0,
                digerOyuncular[i].GetComponent<kiyafet_secim_ai>().kiyafet.Length - 1);

            if (rnd == PlayerPrefs.GetInt("kiyafet_deg"))
            {
                digerOyuncular[i].GetComponent<kiyafet_secim_ai>().kiyafet[rnd + 1].SetActive(true);
                digerOyuncular[i].GetComponent<kiyafet_secim_ai>().mat.color = renk[rnd + 1];
                PlayerPrefs.SetInt("kiyafet" + (i + 1), rnd + 1);
            }
            else
            {
                digerOyuncular[i].GetComponent<kiyafet_secim_ai>().kiyafet[rnd].SetActive(true);
                digerOyuncular[i].GetComponent<kiyafet_secim_ai>().mat.color = renk[rnd];
                PlayerPrefs.SetInt("kiyafet" + (i + 1), rnd);
            }
        }
    }


    private void AdjustSelectedPlayers(int howmuch)
    {
        if (howmuch >= digerOyuncular.Length)
            howmuch = digerOyuncular.Length - 1;
        for (int i = 0; i < howmuch; i++)
        {
            digerOyuncular[i].transform.GetChild(0).GetComponent<AIController>().isSelected = true;
        }
    }

    public static bool timer = true;
    public static DateTime first_time;


    int reklam_deg = 2;


    string rewardedAdUnitId = "6f3bf2499f0fbe7a";
    string interstitialAdUnitId = "34602e0739d99bac";
    string bannerAdUnitId = "3ed596e5c0988562";


    public void InitializeInterstitialAds()
    {
        // Attach callback
        MaxSdkCallbacks.OnInterstitialLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.OnInterstitialLoadFailedEvent += OnInterstitialFailedEvent;
        MaxSdkCallbacks.OnInterstitialAdFailedToDisplayEvent += InterstitialFailedToDisplayEvent;
        MaxSdkCallbacks.OnInterstitialHiddenEvent += OnInterstitialDismissedEvent;

        // Load the first interstitial
        LoadInterstitial();
    }

    private void LoadInterstitial()
    {
        MaxSdk.LoadInterstitial(interstitialAdUnitId);
    }

    private void OnInterstitialLoadedEvent(string adUnitId)
    {
        // Interstitial ad is ready to be shown. MaxSdk.IsInterstitialReady(interstitialAdUnitId) will now return 'true'
    }

    private void OnInterstitialFailedEvent(string adUnitId, int errorCode)
    {
        // Interstitial ad failed to load. We recommend re-trying in 3 seconds.
        Invoke("LoadInterstitial", 3);
    }

    private void InterstitialFailedToDisplayEvent(string adUnitId, int errorCode)
    {
        // Interstitial ad failed to display. We recommend loading the next ad
        LoadInterstitial();
    }

    private void OnInterstitialDismissedEvent(string adUnitId)
    {
        temp_ses = AudioListener.volume;
        AudioListener.volume = 0.8f;
        // if (_isIntDisplayed)
        // {
        //     if (!once_sayac)
        //     {
        //         PlayerPrefs.SetInt("Level_score", PlayerPrefs.GetInt("Level_score") + add_score_deg);
        //     }
        //
        //     if (gameOver)
        //     {
        //         if (allPlayers.Contains(me))
        //         {
        //             PlayerPrefs.SetInt("XP", PlayerPrefs.GetInt("XP") + 1);
        //             if (PlayerPrefs.GetInt("Level") < 14)
        //             {
        //                 PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        //                 PlayerPrefs.SetInt("Scorenew", PlayerPrefs.GetInt("Scorenew") + 1);
        //                 GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "game",
        //                     PlayerPrefs.GetInt("Scorenew").ToString());
        //             }
        //             else
        //             {
        //                 PlayerPrefs.SetInt("Scorenew", PlayerPrefs.GetInt("Scorenew") + 1);
        //                 GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "game",
        //                     PlayerPrefs.GetInt("Scorenew").ToString());
        //
        //                 int rnd = UnityEngine.Random.Range(0, 14);
        //                 PlayerPrefs.SetInt("Level", rnd);
        //                 SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        //             }
        //
        //             SceneManager.LoadScene(14);
        //         }
        //         else
        //         {
        //             SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        //         }
        //     }
        //     else
        //     {
        //         SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        //     }
        //
        //     _isIntDisplayed = false;
        // }

        // Interstitial ad is hidden. Pre-load the next ad
        LoadInterstitial();
    }


    void ShowAds()
    {
        if (PlayerPrefs.GetInt("Reklam") != 1)
        {
            if (MaxSdk.IsInterstitialReady(interstitialAdUnitId))
            {
                MaxSdk.ShowInterstitial(interstitialAdUnitId);
            }

            if (MaxSdk.IsRewardedAdReady(rewardedAdUnitId))
            {
                MaxSdk.ShowRewardedAd(rewardedAdUnitId);
            }
        }
    }


    public void InitializeRewardedAds()
    {
        // Attach callback
        MaxSdkCallbacks.OnRewardedAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.OnRewardedAdLoadFailedEvent += OnRewardedAdFailedEvent;
        MaxSdkCallbacks.OnRewardedAdFailedToDisplayEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.OnRewardedAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.OnRewardedAdClickedEvent += OnRewardedAdClickedEvent;
        MaxSdkCallbacks.OnRewardedAdHiddenEvent += OnRewardedAdDismissedEvent;
        MaxSdkCallbacks.OnRewardedAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

        // Load the first RewardedAd
        LoadRewardedAd();
    }

    public void LoadRewardedAd()
    {
        MaxSdk.LoadRewardedAd(rewardedAdUnitId);
    }

    public void OnRewardedAdLoadedEvent(string adUnitId)
    {
        // Rewarded ad is ready to be shown. MaxSdk.IsRewardedAdReady(rewardedAdUnitId) will now return 'true'
    }

    private void OnRewardedAdFailedEvent(string adUnitId, int errorCode)
    {
        // Rewarded ad failed to load. We recommend re-trying in 3 seconds.
        Invoke("LoadRewardedAd", 3);
    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, int errorCode)
    {
        // Rewarded ad failed to display. We recommend loading the next ad
        LoadRewardedAd();
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId)
    {
        temp_ses = AudioListener.volume;
        AudioListener.volume = 0;
    }

    public void OnRewardedAdClickedEvent(string adUnitId)
    {
    }

    public void OnRewardedAdDismissedEvent(string adUnitId)
    {
        // Rewarded ad is hidden. Pre-load the next ad
        LoadRewardedAd();
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward)
    {
        // Rewarded ad was displayed and user should receive the reward
        temp_ses = AudioListener.volume;
        AudioListener.volume = 0.8f;
    }


    // Retrieve the id from your account

    public void InitializeBannerAds()
    {
        // Banners are automatically sized to 320x50 on phones and 728x90 on tablets
        // You may use the utility method `MaxSdkUtils.isTablet()` to help with view sizing adjustments
        MaxSdk.CreateBanner(bannerAdUnitId, MaxSdkBase.BannerPosition.TopCenter);

        // Set background or background color for banners to be fully functional
        Color color = Color.white;
        MaxSdk.SetBannerBackgroundColor(bannerAdUnitId, color);
    }

    void Start()
    {
        PlayerPrefs.GetInt("kiyafet_deg", 0);
        PlayerPrefs.GetInt("unlocked", 0);
        PlayerPrefs.GetInt("Level", 0);
        GameAnalytics.Initialize();

        anim = player_bas.GetComponent<Animator>();
        currentAddCoin = 0;
        targetAddCoin = currentAddCoin;
        //player = GameObject.FindGameObjectWithTag("Player1");
        //player_bas = GameObject.FindGameObjectWithTag("PlayerBas");

        Application.targetFrameRate = 60;

        MaxSdk.HideBanner(bannerAdUnitId);


        if (timer)
        {
            timer = false;
            first_time = DateTime.Now;
        }

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "game");


        if (!FB.IsInitialized)
        {
            FB.Init(() =>
                {
                    if (FB.IsInitialized)
                    {
                        FB.ActivateApp();
                        FB.GetAppLink(DeepLinkCallback);

                        FB.LogAppEvent(
                            AppEventName.UnlockedAchievement,
                            null,
                            new Dictionary<string, object>()
                            {
                                {AppEventParameterName.Description, "Clicked 'Log AppEvent' button"}
                            });
                    }
                    else
                        Debug.LogError("Couldn't Initialized Facebook!");
                },
                isGameShown =>
                {
                    if (!isGameShown)
                        Time.timeScale = 0;
                    else
                        Time.timeScale = 1;
                });
        }
        else
        {
            //  Debug.LogError("Facebook Activate App");
            FB.ActivateApp();
        }
    }

    void DeepLinkCallback(IAppLinkResult result)
    {
        if (!String.IsNullOrEmpty(result.Url))
        {
            var index = (new Uri(result.Url)).Query.IndexOf("request_ids");
            if (index != -1)
            {
                // ...have the user interact with the friend who sent the request,
                // perhaps by showing them the gift they were given, taking them
                // to their turn in the game with that friend, etc.
            }
        }
    }

    private void RandomPlayerAc()
    {
        for (int i = 0; i < 9 /* digerOyuncular.Length*/; i++)
        {
            int r = UnityEngine.Random.Range(0, 9 /* digerOyuncular.Length*/);
            if (digerOyuncular[r].activeSelf)
            {
                if (r + 1 >= 9 /*digerOyuncular.Length*/)
                {
                    for (int g = 0; g < 9 /* digerOyuncular.Length*/; g++)
                    {
                        if (!digerOyuncular[g].activeSelf)
                        {
                            StartCoroutine(OyuncuAc(digerOyuncular[g], UnityEngine.Random.Range(0.6f, i * 0.6f)));
                        }
                    }
                }
            }
            else
            {
                StartCoroutine(OyuncuAc(digerOyuncular[i], UnityEngine.Random.Range(0.35f, i * 0.4f)));
            }
        }
    }

    IEnumerator OyuncuAc(GameObject oyuncu, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        oyuncu.SetActive(true);
    }

    //public float leveltime;

    IEnumerator Timer()
    {
        /*while (leveltime > 0) {
			yield return new WaitForSecondsRealtime (1f);
			leveltime -= 1f;
			int min = (int)(leveltime / 60f);
			int sn = (int)leveltime%60;
			zaman.text = min.ToString ("00") + "." + sn.ToString ("00");
		}*/
        yield return new WaitForSecondsRealtime(0f);
        TimesUp();
    }

    float targetAddCoin;
    float currentAddCoin;
    int killCoin;
    public GameObject winPoint;

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.R))
        // {
        //     PlayerPrefs.SetInt("kiyafet_deg",0);
        //     Debug.Log(PlayerPrefs.GetInt("kiyafet_deg"));
        //     // PlayerPrefs.SetInt("Level", 0);
        //     // SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        // }
     
        if (targetScore - currentScore < 1)
        {
            targetScore = currentScore;
        }
        else
        {
            currentScore = Mathf.Lerp(currentScore, targetScore, 0.05f);
            Level_score.text = Mathf.FloorToInt(currentScore).ToString();
            endGameCoin = false;
        }

        if (targetAddCoin - currentAddCoin < 1)
        {
            targetAddCoin = currentAddCoin;
        }
        else
        {
            currentAddCoin = Mathf.Lerp(currentAddCoin, targetAddCoin, 0.2f);
            coinText.text = Mathf.FloorToInt(currentAddCoin).ToString();
        }

        if (isTextureScrolling)
        {
            offsetX = Time.time * scrollX;
            offsetY = Time.time * scrollY;
            player_bas.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTextureOffset =
                new Vector2(offsetX, offsetY);
        }

        if (allPlayers.Count != 0)
            if (allPlayers.Count < 2 && oncet)
            {
                oncet = false;
                TimesUp();
            }

        if (dustum)
        {
            dustum = false;
            killScore.text = me.myScore.ToString();
            score.transform.parent.gameObject.SetActive(false);
            zaman.transform.parent.gameObject.SetActive(false);
            matchRank.text = Siralamam().ToString();
            PlayerPrefs.SetInt("Siralamam", Siralamam());


            if (Siralamam() == 1)
            {
                add_score_deg = +50;
                score.text = "+50";
                winCoin = 10;
                //PlayerPrefs.SetInt("first", player.GetComponent<PlayerController_A>().number);
                Level_score.text = (me.myScore * 5).ToString();
                playerAddCoin.SetActive(false);
                winPoint.SetActive(true);
                //PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + winCoin +( me.myScore * 2));
            }
            else
            {
                winPoint.SetActive(false);
                Level_score.text = 0.ToString();
            }


            PlayerPrefs.SetInt("add_score_deg", add_score_deg);


            Invoke("score_degis", 1.5f);
            MaxSdk.HideBanner(bannerAdUnitId);
            // AMR.AMRSDK.hideBanner();
            oyunBitisPanel.SetActive(true);
        }
    }


    public void DoSlowMo()
    {
        inSlowMo = true;
        SlowMoImageEffect();
        Invoke("CloseSlowMo", slowdownfactor * 2f);
        Time.timeScale = slowdownfactor;
    }

    private void CloseSlowMo()
    {
        inSlowMo = false;
    }

    void LateUpdate()
    {
        if (me != null)
        {
            score.text = "Kill : " + me.myScore.ToString();
        }
        else
        {
            score.text = "Kill : 0";
        }
    }

    public ThrowPoint SelectedThrowPoint(PlayerController_A myself)
    {
        //int rp = Random.Range (0, throwPoints.Count);
        float pos = Vector3.Distance(myself.transform.position, throwPoints[0].transform.position);
        int no = 0;

        for (int i = 0; i < throwPoints.Count; i++)
        {
            float diffp = Vector3.Distance(myself.transform.position, throwPoints[i].transform.position);
            if (diffp < pos)
            {
                pos = Vector3.Distance(myself.transform.position, throwPoints[i].transform.position);
                no = i;
            }
        }

        return throwPoints[no];
    }

    public PlayerController_A SelectedTargetPlayer(PlayerController_A myself)
    {
        int rp = UnityEngine.Random.Range(0, allPlayers.Count);
        if (allPlayers[rp] == myself)
        {
            for (int i = 0; i < allPlayers.Count; i++)
            {
                if (rp != i)
                    return allPlayers[i];
            }
        }

        return allPlayers[rp];
    }

    public Transform SelectedTargetNull(Transform myself)
    {
        int rp = UnityEngine.Random.Range(0, null_point.Count);
        return null_point[rp];
    }


    public SpawnPoint SelectedSpawnPoint()
    {
        int r = UnityEngine.Random.Range(0, spawnPoints.Count);
        return spawnPoints[r];
    }

    public Text addCoinText;

    // Oyun sonu
    int x2CoinTimer = 0;

    private void TimesUp()
    {
        MaxSdk.HideBanner(bannerAdUnitId);
        gameOver = true;
        StartCoroutine(ScreenFlasher());
        int kazananoyuncuno = 0;
        for (int i = 0; i < allPlayers.Count; i++)
        {
            if (allPlayers[i] != KazananOyuncu())
            {
                //allPlayers [i].Dusus ();
            }
            else
            {
                kazananoyuncuno = i;
                allPlayers[i].GameOverKalkis();
            }
        }

        oyunCamera.GetComponent<Kamera_A>().KazanmaKameraYaklastir();
        //kazanan ben isem
        if (allPlayers[kazananoyuncuno] == me)
        {
            PlayerPrefs.SetInt("birinci", allPlayers[0].GetComponent<PlayerController_A>().number);
            PrefManager.SetRank(PrefManager.GetRank() + 1);
            VictoryYazi.SetActive(true);
            matchWinner.text = "Match Winner " + PrefManager.GetUserName();
            PlayerPrefs.SetString("birinci_str", PlayerPrefs.GetString(PrefManager.userName));
            StartCoroutine(RankDoldur());
        }
        else
        {
            VictoryYazi.SetActive(false);
            matchWinner.text = "Winner: " +
                               allPlayers[kazananoyuncuno].takipEdecekObje.GetChild(1).GetComponent<TextMesh>().text;
            int rankNo = PrefManager.GetRank();
            float ranknofloat = (float) rankNo;
            float rankoran = ranknofloat % 5f;
            oyunSonuRankBar.fillAmount = rankoran / 5f + 0.2f;
        }

        if (allPlayers.Contains(me))
        {
            if (Siralamam() == 1)
            {
                add_score_deg = +50;
                score.text = "+50";
                winCoin = 10;
                // PlayerPrefs.SetInt("first", allPlayers[0].GetComponent<PlayerController_A>().number);

                PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + (me.myScore * 5) + winCoin);

                Level_score.text = (me.myScore * 5).ToString();
                addCoinText.text = "+" + winCoin.ToString();
                playerAddCoin.SetActive(false);
                Invoke("AddCoin", 1.5f);

                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                }
                else
                {
                    if (me.myScore > 4)
                    {
                        coinManager.claimx3Button.SetActive(true);
                    }
                    else
                    {
                        coinManager.claimButton.SetActive(true);
                    }
                }


                winPoint.SetActive(true);
            }
            else
            {
                winPoint.SetActive(false);
                Level_score.text = 0.ToString();
                addCoinText.text = 0.ToString();
            }

            PlayerPrefs.SetInt("add_score_deg", add_score_deg);

            Invoke("score_degis", 1.5f);


            killScore.text = me.myScore.ToString();
            score.transform.parent.gameObject.SetActive(false);
            zaman.transform.parent.gameObject.SetActive(false);
            matchRank.text = Siralamam().ToString();
            PlayerPrefs.SetInt("Siralamam", Siralamam());
        }

        oyunBitisPanel.SetActive(true);
    }

    float currentScore;
    float targetScore;

    void AddCoin()
    {
        currentScore = me.myScore * 5;
        targetScore = (me.myScore * 5) + 11;
        endGameCoin = true;

        // Level_score.text = Mathf.Lerp(me.myScore*2, ((me.myScore*2)+winCoin), 2).ToString();
    }

    bool once_sayac = false;

    IEnumerator sayac()
    {
        float i = 0;

        once_sayac = true;

        // PlayerPrefs.SetInt("Level_score", PlayerPrefs.GetInt("Level_score") + add_score_deg);
        // PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + ((me.myScore) * 2) + winCoin);

        if (me.myScore <= 0)
            me.myScore = 0;

        i = 0;
        yield return new WaitForSecondsRealtime(0.01f);
    }

    public Text coinText;

    public void score_degis()
    {
        StartCoroutine(sayac());
    }

    IEnumerator RankDoldur()
    {
        int rankNo = PrefManager.GetRank();
        float ranknofloat = (float) rankNo;
        float rankoran = ranknofloat % 5f;
        oyunSonuRankBar.fillAmount = rankoran / 5f;
        while (oyunSonuRankBar.fillAmount <= rankoran / 5f + 0.2f)
        {
            oyunSonuRankBar.fillAmount =
                Mathf.Lerp(oyunSonuRankBar.fillAmount, rankoran / 5f + 0.2f, Time.unscaledDeltaTime);
            yield return Time.unscaledDeltaTime;
        }

        oyunSonuRankBar.fillAmount = rankoran / 5f + 0.2f;
    }

    private PlayerController_A KazananOyuncu()
    {
        listedPlayers = allPlayers.OrderByDescending(a => a.myScore).ToList();
        return listedPlayers[0];
    }

    private int Siralamam()
    {
        listedPlayers = allPlayers.OrderByDescending(a => a.myScore).ToList();
        if (listedPlayers.Contains(me))
        {
            return listedPlayers.Count;
        }
        else
        {
            return listedPlayers.Count + 1;
        }


        for (int i = 0; i < listedPlayers.Count; i++)
        {
            if (listedPlayers[i] == me)
            {
            }
        }

        return 0;
    }

    public void AtanaScoreVer(PlayerController_A atanKisi)
    {
        for (int i = 0; i < allPlayers.Count; i++)
        {
            if (allPlayers[i] == atanKisi)
            {
                allPlayers[i].myScore += 1;

                if (allPlayers.Count > 1)
                    allPlayers[i].KafaVeKollarBuyut();

                if (allPlayers[i] == me)
                {
                    SpawnPerfect();
                    StartCoroutine(ScreenFlasher());
                    playerAddCoin.SetActive(true);
                    Invoke("ClosePlayerAddCoin", 0.7f);
                }
            }
        }

        for (int i = 0; i < allPlayers.Count; i++)
        {
            if (allPlayers[i] != KazananOyuncu())
            {
                allPlayers[i].Tac.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                if (allPlayers[i].isAI)
                {
                    allPlayers[i].bayilmaHealth += 200;
                }

                allPlayers[i].Tac.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    public void ClosePlayerAddCoin()
    {
        playerAddCoin.SetActive(false);
        targetAddCoin = currentAddCoin + 6;
    }

    IEnumerator ScreenFlasher()
    {
        float timer = 0;
        screenFlash.color = new Color(screenFlash.color.r, screenFlash.color.g, screenFlash.color.b, 0.3f);
        MMVibrationManager.Haptic(HapticTypes.Success);

        while (timer <= 0.1f)
        {
            timer += Time.deltaTime;
            screenFlash.color = new Color(screenFlash.color.r, screenFlash.color.g, screenFlash.color.b,
                Mathf.Lerp(screenFlash.color.a, 0, Time.deltaTime * 20f));
            yield return Time.deltaTime;
        }

        screenFlash.color = new Color(screenFlash.color.r, screenFlash.color.g, screenFlash.color.b, 0);
    }

    public void BayilmaEfektor()
    {
        StartCoroutine(_BayilmaEfekt());
    }

    IEnumerator _BayilmaEfekt()
    {
        MMVibrationManager.Haptic(HapticTypes.MediumImpact);

        float timer = 0;
        bayilmaEfekt.color = new Color(bayilmaEfekt.color.r, bayilmaEfekt.color.g, bayilmaEfekt.color.b, 0.6f);
        while (timer <= 0.5f)
        {
            timer += Time.deltaTime;
            bayilmaEfekt.color = new Color(bayilmaEfekt.color.r, bayilmaEfekt.color.g, bayilmaEfekt.color.b,
                Mathf.Lerp(bayilmaEfekt.color.a, 0, Time.deltaTime * 20f));
            yield return Time.deltaTime;
        }

        bayilmaEfekt.color = new Color(bayilmaEfekt.color.r, bayilmaEfekt.color.g, bayilmaEfekt.color.b, 0);
    }

    public void SlowMoImageEffect()
    {
        StartCoroutine(_SlowMoImageEffect());
    }

    IEnumerator _SlowMoImageEffect()
    {
        MMVibrationManager.Haptic(HapticTypes.MediumImpact);

        slowMoEfect.color = new Color(slowMoEfect.color.r, slowMoEfect.color.g, slowMoEfect.color.b, 0.5f);
        while (inSlowMo)
        {
            slowMoEfect.color = new Color(slowMoEfect.color.r, slowMoEfect.color.g, slowMoEfect.color.b,
                Mathf.Clamp(Mathf.Lerp(slowMoEfect.color.a, 0, Time.deltaTime * 20f), 0, 0.5f));

            yield return Time.deltaTime;
        }

        slowMoEfect.color = new Color(slowMoEfect.color.r, slowMoEfect.color.g, slowMoEfect.color.b, 0);
    }

    private void SpawnPerfect()
    {
        int r = UnityEngine.Random.Range(0, perfectYazilar.Length);
        Instantiate(perfectYazilar[r], transform.position, Quaternion.identity, this.transform);
        playerAddCoin.SetActive(false);
    }

    public void SpawnYakaladi()
    {
        int r = UnityEngine.Random.Range(0, yakalandiyazilar.Length);
        Instantiate(yakalandiyazilar[r], transform.position, Quaternion.identity, this.transform);
    }

    public void SpawnKurtuldu()
    {
        int r = UnityEngine.Random.Range(0, kurtulduyazilar.Length);
        Instantiate(kurtulduyazilar[r], transform.position, Quaternion.identity, this.transform);
    }


    public void VurmaPuanCikart()
    {
        Instantiate(puan_yazi, transform.position, Quaternion.identity, this.transform);
    }

    public void StartGame()
    {
        //AMR.AMRSDK.loadBanner(AMR.Enums.AMRSDKBannerPosition.BannerPositionTop, true);

        // AMR.AMRSDK.showBanner();

        if (PlayerPrefs.GetInt("Reklam") != 1)
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                MaxSdk.ShowBanner(bannerAdUnitId);
                Debug.Log("Banner ID" + bannerAdUnitId);
            }
        }

        SaveUserNameOnStartPlay();
        baslangicPanel.SetActive(false);
        baslangicWaitingPanel.SetActive(true);


        isGameStarted = true;
        StartCoroutine(BaslangicPanelWaiting());
        baslangic_adam();
        StartCoroutine(spawn_yerlestir(0.2f));
        menuMusic.Pause();
    }

    IEnumerator spawn_yerlestir(float i)
    {
        //int xx=0;
        while (yerlesmisoyuncular.Count != allPlayers2.Count)
        {
            int rnd1 = (int) UnityEngine.Random.Range(0, (allPlayers2.Count));
            if (!yerlesmisoyuncular.Contains(allPlayers2[rnd1]))
            {
                yerlesmisoyuncular.Add(allPlayers2[rnd1]);


                //xx++;
            }
        }

        yield return new WaitForSecondsRealtime(i);
        for (int x = 0; x < yerlesmisoyuncular.Count; x++)
        {
            yerlesmisoyuncular[x].transform.position = spawnPoints[x].transform.position;
            yerlesmisoyuncular[x].transform.rotation = spawnPoints[x].transform.rotation;
            if (yerlesmisoyuncular[x] == me)
            {
                kamera_ilk.transform.position = spawnPoints[x].transform.position;
            }
        }
    }


    IEnumerator BaslangicPanelWaiting()
    {
        float waittimer = 12;
        int noktasayi = 3;
        while (waittimer > 0)
        {
            yield return new WaitForSecondsRealtime(0.25f);
            waittimer--;
            if (noktasayi <= 0)
                noktasayi = 3;
            if (noktasayi == 3)
            {
                baslangicUcNokta.text = ".";
            }
            else if (noktasayi == 2)
            {
                baslangicUcNokta.text = "..";
            }
            else if (noktasayi == 1)
            {
                baslangicUcNokta.text = "...";
            }

            noktasayi--;
        }

        Time.timeScale = 0;
        player_bas.SetActive(false);
        player.SetActive(true);
        baslangicKamera.SetActive(false);
        oyunCamera.enabled = true;

        RandomPlayerAc();
        baslangicWaitingPanel.SetActive(false);
        StartCoroutine(BaslangicTimer());
        infos.SetActive(true);
    }

    IEnumerator BaslangicTimer()
    {
        baslangicTimer.gameObject.SetActive(true);
        tutorial.SetActive(true);
        float timer = 3;
        baslangicTimer.text = timer.ToString("0");
        while (timer > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            timer--;
            baslangicTimer.text = timer.ToString("0");
        }

        baslangicTimer.text = "GO";
        StartGameAfterCount();
    }

    private void StartGameAfterCount()
    {
        Destroy(baslangicTimer.gameObject, 0.4f);
//        zaman.transform.parent.gameObject.SetActive(true);
        score.transform.parent.gameObject.SetActive(true);
        Time.timeScale = 1f;
        //StartCoroutine (Timer ());
        gameStarted = true;
    }

    public void NextLevel()
    {
        if (PlayerPrefs.GetInt("Reque") == 1)
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                iOSReviewRequest.Request();
                
            }

           // PlayerPrefs.SetInt("Reque", 2);
        }
        else if (PlayerPrefs.GetInt("Reque") > 1)
        {
            if (PlayerPrefs.GetInt("Reklam") != 1)
            {
               

            }

        }
        if (MaxSdk.IsInterstitialReady(interstitialAdUnitId))
        {
            MaxSdk.ShowInterstitial(interstitialAdUnitId);
            temp_ses = AudioListener.volume;
            AudioListener.volume = 0;
        }

        LoadInterstitial();
        PlayerPrefs.SetInt("Reque",PlayerPrefs.GetInt("Reque") +1);
        Debug.Log("Reque" + PlayerPrefs.GetInt("Reque"));

        PlayerPrefs.SetInt("rklm", PlayerPrefs.GetInt("rklm") + 1);

        int sceneToBeLoaded = 0;
        int totalLevelNo = SceneManager.sceneCountInBuildSettings;
        int currentSceneNo = SceneManager.GetActiveScene().buildIndex;

        if (!once_sayac)
        {
            PlayerPrefs.SetInt("Level_score", PlayerPrefs.GetInt("Level_score") + add_score_deg);
        }

        if (gameOver)
        {
            if (allPlayers.Contains(me))
            {
                PlayerPrefs.SetInt("XP", PlayerPrefs.GetInt("XP") + 1);
                if (PlayerPrefs.GetInt("Level") < 14)
                {
                    PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
                    PlayerPrefs.SetInt("Scorenew", PlayerPrefs.GetInt("Scorenew") + 1);
                    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "game",
                        PlayerPrefs.GetInt("Scorenew").ToString());
                }
                else
                {
                    PlayerPrefs.SetInt("Scorenew", PlayerPrefs.GetInt("Scorenew") + 1);
                    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "game",
                        PlayerPrefs.GetInt("Scorenew").ToString());
                    int rnd = UnityEngine.Random.Range(0, 14);
                    PlayerPrefs.SetInt("Level",  rnd);
                    SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
                }

                SceneManager.LoadScene(14);
            }
            else
            {
                SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
            }
        }
        else
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        }
        // if (!_isIntDisplayed)
        // {
        //     
        // }
    }

    private void BaslangicPanelDegerleri()
    {
        //      PlayerPrefs.SetString("USERNAME","");
        int rankNo = PrefManager.GetRank();
        /*rankText.text = rankisimler [rankNo];
                for (int i = 0; i < rankResimler.Length; i++) {
                    rankResimler [i].SetActive (false);
                }
                rankResimler [rankNo/5].SetActive (true);
                float ranknofloat = (float)rankNo;
                float rankoran = ranknofloat % 5f;
                rankBar.fillAmount = rankoran/5f+0.2f;*/

        string guestName = "Guest" + UnityEngine.Random.Range(0000, 5000).ToString();
        if (!string.IsNullOrEmpty(PrefManager.GetUserName()))
        {
            placeHolderText.text = PrefManager.GetUserName();
        }
        else
        {
            PrefManager.SetUserName(guestName);
            placeHolderText.text = PrefManager.GetUserName();
        }

    }

    private void SaveUserNameOnStartPlay()
    {
        if (string.IsNullOrEmpty(isimText.text))
        {
            PrefManager.SetUserName(placeHolderText.text);
        }
        else
        {
            PrefManager.SetUserName(isimText.text);
        }
    }

    public void BayginOyuncuEkle(PlayerController_A bayilan)
    {
        for (int i = 0; i < digerOyuncular.Length; i++)
        {
            if (!digerOyuncular[i].transform.GetChild(0).GetComponent<AIController>().isSelected)
            {
                digerOyuncular[i].transform.GetChild(0).GetComponent<AIController>().currentTarget =
                    SelectedBayginOyuncu(
                        digerOyuncular[i].transform.GetChild(0).GetComponent<PlayerController_A>()).transform;
            }
        }

        if (!bayginoyuncular.Contains(bayilan))
        {
            bayginoyuncular.Add(bayilan);
        }
    }

    public void BayginOyuncuCikar(PlayerController_A bayilan)
    {
        if (bayginoyuncular.Contains(bayilan))
        {
            bayginoyuncular.Remove(bayilan);
        }
    }

    //En Yakin Baygin Oyuncu
    public PlayerController_A SelectedBayginOyuncu(PlayerController_A myself)
    {
        if (bayginoyuncular.Count > 0)
        {
            float pos = Vector3.Distance(myself.transform.position, bayginoyuncular[0].transform.position);
            int no = 0;

            for (int i = 0; i < bayginoyuncular.Count; i++)
            {
                float diffp = Vector3.Distance(myself.transform.position, bayginoyuncular[i].transform.position);
                if (diffp < pos)
                {
                    pos = Vector3.Distance(myself.transform.position, bayginoyuncular[i].transform.position);
                    no = i;
                }
            }

            return bayginoyuncular[no];
        }

        return me;
    }

    [Header("Costume Values")] public GameObject[] kiyafet, kiyafet2;
    public Color[] renk;
    public Color gri;
    int kiyafet_deg;
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

    public GameObject locked;
    public GameObject eyes;
    public GameObject playerEyes;
    public GameObject man;
    float scrollX = 0.02f;
    float scrollY = 0f;
    float offsetX;
    float offsetY;
    bool isTextureScrolling = false;
    bool isGameStarted = false;
    public Text levelText;
    public GameObject playerAddCoin;
    public AudioSource menuMusic;
    public AudioSource coinVoice;


    Animator anim;

    public void baslangic_adam()
    {
        levelText.text = PlayerPrefs.GetInt("XP").ToString();

        //anim.Play("MenuAnimation");
        if (PlayerPrefs.GetInt("kiyafet_deg") > 35)
        {
            PlayerPrefs.SetInt("kiyafet_deg", 36);
        }

        if (isGameStarted)
        {
            if (eyes.activeSelf == false)
            {
                playerEyes.SetActive(false);
            }
        }

        if (!isGameStarted)
        {
            PlayerPrefs.SetInt("kiyafet_deg", PlayerPrefs.GetInt("unlocked") - 1);

            if (eyes.activeSelf == false)
            {
                playerEyes.SetActive(false);
            }
        }

        kiyafet_deg = PlayerPrefs.GetInt("kiyafet_deg");
        for (int i = 0; i < kiyafet.Length; i++)
        {
            kiyafet[i].SetActive(false);
            kiyafet2[i].SetActive(false);
            for (int x = 0; x < kiyafet[i].GetComponent<acilacak_kiyafetler>().kiyafet.Length; x++)
            {
                kiyafet[i].GetComponent<acilacak_kiyafetler>().kiyafet[x].SetActive(false);
                kiyafet2[i].GetComponent<acilacak_kiyafetler>().kiyafet[x].SetActive(false);
            }
        }

        kiyafet[kiyafet_deg].gameObject.SetActive(true);
        kiyafet2[kiyafet_deg].gameObject.SetActive(true);

        switch (kiyafet_deg)
        {
            case 1:
                man.GetComponent<SkinnedMeshRenderer>().material = blackWhite;
                eyes.SetActive(false);
                playerEyes.SetActive(false);

                break;
     
            case 30:
                man.GetComponent<SkinnedMeshRenderer>().material = adam_renk;
                eyes.SetActive(false);
                playerEyes.SetActive(false);

                break;
            case 2:
                man.GetComponent<SkinnedMeshRenderer>().material = jacket;
                eyes.SetActive(true);
                playerEyes.SetActive(true);

                break;
            case 3:
                man.GetComponent<SkinnedMeshRenderer>().material = panda;
                break;
            case 4:
                man.GetComponent<SkinnedMeshRenderer>().material = green;
                eyes.SetActive(true);
                playerEyes.SetActive(true);


                break;
            case 5:
                man.GetComponent<SkinnedMeshRenderer>().material = skull;
                eyes.SetActive(false);
                playerEyes.SetActive(false);

                break;
            case 6:
                man.GetComponent<SkinnedMeshRenderer>().material = marshmallow;
                eyes.SetActive(false);
                playerEyes.SetActive(false);

                break;
            case 8:
                man.GetComponent<SkinnedMeshRenderer>().material = snowMan;
                eyes.SetActive(false);
                playerEyes.SetActive(false);

                break;
            case 9:
                man.GetComponent<SkinnedMeshRenderer>().material = doctor;
                eyes.SetActive(true);
                playerEyes.SetActive(true);

                break;
            case 10:
                man.GetComponent<SkinnedMeshRenderer>().material = clown;
                eyes.SetActive(false);
                playerEyes.SetActive(false);

                break;
            case 15:
                man.GetComponent<SkinnedMeshRenderer>().material = space;
                eyes.SetActive(false);
                playerEyes.SetActive(false);

                isTextureScrolling = true;
                break;
            case 19:
                man.GetComponent<SkinnedMeshRenderer>().material = basketball;
                eyes.SetActive(true);
                playerEyes.SetActive(true);

                break;
            case 20:
                man.GetComponent<SkinnedMeshRenderer>().material = ninja;
                eyes.SetActive(false);
                playerEyes.SetActive(false);

                break;
            case 23:
                man.GetComponent<SkinnedMeshRenderer>().material = orange;
                eyes.SetActive(true);
                playerEyes.SetActive(true);

                break;
            case 27:
                man.GetComponent<SkinnedMeshRenderer>().material = blue;
                eyes.SetActive(true);
                playerEyes.SetActive(true);

                break;
            case 34:
                man.GetComponent<SkinnedMeshRenderer>().material = pink;
                eyes.SetActive(true);
                playerEyes.SetActive(true);

                break;
            case 35:
                man.GetComponent<SkinnedMeshRenderer>().material = robot;
                eyes.SetActive(false);
                playerEyes.SetActive(false);

                break;
            default:
                man.GetComponent<SkinnedMeshRenderer>().material = adam_renk;
                eyes.SetActive(true);
                playerEyes.SetActive(true);

                break;
        }

        adam_renk.color = renk[kiyafet_deg];
        for (int x = 0; x < kiyafet[kiyafet_deg].GetComponent<acilacak_kiyafetler>().kiyafet.Length; x++)
        {
            kiyafet[kiyafet_deg].GetComponent<acilacak_kiyafetler>().kiyafet[x].SetActive(true);
            kiyafet2[kiyafet_deg].GetComponent<acilacak_kiyafetler>().kiyafet[x].SetActive(true);
        }

        if (kiyafet_deg == 15)
            isTextureScrolling = true;
        else
            isTextureScrolling = false;

        locked.SetActive(false);
    }

    public void sag()
    {
        //anim.Play("MenuAnimation");
        MMVibrationManager.Haptic(HapticTypes.Selection);
        if (kiyafet_deg == 15)
            isTextureScrolling = true;
        else
            isTextureScrolling = false;

        for (int i = 0; i < kiyafet.Length; i++)
        {
            kiyafet[i].SetActive(false);
            kiyafet2[i].SetActive(false);
            for (int x = 0; x < kiyafet[i].GetComponent<acilacak_kiyafetler>().kiyafet.Length; x++)
            {
                kiyafet[i].GetComponent<acilacak_kiyafetler>().kiyafet[x].SetActive(false);
                kiyafet2[i].GetComponent<acilacak_kiyafetler>().kiyafet[x].SetActive(false);
            }
        }

        kiyafet_deg++;
        switch (kiyafet_deg)
        {
            case 1:
                man.GetComponent<SkinnedMeshRenderer>().material = blackWhite;
                eyes.SetActive(false);
                break;
            case 30:
                man.GetComponent<SkinnedMeshRenderer>().material = adam_renk;
                eyes.SetActive(false);
                break;
            case 2:
                man.GetComponent<SkinnedMeshRenderer>().material = jacket;
                eyes.SetActive(true);
                break;
            case 3:
                man.GetComponent<SkinnedMeshRenderer>().material = panda;
                break;
            case 4:
                man.GetComponent<SkinnedMeshRenderer>().material = green;
                eyes.SetActive(true);

                break;
            case 5:
                man.GetComponent<SkinnedMeshRenderer>().material = skull;
                eyes.SetActive(false);
                break;
            case 6:
                man.GetComponent<SkinnedMeshRenderer>().material = marshmallow;
                eyes.SetActive(false);
                break;
            case 8:
                man.GetComponent<SkinnedMeshRenderer>().material = snowMan;
                eyes.SetActive(false);
                break;
            case 9:
                man.GetComponent<SkinnedMeshRenderer>().material = doctor;
                eyes.SetActive(true);
                break;
            case 10:
                man.GetComponent<SkinnedMeshRenderer>().material = clown;
                eyes.SetActive(false);
                break;
            case 15:
                man.GetComponent<SkinnedMeshRenderer>().material = space;
                eyes.SetActive(false);
                isTextureScrolling = true;
                break;
            case 19:
                man.GetComponent<SkinnedMeshRenderer>().material = basketball;
                eyes.SetActive(true);
                break;
            case 20:
                man.GetComponent<SkinnedMeshRenderer>().material = ninja;
                eyes.SetActive(false);
                break;
            case 23:
                man.GetComponent<SkinnedMeshRenderer>().material = orange;
                eyes.SetActive(true);
                break;
            case 27:
                man.GetComponent<SkinnedMeshRenderer>().material = blue;
                eyes.SetActive(true);
                break;
            case 34:
                man.GetComponent<SkinnedMeshRenderer>().material = pink;
                eyes.SetActive(true);
                break;
            case 35:
                man.GetComponent<SkinnedMeshRenderer>().material = robot;
                eyes.SetActive(false);
                break;
            default:
                man.GetComponent<SkinnedMeshRenderer>().material = adam_renk;
                eyes.SetActive(true);
                break;
        }

        if (kiyafet_deg >= kiyafet.Length)
        {
            kiyafet_deg = 0;
        }

        kiyafet[kiyafet_deg].gameObject.SetActive(true);
        kiyafet2[kiyafet_deg].gameObject.SetActive(true);
        //if (kiyafet_deg <= PlayerPrefs.GetInt ("unlocked"))
        adam_renk.color = renk[kiyafet_deg];
        //else {
        //	adam_renk.color = gri;
        //}
        for (int x = 0; x < kiyafet[kiyafet_deg].GetComponent<acilacak_kiyafetler>().kiyafet.Length; x++)
        {
            kiyafet[kiyafet_deg].GetComponent<acilacak_kiyafetler>().kiyafet[x].SetActive(true);
            kiyafet2[kiyafet_deg].GetComponent<acilacak_kiyafetler>().kiyafet[x].SetActive(true);
        }

        if (PlayerPrefs.GetInt("unlocked") > kiyafet_deg)
        {
            PlayerPrefs.SetInt("kiyafet_deg", kiyafet_deg);
            locked.SetActive(false);
        }
        else
        {
            locked.SetActive(true);
        }
    }

    public void sol()
    {
        MMVibrationManager.Haptic(HapticTypes.Selection);
        //anim.Play("MenuAnimation");
        if (kiyafet_deg == 15)
            isTextureScrolling = true;
        else
            isTextureScrolling = false;


        for (int i = 0; i < kiyafet.Length; i++)
        {
            kiyafet[i].SetActive(false);
            kiyafet2[i].SetActive(false);
            for (int x = 0; x < kiyafet[i].GetComponent<acilacak_kiyafetler>().kiyafet.Length; x++)
            {
                kiyafet[i].GetComponent<acilacak_kiyafetler>().kiyafet[x].SetActive(false);
                kiyafet2[i].GetComponent<acilacak_kiyafetler>().kiyafet[x].SetActive(false);
            }
        }

        kiyafet_deg--;


        if (kiyafet_deg < 0)
        {
            kiyafet_deg = 35;
            //kiyafet_deg = kiyafet.Length-1;
        }

        switch (kiyafet_deg)
        {
            case 1:
                man.GetComponent<SkinnedMeshRenderer>().material = blackWhite;
                eyes.SetActive(false);
                break;
          
            case 30:
                man.GetComponent<SkinnedMeshRenderer>().material = adam_renk;
                eyes.SetActive(false);
                break;
            case 2:
                man.GetComponent<SkinnedMeshRenderer>().material = jacket;
                eyes.SetActive(true);
                break;
            case 3:
                man.GetComponent<SkinnedMeshRenderer>().material = panda;
                break;
            case 4:
                man.GetComponent<SkinnedMeshRenderer>().material = green;
                eyes.SetActive(true);

                break;
            case 5:
                man.GetComponent<SkinnedMeshRenderer>().material = skull;
                eyes.SetActive(false);
                break;
            case 6:
                man.GetComponent<SkinnedMeshRenderer>().material = marshmallow;
                eyes.SetActive(false);
                break;
            case 8:
                man.GetComponent<SkinnedMeshRenderer>().material = snowMan;
                eyes.SetActive(false);
                break;
            case 9:
                man.GetComponent<SkinnedMeshRenderer>().material = doctor;
                eyes.SetActive(true);
                break;
            case 10:
                man.GetComponent<SkinnedMeshRenderer>().material = clown;
                eyes.SetActive(false);
                break;
            case 15:
                man.GetComponent<SkinnedMeshRenderer>().material = space;
                eyes.SetActive(false);
                isTextureScrolling = true;
                break;
            case 19:
                man.GetComponent<SkinnedMeshRenderer>().material = basketball;
                eyes.SetActive(true);
                break;
            case 20:
                man.GetComponent<SkinnedMeshRenderer>().material = ninja;
                eyes.SetActive(false);
                break;
            case 23:
                man.GetComponent<SkinnedMeshRenderer>().material = orange;
                eyes.SetActive(true);
                break;
            case 27:
                man.GetComponent<SkinnedMeshRenderer>().material = blue;
                eyes.SetActive(true);
                break;
            case 34:
                man.GetComponent<SkinnedMeshRenderer>().material = pink;
                eyes.SetActive(true);
                break;
            case 35:
                man.GetComponent<SkinnedMeshRenderer>().material = robot;
                eyes.SetActive(false);
                break;
            default:
                man.GetComponent<SkinnedMeshRenderer>().material = adam_renk;
                eyes.SetActive(true);
                break;
        }

        kiyafet[kiyafet_deg].gameObject.SetActive(true);
        kiyafet2[kiyafet_deg].gameObject.SetActive(true);

        //if (kiyafet_deg < PlayerPrefs.GetInt ("unlocked"))
        adam_renk.color = renk[kiyafet_deg];
        //else {
        //	adam_renk.color = gri;
        //}

        for (int x = 0; x < kiyafet[kiyafet_deg].GetComponent<acilacak_kiyafetler>().kiyafet.Length; x++)
        {
            kiyafet[kiyafet_deg].GetComponent<acilacak_kiyafetler>().kiyafet[x].SetActive(true);
            kiyafet2[kiyafet_deg].GetComponent<acilacak_kiyafetler>().kiyafet[x].SetActive(true);
        }

        if (PlayerPrefs.GetInt("unlocked") > kiyafet_deg)
        {
            PlayerPrefs.SetInt("kiyafet_deg", kiyafet_deg);
            locked.SetActive(false);
        }
        else
        {
            locked.SetActive(true);
        }
    }


    public void loadlevel(int i)
    {
        SceneManager.LoadScene(i);
    }
}