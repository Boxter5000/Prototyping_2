using System;
using UnityEngine;
using UnityEngine.Events;

public class Interacteble : MonoBehaviour
{
    [SerializeField] private bool inRange;
    [SerializeField] private KeyCode interactKey;
    [SerializeField] private UnityEvent interactAction;

    [SerializeField] private GameObject particle;
    private ParticleSystem _particleSystem;
    private void Awake()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
        _particleSystem = particle.GetComponent<ParticleSystem>();
        particle.SetActive(true);
        _particleSystem.Stop();
    }

    void Update()
    {
        if (!inRange) return;
        if(Input.GetKeyDown(interactKey))
        {
            interactAction.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        _particleSystem.Play();
        inRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        _particleSystem.Stop();
        inRange = false;
    }
}
