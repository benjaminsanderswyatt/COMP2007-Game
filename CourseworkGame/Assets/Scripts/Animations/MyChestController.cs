using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyChestController : MonoBehaviour
{
    private Animator chestAnim;
    private Animator lightAnim;

    [Header("Objects")]
    public GameObject chestLid;
    public GameObject chestLight;

    [Header("Opening Curve")]
    public AnimationCurve shakeOpeningCurve;

    [Header("Closing Curve")]
    public AnimationCurve shakeClosingCurve;

    public TestShake cameraShake;

    private bool chestOpen = false;

    private void Start()
    {
        chestAnim = chestLid.GetComponent<Animator>();
        lightAnim = chestLight.GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if (!chestOpen)
        {
            chestAnim.Play("ChestOpen", 0, 0);
            lightAnim.Play("LightOn", 0, 0);
            cameraShake.StartShake(shakeOpeningCurve);
            chestOpen = true;
        }
        else
        {
            chestAnim.Play("ChestClose", 0, 0);
            lightAnim.Play("LightOff", 0, 0);
            cameraShake.StartShake(shakeClosingCurve);
            chestOpen = false;
        }
    }


}
