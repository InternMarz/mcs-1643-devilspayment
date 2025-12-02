using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerMovement2D : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    [Tooltip("Extra gravity while falling for snappier jumps.")]
    public float fallMultiplier = 2.5f;

    [Tooltip("Extra gravity when releasing the jump early.")]
    public float lowJumpMultiplier = 2f;

    [Header("Input Keys")]
    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode downKey = KeyCode.S; // Shared with the platform script

    [Header("Ground Check")]
    [Tooltip("Empty child object placed at the player's feet.")]
    public Transform groundCheck;

    public float groundCheckRadius = 0.1f;

    [Tooltip("Layers that count as ground (include Ground and OneWayPlatform layers).")]
    public LayerMask groundLayer;

    public bool IsGrounded { get; private set; }

    // Expose the down key for the platform script
    public KeyCode DownKey => downKey;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        groundLayer = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
        ApplyBetterJumpPhysics();
    }

    private void HandleMovement()
    {
        float horizontal = 0f;

        if (Input.GetKey(moveLeftKey))
        {
            horizontal = -1f;
        }
        if (Input.GetKey(moveRightKey))
        {
            horizontal = 1f;
        }

        Vector2 velocity = rb.velocity;
        velocity.x = horizontal * moveSpeed;
        rb.velocity = velocity;
    }

    private void HandleJump()
    {
        // Check for ground using an overlap circle at the player's feet
        IsGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (IsGrounded && Input.GetKeyDown(jumpKey))
        {
            Vector2 velocity = rb.velocity;
            velocity.y = jumpForce;
            rb.velocity = velocity;
        }
    }

    private void ApplyBetterJumpPhysics()
    {
        // Nicer jump feel (optional)
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(jumpKey))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the ground check in the editor
        if (groundCheck == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}