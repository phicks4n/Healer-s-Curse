using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Armor : MonoBehaviour, IDataPersistence
{
    [SerializeField] private float currentArmor;
    [SerializeField] private float prevArmor;
    [SerializeField] private TMP_Text armor;

    private void Start()
    {
        currentArmor = 20;
        prevArmor = 0;
        this.armor.SetText(currentArmor.ToString());
    }

    public void AddArmor(int armor)
    {
        Reduce(prevArmor);
        prevArmor = armor;
        currentArmor += armor;
        this.armor.SetText(currentArmor.ToString());
    }

    public void Reduce(float prevArmor)
    {
        currentArmor -= prevArmor;
        this.armor.SetText(currentArmor.ToString());
    }

    public void SaveData(GameData data) 
    {
        data.armor = (int)currentArmor;
    }
    public void LoadData(GameData data) 
    {
        currentArmor = data.armor;
    }
}
