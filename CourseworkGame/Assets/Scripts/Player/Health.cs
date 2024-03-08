using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Values")]
    public int initialHealth = 10;
    public int maxHealth = 10;
    public int currentHealth = 10;

    public UnityEvent onTakeDamage;
    public UnityEvent onAddHealth;
    public UnityEvent OnDie;

    public void TakeDamage(int damage)
    {
        if (inv)
        {
            return;
        }

        if(damage < 1)
        {
            return;
        }

        if(currentHealth - damage <= 0)
        {
            currentHealth = 0;
            Die();
            return;
        }

        currentHealth -= damage;

        onTakeDamge.Invoke();
    }


    public void AddHealth(int health)
    {
        if (health <= 0)
        {
            return;
        }

        if(currentHealth + health > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += health;
        }

        onAddHealth.Invoke();

    }


}
