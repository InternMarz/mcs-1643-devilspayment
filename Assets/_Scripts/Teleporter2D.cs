using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Teleporter2D : MonoBehaviour
{
    [Header("Teleporter Pairing")]
    [Tooltip("Tag shared by BOTH teleporters in this pair (e.g. 'TP_Pair1').")]
    public string pairTag = "TP_Pair1";

    [Header("Teleport Target")]
    [Tooltip("Tag of the object that will be teleported (e.g. 'Player').")]
    public string teleportTargetTag = "Player";

    [Tooltip("Offset from the destination teleporter's position.")]
    public Vector2 exitOffset = new Vector2(0f, 1f);

    private Transform otherEnd;

    private void Awake()
    {
        // Make sure this collider is a trigger
        Collider2D col = GetComponent<Collider2D>();
        if (col != null && !col.isTrigger)
        {
            col.isTrigger = true;
        }
    }

    private void Start()
    {
        CacheOtherEnd();
    }

    private void CacheOtherEnd()
    {
        GameObject[] teleporters = GameObject.FindGameObjectsWithTag(pairTag);

        if (teleporters.Length != 2)
        {
            Debug.LogError(
                $"Teleporter2D on '{name}': Expected exactly 2 objects with tag '{pairTag}', " +
                $"but found {teleporters.Length}. Check your teleporter setup."
            );
            return;
        }

        // whichever one is not this object is the other end
        if (teleporters[0] == gameObject)
            otherEnd = teleporters[1].transform;
        else
            otherEnd = teleporters[0].transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (otherEnd == null) return;

        // Only teleport objects with the desired tag
        if (!other.CompareTag(teleportTargetTag)) return;

        // Teleport the object to the paired teleporter plus an offset
        Vector3 targetPosition = otherEnd.position + (Vector3)exitOffset;
        other.transform.position = targetPosition;
    }
}
