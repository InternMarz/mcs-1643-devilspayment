using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAbility : MonoBehaviour
{
    [Header("Shield Settings")]
    public KeyCode shieldKey = KeyCode.LeftShift; // The key to activate shield
    public GameObject shieldVisual;              // Prefab or sprite that shows in front of player
    public float normalSpeed = 1f;

    private float currentSpeed;
    private bool shieldActive = false;

    void Update()
    {
        HandleShield();
    }

    void HandleShield()
    {
        if (Input.GetKey(shieldKey))
        {
            ActivateShield();
        }
        else
        {
            DeactivateShield();
        }
    }
    public void ActivateShield()
    {
        if (!shieldActive)
        {
            shieldActive = true;

            // Lower player speed
            currentSpeed = normalSpeed * 0.5f;

            // Show shield in front of player
            if (shieldVisual != null)
                shieldVisual.SetActive(true);
        }
    }

    public void DeactivateShield()
    {
        if (shieldActive)
        {
            shieldActive = false;

            // Restore speed
            currentSpeed = normalSpeed;

            // Hide shield
            if (shieldVisual != null)
                shieldVisual.SetActive(false);
        }
    }
}

