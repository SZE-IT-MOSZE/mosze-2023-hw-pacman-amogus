using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameLogicPlayTest
{
   
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

    [UnityTest]
    public IEnumerator GameLogicPlayerKilled()
    {
        GameObject player = new GameObject();

        player.tag = "Player";

        GameObject Go = new GameObject();

        GameLogic gameLogic  = Go.AddComponent<GameLogic>();

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

