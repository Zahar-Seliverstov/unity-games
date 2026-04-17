using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float jumpForce = 14f;

    private Rigidbody2D rb;
    private BoxCollider2D col;
    private bool isGrounded;
    private bool jumpPressed;
    private float horizontalInput;
    private Vector3 startPosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        horizontalInput = 0f;
        if (keyboard.aKey.isPressed) horizontalInput = -1f;
        if (keyboard.dKey.isPressed) horizontalInput = 1f;

        if (keyboard.spaceKey.wasPressedThisFrame && isGrounded)
            jumpPressed = true;
    }

    void FixedUpdate()
    {
        CheckGrounded();

        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        if (jumpPressed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpPressed = false;
        }

        if (transform.position.y < -12f)
        {
            transform.position = startPosition;
            rb.linearVelocity = Vector2.zero;
        }
    }

    void CheckGrounded()
    {
        var bounds = col.bounds;
        var center = new Vector2(bounds.center.x, bounds.min.y - 0.02f);
        var size = new Vector2(bounds.size.x * 0.85f, 0.08f);
        var filter = new ContactFilter2D { useTriggers = false, useLayerMask = false };
        var results = new List<Collider2D>();
        Physics2D.OverlapBox(center, size, 0f, filter, results);

        isGrounded = false;
        foreach (var h in results)
        {
            if (h.gameObject != gameObject)
            {
                isGrounded = true;
                break;
            }
        }
    }
}
