using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickRandomSound : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] audioClips;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlayRndSound()
    {
        AudioClip sound = GetRandomSound(audioClips);
        audioSource.PlayOneShot(sound);
    }

    private AudioClip GetRandomSound(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }
}
