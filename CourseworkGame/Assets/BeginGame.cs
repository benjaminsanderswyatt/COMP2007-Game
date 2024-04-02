using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginGame : MonoBehaviour
{
    private bool hasDone = false;

    void Update()
    {
        if (!hasDone && Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Mouse0) && !Input.GetKeyDown(KeyCode.Mouse1) && !Input.GetKeyDown(KeyCode.Mouse2))
        {
            hasDone = true;
            ScreenManager.manager.BeginTimer();
        }
    }
}
