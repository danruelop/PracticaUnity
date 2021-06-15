using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float m_maxHealth;
    public float m_health;

    protected CharacterController m_controller;

    [Header("Boing")]
    private float m_verticalVelocity;
    private float m_isGroundedTimer;
    private float m_gravity = 9.81f;

    public void OnTriggerEnter(Collider other)
    {
        if(tag == "Player")
        {
            Debug.Log("Colision" + other.name);

            JumpPoint jumpPoint = other.GetComponent<JumpPoint>();
            if (jumpPoint != null)
            {
                jumpPoint.OnTriggerWithPlayer(this);
            }
            else
            {
                Debug.Log("JumpPoint is null");
            }
        }
    }

    void Awake()
    {
        m_health = m_maxHealth;

    }
    void Start()
    {
        if(tag == "Player")
        {
            m_controller = gameObject.GetComponent<CharacterController>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(tag == "Player")
        {
            bool isInGround = m_controller.isGrounded;
            if (isInGround)
            {
                m_isGroundedTimer = 0.2f;
            }

            if (m_isGroundedTimer > 0)
            {
                m_isGroundedTimer -= Time.deltaTime;
            }

            if (isInGround && m_verticalVelocity < 0)
            {
                m_verticalVelocity = 0f;
            }

            m_verticalVelocity -= m_gravity * Time.deltaTime;
            Vector3 tempMove = Vector3.zero;
            tempMove.y = m_verticalVelocity;

            m_controller.Move(tempMove * Time.deltaTime);
        } 
    }

    public void Boing(float _boingStrength, bool _isSelfJump)
    {
        if(_isSelfJump)
        {
            if (m_isGroundedTimer > 0)
            {
                m_isGroundedTimer = 0;
                m_verticalVelocity += Mathf.Sqrt(_boingStrength * 2 * m_gravity);
            }
        }
        else
        {
            m_verticalVelocity += Mathf.Sqrt(_boingStrength * 2 * m_gravity);
        }
        
    }
}
