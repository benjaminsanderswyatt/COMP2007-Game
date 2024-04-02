using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeySystem : MonoBehaviour
{
    public bool hasKey;

    [Header("Text")]
    [SerializeField]
    public GameObject key;


    private void Start()
    {
        hasKey = false;
        key.SetActive(false);
    }

    private void Update()
    {
        CheckTestingKeys();

    }


    //Testing method
    private void CheckTestingKeys()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddKey();
        }
    }

    public void AddKey()
    {
        hasKey = true;
        key.SetActive(true);
    }


}
