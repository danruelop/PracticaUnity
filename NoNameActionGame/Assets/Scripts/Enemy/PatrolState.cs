using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{
    private Animator  m_animator;
    private Transform m_playerTransform;
    public  Transform[]   m_waypointsVector;
        public  float         m_fieldOfView  = 60;
    private NavMeshAgent  m_agent;
     private float         m_elapsedTime = 0f;
    public  float         m_distanceToPoint = 2f;
    Transform RandomDest;

    void Awake()
    {
        m_animator        = GetComponent<Animator>();
        m_agent           = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void Enter()
    {
        m_animator.SetTrigger("Patrol");
        RandomDest = m_waypointsVector[Random.Range(0, m_waypointsVector.Length)];
        m_agent.SetDestination(RandomDest.position);
    }

    public override void Exit()
    {
        m_animator.ResetTrigger("Patrol");
    }

    public void Update()
    {
        m_elapsedTime += Time.deltaTime;

        if (HaveLineSightToPlayer())
        {
            m_stateMachine.ChangeState(m_character.m_chase);
        }

        if (Vector3.Distance(transform.position, RandomDest.position) < m_distanceToPoint)
        {
            m_stateMachine.ChangeState(m_character.m_idle);
        }
    }

    private bool HaveLineSightToPlayer()
    {
        float Angle = Mathf.Abs(Vector3.Angle(transform.forward, (m_playerTransform.position - transform.position).normalized));

        if (Angle > m_fieldOfView)
            return false;

        return true;
    }
}
