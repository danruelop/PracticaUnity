using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum EAnimState
{
    Idle,
    Shooting,
    Reload,
}

[RequireComponent(typeof(CharacterController))]
public class FPSCharacterController : MonoBehaviour
{
    public CharacterController m_controller;
    public float m_verticalVelocity;
    private float m_isGroundedTimer;
    public float m_walkSpeed = 2.0f;
    public float m_jumpHeight = 1.0f;
    public float m_gravity = 9.81f;
    private Vector2 m_playerCameraRotation = Vector2.zero;
    public float m_playerCameraRotationSpeed = 2.0f;
    public float m_playerCameraRotationXLimit = 45.0f;
    public Camera m_playerCamera;
    private Transform m_transform;

    public GameObject m_weapon;




    private void Start()
    {
        m_transform = transform;
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

    }

    void PlayerMovement()
    {
        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        move *= m_walkSpeed;
        move.y = m_verticalVelocity;
        m_controller.Move(move * Time.deltaTime);
    }

    void PlayerCameraRotate()
    {
        m_playerCameraRotation.y += Input.GetAxis("Mouse X") * m_playerCameraRotationSpeed;
        m_playerCameraRotation.x += -Input.GetAxis("Mouse Y") * m_playerCameraRotationSpeed;
        m_playerCameraRotation.x = Mathf.Clamp(m_playerCameraRotation.x, -m_playerCameraRotationXLimit, m_playerCameraRotationXLimit);
        m_playerCamera.transform.localRotation = Quaternion.Euler(m_playerCameraRotation.x, 0, 0);
        transform.eulerAngles = new Vector2(0, m_playerCameraRotation.y);
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
