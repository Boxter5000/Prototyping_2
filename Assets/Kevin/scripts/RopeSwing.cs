using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSwing : MonoBehaviour
{
    public BoxCollider2D[] myColliders;
    private float timer;
    public bool isOnRope;
    public bool attached = false;
    public Transform attachedtoTransform;
    private GameObject disregard;
    private HingeJoint2D hj;
    public GameObject HingeRope;
    private Rigidbody2D rb;
    [SerializeField] private float pushforce = 7f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hj = GetComponent<HingeJoint2D>();
    }

    private void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                foreach (BoxCollider2D bc in myColliders) bc.enabled = true;
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
        isOnRope = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!attached)
        {
            if (collision.gameObject.tag == "Rope")
            {
                if (attachedtoTransform != collision.gameObject.transform.parent)
                {
                    if (disregard == null || collision.gameObject.transform.parent != disregard)
                    {
                        Attach(collision.gameObject.GetComponent<Rigidbody2D>());
                        isOnRope = true;
                    }
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Rope")
        {
            foreach (BoxCollider2D bc in myColliders) bc.enabled = false;
            timer = 1f;
        }
    }
}

