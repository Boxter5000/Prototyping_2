using Unity.Mathematics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Death : MonoBehaviour
{
    [Header("Tags")]
    [SerializeField] private string deathOnTuch;
    [SerializeField] private string respawnTag;
    [SerializeField] private string respawnChild;
    
    [Header("speed")]
    [SerializeField] [Range(0.0f, 0.9f)] private float transitionSpeed = 0f;
    
    [Header("Effeckts")]
    [SerializeField] private GameObject deathParicle;
    [SerializeField] private Transform spawnpoint;
    
    [HideInInspector] public bool hasDied;

    private Vector2 lastCheckpoint;
    private Vector2 deathPos;
    
    GameObject playerClone;

    private CapsuleCollider2D cc;
    [SerializeField] private Rigidbody2D rb;
    private SpriteRenderer _spriteRenderer;
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        transform.position = spawnpoint.position;
        cc = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        deathParicle = Instantiate(deathParicle, transform);
        _particleSystem = deathParicle.GetComponent<ParticleSystem>();
        _particleSystem.Stop();
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

            hasDied = true;
            rb.velocity = Vector2.zero;
            cc.isTrigger = true;
            rb.isKinematic = true;
            _particleSystem.Play();

        }
        if (other.gameObject.CompareTag(respawnChild) && hasDied)
        {
            cc.isTrigger = false;
            rb.isKinematic = false;
            hasDied = false;
            _particleSystem.Stop();
        }
    }
    private void FixedUpdate()
    {
        if (hasDied)
        {
            deathPos = transform.position;
            transform.position = Vector2.Lerp(deathPos, lastCheckpoint, transitionSpeed);
        }
    }

}
