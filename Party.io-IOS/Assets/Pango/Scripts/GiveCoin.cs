using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MoreMountains.NiceVibrations;

public class GiveCoin : MonoBehaviour {
    bool isCostumeGained;
    public GameObject needCoin;
    public Text needCoinText;
    public bool isUnlocked;
	// Use this for initialization
	void Start () {
        isUnlocked = true;
	}
	
	// Update is called once per frame

    public void PayCoin()
    {
        if (isUnlocked)
        {

        if (PlayerPrefs.GetInt("unlocked") == 6)
        {
            if (PlayerPrefs.GetInt("Coin") > 200)
            {
                MMVibrationManager.Haptic(HapticTypes.Selection);


                isCostumeGained = true;

                if (PlayerPrefs.GetInt("unlocked") > 1)
                {
                    PlayerPrefs.SetInt("unlocked", PlayerPrefs.GetInt("unlocked") - 1);
                    PlayerPrefs.SetInt("temp_unlocked", 0);
                    OnVideoComplete();

                    PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - 200);
                }

            }
            else
            {
                needCoin.SetActive(true);
                needCoinText.text = "You Need "+" "+ (200 - PlayerPrefs.GetInt("Coin")).ToString()+" "+ "Coin To Unlock!" ;
            }
        }
        else if (PlayerPrefs.GetInt("unlocked") == 11)
        {
            if (PlayerPrefs.GetInt("Coin") > 250)
            {
                MMVibrationManager.Haptic(HapticTypes.Selection);


                isCostumeGained = true;

                if (PlayerPrefs.GetInt("unlocked") > 1)
                {
                    PlayerPrefs.SetInt("unlocked", PlayerPrefs.GetInt("unlocked") - 1);
                    PlayerPrefs.SetInt("temp_unlocked", 0);
                    OnVideoComplete();

                    PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - 250);
                }

            }
            else
            {
                needCoin.SetActive(true);
                needCoinText.text = "You Need " + " " + (250 - PlayerPrefs.GetInt("Coin")).ToString() + " " + "Coin To Unlock!";
            }
        }
        else if (PlayerPrefs.GetInt("unlocked") == 16)
        {
            if (PlayerPrefs.GetInt("Coin") > 300)
            {
                MMVibrationManager.Haptic(HapticTypes.Selection);


                isCostumeGained = true;

                if (PlayerPrefs.GetInt("unlocked") > 1)
                {
                    PlayerPrefs.SetInt("unlocked", PlayerPrefs.GetInt("unlocked") - 1);
                    PlayerPrefs.SetInt("temp_unlocked", 0);
                    OnVideoComplete();

                    PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - 300);
                }

            }
            else
            {
                needCoin.SetActive(true);
                needCoinText.text = "You Need " + " " + (300 - PlayerPrefs.GetInt("Coin")).ToString() + " " + "Coin To Unlock!";
            }
        }
        else if (PlayerPrefs.GetInt("unlocked") == 21)
        {
            if (PlayerPrefs.GetInt("Coin") > 350)
            {
                MMVibrationManager.Haptic(HapticTypes.Selection);


                isCostumeGained = true;

                if (PlayerPrefs.GetInt("unlocked") > 1)
                {
                    PlayerPrefs.SetInt("unlocked", PlayerPrefs.GetInt("unlocked") - 1);
                    PlayerPrefs.SetInt("temp_unlocked", 0);
                    OnVideoComplete();

                    PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - 350);
                }

            }
            else
            {
                needCoin.SetActive(true);
                needCoinText.text = "You Need " + " " + (350 - PlayerPrefs.GetInt("Coin")).ToString() + " " + "Coin To Unlock!";
            }
        }
        else if (PlayerPrefs.GetInt("unlocked") == 26)
        {
            if (PlayerPrefs.GetInt("Coin") > 400)
            {
                MMVibrationManager.Haptic(HapticTypes.Selection);


                isCostumeGained = true;

                if (PlayerPrefs.GetInt("unlocked") > 1)
                {
                    PlayerPrefs.SetInt("unlocked", PlayerPrefs.GetInt("unlocked") - 1);
                    PlayerPrefs.SetInt("temp_unlocked", 0);
                    OnVideoComplete();

                    PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - 400);
                }

            }
            else
            {
                needCoin.SetActive(true);
                needCoinText.text = "You Need " + " " + (400 - PlayerPrefs.GetInt("Coin")).ToString() + " " + "Coin To Unlock!";
            }
        }
        else if (PlayerPrefs.GetInt("unlocked") == 31)
        {
            if (PlayerPrefs.GetInt("Coin") > 450)
            {
                MMVibrationManager.Haptic(HapticTypes.Selection);


                isCostumeGained = true;

                if (PlayerPrefs.GetInt("unlocked") > 1)
                {
                    PlayerPrefs.SetInt("unlocked", PlayerPrefs.GetInt("unlocked") - 1);
                    PlayerPrefs.SetInt("temp_unlocked", 0);
                    OnVideoComplete();

                    PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - 450);
                }

            }
            else
            {
                needCoin.SetActive(true);
                needCoinText.text = "You Need " + " " + (450 - PlayerPrefs.GetInt("Coin")).ToString() + " " + "Coin To Unlock!";
            }
        }
        else if (PlayerPrefs.GetInt("unlocked") == 36)
        {
            if (PlayerPrefs.GetInt("Coin") > 500)
            {
                MMVibrationManager.Haptic(HapticTypes.Selection);


                isCostumeGained = true;

                if (PlayerPrefs.GetInt("unlocked") > 1)
                {
                    PlayerPrefs.SetInt("unlocked", PlayerPrefs.GetInt("unlocked") - 1);
                    PlayerPrefs.SetInt("temp_unlocked", 0);
                    OnVideoComplete();

                    PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - 500);
                }

            }
            else
            {
                needCoin.SetActive(true);
                needCoinText.text = "You Need " + " " + (500 - PlayerPrefs.GetInt("Coin")).ToString() + " " + "Coin To Unlock!";
            }
        }
            isUnlocked = false;
        }

    }
    public void OnVideoComplete()
    {
        if (isCostumeGained)
        {
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

            Debug.Log("yeter amk2");
            isCostumeGained = false;

        }
    }
}
