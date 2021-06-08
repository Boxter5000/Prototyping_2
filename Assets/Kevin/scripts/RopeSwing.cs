using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSwing : MonoBehaviour
{
    public Player _player;
    public Collider2D[] myColliders;
    public Collider2D[] myColliderssecond;
    public float timer;
    public float timersecond;
    public bool attached = false;
    public Transform attachedtoTransform;
    public HingeJoint2D hj;
    public Rigidbody2D rb;
    [SerializeField] private float pushforce = 7f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hj = GetComponent<HingeJoint2D>();
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if(attached)
        {
            GetComponent<Player>().extraJumpsValue = +1;
            GetComponent<Player>().isJumping = true;
        }

        if(timer > 0f && attachedtoTransform == null || GetComponent<Player>().onGround == true && timer > 0f)
        {
            
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                foreach (Collider2D bc in myColliders) bc.enabled = true;
            }
        }
        
        if(timersecond > 0f && attachedtoTransform == null || GetComponent<Player>().onGround == true && timersecond > 0f)
        {
            
            timersecond -= Time.deltaTime;
            if (timersecond <= 0f)
            {
                foreach (Collider2D bc in myColliderssecond) bc.enabled = true;
            }
        }
    }

    private void FixedUpdate()
    {
        CheckInputs();
    }

    void CheckInputs()
    {
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            if (attached)
            {
                rb.AddRelativeForce(new Vector3(-1, 0, 0) * pushforce);
            }
        } 
        
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            if (attached)
            {
                rb.AddRelativeForce(new Vector3(1, 0, 0) * pushforce);
            }
        }

        if (Input.GetKey("e"))
        {
            Detach();
        }
    }

    void Attach(Rigidbody2D ropeBone)
    {
        ropeBone.gameObject.GetComponent<RopeSegment>().isPlayerattached = true;
        hj.connectedBody = ropeBone;
        hj.enabled = true;
        attached = true;
        attachedtoTransform = ropeBone.gameObject.transform.parent;
    } 
    
    void Detach()
    {
        hj.connectedBody.gameObject.GetComponent<RopeSegment>().isPlayerattached = false;
        attached = false;
        hj.enabled = false;
        hj.connectedBody = null;
        attachedtoTransform = null;
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!attached)
        {
            if (collision.gameObject.tag == "Rope")
            {
                if (attachedtoTransform != collision.gameObject.transform.parent)
                {
                    if (collision.gameObject.transform.parent)
                    {
                        Attach(collision.gameObject.GetComponent<Rigidbody2D>());
                        foreach (Collider2D bc in myColliders) bc.enabled = false;
                        timer = 1f;
                    }
                }
            }
            
            if (collision.gameObject.tag == "Ropesecond")
            {
                if (attachedtoTransform != collision.gameObject.transform.parent)
                {
                    if (collision.gameObject.transform.parent)
                    {
                        Attach(collision.gameObject.GetComponent<Rigidbody2D>());
                        foreach (Collider2D bc in myColliderssecond) bc.enabled = false;
                        timersecond = 1f;
                    }
                }
            }
        }
    }
}

