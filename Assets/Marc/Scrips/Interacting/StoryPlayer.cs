using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPlayer : MonoBehaviour
{
    [SerializeField] private Animator storys;

    private bool playDashAnimation;
    private bool playWallJumpAnimation;
    private bool playDoublejumpAnimation;

    private void Update()
    {
        storys.SetBool("Dash", playDashAnimation);
        storys.SetBool("Walljump", playWallJumpAnimation);
        storys.SetBool("Doublejump", playDoublejumpAnimation);
    }

    public void PlayDashAnimation()
    {
        playDashAnimation = true;
        
    }
    public void PlayWallJumpAnimation()
    {
        playWallJumpAnimation = true;
    }
    public void PlayDoublejumpAnimation()
    {
        playDoublejumpAnimation = true;
    }
    
    public void StopDashAnimation()
    {
        playDashAnimation = true;
    }
    public void StopWallJumpAnimation()
    {
        playWallJumpAnimation = true;
    }
    public void StopDoublejumpAnimation()
    {
        playDoublejumpAnimation = true;
    }
}
