using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    [SerializeField]
    private string exposedParam;

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat(exposedParam, Mathf.Log10(sliderValue) * 20);
    }
}
