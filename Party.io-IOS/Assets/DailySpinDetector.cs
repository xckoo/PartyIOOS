using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailySpinDetector : MonoBehaviour
{
    //private ulong _lastDailySpin;
    // 1 day 86400000.0f
   // const float msToWait = 86400000.0f;


  //  [SerializeField] private GameObject spinNotification;
    [SerializeField] public GameObject popUp;
    public static DailySpinDetector instance;
   
    private void Start()
    {
        instance = this;
        // ulong diff = ((ulong)DateTime.Now.Ticks - _lastDailySpin);
        // ulong m = diff / TimeSpan.TicksPerMillisecond;
        //
        // float secondsLeft = (float)(msToWait - m) / 1000.0f;
        //
        // if (secondsLeft<0&& PlayerPrefs.GetInt("DailySpinPopUp") == 1)
        // {
        //     popUp.SetActive(true);
        //     PlayerPrefs.SetInt("DailySpinPopUp", 0);
        // }


    }
    // Update is called once per frame
    void Update()
    {
        // if (IsSpinReady())
        // {
        //     spinNotification.SetActive(true);
        //     return;
        //
        // }

    }
    // private bool IsSpinReady()
    // {
    //     ulong diff = ((ulong)DateTime.Now.Ticks - _lastDailySpin);
    //     ulong m = diff / TimeSpan.TicksPerMillisecond;
    //
    //     float secondsLeft = (float)(msToWait - m) / 1000.0f;
    //
    //     if (secondsLeft < 0)
    //     {
    //         return true;
    //
    //     }
    //     spinNotification.SetActive(false);
    //
    //     return false;
    // }

}
