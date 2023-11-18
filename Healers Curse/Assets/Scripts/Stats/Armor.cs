using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Armor : MonoBehaviour
{
    [SerializeField] private float currentArmor;
    [SerializeField] private TMP_Text armor;

    private void Start()
    {
        currentArmor = currentArmor;
        this.armor.SetText(currentArmor.ToString());
    }

    public void AddArmor(int armor)
    {
        currentArmor += armor;
        this.armor.SetText(currentArmor.ToString());
    }
}
