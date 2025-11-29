using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health Settings")]
    [Tooltip("How many hits the player can take.")]
    public int maxHP = 3;

    [Tooltip("Tag used by enemy attack projectiles or hitboxes.")]
    public string enemyAttackTag = "EnemyAttack";

    [Header("Life Objects (Destroyed per hit)")]
    [Tooltip("Assign 3 objects that represent the player's remaining lives.")]
    public GameObject[] lifeObjects; // Should contain 3 elements

    private int currentHP;

    private void Awake()
    {
        currentHP = maxHP;

        // Safety check to avoid missing setup
        if (lifeObjects.Length == 0)
        {
            Debug.LogWarning("No life objects assigned in PlayerHealth.");
        }
    }

    /// <summary>
    /// Damages the player by the given amount.
    /// </summary>
    public void TakeDamage(int amount = 1)
    {
        currentHP -= amount;

        // Destroy corresponding life object (index = remaining lives)
        int lifeIndex = currentHP;

        if (lifeIndex >= 0 && lifeIndex < lifeObjects.Length)
        {
            if (lifeObjects[lifeIndex] != null)
            {
                Destroy(lifeObjects[lifeIndex]);
            }
        }

        // If HP reaches zero → player dies
        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died.");
        Destroy(gameObject); // You can replace this with respawn or death animation logic
    }

    // 3D Trigger Collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyAttackTag))
        {
            TakeDamage(1);
            Destroy(other.gameObject); // Remove the enemy attack after hit
        }
    }

    // 2D Trigger Collision
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(enemyAttackTag))
        {
            TakeDamage(1);
            Destroy(other.gameObject); // Remove the enemy attack after hit
        }
    }
}

