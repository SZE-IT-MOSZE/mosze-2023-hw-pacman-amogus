using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameLogicEditTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void ScoreChangeTest()
    {
        GameLogic scorekeep = new GameLogic();

        int initialscore = scorekeep.score;

        scorekeep.SetScore(200);

        int resultscore = scorekeep.score;

        Assert.AreNotEqual(initialscore, resultscore);
    }
}
