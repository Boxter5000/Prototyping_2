using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]private float dashSpeed;
    private float dashTime;
    [SerializeField]private float startDashTime;
    private int direction;
    public bool isDashing;
    private bool canDash;
    private PlayerController _playerController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _playerController = GetComponent<PlayerController>();
        dashTime = startDashTime;
    }

    private void Update()
    {
        if (direction == 0)
        {
            if (Input.GetAxisRaw("Horizontal") < 0f && Input.GetKeyDown(KeyCode.LeftShift))
            {
                direction = 1;
                isDashing = true;
            }
            else if (Input.GetAxisRaw("Horizontal") > 0f && Input.GetKeyDown(KeyCode.LeftShift))
            {
                direction = 2;
                isDashing = true;
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                direction = 0;
                isDashing = false;
                canDash = false;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
            }
            else if(canDash)
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
            }
        }

        if (_playerController.onGround)
        {
            canDash = true;
        }
    }
}
