using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class FPSCharacterController : MonoBehaviour
{
    private HealthComponent m_healthComponent; 

    [Header("Player variables")]
    private float m_verticalVelocity;
    private float m_isGroundedTimer;
    public float m_walkSpeed = 2.0f;
    public float m_jumpHeight = 1.0f;
    public float m_gravity = 9.81f;

    [Header("Camera")]
    public float m_playerCameraRotationSpeed = 4.0f;
    public float m_playerCameraRotationXLimit = 60.0f;
    public Camera m_playerCamera;
    public Transform m_playerCameraParent;
    Vector2 m_rotation = Vector2.zero;


    [Header("GameObject References")]
    public GameObject m_weapon;
    public CharacterController m_controller;

    private void Start()
    {
        m_healthComponent = gameObject.GetComponent<HealthComponent>();
        /*TEST Third Person Camera*/
        m_rotation.y = transform.eulerAngles.y;

        /*Locks Cursor*/
        Cursor.lockState = CursorLockMode.Locked;

        m_controller = gameObject.GetComponent<CharacterController>();
        ((InputManager)InputManager.Instance).OnPlayerMovement += PlayerMovement;
        ((InputManager)InputManager.Instance).OnPlayerCameraRotate += PlayerCameraRotate;
        ((InputManager)InputManager.Instance).OnPlayerFire += PlayerFire;
        ((InputManager)InputManager.Instance).OnPlayerJump += PlayerJump;
        ((InputManager)InputManager.Instance).OnPlayerReload  += PlayerReload;

    }

    void Update()
    {
        
        bool groundedPlayer = m_controller.isGrounded;
        if (groundedPlayer)
        {
            m_isGroundedTimer = 0.2f;
        }

        if (m_isGroundedTimer > 0)
        {
            m_isGroundedTimer -= Time.deltaTime;
        }

        if (groundedPlayer && m_verticalVelocity < 0)
        {
            m_verticalVelocity = 0f;
        }

        m_verticalVelocity -= m_gravity * Time.deltaTime;

        Vector3 tempMove = Vector3.zero;
        tempMove.y = m_verticalVelocity;
        m_controller.Move(tempMove * Time.deltaTime);
    }

    void PlayerMovement(float _x, float _y)
    {
        Vector3 tempMove = transform.right * _x + transform.forward * _y;
        tempMove *= m_walkSpeed;
        m_controller.Move(tempMove * Time.deltaTime);
    }

    void PlayerCameraRotate(float _x, float _y)
    {
        /*TEST Third Person Camera*/
        m_rotation.y += _x * m_playerCameraRotationSpeed;
        m_rotation.x -= _y * m_playerCameraRotationSpeed;
        m_rotation.x = Mathf.Clamp(m_rotation.x, -m_playerCameraRotationXLimit, m_playerCameraRotationXLimit);
        m_playerCameraParent.localRotation = Quaternion.Euler(m_rotation.x, 0, 0);
        transform.eulerAngles = new Vector2(0, m_rotation.y);

    }

    void PlayerJump()
    {
        if (m_isGroundedTimer > 0)
        {
            m_isGroundedTimer = 0;

            m_verticalVelocity += Mathf.Sqrt(m_jumpHeight * 2 * m_gravity);
        }
    }
    
    void PlayerFire()
    {
        VerySimplePistol weapon_Script = m_weapon.GetComponent<VerySimplePistol>();
        weapon_Script.Shot();
    }

    void PlayerReload()
    {
        VerySimplePistol weapon_Script = m_weapon.GetComponent<VerySimplePistol>();
        weapon_Script.Reload();
    }

}
