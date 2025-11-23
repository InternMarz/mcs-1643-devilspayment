using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerPlatformDrop : MonoBehaviour
{
    [Header("One-Way Platform Settings")]
    [Tooltip("Layer(s) used by one-way platforms the player can jump through.")]
    public LayerMask oneWayPlatformLayer;

    [Tooltip("Maximum time collisions stay disabled while dropping down.")]
    public float maxDropTime = 0.5f;

    private PlayerMovement2D movement;
    private Collider2D playerCollider;
    private bool isDropping = false;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (movement == null || playerCollider == null) return;

        // Use the SAME down key defined on PlayerMovement2D
        if (Input.GetKeyDown(movement.DownKey))
        {
            TryDropThroughPlatformBelow();
        }
    }

    private void TryDropThroughPlatformBelow()
    {
        if (isDropping) return;

        // Small box right under the player's feet
        Bounds b = playerCollider.bounds;
        Vector2 boxCenter = new Vector2(b.center.x, b.min.y - 0.05f);
        Vector2 boxSize = new Vector2(b.size.x * 0.9f, 0.1f);

        Collider2D platform = Physics2D.OverlapBox(boxCenter, boxSize, 0f, oneWayPlatformLayer);
        if (platform != null)
        {
            StartCoroutine(DropThrough(platform));
        }
    }

    private IEnumerator DropThrough(Collider2D platform)
    {
        isDropping = true;

        // Temporarily ignore collision between player and this platform
        Physics2D.IgnoreCollision(playerCollider, platform, true);

        float timer = 0f;
        // Wait until we've passed below the platform OR timed out
        while (timer < maxDropTime)
        {
            timer += Time.deltaTime;

            if (transform.position.y < platform.bounds.min.y - 0.05f)
            {
                break;
            }

            yield return null;
        }

        Physics2D.IgnoreCollision(playerCollider, platform, false);
        isDropping = false;
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the overlap box in-editor
        Collider2D col = GetComponent<Collider2D>();
        if (col == null) return;

        Bounds b = col.bounds;
        Vector2 boxCenter = new Vector2(b.center.x, b.min.y - 0.05f);
        Vector2 boxSize = new Vector2(b.size.x * 0.9f, 0.1f);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(boxCenter, boxSize);
    }
}