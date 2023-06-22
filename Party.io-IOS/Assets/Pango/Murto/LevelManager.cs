using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager I;

    public LevelController[] levels;

    public LevelController currentlevel;

    public int levelIndex;
    private void Awake()
    {
        I = this;
    }


    public void SetLevel()
    {

        for (int i = 0; i < levels.Length; i++)
        {
            currentlevel = levels[levelIndex];
        }
    }

    public void NextLevel()
    {
        
        
    }
    
    
}
