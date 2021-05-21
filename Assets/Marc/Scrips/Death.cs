using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Death : MonoBehaviour
{
    [SerializeField] private string deathOnTuch;
    [SerializeField] private string respawnTag;
    [SerializeField] private float transitionSpeed;

    private float timeeStartedLerping;
    private bool hasDied;

    private Vector2 lastCheckpoint;
    private Vector2 deathPos;

    private CapsuleCollider2D cc;

    private void Awake()
    {
        cc = GetComponent<CapsuleCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag(deathOnTuch))
        {
            deathPos = transform.position;
            cc.isTrigger = true;
            hasDied = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(respawnTag))
        {
            lastCheckpoint = other.gameObject.transform.position;
        }
    }
    private void Update()
    {
        if (hasDied)
        {
            transform.position = lastCheckpoint;
            //Vector2.MoveTowards(transform.position, lastCheckpoint, transitionSpeed);

            cc.isTrigger = false;
            hasDied = false;
        }
    }

}
