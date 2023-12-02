using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem //: IDataPersistence
{

    public event EventHandler OnExpChange;
    public event EventHandler OnLevelChange;

    private static readonly int[] expPerLevel = new[] { 50, 100, 200, 350, 600 };
    private int level;
    private int exp;

    public LevelSystem() 
    {
        //GameData savedData = DataPersistenceManager.instance.GetGameData();
        level = 1;//savedData.lvl;
        exp = 0;//savedData.exp;
    }

    //Method that adds experience to the bar
    public void AddExperience (int amount)
    {
        if (!isMaxLevel())
        {
            exp += amount;
            //if we reach the experience needed
            while (!isMaxLevel() && exp >= GetExpToNextLevel(level))
            {
                exp -= GetExpToNextLevel(level);
                level++;
                if (OnLevelChange != null) OnLevelChange(this, EventArgs.Empty);
            }
            if (OnExpChange != null) OnExpChange(this, EventArgs.Empty);
        }
        
    }

    public int GetLevel()
    {
        return level;
    }

    public float GetExpNormalized()
    {
        if (isMaxLevel())
        {
            return 1f;
        }
        else
        {
            return (float)exp / GetExpToNextLevel(level);
        }
        
    }

    public int GetExpToNextLevel(int level)
    {
        return expPerLevel[level];
    }

    public bool isMaxLevel()
    {
        return isMaxLevel(level);
    }
    public bool isMaxLevel(int level)
    {
        return level == expPerLevel.Length;
    }

    public void SetExpBar(int level)
    {


    }


    /*public void SaveData(GameData data)
    {
        data.lvl = level;
        data.exp = exp;
    }

    public void LoadData(GameData data)
    {
        level = data.lvl;
        exp = data.exp;
    }*/

}
