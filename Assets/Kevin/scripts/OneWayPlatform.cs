using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    public bool onplatform;
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    { 
        if(Input.GetKeyDown(KeyCode.S) && onplatform == true)
        {
             effector.rotationalOffset = 180f;
        }
        
        if(Input.GetKeyUp(KeyCode.S))
        {
             effector.rotationalOffset = 0f;
        }
    }
}



