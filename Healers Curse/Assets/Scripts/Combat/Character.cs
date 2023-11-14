using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int maxHP;
    public int currentHP;
    public int maxEP;
    public int currentEP;
    public int damage;
    public int costOfAttack;
    public int defense;
    public int currentMana;
    public int maxMana;

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;

    }

    public void Heal(int amount)
    {
        currentHP += amount;

        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    public int Attack(int amount)
    {
        currentEP -= amount;

        return currentEP;
    }

}