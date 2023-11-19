using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CritChance : MonoBehaviour
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
}
