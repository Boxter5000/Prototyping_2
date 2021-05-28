using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPointSet : MonoBehaviour
{
    private Vector2 Spawnpoint;

    private void Awake()
    {
        throw new NotImplementedException();
    }

    void Update()
    {
        if (Application.loadedLevelName == SceneManager.GetActiveScene().name)
        {
            Spawnpoint = GameObject.FindGameObjectWithTag("RespawnPoint").transform.position;
            transform.position = Spawnpoint;
        }
    }
}
