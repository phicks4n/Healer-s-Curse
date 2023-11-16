using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Speed : MonoBehaviour
{
    [SerializeField] private float currentSpeed;
    [SerializeField] private TMP_Text speed;

    private void Start()
    {
        currentSpeed = currentSpeed;
        this.speed.SetText(currentSpeed.ToString());
    }

    public void AddSpeed(int speed)
    {
        currentSpeed += speed;
        this.speed.SetText(currentSpeed.ToString());
    }
}
