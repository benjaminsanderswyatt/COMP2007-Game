using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool keyDelivered;

    AudioSource doorAudio;

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
            doorAudio.Play();
            doorAnim.SetBool("isOpening", true);
            cameraShake.StartShake(shakeOpeningCurve);
        }
    }

    private void OnTriggerExit(Collider other)
    {


        if (keyDelivered)
        {
            doorAudio.Play();
            cameraShake.StartShake(shakeClosingCurve);
        }

        doorAnim.SetBool("isOpening", false);
    }

    void Start()
    {
        doorAudio = GetComponent<AudioSource>();
        doorAnim = this.transform.parent.GetComponent<Animator>();
    }
}
