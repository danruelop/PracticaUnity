using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    private Animator      m_animator;
    private Transform     m_playerTransform;
    private NavMeshAgent  m_agent;
    private float         m_elapsedTime = 0f;
    public  float         m_chaseTimeOut = 2f;
    public  float         m_distanceToAttack = 2f;
    public  float         m_fieldOfView  = 60;

    void Awake()
    {
        m_animator        = GetComponent<Animator>();
        m_agent           = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        m_elapsedTime     = 0;
    }

    public override void Enter()
    {
        m_animator.SetTrigger("Chase");
    }

    public override void Exit()
    {
        m_animator.ResetTrigger("Chase");
    }

    public void Update()
    {
        m_agent.SetDestination(m_playerTransform.position);

        if (!HaveLineSightToPlayer())
        {
            m_elapsedTime += Time.deltaTime;

            if (m_elapsedTime >= m_chaseTimeOut)
            {
                m_stateMachine.ChangeState(m_character.m_idle);
            }
        }
        else
        {
            m_elapsedTime = 0;
        }

        if (Vector3.Distance(transform.position, m_playerTransform.position) <= m_distanceToAttack)
        {
            m_stateMachine.ChangeState(m_character.m_attack);
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
