using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public static bool isFirstPerson = true;

    [SerializeField]
    private GameObject thirdPersonCam;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isFirstPerson)
            {
                thirdPersonCam.SetActive(true);
                isFirstPerson = false;
            }
            else
            {
                thirdPersonCam.SetActive(false);
                isFirstPerson = true;
            }
        }

    }
}
