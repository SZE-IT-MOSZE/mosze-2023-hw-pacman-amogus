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
}
