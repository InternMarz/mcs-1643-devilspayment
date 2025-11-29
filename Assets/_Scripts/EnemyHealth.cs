using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [Tooltip("How many hits this enemy can take.")]
    public int maxHP = 3;

    [Tooltip("Tag of the player's attack/projectile.")]
    public string playerAttackTag = "PlayerProjectile";

    [Tooltip("Destroy the attack object when it hits this enemy.")]
    public bool destroyAttackOnHit = true;

    private int currentHP;

    private void Awake()
    {
        currentHP = maxHP;
    }

    /// <summary>
    /// Call this to damage this specific enemy.
    /// </summary>
    /// <param name="amount">Amount of damage (hits) to apply.</param>
    public void TakeDamage(int amount = 1)
    {
        currentHP -= amount;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Destroy THIS enemy object only
        Destroy(gameObject);
    }

    // 3D trigger collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerAttackTag))
        {
            TakeDamage(1);

            if (destroyAttackOnHit)
            {
                Destroy(other.gameObject);
            }
        }
    }

    // 2D trigger collision
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerAttackTag))
        {
            TakeDamage(1);

            if (destroyAttackOnHit)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
