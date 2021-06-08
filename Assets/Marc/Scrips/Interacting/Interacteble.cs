using System;
using UnityEngine;
using UnityEngine.Events;

public class Interacteble : MonoBehaviour
{
    [SerializeField] private bool inRange;
    [SerializeField] private KeyCode interactKey;
    [SerializeField] private UnityEvent interactAction;

    [SerializeField] private GameObject particle;
    private void Awake()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (inRange)
        {
            if(Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            particle.SetActive(true);
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            particle.SetActive(false);
            inRange = false;
        }
    }
}
