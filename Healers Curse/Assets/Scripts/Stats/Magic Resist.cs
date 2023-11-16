using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MagicResist : MonoBehaviour
{
    [SerializeField] private float currentResist;
    [SerializeField] private TMP_Text resist;

    private void Start()
    {
        currentResist = currentResist;
        this.resist.SetText(currentResist.ToString());
    }

    public void AddResist(int resist)
    {
        currentResist += resist;
        this.resist.SetText(currentResist.ToString());
    }
}
