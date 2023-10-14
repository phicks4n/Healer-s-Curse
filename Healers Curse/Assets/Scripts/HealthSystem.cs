using System;

public class HealthSystem
{
    public event EventHandler onHealthChanged;
    private int health;
    private int maxHealth;

    public HealthSystem(int maxHealth)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;
    }

    public int GetHealth()
    {
        return health;
    }

    public float GetHealthPercent()
    {
        return (float)health / maxHealth;
    }


    public void Damage(int damageAmount)
    {
        health -= damageAmount;

        if (health < 0) health = 0;
        if (onHealthChanged != null) onHealthChanged(this, EventArgs.Empty);
    }

    public void Heal(int healAmount)
    {
        health += healAmount;

        if (health > maxHealth) health = maxHealth;
        if (onHealthChanged != null) onHealthChanged(this, EventArgs.Empty);
    }
}
