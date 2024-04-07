using UnityEngine;

public class GotoTutorial : MonoBehaviour
{
    private KeySystem keySystem;

    private AudioSource audioSource;

    private void Start()
    {
        keySystem = FindObjectOfType<KeySystem>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (keySystem.hasKey)
        {
            audioSource.Play();
            ScreenManager.manager.WinGame();
        }
        else
        {
            keySystem.OpenKeyReminder();

        }

    }

    private void OnTriggerExit(Collider other)
    {
        keySystem.CloseKeyReminder();
    }
}
