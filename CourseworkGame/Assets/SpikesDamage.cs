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
        InvokeRepeating("PrintToConsole", 0f, 1f); // Print every second
        
    }

    private void OnTriggerExit(Collider other)
    {
        CancelInvoke("PrintToConsole"); // Stop printing when player exits
    }

    private void PrintToConsole()
    {
        _healthSystem.TakeDamage(1);
    }
}
