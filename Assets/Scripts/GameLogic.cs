using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public static GameLogic instance;

    private void Awake()
    {
        instance = this;
    }

    public int score;
    public int lives = 3;

    private void Start()
    {
        SaveSystem.instance.OnLoad();
    }

    private void Update()
    {
        if (SpawnManager.instance.spawnedPickups <= 0)
        {
            SpawnManager.instance.SpawnObjects();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            KillPlayer();
        }
    }

    public void SetScore(int ScoreToAdd)
    {
        score += ScoreToAdd;
    }

    public void KillPlayer()
    {
        Destroy(GameObject.FindWithTag("Player"));

        lives--;
        SpawnManager.instance.playerSpawned = false;

        if (lives <= 0)
        {
            StartCoroutine(WaitForEndGame());
        }
        else
        {
            StartCoroutine(RespawnPlayer());
        }
    }

    public void GameOver()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(1f);
        SpawnManager.instance.SpawnPlayer();
    }

    private IEnumerator WaitForEndGame()
    {
        yield return new WaitForSeconds(3f);
        GameOver();
    }
}
