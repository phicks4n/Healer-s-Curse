using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Energy : MonoBehaviour, IDataPersistence
{
    [SerializeField] private float maxEnergy;
    [SerializeField] private float currentEnergy;
    [SerializeField] private float prevEnergy;
    [SerializeField] private TMP_Text energy;

    private void Start()
    {
        maxEnergy = 50;
        currentEnergy = 50;
        prevEnergy = 0;
        this.energy.SetText(currentEnergy.ToString() + "/" + maxEnergy);
    }

    public void Reduce(float cost)
    {
        currentEnergy -= prevEnergy;
        maxEnergy -= prevEnergy;
        this.energy.SetText(currentEnergy.ToString() + "/" + maxEnergy);
    }

    public void AddEnergy(int energy)
    {
        Reduce(prevEnergy);
        prevEnergy = energy;
        currentEnergy += energy;
        maxEnergy += energy;
        DataPersistenceManager.instance.SavePlayerStat(5, (int)currentEnergy);
        this.energy.SetText(currentEnergy.ToString() + "/" + maxEnergy);
    }

    public void SaveData(GameData data) 
    {
        data.energy = (int)currentEnergy;
    }
    public void LoadData(GameData data) 
    {
        currentEnergy = data.energy;
    }
}
