using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyControllerPlayTests
{
   
    [UnityTest]
    public IEnumerator KillEnemyTest()
    {
        GameObject enemy = new GameObject();

        enemy.tag = "Enemy";

        EnemyController enemyController= enemy.AddComponent<EnemyController>();

        enemyController.isTest = true;

        enemyController.KillEnemy();

        yield return new WaitForSeconds(1f);

        GameObject go = GameObject.FindGameObjectWithTag("Enemy");

        bool enemyexists = true;

        if(go == null)
        {
            enemyexists = false;
        }

        Assert.IsFalse(enemyexists);
    }
}
