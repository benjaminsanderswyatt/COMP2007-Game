using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField]
    private GameObject keyObj;
    [SerializeField]
    private GameObject lightObj;

    [Header("Key System")]
    [SerializeField]
    private KeySystem keySystem;

    private void Start()
    {
        keyObj.SetActive(true);
        lightObj.SetActive(true);
    }


    public void TakeKey()
    {
        keyObj.SetActive(false);
        lightObj.SetActive(false);
        keySystem.AddKey();
    }
}
