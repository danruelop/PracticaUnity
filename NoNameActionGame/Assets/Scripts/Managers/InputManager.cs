using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void OnStart();
    public event OnStart OnPlayerMovement;
    public event OnStart OnPlayerJump;
    public event OnStart OnPlayerFire;
    public event OnStart OnPlayerReload;
    public event OnStart OnPlayerCameraRotate;

    public static InputManager Instance
    {
        get;
        private set;
    }

    public void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal") != 0.0f || 
            Input.GetAxis("Vertical") != 0.0f)
        {
            OnPlayerMovement();
        }

        if(Input.GetAxis("Mouse X") != 0.0f ||
            Input.GetAxis("Mouse Y") != 0.0f)
        {
            OnPlayerCameraRotate();
        }

        if(Input.GetButtonDown("Jump"))
        {
            OnPlayerJump();
        }

        if(Input.GetButton("Fire1"))
        {
            OnPlayerFire();
        } 
        else if(Input.GetKey(KeyCode.R))
        {
            OnPlayerReload();
        }
        
    }
}
