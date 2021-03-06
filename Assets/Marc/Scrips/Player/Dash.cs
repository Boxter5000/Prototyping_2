using UnityEngine;

public class Dash : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerController _playerController;
    private WallJump _wallJump;
    
    [SerializeField]private float dashSpeed;
    [SerializeField]private float startDashTime;
    private float dashTime;
    
    private int direction;
    
    [HideInInspector] public bool isDashing;
    private bool canDash;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _playerController = GetComponent<PlayerController>();
        _wallJump = GetComponent<WallJump>();
        
        dashTime = startDashTime;
    }

    private void Update()
    {
        if (_playerController.canDash && !_wallJump.isTuchingWall)
        {
            if (direction == 0)
            {
                if (_playerController._horizontalDirection < 0f && Input.GetKeyDown(KeyCode.LeftShift))
                {
                    direction = 1;
                    isDashing = true;
                }
                else if (_playerController._horizontalDirection > 0f && Input.GetKeyDown(KeyCode.LeftShift))
                {
                    direction = 2;
                    isDashing = true;
                }
            }
            else
            {
                Debug.Log(rb.velocity);
                if (dashTime <= 0 || _wallJump.isTuchingWall)
                {
                    direction = 0;
                    isDashing = false;
                    canDash = false;
                    dashTime = startDashTime;
                    rb.velocity = Vector2.zero;
                }
                else if(canDash || !_wallJump.isTuchingWall)
                {
                    dashTime -= Time.deltaTime;

                    if (direction == 1)
                    {
                        rb.velocity = Vector2.left * dashSpeed;
                    }
                    else if (direction == 2)
                    {
                        rb.velocity = Vector2.right * dashSpeed;
                    }
                    if (Mathf.Abs(rb.velocity.x) > dashSpeed)
                    {
                        rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * dashSpeed, rb.velocity.y);
                    }
                }
            }
            if (_playerController.onGround || _wallJump.isTuchingWall)
            {
                canDash = true;
            }
        }
    }
}
