using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Speed of the projectile.")]
    public float speed = 10f;

    [Header("Lifetime")]
    [Tooltip("Maximum distance this projectile can travel before being destroyed.")]
    public float maxTravelDistance = 20f;

    [Header("Collision")]
    [Tooltip("Tag of the object that destroys this projectile when hit.")]
    public string playerTag = "Player";

    private Vector3 spawnPosition;

    private void Start()
    {
        // Remember where the projectile started
        spawnPosition = transform.position;
    }

    private void Update()
    {
        // Move forward every frame (works for 2D or 3D)
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Destroy when it has traveled far enough
        float distanceTraveled = Vector3.Distance(spawnPosition, transform.position);
        if (distanceTraveled >= maxTravelDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 3D collision (for 3D projects)
        if (other.CompareTag(playerTag))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 2D collision (for 2D projects)
        if (other.CompareTag(playerTag))
        {
            Destroy(gameObject);
        }
    }
}
