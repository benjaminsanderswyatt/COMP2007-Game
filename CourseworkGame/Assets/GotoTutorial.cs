using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoTutorial : MonoBehaviour
{
    private KeySystem keySystem;

    private void Start()
    {
        keySystem = FindObjectOfType<KeySystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (keySystem.hasKey)
        {
            ScreenManager.manager.WinGame();
        }
        else
        {
            //No key
        }

        
    }
}
