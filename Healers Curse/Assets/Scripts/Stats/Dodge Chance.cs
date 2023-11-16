using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DodgeChance : MonoBehaviour
{
    [SerializeField] private float currentDodge;
    [SerializeField] private TMP_Text dodge;

    private void Start()
    {
        currentDodge = currentDodge;
        this.dodge.SetText(currentDodge.ToString() + "%");
    }

    public void AddDodge(int dodge)
    {
        currentDodge += dodge;
        this.dodge.SetText(currentDodge.ToString() + "%");
    }
}
