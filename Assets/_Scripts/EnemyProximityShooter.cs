using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProximityShooter : MonoBehaviour
{
    [Header("Projectile Settings")]
    [Tooltip("Projectile prefab that this enemy will shoot (should have EnemyProjectile script).")]
    public GameObject ProjectilePrefab;

    [Tooltip("Point from which projectiles are spawned (e.g., a child transform at the gun muzzle).")]
    public Transform AttackPoint;

    [Tooltip("Scale applied to the spawned projectile.")]
    public float ProjScale = 1.0f;

    [Tooltip("How many shots this enemy can fire. Set to a negative number for infinite ammo.")]
    public int Ammo = 100;

    [Header("Target & Firing")]
    [Tooltip("Tag of the player object.")]
    public string playerTag = "Player";

    [Tooltip("Maximum distance at which this enemy will start shooting at the player.")]
    public float detectionRange = 15f;

    [Tooltip("Time in seconds between shots.")]
    public float timeBetweenShots = 1.5f;

    private Transform player;
    private float shotTimer = 0f;

    private void Start()
    {
        // Try to auto-find the player by tag
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning($"EnemyProximityShooter on '{name}' could not find a GameObject with tag '{playerTag}'.");
        }
    }

    private void Update()
    {
        if (player == null || ProjectilePrefab == null || AttackPoint == null)
            return;

        // Countdown to next shot
        if (shotTimer > 0f)
        {
            shotTimer -= Time.deltaTime;
        }

        // Check distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If player is close enough AND cooldown ready AND we still have ammo
        if (distanceToPlayer <= detectionRange && shotTimer <= 0f && (Ammo != 0))
        {
            ShootAtPlayer();
            shotTimer = timeBetweenShots;
        }
    }

    private void ShootAtPlayer()
    {
        // If Ammo is positive, use one bullet. If Ammo < 0, treat as infinite.
        if (Ammo > 0)
        {
            Ammo--;
        }

        // Aim from AttackPoint towards the player
        Vector3 direction = (player.position - AttackPoint.position).normalized;

        // Create a rotation that looks along this direction
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

        // Instantiate projectile
        GameObject proj = Instantiate(ProjectilePrefab, AttackPoint.position, rotation);

        // Apply scale if desired
        proj.transform.localScale = new Vector3(ProjScale, ProjScale, ProjScale);
    }
}
