using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    
    
    #region Inspector
            
    [SerializeField] private  PlayerMovement playerMovement;
    
    [Header("Attack Stats")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 3;
    [SerializeField] private float attackDamage = 50;
    [SerializeField] private LayerMask attackedLayers;
    [SerializeField] private float attackForce = 2;
    
    [Header("Combo Stats")]
    [SerializeField] private int ComboLength = 3;
    [SerializeField] private float AttackCooldown = 0.3f;
    [SerializeField] private float AfterComboCooldow = 0.7f;
    [SerializeField] private float MidComboResetTime = 1;

    [Header("Debug")] 
    [SerializeField] private bool DrawAttackGizmo;
    
    #endregion
    
    
    #region Fields
        
    private int curComboAttack;
    private float lastAttackTime, nextAttackTime;
    private float _health;
    private PlayerInputActions _playerInputActions;
    private Animator _animator;
    private Rigidbody _physics;
    private static readonly int ComboAttackAnimatorIndex = Animator.StringToHash("ComboAttack");
    private static readonly int AttackAnimatorIndex = Animator.StringToHash("Attack");
    public PlayerStateManager playerStateManager;
    public PlayerState _playerState;

    #endregion
    
    
    #region MonoBehaviour
    
    private void Start()
    {
        _physics = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        curComboAttack = 0;
        _playerInputActions = GetComponent<PlayerMovement>()._playerInputActions;
        _playerInputActions.Player.Attack.performed += Attack;
        lastAttackTime = 0;
        nextAttackTime = 0;
        playerStateManager = GetComponent<PlayerMovement>().playerStateManager;
    }

    private void Update()
    {
        _playerState = playerStateManager.GetPlayerState();
        if (curComboAttack > 0 && Time.time > lastAttackTime + MidComboResetTime)
        {
            playerMovement.isDuringAttack = false;
            curComboAttack = 0;
            _animator.SetInteger(ComboAttackAnimatorIndex, curComboAttack);
            playerStateManager.SetPlayerState(PlayerState.Idle);
        }
    }
        
    #endregion
    
    
    #region Methods
               
    private void Attack(InputAction.CallbackContext context)
    {
        if (Time.time >= nextAttackTime && playerStateManager.CanAttackFromState())
        {
            playerStateManager.SetPlayerState(PlayerState.Attack);
            _physics.velocity = transform.forward * attackForce;

            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, attackedLayers);
            foreach (Collider enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().ReceiveDamage(attackDamage);
            }

            curComboAttack++;
            
            //start attack animation
            _animator.SetTrigger(AttackAnimatorIndex);
            _animator.SetInteger(ComboAttackAnimatorIndex, curComboAttack);
            
            if (curComboAttack == ComboLength)
            {
                playerStateManager.SetPlayerState(PlayerState.Idle);
                nextAttackTime = Time.time + AfterComboCooldow;
                playerMovement.isDuringAttack = false;
            }
            else
            {
                nextAttackTime = Time.time + AttackCooldown;
            }

            lastAttackTime = Time.time;
        }
        
    }
    
    private void OnDrawGizmosSelected()
    {
        if (DrawAttackGizmo)
        {
            Gizmos.DrawSphere(attackPoint.position,attackRange);
        }
    }
    
    #endregion

    

    

    

    
    
    

    
    

    
}
