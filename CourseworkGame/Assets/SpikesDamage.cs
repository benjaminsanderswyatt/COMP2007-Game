using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesDamage : MonoBehaviour
{
    private HealthSystem _healthSystem;

    private void Start()
    {
        _healthSystem = FindObjectOfType<HealthSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        InvokeRepeating("DealDamage", 0f, 1f); // Damage every second inside
        
    }

    private void OnTriggerExit(Collider other)
    {
        CancelInvoke("DealDamage"); // Stop damaging when player exits
    }

    private void DealDamage()
    {
        _healthSystem.TakeDamage(1);
    }
}
