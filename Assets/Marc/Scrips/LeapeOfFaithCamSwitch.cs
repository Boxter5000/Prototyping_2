using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapeOfFaithCamSwitch : MonoBehaviour
{
    [SerializeField] private GameObject deactivateCamera;
    [SerializeField] private GameObject activateCamera;
    private void OnTriggerEnter2D(Collider2D other)
    {
        deactivateCamera.SetActive(false);
        activateCamera.SetActive(true);
    }
}
