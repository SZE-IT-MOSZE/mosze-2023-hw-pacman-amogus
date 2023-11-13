using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerControllerEditTests
{
    [Test]
    public void PlayerControllerSpeedUpTest()
    {
        //Arrange
        PlayerController player = new PlayerController();
        float initialMoveSpeed = player.moveSpeed;
        player.speedUp = true;

        //Act
        player.SetSpeedUp();
        float resultMoveSpeed = player.moveSpeed;

        //Assert
        Assert.AreNotEqual(initialMoveSpeed, resultMoveSpeed);
    }
}
