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

    [Header("Cooldown")]
    public float dashCooldown;
    private float dashCooldownTimer;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.B;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<MoveCharacter>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(dashKey))
        {
            Dash();
        }

        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    private void Dash()
    {
        if (dashCooldownTimer > 0)
        {
            return;
        }
        else
        {
            dashCooldownTimer = dashCooldown;
        }

        pm.dashing = true;


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
