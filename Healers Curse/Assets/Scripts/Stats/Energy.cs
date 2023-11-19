using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Energy : MonoBehaviour
{
    [SerializeField] private float maxEnergy;
    [SerializeField] private float currentEnergy;
    [SerializeField] private float prevEnergy;
    [SerializeField] private TMP_Text energy;

    private void Start()
    {
        maxEnergy = maxEnergy;
        currentEnergy = currentEnergy;
        prevEnergy = prevEnergy;
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
        this.energy.SetText(currentEnergy.ToString() + "/" + maxEnergy);
    }
}
