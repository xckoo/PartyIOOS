using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager I;

    public LevelController[] levels;

    public LevelController currentlevel;

    public LevelController score;

    public int levelIndex;

    private void Awake()
    {
        I = this;
        SetLevel();
    }




    public void SetLevel()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].gameObject.SetActive(false);
        }

        currentlevel = levels[levelIndex];
        currentlevel.gameObject.SetActive(true);
    }

    public void NextLevel()
    {
    }
    
    public void Reset()
    {
       
    }
}