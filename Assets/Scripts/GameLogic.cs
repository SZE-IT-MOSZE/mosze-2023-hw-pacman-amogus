using System.Collections;
using UnityEngine;

/// <summary>
/// A játék logikáját irányító osztály.
/// </summary>
public class GameLogic : MonoBehaviour
{
    /// <summary>
    /// Az aktuális példány azonosítása az osztályból.
    /// </summary>
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

    /// <summary>
    /// A játék indításakor meghívott függvény.
    /// Ellenõrzi, hogy a játék teszt módban van-e, és ha nem, akkor betölti a mentést.
    /// </summary>
    private void Start()
    {
        if (isTest == false)
        {
            SaveSystem.instance.OnLoad();
        }
    }

    /// <summary>
    /// Minden frame-ben frissíti a játék állapotát, ha nem teszt módban van.
    /// Ellenõrzi, hogy szükséges-e új elemeket létrehozni, és ellenõrzi a játék módját, hogy végtelen módban van-e, a játék végsõ teljesülési feltételét.
    /// </summary>
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

    /// <summary>
    /// A játék pontszámának beállítása.
    /// </summary>
    public void SetScore(int ScoreToAdd)
    {
        score += ScoreToAdd;

        if (isTest == false)
        {
            UILogic.instance.SetScoreText(score);
        }
    }

    /// <summary>
    /// A játékos életének csökkentése.
    /// </summary>
    public void SetLives()
    {
        lives--;
        if (isTest == false)
        {
            KillPlayer();
        }
    }

    /// <summary>
    /// A játékos objektum elpusztítása.
    /// </summary>
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

    /// <summary>
    /// Game Over gameEvent indítása.
    /// </summary>
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

    /// <summary>
    /// Játékos újrat elhelyezése a pályán egy kis várakozás után.
    /// </summary>
    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(1f);

        SpawnManager.instance.GetPlayerSpawnPos();

        PlayerController.instance.invulnerable = true;
        PlayerController.instance.Setinvulnerability();
    }

    /// <summary>
    /// Várakoztatás kis ideig a játék végéig.
    /// </summary>
    public IEnumerator WaitForEndGame()
    {
        yield return new WaitForSeconds(3f);

        GameOver();
    }
}
