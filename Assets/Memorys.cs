using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memorys : MonoBehaviour
{
    [SerializeField] private GameObject Dash;
    [SerializeField] private GameObject Walljump;
    [SerializeField] private GameObject Doublejump;
    void Start()
    {
        DontDestroyOnLoad(this);
    }
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.R)) return;
        ShowDash(false);
        ShowWalljump(false);
        ShowDoubleJump(false);
    }
    public void ShowDash(bool state)
    {
        Dash.SetActive(state);
    }
    public void ShowWalljump(bool state)
    {
        Walljump.SetActive(state);
    }
    public void ShowDoubleJump(bool state)
    {
        Doublejump.SetActive(state);
    }
}
