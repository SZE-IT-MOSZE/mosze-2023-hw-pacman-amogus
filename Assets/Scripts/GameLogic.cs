using System.Collections;
using UnityEngine;

/// <summary>
/// A j�t�k logik�j�t ir�ny�t� oszt�ly.
/// </summary>
public class GameLogic : MonoBehaviour
{
    /// <summary>
    /// Az aktu�lis p�ld�ny azonos�t�sa az oszt�lyb�l.
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
    /// A j�t�k ind�t�sakor megh�vott f�ggv�ny.
    /// Ellen�rzi, hogy a j�t�k teszt m�dban van-e, �s ha nem, akkor bet�lti a ment�st.
    /// </summary>
    private void Start()
    {
        if (isTest == false)
        {
            SaveSystem.instance.OnLoad();
        }
    }

    /// <summary>
    /// Minden frame-ben friss�ti a j�t�k �llapot�t, ha nem teszt m�dban van.
    /// Ellen�rzi, hogy sz�ks�ges-e �j elemeket l�trehozni, �s ellen�rzi a j�t�k m�dj�t, hogy v�gtelen m�dban van-e, a j�t�k v�gs� teljes�l�si felt�tel�t.
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
    /// A j�t�k pontsz�m�nak be�ll�t�sa.
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
    /// A j�t�kos �let�nek cs�kkent�se.
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
    /// A j�t�kos objektum elpuszt�t�sa.
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
    /// Game Over gameEvent ind�t�sa.
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
    /// J�t�kos �jrat elhelyez�se a p�ly�n egy kis v�rakoz�s ut�n.
    /// </summary>
    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(1f);

        SpawnManager.instance.GetPlayerSpawnPos();

        PlayerController.instance.invulnerable = true;
        PlayerController.instance.Setinvulnerability();
    }

    /// <summary>
    /// V�rakoztat�s kis ideig a j�t�k v�g�ig.
    /// </summary>
    public IEnumerator WaitForEndGame()
    {
        yield return new WaitForSeconds(3f);

        GameOver();
    }
}
