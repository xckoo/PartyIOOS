using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.NiceVibrations;
using GameAnalyticsSDK;


public class CoinManager : MonoBehaviour
{
    public Text coinText;
    GameManager_A gm;
    public GameObject claimButton;
    public GameObject claimx3Button;
    private GameObject _menuReward;
    bool isAdShowed1 = false;
    bool isAdShowed2 = false;
    public bool isAdShowed3;
    bool isAdShowed4;
    string rewardedAdUnitId = "6f3bf2499f0fbe7a";
    bool lerped;
    int x2coin;
    float curentGold;
    float targetGold;
    float endGameGold;
    float currentEndGameGold;
    private GameObject _removeAd;
    private GameObject _costumeNeedCoin;
    CostumeManager cm;

    public ParticleSystem coinParticle;

    [SerializeField] private GameObject coinWheel;
    // Use this for initialization
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
        if (isAdShowed1)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "MenuRewardPressed");

            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 75);
            targetGold = PlayerPrefs.GetInt("Coin");
            isAdShowed1 = false;
            //            gold.Play();
            lerped = true;
            coinParticle.Play();
            gm.coinVoice.Play();
        }

        if (isAdShowed2)
        {
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + ((gm.me.myScore * 5) + gm.winCoin));


            currentEndGameGold = (gm.me.myScore * 5) + gm.winCoin;
            x2coin = ((gm.me.myScore * 5) + gm.winCoin) * 2;
            endGameGold = x2coin + 1;
            //            gm.Level_score.text = x2kill.ToString();
            claimButton.SetActive(false);
            isAdShowed2 = false;
            gm.coinVoice.Play();
        }

        if (isAdShowed3)
        {
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + ((gm.me.myScore * 2) + gm.winCoin) * 2);


            currentEndGameGold = (gm.me.myScore * 5) + gm.winCoin;
            x2coin = ((gm.me.myScore * 5) + gm.winCoin) * 3;
            endGameGold = x2coin + 1;
            //            gm.Level_score.text = x2kill.ToString();
            claimx3Button.SetActive(false);
            isAdShowed3 = false;
            gm.coinVoice.Play();
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "x2CoinRewardPressed");
        }

        if (isAdShowed4)
        {
            cm.getCostumeCoin = true;
            isAdShowed4 = false;
            //            gold.Play();
            gm.coinVoice.Play();
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "CostumeNeedCoinPressed");
        }

        LoadRewardedAd();
    }

    private void OnEnable()
    {
        coinText.text = PlayerPrefs.GetInt("Coin").ToString();
    }

    private void Awake()
    {
        InitializeRewardedAds();
    }

    void Start()
    {
        _menuReward = GameObject.FindGameObjectWithTag("MenuReward");
        _removeAd = GameObject.FindGameObjectWithTag("RemoveAd");
        _costumeNeedCoin = GameObject.FindGameObjectWithTag("CostumeNeedCoin");
        coinWheel.SetActive(false);
        PlayerPrefs.GetInt("PopUp", 0);
        if (PlayerPrefs.GetInt("Reklam") == 1)
        {
            if(_removeAd != null)
                _removeAd.SetActive(false);
        }

        curentGold = PlayerPrefs.GetInt("Coin");
        targetGold = PlayerPrefs.GetInt("Coin");
        gm = GetComponent<GameManager_A>();
        cm = GetComponent<CostumeManager>();
        InitializeRewardedAds();

        endGameGold = currentEndGameGold;
        if (!MaxSdk.IsRewardedAdReady(rewardedAdUnitId))
        {
            _menuReward.SetActive(false);
        }

        InvokeRepeating("CheckMenuReward", 1, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (targetGold - curentGold < 1)
        {
            targetGold = curentGold;
            coinText.text = PlayerPrefs.GetInt("Coin").ToString();
        }
        else
        {
            curentGold = Mathf.Lerp(curentGold, targetGold, 0.1f);
            coinText.text = Mathf.FloorToInt(curentGold).ToString();
        }

        if (endGameGold - currentEndGameGold < 1)
        {
            endGameGold = currentEndGameGold;
            //gm.Level_score.text = PlayerPrefs.GetInt("Coin").ToString();
        }
        else
        {
            currentEndGameGold = Mathf.Lerp(currentEndGameGold, endGameGold, 0.05f);
            gm.Level_score.text = Mathf.FloorToInt(currentEndGameGold).ToString();
        }
    }

    void CheckMenuReward()
    {
        if (_menuReward != null&& PlayerPrefs.GetInt("Level") !=15)
        {
            if (MaxSdk.IsRewardedAdReady(rewardedAdUnitId))
            {
                coinWheel.SetActive(true);
                _menuReward.SetActive(true);
                if (PlayerPrefs.GetInt("PopUp")== 0)
                {
                    DailySpinDetector.instance.popUp.SetActive(true);
                }
            }
            else
            {
                coinWheel.SetActive(false);
                _menuReward.SetActive(false);
                DailySpinDetector.instance.popUp.SetActive(false);
            }
        }
        
        if (_costumeNeedCoin != null )
        {
            if(MaxSdk.IsRewardedAdReady(rewardedAdUnitId))
             _costumeNeedCoin.SetActive(true);
            else
                _costumeNeedCoin.SetActive(false);
        }
        
    }

    public void WatchReward1()
    {
        MMVibrationManager.Haptic(HapticTypes.Selection);
        if (MaxSdk.IsRewardedAdReady(rewardedAdUnitId))
        {
            MaxSdk.ShowRewardedAd(rewardedAdUnitId);
            isAdShowed4 = true;
        }

        LoadRewardedAd();
    }

    public void WatchReward()
    {
        MMVibrationManager.Haptic(HapticTypes.Selection);
        if (MaxSdk.IsRewardedAdReady(rewardedAdUnitId))
        {
            MaxSdk.ShowRewardedAd(rewardedAdUnitId);
            isAdShowed1 = true;
        }

        LoadRewardedAd();
    }

    public void SetCoin()
    {
        coinText.text = PlayerPrefs.GetInt("Coin").ToString();
    }

    public void x2Coin()
    {
        MMVibrationManager.Haptic(HapticTypes.Selection);
        if (MaxSdk.IsRewardedAdReady(rewardedAdUnitId))
        {
            MaxSdk.ShowRewardedAd(rewardedAdUnitId);
            isAdShowed2 = true;
        }
    }

    public void x3Coin()
    {
        MMVibrationManager.Haptic(HapticTypes.Selection);
        if (MaxSdk.IsRewardedAdReady(rewardedAdUnitId))
        {
            MaxSdk.ShowRewardedAd(rewardedAdUnitId);
            isAdShowed3 = true;
        }
    }


    /* public void NameHolder (){
         PlayerPrefs.SetInt("Name",1);
         if (PlayerPrefs.GetInt("Name")==1)
         {
             PlayerPrefs.SetInt("Name", 2);
         }
         else
         {
             if (MaxSdk.IsRewardedAdReady(rewardedAdUnitId))
             {
                 MaxSdk.ShowRewardedAd(rewardedAdUnitId);

             }
         }
     }*/
}