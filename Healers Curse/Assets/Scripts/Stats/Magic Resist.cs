using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MagicResist : MonoBehaviour, IDataPersistence
{
    [SerializeField] private float currentResist;
    [SerializeField] private float prevResist;
    [SerializeField] private TMP_Text resist;

    private void Start()
    {
        currentResist = 0;
        prevResist = 0;
        this.resist.SetText(currentResist.ToString());
    }

    public void AddResist(int resist)
    {
        Reduce(prevResist);
        prevResist = resist;
        currentResist += resist;
        this.resist.SetText(currentResist.ToString());
    }

    public void Reduce(float prevResist)
    {
        currentResist -= prevResist;
        this.resist.SetText(currentResist.ToString());
    }

    public void SaveData(GameData data) 
    {
        data.resist = (int)currentResist;
    }
    public void LoadData(GameData data) 
    {
        currentResist = data.resist;
    }
}
