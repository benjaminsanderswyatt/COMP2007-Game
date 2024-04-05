using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool keyDelivered;

    Animator doorAnim;

    [Header("Opening Curve")]
    public AnimationCurve shakeOpeningCurve;

    [Header("Closing Curve")]
    public AnimationCurve shakeClosingCurve;

    public DoorCamShake cameraShake;


    private void OnTriggerEnter(Collider other)
    {
        if (keyDelivered)
        {
            doorAnim.SetBool("isOpening", true);
            cameraShake.StartShake(shakeOpeningCurve);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        doorAnim.SetBool("isOpening", false);
        cameraShake.StartShake(shakeClosingCurve);
    }

    void Start()
    {
        doorAnim = this.transform.parent.GetComponent<Animator>();
    }
}
