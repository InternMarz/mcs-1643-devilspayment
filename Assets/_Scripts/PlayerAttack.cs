using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Projectile Settings")]
    [Tooltip("Projectile prefab to spawn when attacking.")]
    public GameObject projectilePrefab;

    [Tooltip("Where the projectile spawns from (e.g., the player's hand or weapon).")]
    public Transform firePoint;

    [Header("Attack Input")]
    [Tooltip("Key used to fire the projectile.")]
    public KeyCode attackKey = KeyCode.Mouse0; // left mouse by default

    [Header("Attack Rate")]
    [Tooltip("Minimum time between shots (seconds). Set to 0 for no cooldown.")]
    public float timeBetweenShots = 0.25f;

    private float shotTimer = 0f;

    private void Update()
    {
        if (projectilePrefab == null || firePoint == null)
            return;

        // Count down cooldown
        if (shotTimer > 0f)
            shotTimer -= Time.deltaTime;

        // Fire when button pressed and cooldown is ready
        if (Input.GetKeyDown(attackKey) && shotTimer <= 0f)
        {
            Shoot();
            shotTimer = timeBetweenShots;
        }
    }

    private void Shoot()
    {
        // Spawn projectile at firePoint position/rotation
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}
