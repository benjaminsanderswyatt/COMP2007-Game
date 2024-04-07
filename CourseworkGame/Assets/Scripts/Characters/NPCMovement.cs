using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{

    NavMeshAgent theAgent;
    public bool InRange = false;
    public GameObject Waypoint1;
    public GameObject Waypoint2;
    public GameObject IdlePosition;
    public Transform Player;
    public float WayPointRange = 2;
    public float PlayerRange = 10;

    public Animator Animator;

    private float orginalBaseOffset;

    void Start()
    {
        theAgent = GetComponent<NavMeshAgent>();
        orginalBaseOffset = theAgent.baseOffset;
    }

    private bool hasDone = false;
    private Vector3 startPosition;
    private Vector3 endPosition;

    public AnimationCurve heightCurve;
    public float journeyDistance;

    public float heightFactor;

    [SerializeField]
    private Desire desire;

    private enum Desire
    {
        idle,
        patrol
    }

    void Update()
    {
        Animate();
        //theAgent.SetDestination(Waypoint1.transform.position);


        //When the player gets close
        if (Vector3.Distance(theAgent.transform.position, Player.transform.position) < PlayerRange)
        {

        }

        //When the player gets close
        if (Vector3.Distance(theAgent.transform.position, Player.transform.position) < PlayerRange)
        {

        }

        switch (desire)
        {
            case Desire.idle:
                //Idle();
                break;
            case Desire.patrol:
                //Patrol();
                break;
        }

    }

    private void Animate()
    {
        //Animating Movement
        if (theAgent.velocity.magnitude > 0)
        {
            Animator.SetBool("IsMoving", true);
            Animator.SetFloat("InputMagnitude", theAgent.velocity.magnitude / theAgent.speed, 0.05f, Time.deltaTime);
        }
        else
        {
            Animator.SetBool("IsMoving", false);
        }


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
