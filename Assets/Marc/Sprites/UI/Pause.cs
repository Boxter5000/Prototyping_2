using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private KeyCode pauseKey;
    
    private static PlayerController instance;
    private GameObject pauseMenu;
    private GameObject[] DestroyOnLoad;
    void Awake()
    {
        pauseMenu = transform.Find("PauseMenu").gameObject;
        
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        DestroyOnLoad = GameObject.FindGameObjectsWithTag("DestroyOnLoad");
        
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
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        foreach (var obj in DestroyOnLoad)
        {
            Destroy(obj);
        }
        pauseMenu.SetActive(false);
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
