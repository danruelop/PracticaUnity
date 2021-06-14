using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAIBehaviour : MonoBehaviour
{
    StateMachine movementSM;
    public PatrolState  m_patrol;
    public IdleState    m_idle;
    public ChaseState   m_chase;
    public AttackState  m_attack;

    public int m_health;
    public SwordTrigger m_sword;

    private void Start()
    {
        movementSM = GetComponent<StateMachine>();

        m_patrol = GetComponent<PatrolState>();
        m_idle = GetComponent<IdleState>();
        m_chase = GetComponent<ChaseState>();
        m_attack = GetComponent<AttackState>();
        m_patrol.Init(this, movementSM);
        m_idle.Init(this, movementSM);
        m_chase.Init(this, movementSM);
        m_attack.Init(this, movementSM);

        movementSM.Init(m_idle);
    }

    public void StartAttack ()
    {
        m_sword.EnableCollider(true);
    }

    public void FinishAttack ()
    {
        m_sword.EnableCollider(false);
    }
}
