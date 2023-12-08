using NUnit.Framework;

public class PlayerControllerEditTests
{
    [Test]
    public void PlayerControllerSpeedUpTest()
    {

        PlayerController player = new PlayerController();







        float initialMoveSpeed = player.moveSpeed;



        player.speedUp = true;


        player.SetSpeedUp();


        float resultMoveSpeed = player.moveSpeed;

        Assert.AreNotEqual(initialMoveSpeed, resultMoveSpeed);
    }
}
