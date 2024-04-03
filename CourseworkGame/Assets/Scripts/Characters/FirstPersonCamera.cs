using JetBrains.Rider.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [Header("References")]
    public float sensX;
    public float sensY;

    public Transform orientation;
    public Transform camHolder;
    public Transform playerObj;

    float xRotation;
    float yRotation;

    public float startCamFov;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        startCamFov = GetComponent<Camera>().fieldOfView;
    }

    private void Update()
    {
        //get input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //rotate cam & orientation
        camHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        playerObj.rotation = Quaternion.Euler(0, yRotation, 0);

    }

    public void DoFov(float endValue, float duration)
    {
        float startValue = GetComponent<Camera>().fieldOfView;
        StartCoroutine(LerpFieldOfView(startValue, endValue, duration));
    }

    private IEnumerator LerpFieldOfView(float startValue, float endValue, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float newValue = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            GetComponent<Camera>().fieldOfView = newValue;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        GetComponent<Camera>().fieldOfView = endValue;
    }

    public void DoTilt(float tiltAmount)
    {
        Vector3 startRotation = transform.localEulerAngles;
        float endRotationZ = Mathf.Clamp(tiltAmount, -180f, 180f);
        Vector3 endRotation = new Vector3(0, 0, endRotationZ);
        StartCoroutine(LerpRotation(startRotation, endRotation, 0.25f));
    }

    private IEnumerator LerpRotation(Vector3 startRotation, Vector3 endRotation, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float lerpedZ = Mathf.LerpAngle(startRotation.z, endRotation.z, elapsedTime / duration);
            transform.localRotation = Quaternion.Euler(new Vector3(startRotation.x, startRotation.y, lerpedZ));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = Quaternion.Euler(endRotation);
    }
}
