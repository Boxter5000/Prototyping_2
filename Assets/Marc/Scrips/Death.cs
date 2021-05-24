using Unity.Mathematics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Death : MonoBehaviour
{
    [SerializeField] private string deathOnTuch;
    [SerializeField] private string respawnTag;
    [SerializeField] private string respawnChild;
    [SerializeField] private float transitionSpeed = 0f;
    [SerializeField] private GameObject deathParicle;
    [SerializeField] private GameObject spriteChild;
    
    [HideInInspector] public bool hasDied;

    private Vector2 lastCheckpoint;
    private Vector2 deathPos;
    
    GameObject playerClone;

    private CapsuleCollider2D cc;
    private Rigidbody2D rb;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        cc = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag(respawnTag))
        {
            lastCheckpoint = other.gameObject.transform.position;
        }
        if (other.gameObject.CompareTag(deathOnTuch))
        {
            deathPos = transform.position;

            playerClone = Instantiate(spriteChild, lastCheckpoint, quaternion.identity);
            
            cc.isTrigger = true;
            rb.isKinematic = true;
            hasDied = true;
            deathParicle.SetActive(true);
            spriteChild.SetActive(false);
            
        }
        if (other.gameObject.CompareTag(respawnChild) && hasDied)
        {
            Destroy(playerClone.gameObject);
            cc.isTrigger = false;
            rb.isKinematic = false;
            hasDied = false;
            deathParicle.SetActive(false);
            spriteChild.SetActive(true);
        }
    }
    private void FixedUpdate()
    {
        Debug.Log(hasDied);
        if (hasDied)
        {
            deathPos = transform.position;
            transform.position = Vector2.Lerp(deathPos, lastCheckpoint, transitionSpeed);
        }
    }

}
