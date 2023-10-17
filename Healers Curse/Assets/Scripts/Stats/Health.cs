using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private GameObject bloodParticle;
    [SerializeField] private Renderer renderer;
    [SerializeField] private float flashTime = 0.2f;
    [SerializeField] private TMP_Text health;

    private void Start()
    {
        maxHealth = maxHealth;
        currentHealth = currentHealth;
        this.health.SetText(currentHealth.ToString() + "/" + maxHealth);
    }

    public void Reduce(int damage)
    {
        currentHealth -= damage;
        CreateHitFeedback();
        if (currentHealth <= 0)
           {
            Die();
         }
    }

    public void AddHealth(int healing)
    {
        if (currentHealth + healing < maxHealth)
        {
            currentHealth += healing;
            this.health.SetText(currentHealth.ToString() + "/" + maxHealth);
        }
        else
        {
            currentHealth = maxHealth;
            this.health.SetText(currentHealth.ToString() + "/" + maxHealth);
        }
    }

    private void CreateHitFeedback()
    {
        Instantiate(bloodParticle, transform.position, Quaternion.identity);
        StartCoroutine(FlashFeedback());
    }

    private IEnumerator FlashFeedback()
    {
        renderer.material.SetInt("_Flash", 1);
        yield return new WaitForSeconds(flashTime);
        renderer.material.SetInt("_Flash", 0);
    }

    private void Die()
    {
         Debug.Log("Died");
        currentHealth = 1;
    }
}