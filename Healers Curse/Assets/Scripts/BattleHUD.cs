using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text characterName;
    public Text characterMana;
    public Text levelText;
    public Slider hpSlider;
    public Slider manaSlider;

    public Text enemyName;
    public Text enemyLevelText;
    public Slider enemyHPSlider;


    public void SetHUDCharacter(Character character)
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
    }

}
