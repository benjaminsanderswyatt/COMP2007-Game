using System.Collections;
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
        if (!DialogManager.inDialog)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

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
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }


        

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
}
