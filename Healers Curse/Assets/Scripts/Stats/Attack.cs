using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Attack : MonoBehaviour
{
    [SerializeField] private float currentAttack;
    [SerializeField] private TMP_Text attack;

    private void Start()
    {
        currentAttack = currentAttack;
        this.attack.SetText(currentAttack.ToString());
    }

    public void AddAttack(int attack)
    {
        currentAttack += attack;
        this.attack.SetText(currentAttack.ToString());
    }
}
