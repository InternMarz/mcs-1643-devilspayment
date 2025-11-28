using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Speed of the projectile.")]
    public float speed = 15f;

    [Header("Lifetime")]
    [Tooltip("Maximum distance before this projectile is destroyed.")]
    public float maxTravelDistance = 25f;

    [Header("Collision")]
    [Tooltip("Tag of objects considered enemies.")]
    public string enemyTag = "Enemy";

    private Vector3 spawnPosition;

    private void Start()
    {
        // Remember where the projectile started
        spawnPosition = transform.position;
    }

    private void Update()
    {
        // Move forward (works for 2D or 3D as long as your firePoint faces the right way)
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Destroy when distance exceeded
        float traveled = Vector3.Distance(spawnPosition, transform.position);
        if (traveled >= maxTravelDistance)
        {
            Destroy(gameObject);
        }
    }

    // 3D trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            Destroy(gameObject);
        }
    }

    // 2D trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(enemyTag))
        {
            Destroy(gameObject);
        }
    }
}