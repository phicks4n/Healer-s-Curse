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
    public TextMeshProUGUI skill1;
    public TextMeshProUGUI skill2;
    public TextMeshProUGUI skill3;
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

    public void SetHP(float hp)
    {
        hpSlider.value = hp;
        hpCounter.text = hpSlider.value.ToString();
    }

    public void SetEP(int ep)
    {
        epSlider.value = ep;
        epCounter.text = epSlider.value.ToString();
    }

}
