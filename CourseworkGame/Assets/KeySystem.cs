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

    public void AddKey()
    {
        hasKey = true;
        key.SetActive(true);
    }


}
