using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public Transform pfHealthBar;

    private void Start()
    {
        HealthSystem healthSystem = new HealthSystem(10);
        //Need to make pfHealthBar which is a Prefabs health bar

        Transform healthBarTransform = Instantiate(pfHealthBar, new Vector3(0, 10), Quaternion.identity);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();

        Debug.Log("Health: " + healthSystem.GetHealthPercent());
        /*
        CMDebug.ButtonUI(new Vector2(100, 100), "damage", () =>{
        
            healthSystem.Damage(5);
            Debug.Log("Damage: " + healthSystem.GetHealth());
        });

        CMDebug.ButtonUI(new Vector2(100, 100), "damage", () =>
        {
            healthSystem.Damage(5);
            Debug.Log("Damage: " + healthSystem.GetHealth());
        });
        */
    }
}
