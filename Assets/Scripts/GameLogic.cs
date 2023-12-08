using System.Collections;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public static GameLogic instance;

    private void Awake()
    {
        instance = this;
    }

    [HideInInspector]
    public int scoreGoal;
    [HideInInspector]
    public bool isEndless;
    [HideInInspector]
    public bool gameOver;
    [HideInInspector]
    public bool isWin;

    public int score;
    public int lives;

    [Header("Test settings")]
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
                SpawnManager.instance.SpawnPickups();
            }

            if (SpawnManager.instance.spawnedEnemies <= 0)
            {
                SpawnManager.instance.SpawnEnemies();
            }

            if (SpawnManager.instance.spawnedTraps <= 0)
            {
                SpawnManager.instance.SpawnTraps();
            }

            if (isEndless == false)
            {
                if (score >= scoreGoal)
                {
                    isWin = true;
                    GameOver();
                }
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
            SFXLogic.instance.PlaySFX(0);

            UILogic.instance.SetLivesText(lives);
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
    }

    public void GameOver()
    {
        gameOver = true;

        if (isTest == false)
        {
            if (isWin == true)
            {
                Time.timeScale = 0f;
                UILogic.instance.ShowEndScreen(true);
            }
            else
            {
                Time.timeScale = 0f;
                UILogic.instance.ShowEndScreen(false);
            }
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
