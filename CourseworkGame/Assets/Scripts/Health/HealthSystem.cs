using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [Header("Heart Animation")]
    [SerializeField]
    private Animator heartAnim; // Heart Icon

    [Header("Bars")]
    [SerializeField]
    private Slider currentHealthBar; // Green bar. Represents the current health of the player
    [SerializeField]
    private Slider healthBar; // Red bar. The players current health but with a animation while healing
    [SerializeField]
    private Slider damageBar; // Grey bar. Represents the damage taken by the player when the TakeDamage is called


    [Header("Damage Bar")]
    //Details for how the damage bar animation displays
    [SerializeField]
    private AnimationCurve damageCurve;
    [SerializeField]
    private float damageDuration = 2f;

    [Header("Health Bar")]
    //Details for how the health bar animation displays
    [SerializeField]
    private AnimationCurve healCurve;
    [SerializeField]
    private float healDuration = 2f;

    [Header("Starting Stats")]
    // The starting stats for the player
    [SerializeField]
    private int initialHealth;
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int currentHealth;


    //variables to help the animations
    private float healthBeforeDamage; // Damage animation starting point
    private float damageTimer; // Damage animation length

    private float healthBeforeHeal; // Healing animation starting point
    private float healTimer; // Healing animation length

    private void Start()
    {
        //Set bars to match initial stats
        healthBar.maxValue = maxHealth;
        damageBar.maxValue = maxHealth;
        currentHealthBar.maxValue = maxHealth;

        currentHealth = initialHealth;

        healthBar.value = initialHealth;
        damageBar.value = initialHealth;
        currentHealthBar.value = initialHealth;


        healthBeforeDamage = initialHealth;

        damageTimer = damageDuration;
        healTimer = healDuration;
    }

    private void Update()
    {
        CheckTestingKeys(); // Test code which uses keys to demonstrate healthbar functionality


        //Bar Animations

        // Damage animation. Decreases the Damage bar over a given damage duration using the AnimationCurve
        if (damageTimer < damageDuration)
        {
            damageTimer += Time.deltaTime;
            float normalizedTime = damageTimer / damageDuration;
            float curveValue = damageCurve.Evaluate(normalizedTime);
            damageBar.value = Mathf.Lerp(healthBeforeDamage, currentHealth, curveValue);
        }

        // Healing animation. Decreases the Health bar over a given heal duration using the AnimationCurve
        if (healTimer < healDuration)
        {
            healTimer += Time.deltaTime;
            float normalizedTime = healTimer / healDuration;
            float curveValue = healCurve.Evaluate(normalizedTime);
            healthBar.value = Mathf.Lerp(healthBeforeHeal, currentHealth, curveValue);
        }
        
    }


    //Testing method
    private void CheckTestingKeys()
    {
        if (Input.GetKeyDown(KeyCode.G)) // Take damage. G key
        {
            TakeDamage(1); // Does 1 damage
        }

        if (Input.GetKeyDown(KeyCode.H)) // Heal. H key
        {
            AddHealth(1); // Heals 1 health
        }
    }

    public void TakeDamage(int damageAmount)
    {
        // Ensures that the healthbar is never greater than the current health bar
        if (healthBar.value > currentHealth)
        {
            healthBar.value = currentHealth;
            healTimer = healDuration; // Stops the animation
        }

        // Ensures the damage animation is smooth when damage overlaps
        if (damageTimer < damageDuration) 
        {
            // The previous damage animation is still running

            healthBeforeDamage = damageBar.value; // Gives the damage animation a new starting point for interpolation
        }
        else
        {
            healthBeforeDamage = currentHealth; // Sets the original starting point for interpolation
        }

        if (currentHealth - damageAmount <= 0)
        {
            //The player has died

            currentHealth = 0;
            
            // Death will be called here

            return;
        }

        //Take damage
        currentHealth -= damageAmount;
        currentHealthBar.value = currentHealth;
        healthBar.value -= damageAmount;

        // Start the damage animation
        damageTimer = 0f;

    }

    public void AddHealth(int healAmount)
    {
        // Checks if the player is dead
        if (currentHealth <= 0)
        {
            return;
        }

        // Ensures that the damage bar is never less than the current health bar
        if (damageBar.value < currentHealth)
        {
            damageBar.value = currentHealth;
            damageTimer = damageDuration; // Stops the animation
        }

        // Ensures the health bar filling animation is smooth when healing overlaps
        if (healTimer < healDuration)
        {
            healthBeforeHeal = healthBar.value; // Gives the damage animation a new starting point for interpolation
        }
        else
        {
            healthBeforeHeal = currentHealth; // Sets the original starting point for animations curves interpolation
        }

        //Ensures the player doesnt heal more than max health
        if (currentHealth + healAmount >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += healAmount; // Heals the player
        }

        currentHealthBar.value = currentHealth;

        // Start the damage animation
        healTimer = 0f;

        // Play the heart icon animation for healing
        heartAnim.Play("HeartPump");
    }

}
