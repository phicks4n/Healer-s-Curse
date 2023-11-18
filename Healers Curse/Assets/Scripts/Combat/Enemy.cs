using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public int enemyUnitLevel;

    public int maxHP;
    public int currentHP;
    public int armor;

    public int damage;

    // parameters are character dmg, enemy armor, characterCurrentHealth, characterMaxHealth
    public bool TakeDamage(int dmg, int armor, int currentHealth, int maxHealth)
    {
        if (maxHealth - currentHealth >= .3 * maxHealth)
        {
            currentHP = currentHP - (dmg - (int)(.4 * armor));
        }
        else if (maxHealth - currentHealth < .3 * maxHealth)
        {
            currentHP = currentHP - (dmg - (int)(.1 * armor));
        }

        if (currentHP <= 0)
            return true;
        else
            return false;

    }
}
