using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelEnd : MonoBehaviour
{
    [SerializeField] private int levelToLoad;
    [SerializeField] private float transitionTime;
    public Animator transition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(LoadLevel(levelToLoad));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        
        SceneManager.LoadScene(levelToLoad);
    }
}
