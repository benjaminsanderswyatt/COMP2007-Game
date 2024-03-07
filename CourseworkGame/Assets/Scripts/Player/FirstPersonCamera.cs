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
    public Transform playerObj;

    float xRotation;
    float yRotation;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        playerObj.rotation = Quaternion.Euler(0, yRotation, 0);

    }


}
