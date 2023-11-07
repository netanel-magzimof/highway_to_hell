using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    #region Inspector
    public PlayerState playerState;

    [Header("Dash Stats")]
    [SerializeField] private float dashSpeed = 30;
    [SerializeField] private float dashDuration = 0.1f;
    [SerializeField] private float dashCooldown = 1.3f;

    
    [Header("Movement Stats")]
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float rotationSpeed = 5;
    #endregion



    #region Fields
    public bool isDuringDash, canDash, isDuringAttack;
    private Rigidbody _physics;
    private Animator _animator;
    public PlayerInputActions _playerInputActions;
    #endregion



    #region MonoBehaviour
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _physics = GetComponent<Rigidbody>();
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Dash.performed += Dash;
        isDuringDash = false;
        canDash = true;
    }
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            _playerInputActions.Player.Disable();
            _playerInputActions.UI.Enable();
        }
    }
    private void FixedUpdate()
    {
        if (isDuringDash || isDuringAttack) return;
        
        
        Vector2 inputVec = _playerInputActions.Player.Movement.ReadValue<Vector2>();
        if (!inputVec.Equals(Vector2.zero))
        {
            _animator.SetBool("Run", true);
            _physics.velocity = new Vector3(inputVec.x* movementSpeed, _physics.velocity.y , inputVec.y* movementSpeed);
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(inputVec.x, 0, inputVec.y), Vector3.up);
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.fixedTime);
        }
        else
        {
            _animator.SetBool("Run", false);
            _physics.velocity = new Vector3(0, _physics.velocity.y , 0);
        }
    }
    

    #endregion


    #region Methods

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            isDuringDash = true;
            canDash = false;
            StartCoroutine(PerformDash());
            // _physics.AddForce(Vector3.up*jumpSpeed, ForceMode.Impulse);
        }
    }

    private IEnumerator PerformDash()
    {
        float timeLeft = dashDuration;
        // while (timeLeft > 0)
        // {
        Vector3 direction = transform.forward * dashSpeed;
        direction.y = 0;
        // direction.y = _physics.velocity.y;
        _physics.AddForce(direction, ForceMode.VelocityChange);
        // timeLeft -= Time.deltaTime;
        // yield return null;
            
        // }
        yield return new WaitForSeconds(dashDuration);
        isDuringDash = false;
        _physics.velocity = Vector3.zero;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    #endregion
    
}
