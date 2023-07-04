using System.Collections;
using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UnlockedSceneMax : MonoBehaviour
{
    string rewardedAdUnitId = "6f3bf2499f0fbe7a";
    bool isCostumeGained = false;
    private GiveCoin _gc;

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
            // PlayerPrefs.SetInt("unlocked", PlayerPrefs.GetInt("unlocked") - 1);

        }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
        {
            // AppLovin SDK is initialized, start loading ads
        };
        MaxSdk.SetSdkKey("cHgLnnoKECUB17AIy93T68opMr-9z0yZIdv3OhOdutpw_-Dx8GUFPG2m61v32sdKTXNtDmV6dJ0cHETQV1fvIi");
        MaxSdk.InitializeSdk();
    }

    void Start()
    {
        _gc = gameObject.GetComponent<GiveCoin>();
        InitializeRewardedAds();
    }

    public void UnlockCharacterReward()
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
            }
            else
            {
                LoadRewardedAd();
            }
        }
    }

    public void LoadScene()
    
    {
        LevelManager.I.CloseEverything();

        LevelManager.I.NextLevel();
        
        // if (PlayerPrefs.GetInt("Level") < 14)
        // {
        //     // SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        //     
        //
        // }
        // else
        // {
        //     // int rnd2 = UnityEngine.Random.Range(0, 14);
        //     // PlayerPrefs.SetInt("Level", rnd2);
        //     LevelManager.I.NextLevel();
        // }


        _gc.isUnlocked = true;
    }
}