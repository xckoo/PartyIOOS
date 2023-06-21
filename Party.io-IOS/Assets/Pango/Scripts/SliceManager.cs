using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceManager : MonoBehaviour
{
    public int coin;
    ParticleSystem coinParticle;
    public enum CoinValues
    {
        Coin30,
        Coin50,
        Coin60,
        Coin100,
        Coin125,
        Coin150,
        Coin200,
        Coin1000
    }
    public CoinValues coinValues;
    // Start is called before the first frame update
    void Start()
    {
        SliceValues();
    }

    void SliceValues()
    {
        switch (coinValues)
        {
            case CoinValues.Coin30:
                {
                    coinParticle = WheelManager.instance.wheelCoinParticle;
                    coin = 30;
                    break;
                }
            case CoinValues.Coin50:
                {
                    coinParticle = WheelManager.instance.wheelCoinParticle;

                    coin = 50;
                    break;
                }
            case CoinValues.Coin60:
                {
                    coinParticle = WheelManager.instance.wheelCoinParticle;

                    coin = 60;
                    break;
                }
            case CoinValues.Coin100:
                {
                    coinParticle = WheelManager.instance.wheelCoinParticle250;

                    coin = 100;
                    break;
                }
            case CoinValues.Coin125:
                {
                    coinParticle = WheelManager.instance.wheelCoinParticle250;

                    coin = 125;
                    break;
                }
            case CoinValues.Coin150:
                {
                    coinParticle = WheelManager.instance.wheelCoinParticle1000;

                    coin = 150;
                    break;
                }
            case CoinValues.Coin200:
                {
                    coinParticle = WheelManager.instance.wheelCoinParticle1000;


                    coin = 200;
                    break;
                }
            case CoinValues.Coin1000:
                {
                    coinParticle = WheelManager.instance.wheelCoinParticle1000;

                    coin = 1000;
                    break;
                }

        }
    }
    // Update is called once per frame
    public void GetCoin()
    {
        WheelManager.instance.coinLerp = true;
        WheelManager.instance.targetCoin = WheelManager.instance.currentCoin + coin;
        coinParticle.Play();
    }
}
