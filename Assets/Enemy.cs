using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float FullHealth = 100;
    
    private float currentHealth;

    private void Start()
    {
        currentHealth = FullHealth;
    }

    public void ReceiveDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //TODO add GameManager behaviour for when enemy dies
        //TODO switch to pool if there is time
        //TODO add animation
        Destroy(gameObject);
    }
}
