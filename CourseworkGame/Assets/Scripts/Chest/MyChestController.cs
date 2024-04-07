using UnityEngine;

public class MyChestController : MonoBehaviour
{
    private Animator chestAnim;
    private Animator lightAnim;
    private Animator keyAnim;

    [Header("Audio")]
    private AudioSource chestAudioSource;
    [SerializeField]
    private AudioClip openChestClip;


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

        chestAudioSource = chestLid.GetComponent<AudioSource>();
    }

    public void PlayAnimation()
    {
        if (!chestOpen)
        {
            chestAnim.SetBool("IsOpening", true);
            keyAnim.SetBool("IsOpening", true);
            lightAnim.SetBool("IsLighting", true);

            //play opening sound
            chestAudioSource.PlayOneShot(openChestClip);

            chestOpen = true;
        }
        else
        {
            chestAnim.SetBool("IsOpening", false);
            keyAnim.SetBool("IsOpening", false);
            lightAnim.SetBool("IsLighting", false);

            //play closing sound
            chestAudioSource.PlayOneShot(openChestClip);

            chestOpen = false;
        }
    }


}
