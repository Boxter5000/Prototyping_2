using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cinemachine.Editor;

public class cameraFindPlayer : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    private GameObject playerObj;
    private Transform tFollowTarget;
    void Awake()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        if (playerObj == null)
        {
            playerObj = GameObject.FindWithTag("Player");
        }

        tFollowTarget = playerObj.transform;
        cam.Follow = tFollowTarget;

    }
}
