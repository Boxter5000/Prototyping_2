using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] footstep;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void PlayFootstep()
    {
        int Index = Random.Range(0, footstep.Length);

        audioSource.clip = footstep[Index];
        audioSource.Play();
    }
}
