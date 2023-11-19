using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DodgeChance : MonoBehaviour
{
    [SerializeField] private float currentDodge;
    [SerializeField] private float prevDodge;
    [SerializeField] private TMP_Text dodge;

    private void Start()
    {
        currentDodge = currentDodge;
        prevDodge = prevDodge;
        this.dodge.SetText(currentDodge.ToString() + "%");
    }

    public void AddDodge(int dodge)
    {
        Reduce(prevDodge);
        prevDodge = dodge;
        currentDodge += dodge;
        this.dodge.SetText(currentDodge.ToString() + "%");
    }

    public void Reduce(float prevDodge)
    {
        currentDodge -= prevDodge;
        this.dodge.SetText(currentDodge.ToString());
    }
}
