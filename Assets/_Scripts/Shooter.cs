using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Projectile Settings")]
    [Tooltip("The projectile prefab to shoot.")]
    public GameObject projectilePrefab;

    [Tooltip("Where the projectile spawns from.")]
    public Transform firePoint;

    [Header("Fire Rate")]
    [Tooltip("Time in seconds between each shot.")]
    public float timeBetweenShots = 0.5f;

    private float shotTimer = 0f;

    private void Update()
    {
        if (projectilePrefab == null || firePoint == null)
            return;

        shotTimer -= Time.deltaTime;

        if (shotTimer <= 0f)
        {
            Shoot();
            shotTimer = timeBetweenShots;
        }
    }

    private void Shoot()
    {
        // Spawn the projectile at firePoint position and rotation
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}