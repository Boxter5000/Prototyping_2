using UnityEngine;

public class WallJump : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private PlayerController _playerController;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask wallLayer;
    
    [Header("Wall Jump Variables")] 
    [SerializeField] private float wallJumpForce = 12f;
    [SerializeField] private float pushAwayForce = 5f;
    [SerializeField] private float slideSpeed = 3f;
    [SerializeField] private float wallDelay = 0.1f;

    private float timer;
    private bool CanWallJump => Input.GetButtonDown("Jump") && isTuchingWall;
    
    [Header("Ground Collision Variables")]
    [SerializeField] private float wallRaycastLength;
    public bool isTuchingWall;

    private void Awake()
    {
        rb.GetComponent<Rigidbody2D>();
        _playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (_playerController.canWalljump || _playerController.onGround)
        {
            if (CanWallJump) Jump();
            if (isTuchingWall) _playerController.FallMultiplier(slideSpeed);
        }
    }
    private void FixedUpdate()
    {
        if (_playerController.canWalljump || _playerController.onGround)
        {
            CheckCollisions();
        }
    }
    private void Jump()
    {
        if (_playerController.onGround)
        {
            rb.AddForce(new Vector2(pushAwayForce * _playerController._horizontalDirection * -1f, 1f * wallJumpForce / 1.5f));
        }
        else
        {
            rb.AddForce(new Vector2(pushAwayForce * _playerController._horizontalDirection * -1f, 1f * wallJumpForce));
        }
        
    }
    private void CheckCollisions()
    {
        if(Physics2D.Raycast(transform.position , new Vector2(_playerController._horizontalDirection, 0f), wallRaycastLength, wallLayer))
        {
            isTuchingWall = true;
        }
        else
        {
            Invoke("notTuchingWall", wallDelay);
        }
    }

    private void notTuchingWall()
    {
        isTuchingWall = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 pos = transform.position;
        Gizmos.DrawLine(pos, pos + Vector3.right * wallRaycastLength);
    }
}
