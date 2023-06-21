using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GameAnalyticsSDK;


public class WheelTimer : MonoBehaviour
{

    public static WheelTimer instance;
    private ulong lastDailySpin;

    // 1 day 86400000.0f
    float msToWait = 86400000.0f;

    private Text spinTimer;

   public Button dailySpinButton;
    Button spinAgain;
    private Text _freeSpinText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        lastDailySpin = ulong.Parse(PlayerPrefs.GetString("LastDailySpin", "0"));

        dailySpinButton = gameObject.transform.GetChild(1).GetComponent<Button>();
        spinTimer=GameObject.FindGameObjectWithTag("SpinTimer").GetComponent<Text>();
        spinAgain = gameObject.transform.GetChild(6).GetComponent<Button>();
        _freeSpinText = dailySpinButton.gameObject.transform.GetChild(0).GetComponent<Text>();
        
        if (!IsSpinReady())
        {
            dailySpinButton.interactable = false;
            _freeSpinText.color = new Color32(255, 255, 255, 50);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            PlayerPrefs.SetString("LastDailySpin", "0");
            lastDailySpin = ulong.Parse(PlayerPrefs.GetString("LastDailySpin", "0"));
        }
      
        if (dailySpinButton.interactable == false)
        {
            if (IsSpinReady())
            {
                dailySpinButton.interactable = true;
                _freeSpinText.color = new Color32(255, 255, 255, 100);
                return;
            }

        }

        // Set the timer
        ulong diff = ((ulong)DateTime.Now.Ticks - lastDailySpin);
        ulong m = diff / TimeSpan.TicksPerMillisecond;

        float secondsLeft = (float)(msToWait - m) / 1000.0f;

        string r = "";
        //Hours

        r += ((int)secondsLeft / 3600).ToString() + "h ";
        secondsLeft -= ((int)secondsLeft / 3600) * 3600;

        //Minutes

        r += ((int)secondsLeft / 60).ToString("00") + "m ";

        //Second

        r += ((int)secondsLeft % 60).ToString("00") + "s ";

        spinTimer.text = r;
    }


    public void DailySpin()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail,"FreeWheelSpinPressed");

        lastDailySpin = (ulong)DateTime.Now.Ticks;
        PlayerPrefs.SetString("LastDailySpin", DateTime.Now.Ticks.ToString());


        dailySpinButton.interactable = false;
        spinTimer.gameObject.SetActive(true);
        _freeSpinText.color = new Color32(255, 255, 255, 50);
    }

    private bool IsSpinReady()
    {
        ulong diff = ((ulong)DateTime.Now.Ticks - lastDailySpin);
        ulong m = diff / TimeSpan.TicksPerMillisecond;

        float secondsLeft = (float)(msToWait - m) / 1000.0f;
        
        if (secondsLeft < 0)
        {
            spinTimer.gameObject.SetActive(false);
            spinAgain.gameObject.SetActive(false);
            return true;

        }

        return false;
    }
}
