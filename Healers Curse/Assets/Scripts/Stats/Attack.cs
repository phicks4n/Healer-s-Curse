using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Attack : MonoBehaviour
{
    [SerializeField] private float currentAttack;
    [SerializeField] private float prevAttack;
    [SerializeField] private TMP_Text attack;

    private void Start()
    {
        currentAttack = currentAttack;
        prevAttack = prevAttack;
        this.attack.SetText(currentAttack.ToString());
    }

    public void AddAttack(int attack)
    {
        Reduce(prevAttack);
        prevAttack = attack;
        currentAttack += attack;
        this.attack.SetText(currentAttack.ToString());
    }

    public void Reduce(float prevAttack)
    {
        currentAttack -= prevAttack;
        this.attack.SetText(currentAttack.ToString());
    }
}
