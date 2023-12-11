using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// A GameLogic játékbeli tesztjeihez tartozó osztály.
/// </summary>
public class GameLogicPlayTest
{
    /// <summary>
    /// A pontszámot tesztelő függvény.
    /// </summary>
    [UnityTest]
    public IEnumerator GameLogicPlayScore()
    {
        GameObject gameobject = new GameObject();

        GameLogic gameLogic = gameobject.AddComponent<GameLogic>();

        int intialscore = gameLogic.score;

        gameLogic.SetScore(200);

        int resultscore = gameLogic.score;

        yield return null;

        Assert.AreNotEqual(intialscore, resultscore);
    }

    /// <summary>
    /// A játékos halálát tesztelő függvény.
    /// </summary>
    [UnityTest]
    public IEnumerator GameLogicPlayerKilled()
    {
        GameObject player = new GameObject();

        player.tag = "Player";

        GameObject Go = new GameObject();

        GameLogic gameLogic = Go.AddComponent<GameLogic>();

        gameLogic.isTest = true;

        gameLogic.KillPlayer();

        yield return new WaitForSeconds(1f);

        GameObject playerobject = GameObject.FindGameObjectWithTag("Player");

        bool playerkilled = false;

        if (playerobject == null)
        {
            playerkilled = true;
        }

        Assert.IsTrue(playerkilled);
    }
}

