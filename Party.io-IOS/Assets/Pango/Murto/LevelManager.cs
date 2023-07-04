using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager I;

    public LevelController[] levels;

    public LevelController currentlevel;

    
    public LevelController score;

    public int levelIndex;

    public Camera mainCamera;

    public Kamera_A camScript;

    public bool isPlacementPhase;

    public GameObject unlockedSkin;

    public GameObject gameEndPanel;
        
    public Transform placementTransform;   
    
    public GameObject placementObject;

    private void Awake()
    {
        I = this;
        camScript = mainCamera.GetComponent<Kamera_A>();
    }


    public void SetLevel()
    {
        camScript.SetThis();
        isPlacementPhase = false;

        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].gameObject.SetActive(false);
        }

        currentlevel = levels[levelIndex];
        currentlevel.gameObject.SetActive(true);
        GameManager_A.I.StartGame();
        mainCamera.GetComponent<Rigidbody>().isKinematic = false;

        
    }

    public void NextLevel()
    {
        if (PlayerPrefs.GetInt("unlocked")<37)
        {
            if (PlayerPrefs.GetInt("Level") < 14)
            {
                levelIndex = PlayerPrefs.GetInt("Level");
                levelIndex++;
                SetLevel();
            }
           
            else
            {
                int rnd2 = UnityEngine.Random.Range(0, 14);
                PlayerPrefs.SetInt("Level", rnd2);
                levelIndex = PlayerPrefs.GetInt("Level");
                SetLevel();
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("Level") < 14)
            {
                levelIndex = PlayerPrefs.GetInt("Level");
                levelIndex++;
                SetLevel();
            }
           
            else
            {
                int rnd2 = UnityEngine.Random.Range(0, 14);
                PlayerPrefs.SetInt("Level", rnd2);
                levelIndex = PlayerPrefs.GetInt("Level");
                SetLevel();
            }
        }
    }

    public void OpenPlacement()
    {
        gameEndPanel.SetActive(false);
        isPlacementPhase = true;
        currentlevel.gameObject.SetActive(false);
        placementObject.SetActive(true);
        mainCamera.GetComponent<Rigidbody>().isKinematic = true;
        mainCamera.transform.DOMove(placementTransform.position, 0.25f).SetEase(Ease.InFlash);


    }


    public void OpenUnlockedSkin()
    {
        unlockedSkin.SetActive(true);
        placementObject.SetActive(false);
        currentlevel.gameObject.SetActive(false);
        isPlacementPhase = false;


    }
}