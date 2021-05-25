using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interacteble : MonoBehaviour
{
    [SerializeField] private bool inRange;
    [SerializeField] private KeyCode interactKey;
    [SerializeField] private UnityEvent interactAction;
    
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
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
