using System;
using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WheelManager : MonoBehaviour
{
    public static WheelManager instance;
    public int turnSpeedEditor; 
    public bool getCollide;
    public int currentCoin;
    public int targetCoin;
    public float rayLength;
    bool startRotate;
    int finalAngle;
    int wheelRatio;
    [SerializeField] Text coinText;
    float turnSpeed;
    int finalCoin;
    public bool inc = true;
    float _counter;
    public bool coinLerp = true;
    [SerializeField] GameObject pick;
    [SerializeField] Button spinRewardButton;
    private Button _spinCloseButton;
    float lerpCounter;
    public ParticleSystem wheelCoinParticle, wheelCoinParticle1000, wheelCoinParticle250;
    SliceManager sliceManager;
    string rewardedAdUnitId = "6f3bf2499f0fbe7a";
    bool giveSpin;


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

    private void OnRewardedAdDisplayedEvent(string adUnitId) { }

    public void OnRewardedAdClickedEvent(string adUnitId) { }

    public void OnRewardedAdDismissedEvent(string adUnitId)
    {

        // Rewarded ad is hidden. Pre-load the next ad
        LoadRewardedAd();
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward)
    {
        if (giveSpin)
        {
            StartRotate();
            giveSpin = false;
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail,"WheelRewardSpinPressed");
            PlayerPrefs.SetInt("PopUp",1);
        }
        LoadRewardedAd();
        // Rewarded ad was displayed and user should receive the reward
    }

    private void OnEnable()
    {
        currentCoin = PlayerPrefs.GetInt("Coin",0);
        coinText.text =currentCoin.ToString();
        targetCoin = currentCoin;
    }

    void Start()
    {
        InitializeRewardedAds();
        instance = this;
        _spinCloseButton = GameObject.FindWithTag("SpinClose").GetComponent<Button>();
        
        
      //  InvokeRepeating("CheckSpinReward",0,2);
    }
    // Update is called once per frame
    void Update()
    {

        if (startRotate)
        {

            if (inc)
            {
                if (_counter >= turnSpeed)
                {
                    inc = false;
                }
                _counter += Time.deltaTime * 10;
                transform.Rotate(0, 0, _counter);
            }
            else
            {
                if (turnSpeed < 1)
                {
                    turnSpeed = 0;
                    finalAngle = Mathf.RoundToInt(transform.eulerAngles.z);
                    if (finalAngle % 30 <= 5)
                    {
                        transform.Rotate(0, 0, 5);
                    }
                    FindSlice();
                    getCollide = true;
                    inc = true;
                    startRotate = false;
                    _counter = 0;

                    if (!spinRewardButton.gameObject.activeSelf)
                    {
                        spinRewardButton.gameObject.SetActive(true);
                        spinRewardButton.interactable = true;
                    }
                    else
                    {
                        spinRewardButton.interactable = true;
                    }


                }
                else
                {
                    transform.Rotate(0, 0, turnSpeed);
                    turnSpeed -= Time.deltaTime * 10;
                }
            }
        }
        if (coinLerp)
        {
            if (lerpCounter < 1)
            {

                lerpCounter += Time.deltaTime / 1.5f;
                finalCoin = (int)Mathf.Lerp(currentCoin, targetCoin, lerpCounter);
                coinText.text = (finalCoin).ToString();

            }
            else
            {
                currentCoin = targetCoin;
                lerpCounter = 0;
                PlayerPrefs.SetInt("Coin", finalCoin);
                _spinCloseButton.interactable = true;
            }

        }

    }

    public void CheckSpinReward()
    {
        if (MaxSdk.IsRewardedAdReady(rewardedAdUnitId)&& !WheelTimer.instance.dailySpinButton.IsInteractable() && turnSpeed<1)
            spinRewardButton.gameObject.SetActive(true);
        else
            spinRewardButton.gameObject.SetActive(false);

    }
    public void FindSlice()
    {
        RaycastHit hit;
        if (Physics.Raycast(pick.transform.position, -pick.transform.up, out hit, rayLength))
        {
            sliceManager = hit.collider.GetComponentInParent<SliceManager>();
            sliceManager.GetCoin();
        }

    }
    public void StartRotate()
    {
        startRotate = true;
        coinLerp = false;

        wheelRatio = Random.Range(1, 100);

        float randTurnSpeed = Random.Range(1.5f, 2.5f);
        // turnSpeed = randTurnSpeed * 15;
        turnSpeed = turnSpeedEditor * randTurnSpeed;
        spinRewardButton.interactable = false;
        _spinCloseButton.interactable = false;

    }

    public void WatchReward()
    {
        MMVibrationManager.Haptic(HapticTypes.Selection);

        if (MaxSdk.IsRewardedAdReady(rewardedAdUnitId))
        {
            MaxSdk.ShowRewardedAd(rewardedAdUnitId);
            giveSpin = true;
        }
        LoadRewardedAd();
    }



}
