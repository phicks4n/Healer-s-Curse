using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Speed : MonoBehaviour
{
    [SerializeField] private float currentSpeed;
    [SerializeField] private float prevSpeed;
    [SerializeField] private TMP_Text speed;

    private void Start()
    {
        currentSpeed = currentSpeed;
        prevSpeed = prevSpeed;
        this.speed.SetText(currentSpeed.ToString());
    }

    public void AddSpeed(int speed)
    {
        Reduce(prevSpeed);
        prevSpeed = speed;
        currentSpeed += speed;
        this.speed.SetText(currentSpeed.ToString());
    }

    public void Reduce(float prevSpeed)
    {
        currentSpeed -= prevSpeed;
        this.speed.SetText(currentSpeed.ToString());
    }
}
