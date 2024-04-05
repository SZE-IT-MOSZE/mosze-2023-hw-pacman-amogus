using NUnit.Framework;

/// <summary>
/// A GameLogic osztály tesztjeihez tartozó osztály.
/// </summary>
public class GameLogicEditTest
{
    /// <summary>
    /// A pontszámváltozást tesztelõ függvény.
    /// </summary>
    [Test]
    public void ScoreChangeTest()
    {
        GameLogic scorekeep = new GameLogic();

        int initialscore = scorekeep.score;

        scorekeep.SetScore(200);

        int resultscore = scorekeep.score;

        Assert.AreNotEqual(initialscore, resultscore);
    }

    /// <summary>
    /// Az életek változását tesztelõ függvény.
    /// </summary>
    [Test]
    public void LifeChangeTest()
    {
        //Arrange
        GameLogic lifekeep = new GameLogic();
        int initialLives = lifekeep.lives;

        //Act
        lifekeep.SetLives();
        int resultLives = lifekeep.lives;

        //Assert
        Assert.AreNotEqual(initialLives, resultLives);
    }

    /// <summary>
    /// A játék végét tesztelõ függvény.
    /// </summary>
    [Test]
    public void GameOverTest()
    {
        //Arrange
        GameLogic gameLogic = new GameLogic();
        bool initialState = gameLogic.gameOver;

        //Act
        gameLogic.GameOver();
        bool resultState = gameLogic.gameOver;

        //Assert
        Assert.AreNotEqual(initialState, resultState);
    }
}
