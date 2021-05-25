using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private float vertical;
    private float speed = 8;
    private bool isLadder;
    private bool isClimbing;
    [SerializeField] private Rigidbody2D rb;

    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        if (isLadder && Mathf.Abs(vertical) >0f)
        {
            isClimbing = true;
            rb.velocity = new Vector2(rb.position.x, vertical * speed);
        }

        else
        {
            rb.gravityScale = 1f;
        }
    }

    void FixedUpdate()
    {
        if(isClimbing)
        {
            rb.gravityScale = 0f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag ("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag ("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }
}
