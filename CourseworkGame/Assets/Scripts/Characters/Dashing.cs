using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    private Rigidbody rb;
    private MoveCharacter pm;

    [Header("Dash")]
    public float dashForce;
    public float dashUpwardForce;
    public float dashDuration;

    [Header("Camera Effects")]
    public FirstPersonCamera cam;
    public float dashFov;
    public float dashFovStartTime;
    public float dashFovEndTime;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.E;

    private DashUiSystem dashUiSystem;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<MoveCharacter>();

        // Find the DashUiSystem
        dashUiSystem = FindObjectOfType<DashUiSystem>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(dashKey))
        {
            Dash();
        }
    }

    private void Dash()
    {
        if (dashUiSystem.dashBar.value < 1)
        {
            return;
        }

        pm.dashing = true;

        //Update Ui
        dashUiSystem.DashUsed();


        cam.DoFov(dashFov, dashFovStartTime);

        Transform forwardT;
        forwardT = playerCam;

        Vector3 forceToApply = forwardT.forward.normalized * dashForce + orientation.up * dashUpwardForce;

        delayedForce = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.025f);

        Invoke(nameof(ResetDash), dashDuration);

    }

    private Vector3 delayedForce;

    private void DelayedDashForce()
    {
        rb.velocity = Vector3.zero;

        rb.AddForce(delayedForce, ForceMode.Impulse);
    }


    private void ResetDash()
    {
        cam.DoFov(cam.startCamFov, dashFovEndTime);

        pm.dashing = false;
    }
}
