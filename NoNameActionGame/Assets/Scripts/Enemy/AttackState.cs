using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    private Animator  m_animator;
    private Transform m_playerTransform;
    private float     m_elapsedTime = 0f;
    private float     m_attackDelay = 2f;
    private bool      m_isAttacking = false;
    public  float     m_distanceToAttack = 2.5f;
    public  float     m_fieldOfView  = 60;

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
        m_animator.SetTrigger("Attack");
        m_isAttacking = true;
        m_elapsedTime = 0f;
    }

    public override void Exit()
    {
        m_animator.ResetTrigger("Attack");
    }

    public void Update()
    {
        m_elapsedTime += Time.deltaTime;

        if (!m_isAttacking && (!HaveLineSightToPlayer() || Vector3.Distance(transform.position, m_playerTransform.position) > m_distanceToAttack))
        {
            m_stateMachine.ChangeState(m_character.m_chase);
        }

        if (m_elapsedTime >= m_attackDelay)
        {
            m_elapsedTime = 0f;
            m_animator.SetTrigger("Attack");
            m_isAttacking = true;
            // Do Damage To Player
        }
    }

    private bool HaveLineSightToPlayer()
    {
        float Angle = Mathf.Abs(Vector3.Angle(transform.forward, (m_playerTransform.position - transform.position).normalized));

        if (Angle > m_fieldOfView)
            return false;

        return true;
    }

    public void FinishAttack ()
    {
        m_isAttacking = false;
    }
}
