using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI hpCounter;
    public TextMeshProUGUI epCounter;
    public Slider hpSlider;
    public Slider epSlider;
    //public Slider manaSlider;

    public void SetHUD(Character unit) 
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl " + unit.unitLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
        epSlider.maxValue = unit.maxEP;
        epSlider.value = unit.currentEP;
        hpCounter.text = hpSlider.value.ToString();
        epCounter.text = epSlider.value.ToString();

    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
        hpCounter.text = hpSlider.value.ToString();
    }

    public void SetEP(int ep)
    {
        epSlider.value = ep;
        epCounter.text = epSlider.value.ToString();
    }


    /*public void SetHUDCharacter(Character character)
    {
        characterName.text = character.unitName;
        levelText.text = "Lvl " + character.unitLevel;
        hpSlider.maxValue = character.maxHP;
        hpSlider.value = character.currentHP;
        manaSlider.maxValue = character.maxMana;
        manaSlider.value = character.currentMana;
    }

    public void SetHUDEnemy(Enemy enemy)
    {
        enemyName.text = enemy.enemyName;
        enemyLevelText.text = "Lvl " + enemy.enemyUnitLevel;
        enemyHPSlider.maxValue = enemy.maxHP;
        enemyHPSlider.value = enemy.currentHP;
    }

    public void SetHPCharacter (int characterHP)
    {
        hpSlider.value = characterHP;
    }

    public void SetHPEnemy(int enemyHP)
    {
        enemyHPSlider.value = enemyHP;
    }*/

}
