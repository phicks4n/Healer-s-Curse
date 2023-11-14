using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI hpCounter;
    public Slider hpSlider;
    //public Slider manaSlider;

    public void SetHUD(Enemy unit)
    {
        nameText.text = unit.enemyName;
        levelText.text = "Lvl " + unit.enemyUnitLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
        hpCounter.text = hpSlider.value.ToString();
        

    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
        hpCounter.text = hpSlider.value.ToString();
    }
}
