using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRepair : MonoBehaviour
{
    private AudioSource audioSource;

    [Range(0.1f, 0.4f)]
    public float volumeChangeMult = 0.2f;    
    [Range(0.1f, 0.5f)]
    public float pitchChangeMult = 0.2f;

    [SerializeField]
    private AudioClip[] randomSounds;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

        StartCoroutine(RunRandomSounds());
    }

    private void PlaySound() //jump landing
    {
        AudioClip sound = GetRandomSound(randomSounds);
        audioSource.volume = Random.Range(0.5f - volumeChangeMult , 0.5f);
        audioSource.pitch = Random.Range(1 - pitchChangeMult , 1);
        audioSource.PlayOneShot(sound);
    }

    private AudioClip GetRandomSound(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }



    public float minDelay = 1f;
    public float maxDelay = 5f;

    IEnumerator RunRandomSounds()
    {
        while (true)
        {
            // wait for a random amount of time
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            PlaySound();
        }
    }
}
