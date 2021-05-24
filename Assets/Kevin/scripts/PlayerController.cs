using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private Rigidbody2D rb;
    private WallJump _wallJump;
    private SlopeSlide _slopeSlide;
    private Dash _dash;
    private Death _death;

    [Header("Layer Masks")]
    [SerializeField] public LayerMask groundLayer;
    
    [Header("Movement Variables")]
    [SerializeField] private float movementAcceleration = 70f;
    [SerializeField] private float maxMoveSpeed = 12f;
    [SerializeField] private float groundLinearDrag = 7f;
    private float horizontalDirection;
    private bool changingDirection => (rb.velocity.x > 0f && horizontalDirection < 0f) || (rb.velocity.x < 0f && horizontalDirection > 0f);
    [HideInInspector] public float _horizontalDirection;
    private bool ChangingDirection => (rb.velocity.x > 0f && _horizontalDirection < 0f) || (rb.velocity.x < 0f && _horizontalDirection > 0f);

    [Header("Jump Variables")] 
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float airLinearDrag = 2.5f;
    [SerializeField] private float fallMultipier = 8f;
    [SerializeField] private float lowJumpFallMultiplier = 5f;
    [SerializeField] private int extraJumps = 0;
    private int extraJumpsValue;
    private bool isJumping;
    private bool canJump => Input.GetButtonDown("Jump") && (onGround || extraJumpsValue > 0) && !isJumping;
    [Header("Ground Collision Variables")]
    [SerializeField] private float groundRaycastLength;
    public bool onGround;

    private Vector2 NewForce;
    private Vector2 Debugforce;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _wallJump = GetComponent<WallJump>();
        _slopeSlide = GetComponent<SlopeSlide>();
        _dash = GetComponent<Dash>();
        _death = GetComponent<Death>();
    }
    private void Update()
    {
        _horizontalDirection = GetInput().x;
        if (canJump) Jump();
    }
    private void FixedUpdate()
    {
        CheckCollisions();
        MoveCharacter();
        if (onGround)
        {
            extraJumpsValue = extraJumps;
            ApplyGroundLinearDrag();
        }
        else if(!onGround && !_wallJump.isTuchingWall)
        {
            ApplyAirLinearDrag();
            FallMultiplier(fallMultipier);
        }
    }
    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    private void MoveCharacter()
    {
        //rb.AddForce(new Vector2(_horizontalDirection, 0f) * movementAcceleration);
            
        
        if (onGround && !_slopeSlide.isOnSlope && !isJumping)
        {
            rb.AddForce(new Vector2(_horizontalDirection, 0.0f) * movementAcceleration);
        }
        else if (onGround && _slopeSlide.isOnSlope && !isJumping)
        {
            NewForce = new Vector2( maxMoveSpeed * _slopeSlide.slopeNormalPerp.x * -_horizontalDirection,maxMoveSpeed * _slopeSlide.slopeNormalPerp.y * -_horizontalDirection);
            rb.velocity = NewForce;
            
            Debugforce = new Vector2(_slopeSlide.slopeNormalPerp.x * -_horizontalDirection, _slopeSlide.slopeNormalPerp.x * -_horizontalDirection) * movementAcceleration;
            
        }
        else if (!onGround)
        {
            rb.AddForce(new Vector2(_horizontalDirection, 0.0f) * movementAcceleration);
        }

        if (Mathf.Abs(rb.velocity.x) > maxMoveSpeed && !_dash.isDashing)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxMoveSpeed, rb.velocity.y);
        }
    }
    private void ApplyGroundLinearDrag()
    {
        if (Mathf.Abs(_horizontalDirection) < 0.4f || changingDirection)
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
        isJumping = true;
        if (!_wallJump.isTuchingWall)
        {
            if (!onGround)
            {
                extraJumpsValue--;
            }
            
            rb.velocity = new Vector2(rb.velocity.x, 0f);

            if (Mathf.Abs(_horizontalDirection) <= 0.0f)
            {
                rb.AddForce(Vector2.up * (maxMoveSpeed * jumpForce / 4) , ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
    public void FallMultiplier(float downForce)
    {
        if (!_death.hasDied)
        {
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = downForce;
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
    }
    private void CheckCollisions()
    {
        onGround = Physics2D.Raycast(transform.position , Vector2.down, groundRaycastLength, groundLayer);

        if (rb.velocity.y <= 0.0f)
        {
            isJumping = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 pos = transform.position;
        Gizmos.DrawLine(pos, pos + Vector3.down * groundRaycastLength);
        
        Gizmos.DrawLine(pos, pos + (Vector3)Debugforce);
    }
}