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
        maxMana = maxMana;
        currentMana = currentMana;
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
            this.mana.SetText(currentMana.ToString() + "/" + maxMana);
        }
        else
        {
            currentMana = maxMana;
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
