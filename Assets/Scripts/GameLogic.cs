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

    [HideInInspector]
    public int scoreGoal;

    public int score;
    public int lives;

    [Header("Test settings")]
    public bool isTest = true;
    [HideInInspector]
    public bool gameOver;

    private void Start()
    {
        if (isTest == false)
        {
            SaveSystem.instance.OnLoad();
        }
    }

    private void Update()
    {
        if (isTest == false)
        {
            if (SpawnManager.instance.spawnedPickups <= 0)
            {
                SpawnManager.instance.SpawnPickups();
            }

            if (SpawnManager.instance.spawnedEnemies <= 0)
            {
                SpawnManager.instance.SpawnEnemies();
            }
        }
    }

    public void SetScore(int ScoreToAdd)
    {
        score += ScoreToAdd;

        if (isTest == false)
        {
            UILogic.instance.SetScoreText(score);
        }
    }

    public void SetLives()
    {
        lives--;
        if (isTest == false)
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        Destroy(GameObject.FindWithTag("Player"));

        if (isTest == false)
        {
            UILogic.instance.SetLivesText(lives);
            SpawnManager.instance.playerSpawned = false;

            if (lives <= 0)
            {
                UILogic.instance.ShowGameOverText();
                StartCoroutine(WaitForEndGame());
            }
            else
            {
                StartCoroutine(RespawnPlayer());
            }
        }
    }

    public void GameOver()
    {
        gameOver = true;

        if (isTest == false)
        {
            string currentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentScene);

        }
    }

    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(1f);

        SpawnManager.instance.GetPlayerSpawnPos();

        PlayerController.instance.invulnerable = true;
        PlayerController.instance.Setinvulnerability();
    }

    public IEnumerator WaitForEndGame()
    {
        yield return new WaitForSeconds(3f);

        GameOver();
    }
}
