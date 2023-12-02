using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public float maxHP;
    public float currentHP;
    public int maxEP;
    public int currentEP;
    public float damage;
    public int costOfAttack;
    public int costOfBash;
    public int costOfSpook;
    public int costOfBlock;
    public int armor;
    public int currentMana;
    public int maxMana;
    public float damageTaken;

    // parameters are enemy dmg, character armor, enemyCurrentHealth, enemyMaxHealth
    public bool TakeDamage(float dmg, int armor, float currentHealth, float maxHealth, bool Block)
    {
        if (Block == true)
        {
            currentHP -= dmg;
            damageTaken = dmg;
        }
        else
        {
            if (((currentHealth / maxHealth) * 100) >= .5 * maxHealth)
            {
                if (dmg < (int) (.65 * armor)) 
                {
                    currentHP = currentHP - 1;
                    damageTaken = 1;
                }
                else
                {
                    currentHP = currentHP - (dmg - (int)(0.65 * armor));
                    damageTaken = (dmg - (int)(0.65 * armor));
                }
            }
            else if ((((currentHealth / maxHealth) * 100) < .5 * maxHealth) && (((currentHealth / maxHealth) * 100) >= .25 * maxHealth))
            {
                if (dmg < (int) (.45 * armor))
                {   
                    currentHP = currentHP - 1;
                    damageTaken = 1;
                }
                else
                {
                    currentHP = currentHP - (dmg - (int)(0.45 * armor));
                    damageTaken = (dmg - (int)(0.45 * armor));
                }
            }
            else if ((((currentHealth / maxHealth) * 100) < .25 * maxHealth) && (((currentHealth / maxHealth) * 100) >= .1 * maxHealth))
            {
                if (dmg < (int) (.25 * armor))
                {
                    currentHP = currentHP - 1;
                    damageTaken = 1;
                }
                else
                {
                    currentHP = currentHP - (dmg - (int)(0.25 * armor));
                    damageTaken = (dmg - (int)(0.25 * armor));
                }
            }
            else if (((currentHealth / maxHealth) * 100) < .1 * maxHealth)
            {
                currentHP = (currentHP - (int) (0.15 * armor));
                damageTaken = (int) (0.15 * armor);
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
