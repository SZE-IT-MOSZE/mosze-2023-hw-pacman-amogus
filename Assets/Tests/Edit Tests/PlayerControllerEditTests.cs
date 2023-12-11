using NUnit.Framework;

/// <summary>
/// A PlayerControllerEditTests osztály tartalmazza a PlayerController tesztjeit módosított függvényekkel.
/// </summary>
public class PlayerControllerEditTests
{
    /// <summary>
    /// Teszteli a játékos sebességének növelését.
    /// </summary>
    [Test]
    public void PlayerControllerSpeedUpTest()
    {
        //Előkészítés
        PlayerController player = new PlayerController();
        
        //Tesztterületen belüli üres sorok elhagyása
        float initialMoveSpeed = player.moveSpeed;

        //Cselekvés
        player.speedUp = true;

        player.SetSpeedUp();

        //Ellenőrzés
        float resultMoveSpeed = player.moveSpeed;

        Assert.AreNotEqual(initialMoveSpeed, resultMoveSpeed);
    }
}
