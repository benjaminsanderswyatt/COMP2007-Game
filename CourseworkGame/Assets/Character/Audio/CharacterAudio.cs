using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    private AudioSource CharacterAudioSource;

    [SerializeField]
    private AudioClip[] footStepAudioClips;

    [SerializeField]
    private AudioClip[] walkStepAudioClips;

    [SerializeField]
    private AudioClip[] sneakStepAudioClips;

    [SerializeField]
    private AudioClip[] jumpAudioClips;

    [SerializeField]
    private AudioClip[] landAudioClips;

    void Start()
    {
        CharacterAudioSource = gameObject.GetComponent<AudioSource>();
    }

    private void FootStep() //runnning
    {
        AudioClip sound = GetRandomSound(footStepAudioClips);
        CharacterAudioSource.PlayOneShot(sound);
    }
    private void WalkStep()
    {
        AudioClip sound = GetRandomSound(walkStepAudioClips);
        CharacterAudioSource.PlayOneShot(sound);
    }

    private void SneakStep()
    {
        AudioClip sound = GetRandomSound(sneakStepAudioClips);
        CharacterAudioSource.PlayOneShot(sound);
    }



    private void Jump()
    {
        AudioClip sound = GetRandomSound(jumpAudioClips);
        CharacterAudioSource.PlayOneShot(sound);
    }

    private void Land() //jump landing
    {
        AudioClip sound = GetRandomSound(landAudioClips);
        CharacterAudioSource.PlayOneShot(sound);
    }




    private AudioClip GetRandomSound(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }

}
