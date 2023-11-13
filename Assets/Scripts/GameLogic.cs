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

    public bool isTest = true;

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
                SpawnManager.instance.SpawnObjects();
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

    public void KillPlayer()
    {
        Destroy(GameObject.FindWithTag("Player"));

        lives--;
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

    public void GameOver()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(1f);

        SpawnManager.instance.SpawnPlayer();

        PlayerController.instance.invulnerable = true;
        PlayerController.instance.Setinvulnerability();
    }

    private IEnumerator WaitForEndGame()
    {
        yield return new WaitForSeconds(3f);

        GameOver();
    }
}
