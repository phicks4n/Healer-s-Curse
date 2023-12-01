using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mana : MonoBehaviour, IDataPersistence
{
    [SerializeField] private float maxMana;
    [SerializeField] private float currentMana;
    [SerializeField] private TMP_Text mana;

    private void Start()
    {
        maxMana = 10;
        currentMana = 10;
        this.mana.SetText(currentMana.ToString() + "/" + maxMana);
    }

    public void Reduce(int cost)
    {
        currentMana -= cost;
    }

    public void AddMana(int mana)
    {
        if (currentMana + mana < maxMana)
        {
            currentMana += mana;
            DataPersistenceManager.instance.SavePlayerStat(9, (int)currentMana);
            this.mana.SetText(currentMana.ToString() + "/" + maxMana);
        }
        else
        {
            currentMana = maxMana;
            DataPersistenceManager.instance.SavePlayerStat(9, (int)currentMana);
            this.mana.SetText(currentMana.ToString() + "/" + maxMana);
        }
    }

    public void SaveData(GameData data) 
    {
        data.mana = (int)currentMana;
    }
    public void LoadData(GameData data) 
    {
        currentMana = data.mana;
    }
}
