using UnityEngine;

public class KeySystem : MonoBehaviour
{
    public bool hasKey;

    [Header("Audio")]
    [SerializeField]
    private PickRandomSound pickupKeyAudio;

    [Header("Text")]
    [SerializeField]
    public GameObject key;

    [Header("KeyReminder")]
    [SerializeField]
    private Animator keyReminderAnim;


    private void Start()
    {
        hasKey = false;
        key.SetActive(false);
    }

    public void AddKey()
    {
        hasKey = true;
        key.SetActive(true);

        // Play pickup sound
        pickupKeyAudio.PlayRndSound();
    }


    public void OpenKeyReminder()
    {
        keyReminderAnim.SetBool("IsOpen", true);
    }

    public void CloseKeyReminder()
    {
        keyReminderAnim.SetBool("IsOpen", false);
    }


}
