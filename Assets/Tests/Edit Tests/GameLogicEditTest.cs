using NUnit.Framework;

public class GameLogicEditTest
{
    [Test]
    public void ScoreChangeTest()
    {
        GameLogic scorekeep = new GameLogic();

        int initialscore = scorekeep.score;

        scorekeep.SetScore(200);

        int resultscore = scorekeep.score;

        Assert.AreNotEqual(initialscore, resultscore);
    }
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
