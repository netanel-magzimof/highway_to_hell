using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    [Header("Attack Stats")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 3;
    [SerializeField] private float attackDamage = 50;
    [SerializeField] private LayerMask attackedLayers;
    // [SerializeField] private float FullHealth = 100;

    [Header("Combo Stats")] 
    [SerializeField] private int ComboLength = 3;
    [SerializeField] private float AttackCooldown = 0.3f;
    [SerializeField] private float AfterComboCooldow = 0.7f;
    [SerializeField] private float MidComboResetTime = 1; 

    private int curComboAttack;
    private float lastAttackTime, nextAttackTime;
    private float _health;
    private PlayerInputActions _playerInputActions;
    
    private void Start()
    {
        // _health = FullHealth;
        curComboAttack = 0;
        _playerInputActions = GetComponent<PlayerMovement>()._playerInputActions;
        _playerInputActions.Player.Attack.performed += Attack;
        lastAttackTime = 0;
        nextAttackTime = 0;
    }

    private void Update()
    {
        if (curComboAttack > 0 && Time.time > lastAttackTime + MidComboResetTime)
        {
            Debug.Log("stopped mid combo");
            curComboAttack = 0;
            //TODO change animation to idle no attack
        }
        
    }

    private void Attack(InputAction.CallbackContext context)
    {
        
        if (Time.time >= nextAttackTime)
        {
            //start attack animation
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, attackedLayers);
            foreach (Collider enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().ReceiveDamage(attackDamage);
            }

            curComboAttack++;
            //TODO play right combo attack
            Debug.Log("Combo attack number " + curComboAttack);
            if (curComboAttack == ComboLength)
            {
                nextAttackTime = Time.time + AfterComboCooldow;
                curComboAttack = 0;
            }
            else
            {
                nextAttackTime = Time.time + AttackCooldown;
            }

            lastAttackTime = Time.time;
        }
        
    }
    

    // private void OnDrawGizmosSelected()
    // {
        // Gizmos.DrawSphere(attackPoint.position,attackRange);
    // }
}
