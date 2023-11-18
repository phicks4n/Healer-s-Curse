using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CritChance : MonoBehaviour
{
    [SerializeField] private float currentCrit;
    [SerializeField] private TMP_Text crit;

    private void Start()
    {
        currentCrit = currentCrit;
        this.crit.SetText(currentCrit.ToString() + "%");
    }

    public void AddCrit(int crit)
    {
        currentCrit += crit;
        this.crit.SetText(currentCrit.ToString() + "%");
    }
}
