using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncepad : MonoBehaviour
{
    public Rigidbody2D playerrb2D;
    public float launchforce;
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerrb2D.velocity = Vector2.up * launchforce;
            playerrb2D.AddForce(playerrb2D.transform.up * launchforce, ForceMode2D.Impulse);
        }
    }
}

