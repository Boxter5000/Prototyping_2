using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Transform FollowObject;
    [SerializeField] private GameObject PlayQuit;
    [SerializeField] private GameObject Options;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void OptionsOpen()
    {
        FollowObject.position = new Vector2(FollowObject.position.x + 2f, FollowObject.position.y);
        PlayQuit.SetActive(false);
        Options.SetActive(true);
    }
    
    public void OptionsClose()
    {
        FollowObject.position = new Vector2(FollowObject.position.x - 2f, FollowObject.position.y);
        PlayQuit.SetActive(true);
        Options.SetActive(false);
    }
}
