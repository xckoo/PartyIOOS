using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.NiceVibrations;
using GameAnalyticsSDK;


public class CostumeManager : MonoBehaviour
{
    public GameObject costumePanel;

    public enum Costumes
    {
        Leg1,
        Leg2,
        Leg3,
        Leg4,
        Epic1,
        Epic2,
        Epic3
    };

    public Costumes costumes;
    public GameObject needCoin;
    public Text needCointext;
    public GameObject Epic1;
    public GameObject Epic2;
    public GameObject Epic3;
    public GameObject Leg1;
    public GameObject Leg2;
    public GameObject Leg3;
    public GameObject Leg4;
    public GameObject level7Panel;
    public GameObject level12Panel;
    public GameObject level17Panel;
    public GameObject level22Panel;
    public GameObject level27Panel;
    public GameObject level32Panel;

    int costumeCoin;
    public Text costumeCoinText;
    public Text costumeCoinText1;
    public Text costumeCoinText2;
    public Text costumeCoinText3;
    public Text costumeCoinText4;
    public Text costumeCoinText5;
    public Text costumeCoinText6;


    public GameManager_A gm;
    public Text currentCostumeCoinText;


    public float targetCoin;
    float currentCoin;
    public bool getCostumeCoin;

    // Use this for initialization
    void Start()
    {
        currentCoin = PlayerPrefs.GetInt("Coin");
        targetCoin = currentCoin;
        CostumeCoinScaler();
    }

    // Update is called once per frame
    void Update()
    {
        if (getCostumeCoin)
        {
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 75);
            targetCoin = PlayerPrefs.GetInt("Coin") + 1;
            //            gold.Play();
            if (needCoin.activeSelf == true)
            {
                CloseNeedCoin();
            }

            currentCostumeCoinText.text = PlayerPrefs.GetInt("Coin").ToString();
            getCostumeCoin = false;
        }

        if (targetCoin - currentCoin < 1)
        {
            targetCoin = currentCoin;
        }
        else
        {
            currentCoin = Mathf.Lerp(currentCoin, targetCoin, 0.1f);
            currentCostumeCoinText.text = Mathf.FloorToInt(currentCoin).ToString();
        }
    }

    public void OpenCostumePanel()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "CostumePanelOpened");

        MMVibrationManager.Haptic(HapticTypes.Selection);

        currentCostumeCoinText.text = PlayerPrefs.GetInt("Coin").ToString();

        CostumeCoinScaler();
        costumePanel.SetActive(true);
        gm.baslangicPanel.SetActive(false);
        gm.baslangicWaitingPanel.SetActive(false);
        CheckCostumeLevel();
    }

    public void CloseCostumePanel()
    {
        MMVibrationManager.Haptic(HapticTypes.Selection);

        costumePanel.SetActive(false);
        gm.baslangicPanel.SetActive(true);

        PlayerPrefs.SetInt("kiyafet_deg", PlayerPrefs.GetInt("unlocked") - 1);

        gm.baslangic_adam();
        CostumeCoinScaler();
    }


    public void LegendaryCostume()
    {
        //PlayerPrefs.SetInt("unlocked", 11);
        switch (costumes)
        {
            case Costumes.Epic1:


                if (PlayerPrefs.GetInt("Coin") >= costumeCoin)
                {
                    PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - costumeCoin);
                    PlayerPrefs.SetInt("unlocked", 6);
                    CloseCostumePanel();
                    CostumeCoinScaler();
                    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Epic1");
                }
                else
                {
                    needCoin.SetActive(true);
                    needCointext.text = "You Need" + " " + (costumeCoin - PlayerPrefs.GetInt("Coin")) + " " +
                                        "Coin To Unlock Package!";
                }

                break;


            case Costumes.Epic2:
                if (PlayerPrefs.GetInt("Coin") >= costumeCoin)
                {
                    PlayerPrefs.SetInt("Coin", (PlayerPrefs.GetInt("Coin") - costumeCoin));
                    PlayerPrefs.SetInt("unlocked", 11);
                    CloseCostumePanel();
                    CostumeCoinScaler();
                    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Epic2");
                }
                else
                {
                    needCoin.SetActive(true);
                    needCointext.text = "You Need" + " " + (costumeCoin - PlayerPrefs.GetInt("Coin")) + " " +
                                        "Coin To Unlock Package!";
                }

                break;


            case Costumes.Epic3:
                if (PlayerPrefs.GetInt("Coin") >= costumeCoin)
                {
                    PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - costumeCoin);
                    PlayerPrefs.SetInt("unlocked", 16);
                    CloseCostumePanel();
                    CostumeCoinScaler();
                    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Epic3");
                }
                else
                {
                    needCoin.SetActive(true);
                    needCointext.text = "You Need" + " " + (costumeCoin - PlayerPrefs.GetInt("Coin")) + " " +
                                        "Coin To Unlock Package!";
                }

                break;


            case Costumes.Leg1:
                if (PlayerPrefs.GetInt("Coin") >= costumeCoin)
                {
                    PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - costumeCoin);
                    PlayerPrefs.SetInt("unlocked", 21);
                    CloseCostumePanel();
                    CostumeCoinScaler();
                    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Leg1");
                }
                else
                {
                    needCoin.SetActive(true);
                    needCointext.text = "You Need" + " " + (costumeCoin - PlayerPrefs.GetInt("Coin")) + " " +
                                        "Coin To Unlock Package!";
                }

                break;


            case Costumes.Leg2:
                if (PlayerPrefs.GetInt("Coin") >= costumeCoin)
                {
                    PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - costumeCoin);
                    PlayerPrefs.SetInt("unlocked", 26);
                    CloseCostumePanel();
                    CostumeCoinScaler();
                    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Leg2");
                }
                else
                {
                    needCoin.SetActive(true);
                    needCointext.text = "You Need" + " " + (costumeCoin - PlayerPrefs.GetInt("Coin")) + " " +
                                        "Coin To Unlock Package!";
                }

                break;


            case Costumes.Leg3:
                if (PlayerPrefs.GetInt("Coin") >= costumeCoin)
                {
                    PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - costumeCoin);
                    PlayerPrefs.SetInt("unlocked", 31);
                    CloseCostumePanel();
                    CostumeCoinScaler();
                    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Leg3");
                }
                else
                {
                    needCoin.SetActive(true);
                    needCointext.text = "You Need" + " " + (costumeCoin - PlayerPrefs.GetInt("Coin")) + " " +
                                        "Coin To Unlock Package!";
                }

                break;

            case Costumes.Leg4:
                if (PlayerPrefs.GetInt("Coin") >= costumeCoin)
                {
                    PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - costumeCoin);
                    PlayerPrefs.SetInt("unlocked", 36);
                    Leg4.SetActive(false);
                    CloseCostumePanel();
                    CostumeCoinScaler();
                    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Leg4");
                }
                else
                {
                    needCoin.SetActive(true);
                    needCointext.text = "You Need" + " " + (costumeCoin - PlayerPrefs.GetInt("Coin")) + " " +
                                        "Coin To Unlock Package!";
                }

                break;


            default:
                break;
        }
    }

    public void CloseNeedCoin()
    {
        needCoin.SetActive(false);
        currentCostumeCoinText.text = PlayerPrefs.GetInt("Coin").ToString();
    }

    public void CheckCostumeLevel()
    {
        if (PlayerPrefs.GetInt("unlocked") < 6)
        {
            Epic1.SetActive(true);
            level7Panel.SetActive(true);
            level12Panel.SetActive(true);
            level17Panel.SetActive(true);
            level22Panel.SetActive(true);
            level27Panel.SetActive(true);
            level32Panel.SetActive(true);
            Epic2.SetActive(false);
            Epic3.SetActive(false);
            Leg1.SetActive(false);
            Leg2.SetActive(false);
            Leg3.SetActive(false);
            Leg4.SetActive(false);
        }

        if ((PlayerPrefs.GetInt("unlocked") >= 6 && PlayerPrefs.GetInt("unlocked") < 11))
        {
            Epic1.SetActive(false);
            level7Panel.SetActive(false);
            Epic2.SetActive(true);
            Epic3.SetActive(false);
            Leg1.SetActive(false);
            Leg2.SetActive(false);
            Leg3.SetActive(false);
            Leg4.SetActive(false);
        }
        else if ((PlayerPrefs.GetInt("unlocked") >= 11 && PlayerPrefs.GetInt("unlocked") < 16))
        {
            Epic1.SetActive(false);
            level7Panel.SetActive(false);
            level12Panel.SetActive(false);
            Epic2.SetActive(false);
            Epic3.SetActive(true);
            Leg1.SetActive(false);
            Leg2.SetActive(false);
            Leg3.SetActive(false);
            Leg4.SetActive(false);
        }

        else if ((PlayerPrefs.GetInt("unlocked") >= 16 && PlayerPrefs.GetInt("unlocked") < 21))
        {
            Epic1.SetActive(false);
            level7Panel.SetActive(false);
            level12Panel.SetActive(false);
            level17Panel.SetActive(false);
            Epic2.SetActive(false);
            Epic3.SetActive(false);
            Leg1.SetActive(true);
            Leg2.SetActive(false);
            Leg3.SetActive(false);
            Leg4.SetActive(false);
        }

        else if ((PlayerPrefs.GetInt("unlocked") >= 21 && PlayerPrefs.GetInt("unlocked") < 26))
        {
            level7Panel.SetActive(false);
            level12Panel.SetActive(false);
            level17Panel.SetActive(false);
            level22Panel.SetActive(false);

            Epic1.SetActive(false);
            Epic2.SetActive(false);
            Epic3.SetActive(false);
            Leg1.SetActive(false);
            Leg2.SetActive(true);
            Leg3.SetActive(false);
            Leg4.SetActive(false);
        }
        else if ((PlayerPrefs.GetInt("unlocked") >= 26 && PlayerPrefs.GetInt("unlocked") < 31))
        {
            level7Panel.SetActive(false);
            level12Panel.SetActive(false);
            level17Panel.SetActive(false);
            level22Panel.SetActive(false);
            level27Panel.SetActive(false);

            Epic1.SetActive(false);
            Epic2.SetActive(false);
            Epic3.SetActive(false);
            Leg1.SetActive(false);
            Leg2.SetActive(false);
            Leg3.SetActive(true);
            Leg4.SetActive(false);
        }
        else if ((PlayerPrefs.GetInt("unlocked") >= 31 && PlayerPrefs.GetInt("unlocked") < 36))
        {
            level7Panel.SetActive(false);
            level12Panel.SetActive(false);
            level17Panel.SetActive(false);
            level22Panel.SetActive(false);
            level27Panel.SetActive(false);
            level32Panel.SetActive(false);

            Epic1.SetActive(false);
            Epic3.SetActive(false);
            Epic3.SetActive(false);
            Leg1.SetActive(false);
            Leg2.SetActive(false);
            Leg3.SetActive(false);
            Leg4.SetActive(true);
        }
    }

    public void CostumeCoinScaler()
    {
        switch (PlayerPrefs.GetInt("unlocked"))
        {
            case 1:
            case 2:
                costumeCoinText.text = 300.ToString();
                costumeCoin = 300;
                break;
            case 3:
                costumeCoinText.text = 275.ToString();
                costumeCoin = 275;

                break;
            case 4:
                costumeCoinText.text = 250.ToString();
                costumeCoin = 250;

                break;
            case 5:
                costumeCoinText.text = 225.ToString();
                costumeCoin = 225;

                break;
            case 6:
                costumeCoinText.text = 350.ToString();
                costumeCoin = 350;

                break;


            case 7:
                costumeCoinText1.text = 350.ToString();
                costumeCoin = 350;
                break;
            case 8:

                costumeCoinText1.text = 325.ToString();
                costumeCoin = 325;

                break;
            case 9:
                costumeCoinText1.text = 300.ToString();
                costumeCoin = 300;

                break;
            case 10:
                costumeCoinText1.text = 275.ToString();
                costumeCoin = 275;

                break;
            case 11:
                costumeCoinText1.text = 400.ToString();
                costumeCoin = 400;

                break;


            case 12:
                costumeCoinText2.text = 400.ToString();
                costumeCoin = 400;
                break;
            case 13:
                costumeCoinText2.text = 375.ToString();
                costumeCoin = 375;

                break;
            case 14:
                costumeCoinText2.text = 350.ToString();
                costumeCoin = 350;

                break;
            case 15:
                costumeCoinText2.text = 325.ToString();
                costumeCoin = 325;

                break;
            case 16:
                costumeCoinText2.text = 500.ToString();
                costumeCoin = 500;

                break;


            case 17:
                costumeCoinText3.text = 500.ToString();
                costumeCoin = 500;
                break;
            case 18:
                costumeCoinText3.text = 475.ToString();
                costumeCoin = 475;


                break;
            case 19:
                costumeCoinText3.text = 450.ToString();
                costumeCoin = 450;

                break;
            case 20:
                costumeCoinText3.text = 425.ToString();
                costumeCoin = 425;


                break;
            case 21:
                costumeCoinText3.text = 550.ToString();
                costumeCoin = 550;

                break;


            case 22:
                costumeCoinText4.text = 550.ToString();
                costumeCoin = 550;

                break;
            case 23:
                costumeCoinText4.text = 500.ToString();
                costumeCoin = 500;

                break;
            case 24:
                costumeCoinText4.text = 475.ToString();
                costumeCoin = 475;

                break;
            case 25:
                costumeCoinText4.text = 450.ToString();
                costumeCoin = 450;

                break;
            case 26:
                costumeCoinText4.text = 600.ToString();
                costumeCoin = 600;

                break;


            case 27:
                costumeCoinText5.text = 600.ToString();
                costumeCoin = 600;

                break;
            case 28:
                costumeCoinText5.text = 575.ToString();
                costumeCoin = 575;

                break;
            case 29:
                costumeCoinText5.text = 550.ToString();
                costumeCoin = 550;

                break;
            case 30:
                costumeCoinText5.text = 500.ToString();
                costumeCoin = 500;

                break;
            case 31:
                costumeCoinText5.text = 650.ToString();
                costumeCoin = 650;

                break;


            case 32:
                costumeCoinText6.text = 650.ToString();
                costumeCoin = 650;

                break;
            case 33:
                costumeCoinText6.text = 625.ToString();
                costumeCoin = 625;

                break;
            case 34:
                costumeCoinText6.text = 575.ToString();
                costumeCoin = 575;

                break;
            case 35:
                costumeCoinText6.text = 525.ToString();
                costumeCoin = 525;

                break;
            case 36:
                costumeCoinText6.text = 525.ToString();
                costumeCoin = 525;

                break;
            default:
                break;
        }
    }

    public void BuyCostume()
    {
    }
}