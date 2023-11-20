using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CritChance : MonoBehaviour, IDataPersistence
{
    [SerializeField] private float currentCrit;
    [SerializeField] private float prevCrit;
    [SerializeField] private TMP_Text crit;

    private void Start()
    {
        currentCrit = currentCrit;
        prevCrit = prevCrit;
        this.crit.SetText(currentCrit.ToString() + "%");
    }

    public void AddCrit(int crit)
    {
        Reduce(prevCrit);
        prevCrit = crit;
        currentCrit += crit;
        this.crit.SetText(currentCrit.ToString() + "%");
    }

    public void Reduce(float prevCrit)
    {
        currentCrit -= prevCrit;
        this.crit.SetText(currentCrit.ToString());
    }

    public void SaveData(GameData data) 
    {
        data.crit = (int)currentCrit;
    }
    public void LoadData(GameData data) 
    {
        currentCrit = data.crit;
    }
}
