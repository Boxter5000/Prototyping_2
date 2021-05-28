using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraFindPlayer : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    private GameObject playerObj;
    private Transform tFollowTarget;

    void Awake()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();

        playerObj = GameObject.FindWithTag("Player");

        tFollowTarget = playerObj.transform;
        cam.Follow = tFollowTarget;
        cam.LookAt = tFollowTarget;
    }
}