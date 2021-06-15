using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent { get; private set; }
    private Transform target;

    private void Start()
    {
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = true;
        agent.updatePosition = true;
        target = GameObject.FindGameObjectWithTag("Human").transform;
    }

    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);

            if (!agent.pathPending && agent.remainingDistance < 2.5f)
            {
                GameObject.Destroy(target.parent.gameObject);
            }
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("Human") != null)
                target = GameObject.FindGameObjectWithTag("Human").transform;
        }
    }
}
