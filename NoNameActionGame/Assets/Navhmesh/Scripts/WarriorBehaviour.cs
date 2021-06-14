using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WarriorBehaviour : MonoBehaviour
{
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private bool isIdle = true;
    private Transform enemy;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;

        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }

    void Update()
    {
        if (isIdle && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
        else if (!isIdle)
        {
            if (enemy != null)
            {
                agent.SetDestination(enemy.position);

                if (!agent.pathPending && agent.remainingDistance < 2.5f)
                {
                    GameObject.Destroy(enemy.gameObject);
                    enemy = null;
                    isIdle = true;
                }
            }
            else
                isIdle = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            isIdle = false;
            enemy = collision.gameObject.transform;
        }
    }
}
