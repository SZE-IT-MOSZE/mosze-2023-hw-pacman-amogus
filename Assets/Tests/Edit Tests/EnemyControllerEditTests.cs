using NUnit.Framework;

/// <summary>
/// Az EnemyControllerEditTests osztály tartalmazza az EnemyController tesztjeit módosított függvényekkel.
/// </summary>
public class EnemyControllerEditTests
{
    /// <summary>
    /// Teszteli az ellenség irányváltását.
    /// </summary>
    [Test]
    public void EnemyDirectionChangeTest()
    {
        //Arrange
        EnemyController enemy = new EnemyController();
        string initialDirection = enemy.direction.ToString();

        //Act
        enemy.ChangeDir();

        //Assert
        Assert.AreNotEqual(initialDirection, enemy.direction.ToString());
    }
}
