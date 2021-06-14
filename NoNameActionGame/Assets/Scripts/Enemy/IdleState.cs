using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private Animator  m_animator;
    public  float     m_fieldOfView  = 60;
    private Transform m_playerTransform;

    void Awake()
    {
        m_animator        = GetComponent<Animator>();
    }

    void Start()
    {
        m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void Enter()
    {
        m_animator.SetTrigger("Idle");
    }

    public override void Exit()
    {
        m_animator.ResetTrigger("Idle");
    }

    public void Update()
    {
        if (CanSeePlayer())
        {
            m_stateMachine.ChangeState(m_character.m_chase);
        }
    }

    private bool CanSeePlayer()
    {
        float Angle = Mathf.Abs(Vector3.Angle(transform.forward, (m_playerTransform.position - transform.position).normalized));

        if (Angle > m_fieldOfView)
            return false;

        return true;
    }

    public void FinishIdle ()
    {
        m_stateMachine.ChangeState(m_character.m_patrol);
    }
}
