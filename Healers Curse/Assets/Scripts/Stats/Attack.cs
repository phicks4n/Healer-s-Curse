using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Attack : MonoBehaviour, IDataPersistence
{
    [SerializeField] private float currentAttack;
    [SerializeField] private float prevAttack;
    [SerializeField] private TMP_Text attack;

    private void Start()
    {
        //currentAttack = currentAttack;
        //prevAttack = prevAttack;
        currentAttack = 15;
        prevAttack = 0;
        this.attack.SetText(currentAttack.ToString());
    }
    
    public void AddAttack(int attack)
    {
        Debug.Log(attack);
        Debug.Log(currentAttack);
        Reduce(prevAttack);
        prevAttack = attack;
        currentAttack += attack;
        this.attack.SetText(currentAttack.ToString());
    }

    public void Reduce(float prevAttack)
    {
        currentAttack -= prevAttack;
        this.attack.SetText(currentAttack.ToString());
    }

    public void SaveData(GameData data) 
    {
        data.attack = (int)currentAttack;
    }
    public void LoadData(GameData data) 
    {
        currentAttack = data.attack;
    }
}
