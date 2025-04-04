﻿using System.Collections;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    public Animator animator;


    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float dashSpeed;
    public float dashSpeedChangeFactor;

    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Crounching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;


    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftControl;
    public KeyCode crouchKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;



    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;


    public bool dashing;

    public MovementState state;

    public enum MovementState
    {
        walking,
        sprinting,
        dashing,
        crouching,
        air
    }

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;
    private MovementState lastState;
    private bool keepMomentum;

    public void MovementStateHandler()
    {
        //Dashing
        if (dashing)
        {
            state = MovementState.dashing;
            desiredMoveSpeed = dashSpeed;
            speedChangeFactor = dashSpeedChangeFactor;
        }
        //Crouching
        else if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            desiredMoveSpeed = crouchSpeed;
        }
        //Sprinting
        else if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            desiredMoveSpeed = sprintSpeed;
        }
        //Walking
        else if (grounded)
        {
            state = MovementState.walking;
            desiredMoveSpeed = walkSpeed;
        }
        else
        {
            state = MovementState.air;

            if (desiredMoveSpeed < sprintSpeed)
            {
                desiredMoveSpeed = walkSpeed;
            }
            else
            {
                desiredMoveSpeed = sprintSpeed;
            }
        }

        bool desiredMoveSpeedChanged = desiredMoveSpeed != lastDesiredMoveSpeed;
        if (lastState == MovementState.dashing)
        {
            keepMomentum = true;
        }

        if (desiredMoveSpeedChanged)
        {
            if (keepMomentum)
            {
                StopAllCoroutines();
                StartCoroutine(SmoothlyLerpMoveSpeed());
            }
            else
            {
                StopAllCoroutines();
                moveSpeed = desiredMoveSpeed;
            }
        }


        lastDesiredMoveSpeed = desiredMoveSpeed;
        lastState = state;

    }

    private float speedChangeFactor;

    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        float boostFactor = speedChangeFactor;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);

            time += Time.deltaTime * boostFactor;

            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
        speedChangeFactor = 1f;
        keepMomentum = false;
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        animator.SetBool("IsJumping", false);
        readyToJump = true;

        //Crouching starting size
        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        //When talking stop movement
        if (!DialogManager.inDialog)
        {

            // ground check
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

            MyInput();
            SpeedControl();
            MovementStateHandler();

            // handle drag
            if (state == MovementState.walking || state == MovementState.sprinting || state == MovementState.crouching)
                rb.drag = groundDrag;
            else
                rb.drag = 0;
        }

        //animate the player shadow
        Animate();
    }

    private void FixedUpdate()
    {
        if (!DialogManager.inDialog)
        {
            MovePlayer();
        }

    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // Crouching
        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        //Stopped Crouching
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }


    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }

        // on ground
        else if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

    }

    private void SpeedControl()
    {
        // limit speed on slope
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        }
        // limit speed on ground or air
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }


    }

    private void Jump()
    {
        exitingSlope = true;
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
        exitingSlope = false;
    }

    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.2f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }





    private void Animate()
    {
        if (grounded)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", false);
            animator.SetBool("IsGrounded", true);
        }
        else
        {
            animator.SetBool("IsGrounded", false);
        }


        //Animating Movement
        if (rb.velocity.magnitude > 0.05)
        {
            animator.SetBool("IsMoving", true);
            animator.SetFloat("InputMagnitude", rb.velocity.magnitude / desiredMoveSpeed, 0.05f, Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        if (state == MovementState.crouching)
        {
            animator.SetBool("IsSneaking", true);
            animator.SetFloat("InputMagnitude", rb.velocity.magnitude / desiredMoveSpeed, 0.05f, Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsSneaking", false);
        }


        //Jumping
        if (state == MovementState.air)
        {
            animator.SetBool("IsJumping", true);
            animator.SetBool("IsFalling", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }

    }









}