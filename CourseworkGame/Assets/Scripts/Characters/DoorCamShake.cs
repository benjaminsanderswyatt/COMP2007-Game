using UnityEngine;

public class DoorCamShake : MonoBehaviour
{
    public Transform camTransform;

    [Header("Settings")]
    public float shakeDamper = 10f; //higher means shake fades off quicker
    public float shakeTime = 1f; //how long does shake happen for
    public float shakeIntensityFactor = 1f; //intensity

    private AnimationCurve shakeCurve; //curve

    private float t = 100f;

    Vector3 originalPos;

    public void StartShake(AnimationCurve animationCurve)
    {
        t = 0f;
        shakeCurve = animationCurve;
    }

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (t < shakeTime)
        {
            float shakeIntensity = Mathf.PerlinNoise(t / shakeDamper, 0f) * shakeCurve.Evaluate(t / shakeTime);
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeIntensity * shakeIntensityFactor;

            t += Time.deltaTime;
        }
        else
        {
            t = shakeTime;
            camTransform.localPosition = originalPos;
        }
    }
}
