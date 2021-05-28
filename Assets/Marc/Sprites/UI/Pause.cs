using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private KeyCode pauseKey;
    
    private static PlayerController instance;
    private GameObject pauseMenu;
    void Awake()
    {
        pauseMenu = transform.Find("PauseMenu").gameObject;
        
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey) && !pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(true);
        }
        else if(Input.GetKeyDown(pauseKey) && pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }
    }

    public void Options()
    {
        
    }

    public void MainMenu()
    {
        pauseMenu.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
