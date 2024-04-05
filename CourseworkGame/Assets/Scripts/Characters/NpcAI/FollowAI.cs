using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static NPCAI;
using static UnityEditor.FilePathAttribute;

public class FollowAI : MonoBehaviour
{
    private NavMeshAgent theAgent;
    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private float speedAnim = 7; //their max speed is 7


    [SerializeField]
    private GameObject startDestination;

    [SerializeField]
    private float RangeFromPlayer = 7;
    

    Animator Animator;

    [Header("Jumping")]
    private bool hasDone = false;
    private float orginalBaseOffset;
    private Vector3 startPosition;
    private Vector3 endPosition;
    public AnimationCurve heightCurve;
    public float journeyDistance;
    public float heightFactor;

    void Start()
    {
        theAgent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();

        theAgent.SetDestination(startDestination.transform.position);

        orginalBaseOffset = theAgent.baseOffset;
    }

    void Update()
    {
        Animate();

        if (!GetComponent<NpcDialogTrigger>().IsTalkingToPlayer)
        {
            theAgent.updateRotation = true;
            theAgent.isStopped = false;

            //PatrolPoints();
            FollowPlayer();

        }
        else
        {
            theAgent.velocity = Vector3.zero;
            theAgent.isStopped = true;

            LookAtPlayer();
            
        }
    }

    private void LookAtPlayer()
    {
        theAgent.updateRotation = false;

        StartCoroutine(RotateAgentTowardsPlayer());

    }
    IEnumerator RotateAgentTowardsPlayer()
    {
        Vector3 dir = Player.transform.position - theAgent.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);

        float timeToRotate = 1.5f;
        float elapsedTime = 0f;

        while (elapsedTime < timeToRotate)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / timeToRotate);
            Quaternion newRotation = Quaternion.Slerp(theAgent.transform.rotation, lookRotation, t);
            theAgent.gameObject.transform.rotation = Quaternion.Euler(0f, newRotation.eulerAngles.y, 0f);
            yield return null;
        }
    }

    private bool IsClose(Vector3 object1, Vector3 object2, float range)
    {
        //When the player gets close
        if (Vector3.Distance(object1, object2) < range)
        {
            return true;
        }
        return false;
    }


    public void FollowPlayer()
    {
        if (!IsClose(theAgent.transform.position, Player.transform.position, RangeFromPlayer))
        {
            theAgent.GetComponent<NavMeshAgent>().SetDestination(Player.transform.position);
        }
        else
        {
            theAgent.velocity = Vector3.zero;
        }
    }


    private void Animate()
    {
        //Animating Movement
        if (theAgent.velocity.magnitude > 0)
        {
            Animator.SetBool("IsMoving", true);
            Animator.SetFloat("InputMagnitude", theAgent.velocity.magnitude / speedAnim, 0.05f, Time.deltaTime);
        }
        else
        {
            Animator.SetBool("IsMoving", false);
        }

        //Jump
        //Jumping
        if (theAgent.isOnOffMeshLink)
        {
            Animator.SetBool("IsJumping", true);

            Animator.SetBool("IsGrounded", false);

            if (!hasDone)
            {
                startPosition = theAgent.transform.position;
                endPosition = theAgent.nextOffMeshLinkData.startPos;
                journeyDistance = Vector3.Distance(startPosition, endPosition) - 1;
                hasDone = true;
            }

            ChangeHeight();

            if (Vector3.Distance(theAgent.transform.position, startPosition) / journeyDistance >= 0.5)
            {
                Animator.SetBool("IsFalling", true);
                Animator.SetBool("IsJumping", false);
            }
            else
            {
                Animator.SetBool("IsFalling", false);
                Animator.SetBool("IsJumping", true);
            }

        }
        else
        {
            if (hasDone)
            {
                hasDone = false;
                theAgent.baseOffset = orginalBaseOffset;
            }

            Animator.SetBool("IsJumping", false);
            Animator.SetBool("IsGrounded", true);
            Animator.SetBool("IsFalling", false);

        }

    }

    private void ChangeHeight()
    {
        var onCurve = Mathf.Clamp01(Vector3.Distance(theAgent.transform.position, startPosition) / journeyDistance);

        // Interpolate height using curve
        float howHigh = heightCurve.Evaluate(onCurve) * heightFactor;

        theAgent.baseOffset = howHigh;

    }

}
