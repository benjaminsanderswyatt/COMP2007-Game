using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    

    void Start()
    {
        theAgent = GetComponent<NavMeshAgent>();
        Animator.SetBool("IsIdle", true);
    }


    public void TestTriggerMethod()
    {
        Debug.Log("Enemy entered the trigger!");
        Animator.SetBool("testTrigger", true);

    }



    void Update()
    {
        //Animator.SetBool("IsMoving", theAgent.velocity.magnitude > 0.01f);

        //Animation Idle


        //When the player gets close
        //Jump
        if (Vector3.Distance(theAgent.transform.position, Player.transform.position) < PlayerRange)
        {
            InRange = true;
            theAgent.SetDestination(Waypoint2.transform.position);
            Animator.SetBool("Jumping", true);
        }

        //Landed
        if (Vector3.Distance(theAgent.transform.position, Waypoint2.transform.position) < WayPointRange)
        {
            Animator.SetBool("Jumping", false);
            Animator.SetBool("IsMoving", true);
            
            theAgent.SetDestination(IdlePosition.transform.position);
        }

        /*

        theAgent.SetDestination(startDestination.transform.position);

        if (Vector3.Distance(theAgent.transform.position, Waypoint2.transform.position) < WayPointRange && InRange == false)
        {
            theAgent.SetDestination(Waypoint1.transform.position);
        }

        if (Vector3.Distance(theAgent.transform.position, Waypoint1.transform.position) < WayPointRange && InRange == false)
        {
            theAgent.SetDestination(Waypoint2.transform.position);
        }



        if (Vector3.Distance(theAgent.transform.position, Player.transform.position) > PlayerRange && InRange == true)
        {
            InRange = false;
            theAgent.SetDestination(Waypoint1.transform.position);
        }
        */
    }
}
