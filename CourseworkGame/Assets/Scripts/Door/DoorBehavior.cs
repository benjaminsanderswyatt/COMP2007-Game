using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    private Animator doorAnim;

    [Header("Objects")]
    public GameObject doors;

    [Header("Opening Curve")]
    public AnimationCurve shakeOpeningCurve;

    [Header("Closing Curve")]
    public AnimationCurve shakeClosingCurve;

    public DoorCamShake cameraShake;

    private bool doorOpen = false;

    private void Start()
    {
        doorAnim = doors.GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if (!doorOpen)
        {
            doorAnim.Play("DoorOpen", 0, 0);
            cameraShake.StartShake(shakeOpeningCurve);
            doorOpen = true;
        }
        else
        {
            doorAnim.Play("DoorClose", 0, 0);
            cameraShake.StartShake(shakeClosingCurve);
            doorOpen = false;
        }
    }
}
