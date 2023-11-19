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
    public int armor;
    public int currentMana;
    public int maxMana;

    // parameters are enemy dmg, character armor, enemyCurrentHealth, enemyMaxHealth
    public bool TakeDamage(int dmg, int armor, int currentHealth, int maxHealth, bool Block)
    {
        if (Block == true)
        {
            currentHP -= dmg;
        }
        else
        {
            if ((int) (currentHealth / maxHealth) >= .5 * maxHealth)
            {
                currentHP = currentHP - (dmg - (int)(0.35 * armor));
            }
            else if (((int) (currentHealth / maxHealth) < .5 * maxHealth) && ((int) (currentHealth / maxHealth) >= .25 * maxHealth))
            {
                currentHP = currentHP - (dmg - (int)(0.25 * armor));

            }
            else if (((int) (currentHealth / maxHealth) < .25 * maxHealth) && ((int) (currentHealth / maxHealth) >= .1 * maxHealth))
            {
                currentHP = currentHP - (dmg - (int)(0.15 * armor));
            }
            else if ((int) (currentHealth / maxHealth) < .1 * maxHealth)
            {
                currentHP = currentHP - (dmg * 1.1125);
            }
        }

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

    public void Recover(int amount)
    {
        currentEP += amount;

        if(currentEP > maxEP)
            currentEP = maxEP;
    }

    public void Energy(int amount)
    {
        currentEP -= amount;

        if (currentEP < 0)
            currentEP = 0;
    }

}
