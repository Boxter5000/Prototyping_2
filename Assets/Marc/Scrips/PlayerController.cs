using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private Rigidbody2D rb;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask groundLayer;
    
    [Header("Movement Variables")]
    [SerializeField] private float movementAcceleration = 70f;
    [SerializeField] private float maxMoveSpeed = 12f;
    [SerializeField] private float groundLinearDrag = 7f;
    private float _horizontalDirection;
    private bool ChangingDirection => (rb.velocity.x > 0f && _horizontalDirection < 0f) || (rb.velocity.x < 0f && _horizontalDirection > 0f);

    [Header("Jump Variables")] 
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float airLinearDrag = 2.5f;
    [SerializeField] private float fallMultipier = 8f;
    [SerializeField] private float lowJumpFallMultiplier = 5f;
    [SerializeField] private int extraJumps;
    private int _extraJumpsValue;
    private bool CanJump => Input.GetButtonDown("Jump") && (_onGround || _extraJumpsValue > 0);

    [Header("Ground Collision Variables")]
    [SerializeField] private float groundRaycastLength;
    private bool _onGround;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        _horizontalDirection = GetInput().x;
        if (CanJump) Jump();
    }
    private void FixedUpdate()
    {
        
        CheckCollisions();
        MoveCharacter();
        if (_onGround)
        {
            _extraJumpsValue = extraJumps;
            ApplyGroundLinearDrag();
        }
        else
        {
            ApplyAirLinearDrag();
            FallMultiplier();
        }
    }
    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    private void MoveCharacter()
    { 
        rb.AddForce(new Vector2(_horizontalDirection, 0f) * movementAcceleration);
        if (Mathf.Abs(rb.velocity.x) > maxMoveSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxMoveSpeed, rb.velocity.y);
        }
    }
    private void ApplyGroundLinearDrag()
    {
        if (Mathf.Abs(_horizontalDirection) < 0.4f || ChangingDirection)
        {
            rb.drag = groundLinearDrag;
        }
        else
        {
            rb.drag = 0f;
        }
    }
    private void ApplyAirLinearDrag()
    {
        rb.drag = airLinearDrag;
    }
    private void Jump()
    {
        if (!_onGround)
        {
            _extraJumpsValue--;
        }
        
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    private void FallMultiplier()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallMultipier;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowJumpFallMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }
    private void CheckCollisions()
    {
        _onGround = Physics2D.Raycast(transform.position , Vector2.down, groundRaycastLength, groundLayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 pos = transform.position;
        Gizmos.DrawLine(pos, pos + Vector3.down * groundRaycastLength);
    }
}
