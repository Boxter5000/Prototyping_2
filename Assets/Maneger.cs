using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Maneger : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void LoadEnding()
    {
        SceneManager.LoadScene(7);
    }
}
