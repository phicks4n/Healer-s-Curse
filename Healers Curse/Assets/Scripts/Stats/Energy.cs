using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Energy : MonoBehaviour
{
    [SerializeField] private float maxEnergy;
    [SerializeField] private float currentEnergy;
    [SerializeField] private TMP_Text energy;

    private void Start()
    {
        maxEnergy = maxEnergy;
        currentEnergy = currentEnergy;
        this.energy.SetText(currentEnergy.ToString() + "/" + maxEnergy);
    }

    public void Reduce(int cost)
    {
        currentEnergy -= cost;
    }

    public void AddEnergy(int energy)
    {
        if (currentEnergy + energy < maxEnergy)
        {
            currentEnergy += energy;
            this.energy.SetText(currentEnergy.ToString() + "/" + maxEnergy);
        }
        else
        {
            currentEnergy += maxEnergy;
            this.energy.SetText(currentEnergy.ToString() + "/" + maxEnergy);
        }
    }

}
