using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AshGonnaPatrol : StateMachineBehaviour
{
    private Transform[] points;
    private GameObject Player;
    private int destPoint = 0;
    public float HearingDistance=50;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        DataPoints data = animator.gameObject.GetComponent<DataPoints>();
        Player = data.Player;
        points = data.Points;
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NavMeshAgent agent = animator.gameObject.GetComponent<NavMeshAgent>();
        if (agent.enabled)
        {
            if (points.Length == 0)
                return;
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                agent.destination = points[destPoint].position;
                destPoint = (destPoint + 1) % points.Length;
                animator.Play("walking");
            }
            if (Vector3.Distance(animator.gameObject.transform.position, Player.transform.position) < HearingDistance)
            {
                agent.destination = animator.gameObject.transform.position;
                animator.Play("Throwing");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
