using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField]
    private GameObject potion;


    private HealthSystem _healthSystem;

    private void Start()
    {
        _healthSystem = FindObjectOfType<HealthSystem>();

        potion.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        _healthSystem.AddHealth(1);
        potion.SetActive(false);

    }

}
