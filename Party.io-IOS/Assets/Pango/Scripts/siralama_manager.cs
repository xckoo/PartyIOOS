using System.Collections;
using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class siralama_manager : MonoBehaviour
{
    public GameObject[] isim;
    public GameObject[] sira;
    public Color[] renk;
    [SerializeField] GameObject needCoin;
    [SerializeField] GiveCoin gc;

    // Use this for initialization
    bool isCostumeGained = false;


    string rewardedAdUnitId = "6f3bf2499f0fbe7a";
    string interstitialAdUnitId = "34602e0739d99bac";
    string bannerAdUnitId = "3ed596e5c0988562";

    private void Awake()
    {
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
        {
            // AppLovin SDK is initialized, start loading ads
        };
        MaxSdk.SetSdkKey("cHgLnnoKECUB17AIy93T68opMr-9z0yZIdv3OhOdutpw_-Dx8GUFPG2m61v32sdKTXNtDmV6dJ0cHETQV1fvIi");
        MaxSdk.InitializeSdk();
    }


    void ShowAds()
    {
        if (PlayerPrefs.GetInt("Reklam") != 1)
        {
            if (MaxSdk.IsInterstitialReady(interstitialAdUnitId))
            {
                MaxSdk.ShowInterstitial(interstitialAdUnitId);
            }
        }

        if (MaxSdk.IsRewardedAdReady(rewardedAdUnitId))
        {
            MaxSdk.ShowRewardedAd(rewardedAdUnitId);
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

    private void LoadRewardedAd()
    {
        MaxSdk.LoadRewardedAd(rewardedAdUnitId);
    }

    private void OnRewardedAdLoadedEvent(string adUnitId)
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
    }

    public void OnRewardedAdClickedEvent(string adUnitId)
    {
    }

    public void OnRewardedAdDismissedEvent(string adUnitId)
    {
        LoadRewardedAd();
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward)
    {
        // Rewarded ad was displayed and user should receive the reward
        if (isCostumeGained)
        {
            isCostumeGained = false;

            if (PlayerPrefs.GetInt("Level") < 14)
                SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
            else
            {
                int rnd2 = UnityEngine.Random.Range(0, 14);
                PlayerPrefs.SetInt("Level", rnd2);
                SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
            }

            PlayerPrefs.SetInt("unlocked", PlayerPrefs.GetInt("unlocked") + 1);
            PlayerPrefs.SetInt("temp_unlocked", 0);
        }
    }

    void Start()
    {
        InitializeRewardedAds();

        isim[6].GetComponent<Text>().text = PlayerPrefs.GetString("yedinci_str").ToString();
        isim[5].GetComponent<Text>().text = PlayerPrefs.GetString("altinci_str").ToString();
        isim[4].GetComponent<Text>().text = PlayerPrefs.GetString("besinci_str").ToString();
        isim[3].GetComponent<Text>().text = PlayerPrefs.GetString("dorduncu_str").ToString();
        isim[2].GetComponent<Text>().text = PlayerPrefs.GetString("ucuncu_str").ToString();
        isim[1].GetComponent<Text>().text = PlayerPrefs.GetString("ikinci_str").ToString();
        isim[0].GetComponent<Text>().text = PlayerPrefs.GetString("birinci_str").ToString();


        MaxSdk.HideBanner(bannerAdUnitId);

        sira[0].transform.GetChild(0).gameObject.SetActive(true);
         sira[1].transform.GetChild(PlayerPrefs.GetInt("ikinci")).gameObject.SetActive(true);
         sira[2].transform.GetChild(PlayerPrefs.GetInt("ucuncu")).gameObject.SetActive(true);


        if (PlayerPrefs.GetInt("birinci") > 0)
            // sira[0].transform.GetChild(PlayerPrefs.GetInt("birinci")).GetComponent<kiyafet_secim_ai>().kiyafet[PlayerPrefs.GetInt("kiyafet" + PlayerPrefs.GetInt("birinci"))].SetActive(true);
            sira[0].transform.GetChild(0).GetComponent<SiralamaCostume>()
                .kiyafet[PlayerPrefs.GetInt("kiyafet_deg")].SetActive(true);
        else
        {
            sira[0].transform.GetChild(PlayerPrefs.GetInt("birinci")).GetComponent<YeniKiyafetSecim>()
                .kiyafet[PlayerPrefs.GetInt("kiyafet_deg")].SetActive(true);
        }

        if (PlayerPrefs.GetInt("ikinci") > 0)
            sira[1].transform.GetChild(PlayerPrefs.GetInt("ikinci")).gameObject.GetComponent<kiyafet_secim_ai>()
                .kiyafet[PlayerPrefs.GetInt("kiyafet" + PlayerPrefs.GetInt("ikinci"))].SetActive(true);
        else
        {
            sira[1].transform.GetChild(PlayerPrefs.GetInt("ikinci")).GetComponent<kiyafet_secim_ai>()
                .kiyafet[PlayerPrefs.GetInt("kiyafet_deg")].SetActive(true);
        }

        if (PlayerPrefs.GetInt("ucuncu") > 0)
        {
            sira[2].transform.GetChild(PlayerPrefs.GetInt("ucuncu")).gameObject.GetComponent<kiyafet_secim_ai>()
                .kiyafet[PlayerPrefs.GetInt("kiyafet" + PlayerPrefs.GetInt("ucuncu"))].SetActive(true);
        }
        else
        {
            sira[2].transform.GetChild(PlayerPrefs.GetInt("ucuncu")).GetComponent<kiyafet_secim_ai>()
                .kiyafet[PlayerPrefs.GetInt("kiyafet_deg")].SetActive(true);
        }

        for (int x = 0;
            x < sira[0].transform.GetChild(0).GetComponent<SiralamaCostume>()
                .kiyafet[PlayerPrefs.GetInt("kiyafet_deg")].GetComponent<acilacak_kiyafetler>().kiyafet.Length;
            x++)
        {
            sira[0].transform.GetChild(0).GetComponent<SiralamaCostume>()
                .kiyafet[PlayerPrefs.GetInt("kiyafet_deg")].GetComponent<acilacak_kiyafetler>().kiyafet[x]
                .SetActive(true);
        }
    }


    IEnumerator sayac7()
    {
        yield return new WaitForSecondsRealtime(0);
    }

    bool once_score = false;

    void score_add()
    {
        for (int i = 0; i < isim.Length; i++)
        {
            if (PlayerPrefs.GetInt("Siralamam") - 1 != i)
            {
                if (PlayerPrefs.GetInt("Level_score_add" + i) > 0)
                {
                    isim[i].transform.parent.GetChild(4).GetComponent<Text>().color = Color.green;
                }

                if (PlayerPrefs.GetInt("Level_score_add" + i) < 0)
                {
                    isim[i].transform.parent.GetChild(4).GetComponent<Text>().color = Color.red;
                }

                //botlarin skorunu ekrana yaziyor
                isim[i].transform.parent.GetChild(4).GetComponent<Text>().text =
                    (PlayerPrefs.GetInt("Level_score_AI" + i) + PlayerPrefs.GetInt("Level_score_add" + i)).ToString();
            
            }
            else
            {
                if (PlayerPrefs.GetInt("Level_score") + PlayerPrefs.GetInt("add_score_deg") > 0)
                {
                    isim[i].transform.parent.GetChild(4).GetComponent<Text>().text =
                        (PlayerPrefs.GetInt("Level_score") + PlayerPrefs.GetInt("add_score_deg")).ToString(); //Score
                    isim[i].transform.parent.GetChild(4).GetComponent<Text>().color = Color.green;
                }
                else
                {
                    isim[i].transform.parent.GetChild(4).GetComponent<Text>().text = "0";
                    isim[i].transform.parent.GetChild(4).GetComponent<Text>().color = Color.red;
                }


                PlayerPrefs.SetInt("Level_score",
                    PlayerPrefs.GetInt("Level_score") + PlayerPrefs.GetInt("add_score_deg"));

                if (PlayerPrefs.GetInt("Level_score") <= 0)
                    PlayerPrefs.SetInt("Level_score", 0);
            }
        }

        once_score = true;
    }
    public void Load_Scene()
    {
        if (PlayerPrefs.GetInt("unlocked")<37)
        {
            SceneManager.LoadScene(15);
        }
        else
        {
            if (PlayerPrefs.GetInt("Level") < 14)
                SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
            else
            {
                int rnd2 = UnityEngine.Random.Range(0, 14);
                PlayerPrefs.SetInt("Level", rnd2);
                SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
            }
        }
        // if (PlayerPrefs.GetInt("temp_unlocked") == PlayerPrefs.GetInt("unlocked"))
        // {
        //     PlayerPrefs.SetInt("temp_unlocked", 0);
        //     SceneManager.LoadScene(15);
        // }
        // else
        // {
        //     if (PlayerPrefs.GetInt("Level") < 15)
        //         SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        //     else
        //     {
        //         int rnd2 = UnityEngine.Random.Range(0, 14);
        //         PlayerPrefs.SetInt("Level", rnd2);
        //         SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        //     }
        // }

        if (!once_score)
        {
            PlayerPrefs.SetInt("Level_score", PlayerPrefs.GetInt("Level_score") + PlayerPrefs.GetInt("add_score_deg"));

            if (PlayerPrefs.GetInt("Level_score") <= 0)
                PlayerPrefs.SetInt("Level_score", 0);
        }

        gc.isUnlocked = true;
    }

    bool once_load = true;

    public void Load_Game_Scene()
    {
        if (once_load)
        {
            once_load = false;
            PlayerPrefs.SetInt("unlocked", PlayerPrefs.GetInt("unlocked") - 1);
            PlayerPrefs.SetInt("temp_unlocked", 0);


            if (PlayerPrefs.GetInt("Level") < 14)
                SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
            else
            {
                int rnd2 = UnityEngine.Random.Range(0, 14);
                PlayerPrefs.SetInt("Level", rnd2);
                SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
            }
        }
    }


    public void reklam_izle()
    {
        MMVibrationManager.Haptic(HapticTypes.Selection);

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
        }
        else
        {
            if (MaxSdk.IsRewardedAdReady(rewardedAdUnitId))
            {
                isCostumeGained = true;
                MaxSdk.ShowRewardedAd(rewardedAdUnitId);
                if (PlayerPrefs.GetInt("unlocked") > 1)
                {
                    PlayerPrefs.SetInt("unlocked", PlayerPrefs.GetInt("unlocked") - 1);
                    PlayerPrefs.SetInt("temp_unlocked", 0);
                }
            }
            else
            {
                LoadRewardedAd();
            }
        }
    }

    public void closeNeedCoin()
    {
        needCoin.SetActive(false);
        gc.isUnlocked = true;
    }
}