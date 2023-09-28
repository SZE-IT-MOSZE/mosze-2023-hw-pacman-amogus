using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public EnemyController[] enemies; //enemy array

    public PlayerController player; //player gameObject

    public Transform pellets; //pickup gameObjects

    public int score { get; private set; } //Score getter && setter

    public int lives { get; private set; } //Lives getter && setter

    private void Start()
    {
        NewGame();
    }

    private void NewRound() //Start new round after game is over
    {
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState(); 
    }

    private void ResetState() //Reset enemies && player after player death
    {
        for (int i = 0; i < this.enemies.Length; i++)
        {
            this.enemies[i].gameObject.SetActive(true);
        }

        this.player.gameObject.SetActive(true);
    }

    private void NewGame() //Start new game (reset score, lives, call NewRound method)
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void GameOver() //Set enemy gameObjects && player gameObject inactive
    {
        for (int i = 0; i < this.enemies.Length; i++)
        {
            this.enemies[i].gameObject.SetActive(false);
        }

        this.player.gameObject.SetActive(false);
    }

    private void SetScore(int score) //Set score
    {
        this.score = score;
    }

    private void SetLives(int lives) //Set lives
    {
        this.lives = lives;
    }

    public void EnemyDeath(EnemyController enemy) //Add enemy points to score
    {
        SetScore(this.score + enemy.points);
    }

    public void PlayerDeath() //Set player gameObject inactive; subtract lives by 1; check if any lives remain (if not, call GameOver method)
    {
        this.player.gameObject.SetActive(false);

        SetLives(this.lives - 1);

        if (this.lives > 0)
        {
            StartCoroutine(WaitForDeath(3f)); //Call WaitForDeath coroutine (wait for X seconds)
        }
        else
        {
            GameOver();
        }
    }

    private IEnumerator WaitForDeath(float deathTime) //Pass in deathTime float && wait for x seconds before calling ResetState method
    {
        yield return new WaitForSeconds(deathTime);
        ResetState();
    }
}


