using NUnit.Framework;

/// <summary>
/// A GameLogic oszt�ly tesztjeihez tartoz� oszt�ly.
/// </summary>
public class GameLogicEditTest
{
    /// <summary>
    /// A pontsz�mv�ltoz�st tesztel� f�ggv�ny.
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
    /// Az �letek v�ltoz�s�t tesztel� f�ggv�ny.
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
    /// A j�t�k v�g�t tesztel� f�ggv�ny.
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
