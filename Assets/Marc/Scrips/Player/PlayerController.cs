using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private WallJump _wallJump;
    private SlopeSlide _slopeSlide;
    private Dash _dash;
    private Death _death;
    public Animator animator;

    [Header("Layer Masks")]
    [SerializeField] public LayerMask groundLayer;
    
    
    [Header("Movement Variables")]
    [SerializeField] private float movementAcceleration = 70f;
    [SerializeField] private float maxMoveSpeed = 12f;
    [SerializeField] private float groundLinearDrag = 7f;
    private float horizontalDirection;
    private bool changingDirection => (rb.velocity.x > 0f && horizontalDirection < 0f) || (rb.velocity.x < 0f && horizontalDirection > 0f);
    [HideInInspector] public float _horizontalDirection;


    [Header("Jump Variables")] 
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float airLinearDrag = 2.5f;
    [SerializeField] private float fallMultipier = 8f;
    [SerializeField] private float lowJumpFallMultiplier = 5f;
    [SerializeField] private float groundetDelay = 1f;
    [SerializeField] private float groundetdelay = 0.3f;
    private float _verticalVelocity;
    private bool groundet;
    private int extraJumpsValue;
    private bool isJumping;
    private bool canJump => Input.GetButtonDown("Jump") && (groundet || extraJumpsValue > 0);
    
    
    [Header("Ground Collision Variables")]
    [SerializeField] private float groundRaycastLength;
    [HideInInspector] public bool onGround;

    [Header("Powerups")]
    [SerializeField] private int extraJumps = 0;
    [SerializeField] public bool canWalljump;
    [SerializeField] public bool canDash;
    private bool canDoubbleJump;


    private bool jumpAnimationStart;
    private bool isFalling;
    private bool isDashing;
    
    
    bool isLeft;
    bool isRight;
    Vector3 X;

    private float timer;

    private Vector2 NewForce;
    private Vector2 Debugforce;

    public static PlayerController instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
        
        rb = GetComponent<Rigidbody2D>();
        _wallJump = GetComponent<WallJump>();
        _slopeSlide = GetComponent<SlopeSlide>();
        _dash = GetComponent<Dash>();
        _death = GetComponent<Death>();


        X = transform.localScale;
        isRight = true;
        timer = groundetDelay;
    }
    private void Update()
    {
        _horizontalDirection = GetInput().x;
        _verticalVelocity = rb.velocity.y;

        if (Input.GetButtonDown("Jump"))
        {
            jumpAnimationStart = true;
        }
        else
        {
            jumpAnimationStart = false;
        }
        if (!Input.GetButton("Jump"))
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isDashing = true;
        }
        else
        {
            isDashing = false;
        }

        if (_wallJump.isTuchingWall)
        {
            animator.SetTrigger("isTuchingWall");
        }
        
        animator.SetBool("isFalling", isFalling);
        animator.SetBool("Jumped", jumpAnimationStart);
        animator.SetFloat("JumpVelocity", _verticalVelocity);
        animator.SetBool("isGroundet", Physics2D.Raycast(transform.position , Vector2.down, groundRaycastLength, groundLayer));
        if (_dash.isDashing)
        {
            animator.SetBool("isDashing", isDashing);
        }
        if (canJump) Jump();
        
        GroundetDelay();
        FlipCharacter();
    }
    private void FixedUpdate()
    {
        CheckCollisions();
        MoveCharacter();
        if (onGround )
        {
            extraJumpsValue = extraJumps;
            ApplyGroundLinearDrag();
        }
        else if(!onGround && !_wallJump.isTuchingWall)
        {
            ApplyAirLinearDrag();
            FallMultiplier(fallMultipier);
        }
        else if (_wallJump.isTuchingWall)
        {
            extraJumpsValue = extraJumps;
        }
    }
    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    private void MoveCharacter()
    {
        if (!_death.hasDied)
        {
            if (onGround && !_slopeSlide.isOnSlope && !isJumping)
            {
                rb.AddForce(new Vector2(_horizontalDirection, 0.0f) * (movementAcceleration ));
            }
            else if (onGround && _slopeSlide.isOnSlope && !isJumping)
            {
                rb.velocity = new Vector2( maxMoveSpeed * _slopeSlide.slopeNormalPerp.x * -_horizontalDirection ,maxMoveSpeed * _slopeSlide.slopeNormalPerp.y * -_horizontalDirection);
                
                Debugforce = new Vector2(_slopeSlide.slopeNormalPerp.x * -_horizontalDirection, _slopeSlide.slopeNormalPerp.x * -_horizontalDirection) * movementAcceleration;
            
            }
            else if (!onGround)
            {
                rb.AddForce(new Vector2(_horizontalDirection, 0.0f) * (movementAcceleration));
            }

            if (Mathf.Abs(rb.velocity.x) > maxMoveSpeed && !_dash.isDashing)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxMoveSpeed , rb.velocity.y);
            }
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
            
            animator.SetTrigger("Jumped");
            
            rb.velocity = new Vector2(rb.velocity.x, 0f);

            if (Mathf.Abs(_horizontalDirection) <= 0.0f && extraJumpsValue == 1 && canDoubbleJump)
            {
                rb.AddForce(Vector2.up * (maxMoveSpeed * jumpForce / 4) , ForceMode2D.Impulse);
            }
            else if (Mathf.Abs(_horizontalDirection) <= 0.0f && extraJumpsValue == 0 && canDoubbleJump)
            {
                rb.AddForce(Vector2.up * (maxMoveSpeed * jumpForce / 8) , ForceMode2D.Impulse);
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
        if (Physics2D.Raycast(transform.position, Vector2.down, groundRaycastLength, groundLayer))
        {
            onGround = true;
        }
        else
        {
            StartCoroutine("groundDelay");
        }
        if (onGround)
        {
            timer = groundetDelay + Time.time;
            groundet = true;
        }
        
        if (rb.velocity.y <= 0.0f)
        {
            isJumping = false;
        }
    }
    private void FlipCharacter()
    {
        if(!isLeft && (Input.GetKeyDown (KeyCode.A)||Input.GetKeyDown (KeyCode.LeftArrow))){
            isRight=false;
            isLeft=true;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (!isRight &&(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            isRight = true;
            isLeft = false;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }
    private void GroundetDelay()
    {
        if (!onGround)
        {
            if (timer >= 0f)
            {
                groundet = true;
                timer -= Time.time;
            }
            else
            {
                groundet = false;
            }
        }

    }

    IEnumerator groundDelay()
    {
        yield return new WaitForSeconds(groundetdelay);
        onGround = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 pos = transform.position;
        Gizmos.DrawLine(pos, pos + Vector3.down * groundRaycastLength);
        
        Gizmos.DrawLine(pos, pos + (Vector3)Debugforce);
    }
    public void SetWalljumpActive()
    {
        canWalljump = true;
    }
    public void SetDoubleJumpActive()
    {
        canDoubbleJump = true;
        extraJumps = 1;
    }
    public void SetDashActive()
    {
        canDash = true;
    }
}