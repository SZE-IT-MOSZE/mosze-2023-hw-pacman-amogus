using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// Az EnemyController játékbeli tesztjeihez tartozó osztály.
/// </summary>
public class EnemyControllerPlayTests
{
    /// <summary>
    /// Az ellenfelek megölését tesztelõ függvény.
    /// </summary>
    [UnityTest]
    public IEnumerator KillEnemyTest()
    {
        GameObject enemy = new GameObject();

        enemy.tag = "Enemy";

        EnemyController enemyController = enemy.AddComponent<EnemyController>();

        enemyController.isTest = true;

        enemyController.KillEnemy();

        yield return new WaitForSeconds(1f);

        GameObject go = GameObject.FindGameObjectWithTag("Enemy");

        bool enemyexists = true;

        if (go == null)
        {
            enemyexists = false;
        }

        Assert.IsFalse(enemyexists);
    }

    /// <summary>
    /// Az ellenfelek mozgását tesztelõ függvény.
    /// </summary>
    [UnityTest]
    public IEnumerator EnemyMoveTest()
    {
        //Arrange
        GameObject enemy = new GameObject();
        enemy.AddComponent<Rigidbody>();

        EnemyController enemyController = enemy.AddComponent<EnemyController>();

        Vector3 initialPos = Vector3.zero;
        enemyController.ChangeDir();

        //Wait
        yield return new WaitForSeconds(1f);

        //Act
        Vector3 resultPos = enemy.gameObject.transform.position;

        //Assert
        Assert.AreNotEqual(initialPos, resultPos);
    }
}
