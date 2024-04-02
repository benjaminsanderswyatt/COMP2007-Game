using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyChestController : MonoBehaviour
{
    private Animator chestAnim;
    private Animator lightAnim;
    private Animator keyAnim;

    [Header("Objects")]
    public GameObject chestLid;
    public GameObject chestLight;
    public GameObject key;

    [Header("Opening Curve")]
    public AnimationCurve shakeOpeningCurve;

    [Header("Closing Curve")]
    public AnimationCurve shakeClosingCurve;

    private bool chestOpen = false;

    private void Start()
    {
        chestAnim = chestLid.GetComponent<Animator>();
        lightAnim = chestLight.GetComponent<Animator>();
        keyAnim = key.GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if (!chestOpen)
        {
            chestAnim.SetBool("IsOpening", true);
            keyAnim.SetBool("IsOpening", true);
            lightAnim.SetBool("IsLighting", true);
            chestOpen = true;
        }
        else
        {
            chestAnim.SetBool("IsOpening", false);
            keyAnim.SetBool("IsOpening", false);
            lightAnim.SetBool("IsLighting", false);
            chestOpen = false;
        }
    }


}
