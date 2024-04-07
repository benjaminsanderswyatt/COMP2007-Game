using Cinemachine;
using System.Collections;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject thirdPersonCam;


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

        startCamFov = GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView;
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


            if (SwitchCamera.isFirstPerson)
            {
                //rotate cam & orientation
                camHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0);

                orientation.rotation = Quaternion.Euler(0, yRotation, 0);

                playerObj.rotation = Quaternion.Euler(0, yRotation, 0);
            }
            else
            {
                //rotate cam & orientation
                transform.forward = camHolder.forward;

                Vector3 dirToCombatLookAt = playerObj.position - new Vector3(thirdPersonCam.transform.position.x, playerObj.position.y, thirdPersonCam.transform.position.z);
                orientation.forward = dirToCombatLookAt.normalized;

                playerObj.forward = dirToCombatLookAt.normalized;
            }



        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }




    }

    public void DoFov(float endValue, float duration)
    {
        float startValue = GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView;
        StartCoroutine(LerpFieldOfView(startValue, endValue, duration));
    }

    private IEnumerator LerpFieldOfView(float startValue, float endValue, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float newValue = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = newValue;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = endValue;
    }
}
