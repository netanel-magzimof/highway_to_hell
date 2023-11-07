using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    #region Inspector
    public PlayerStateManager playerStateManager;

    [Header("Dash Stats")]
    [SerializeField] private float dashSpeed = 30;
    [SerializeField] private float dashDuration = 0.1f;
    [SerializeField] private float dashCooldown = 1.3f;

    
    [Header("Movement Stats")]
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float rotationSpeed = 5;
    #endregion



    #region Fields
    public bool canDash, isDuringAttack;
    private Rigidbody _physics;
    private Animator _animator;
    public PlayerInputActions _playerInputActions;
    private static readonly int RunAnimatorIndex = Animator.StringToHash("Run");

    #endregion



    #region MonoBehaviour
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _physics = GetComponent<Rigidbody>();
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Dash.performed += Dash;
        playerStateManager = new PlayerStateManager();
        playerStateManager.SetPlayerState(PlayerState.Idle);
        canDash = true;
    }
    private void Update()
    {
        //TODO change this to a different way to switch to ui mode
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            _playerInputActions.Player.Disable();
            _playerInputActions.UI.Enable();
        }
    }
    private void FixedUpdate()
    {
        if (!playerStateManager.CanRunFromState()) return;
        
        Vector2 inputVec = _playerInputActions.Player.Movement.ReadValue<Vector2>();
        if (!inputVec.Equals(Vector2.zero))
        {
            playerStateManager.SetPlayerState(PlayerState.Run);
            _animator.SetBool(RunAnimatorIndex, true);
            _physics.velocity = new Vector3(inputVec.x* movementSpeed, _physics.velocity.y , inputVec.y* movementSpeed);
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(inputVec.x, 0, inputVec.y), Vector3.up);
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.fixedTime);
        }
        else
        {
            playerStateManager.SetPlayerState(PlayerState.Idle);
            _animator.SetBool(RunAnimatorIndex, false);
            _physics.velocity = new Vector3(0, _physics.velocity.y , 0);
        }
    }
    

    #endregion


    #region Methods

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash && playerStateManager.CanDashFromState())
        {
            playerStateManager.SetPlayerState(PlayerState.Dash);
            canDash = false;
            StartCoroutine(PerformDash());
        }
    }

    private IEnumerator PerformDash()
    {
        Vector3 direction = transform.forward * dashSpeed;
        direction.y = 0;
        _physics.AddForce(direction, ForceMode.VelocityChange);
        yield return new WaitForSeconds(dashDuration);
        playerStateManager.SetPlayerState(PlayerState.Idle);
        _physics.velocity = Vector3.zero;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    #endregion
    
}
