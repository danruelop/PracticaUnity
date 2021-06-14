using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent { get; private set; }             
    public Transform target;                                    
    
    private void Start()
    {
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = true;
        agent.updatePosition = true;
    }

    private void Update()
    {
        if (target != null)
            agent.SetDestination(target.position);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}

