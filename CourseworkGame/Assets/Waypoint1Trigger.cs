using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Waypoint1Trigger : MonoBehaviour
{
    public UnityEvent<Collider> onTriggerEnter;

    void OnTriggerEnter(Collider col)
    {
        Invoke("TestTriggerMethod", 0);
    }
}
