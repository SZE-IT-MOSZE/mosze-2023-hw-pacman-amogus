using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyControllerEditTests
{
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
