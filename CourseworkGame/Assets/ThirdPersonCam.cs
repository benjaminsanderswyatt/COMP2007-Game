using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientaion;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;


    public Transform firstPersonLookAt;

    public GameObject thirdPersonCam;
    public GameObject firstPersonCam;



    public CameraStyle currentStyle;
    public enum CameraStyle
    {
        ThirdPerson,
        FirstPerson
    }



    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("FreelookCam")) SwitchCameraStyle(CameraStyle.ThirdPerson);
        if (Input.GetButtonDown("FirstPersonCam")) SwitchCameraStyle(CameraStyle.FirstPerson);



        if (currentStyle == CameraStyle.ThirdPerson)
        {
            //rotate direction
            Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
            orientaion.forward = viewDir.normalized;

            //rotate the player
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientaion.forward * verticalInput + orientaion.right * horizontalInput;

            if (inputDir != Vector3.zero)
            {
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }

        }
        else if (currentStyle == CameraStyle.FirstPerson)
        {
            Vector3 dirToFirstPersonLookAt = firstPersonLookAt.position - new Vector3(transform.position.x, firstPersonLookAt.position.y, transform.position.z);
            orientaion.forward = dirToFirstPersonLookAt.normalized;

            playerObj.forward = dirToFirstPersonLookAt.normalized;
        }

    }

    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        thirdPersonCam.SetActive(false);
        firstPersonCam.SetActive(false);


        if (newStyle == CameraStyle.ThirdPerson) thirdPersonCam.SetActive(true);
        if (newStyle == CameraStyle.FirstPerson) firstPersonCam.SetActive(true);

        currentStyle = newStyle;
    }
}
