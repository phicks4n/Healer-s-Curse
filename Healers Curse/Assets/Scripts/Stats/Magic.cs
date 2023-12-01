using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Magic : MonoBehaviour, IDataPersistence
{
    [SerializeField] private float currentMagic;
    [SerializeField] private float prevMagic;
    [SerializeField] private TMP_Text magic;

    private void Start()
    {
        currentMagic = 0;
        prevMagic = 0;
        this.magic.SetText(currentMagic.ToString());
    }

    public void AddMagic(int magic)
    {
        Reduce(prevMagic);
        prevMagic = magic;
        currentMagic += magic;
        this.magic.SetText(currentMagic.ToString());
    }

    public void Reduce(float prevmagic)
    {
        currentMagic -= prevMagic;
        this.magic.SetText(currentMagic.ToString());
    }

    public void SaveData(GameData data) 
    {
        data.magic = (int)currentMagic;
    }
    public void LoadData(GameData data) 
    {
        currentMagic = data.magic;
    }
}
