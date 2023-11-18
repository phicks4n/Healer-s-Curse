using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Magic : MonoBehaviour
{
    [SerializeField] private float currentMagic;
    [SerializeField] private TMP_Text magic;

    private void Start()
    {
        currentMagic = currentMagic;
        this.magic.SetText(currentMagic.ToString());
    }

    public void AddMagic(int magic)
    {
        currentMagic += magic;
        this.magic.SetText(currentMagic.ToString());
    }
}
