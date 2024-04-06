using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private AudioSource PlayerAudioSource;

    [SerializeField]
    private AudioClip[] damageAudioClips;

    [SerializeField]
    private AudioClip[] dieAudioClips;    
    
    [SerializeField]
    private AudioClip[] healAudioClips;    
    
    [SerializeField]
    private AudioClip[] keyAudioClips;

    void Start()
    {
        PlayerAudioSource = gameObject.GetComponent<AudioSource>();
    }

    public void TakeDamage()
    {
        AudioClip sound = GetRandomSound(damageAudioClips);
        PlayerAudioSource.PlayOneShot(sound);
    }

    public void Die()
    {
        AudioClip sound = GetRandomSound(dieAudioClips);
        PlayerAudioSource.PlayOneShot(sound);
    }

    public void Heal()
    {
        AudioClip sound = GetRandomSound(healAudioClips);
        PlayerAudioSource.PlayOneShot(sound);
    }

    public void PickupKey()
    {
        AudioClip sound = GetRandomSound(keyAudioClips);
        PlayerAudioSource.PlayOneShot(sound);
    }

    private AudioClip GetRandomSound(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }
}
