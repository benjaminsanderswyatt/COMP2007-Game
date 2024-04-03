using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashUiSystem : MonoBehaviour
{
    [Header("Player Dashing")]
    [SerializeField]
    private Dashing dashing;

    [Header("Dash Animation")]
    [SerializeField]
    private Animator dashAnim; // Dash Icon

    [Header("Bar")]
    [SerializeField]
    public Slider dashBar; // Red bar. The players current health but with a animation while healing

    [Header("Dash Bar")]
    //Details for how the dash bar animation displays
    [SerializeField]
    private AnimationCurve dashCurve;
    [SerializeField]
    private float dashDuration = 2f;

    [Header("Recharge Bar")]
    //Details for how the recharge bar animation displays
    [SerializeField]
    private AnimationCurve rechargeCurve;

    [Header("Cooldown")]
    [SerializeField]
    private float dashCooldown;

    private float dashTimer;
    private float rechargeTimer;

    private void Start()
    {
        dashBar.value = 1;

        dashTimer = dashDuration;
        rechargeTimer = dashCooldown;
    }

    private void Update()
    {
        //Bar Animations

        // Dash animation. Decreases the Dash bar over a given damage duration using the AnimationCurve
        if (dashTimer < dashDuration)
        {
            dashTimer += Time.deltaTime;
            float normalizedTime = dashTimer / dashDuration;
            float curveValue = dashCurve.Evaluate(normalizedTime);
            dashBar.value = Mathf.Lerp(1, 0, curveValue);
        }

        // Recharge animation. Decreases the Recharge bar over a given heal duration using the AnimationCurve
        if (rechargeTimer < dashCooldown)
        {
            rechargeTimer += Time.deltaTime;
            float normalizedTime = rechargeTimer / dashCooldown;
            float curveValue = rechargeCurve.Evaluate(normalizedTime);
            dashBar.value = Mathf.Lerp(0, 1, curveValue);

            //The bar is back to full play animation
            if (rechargeTimer > dashCooldown)
            {
                // Play the heart icon animation
                dashAnim.Play("Dash");
            }
        }

        if (dashBar.value == 0)
        {
            RechargeBar();
        }

    }

    public void DashUsed()
    {
        dashTimer = 0f;

        return;
    }

    public void RechargeBar()
    {
        // Start the damage animation
        rechargeTimer = 0f;  

        return;
    }

}
