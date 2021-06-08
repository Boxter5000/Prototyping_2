using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class videoStart : MonoBehaviour
{
    private void Start()
    {
        Invoke("LoadSceneAfterTime", 24f);
    }

    private void LoadSceneAfterTime()
    {
        SceneManager.LoadScene(1);
    }
}
