using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class SpawnManagerPlayTests
{

    [UnityTest]
    public IEnumerator SpawnPlayerTest()
    {
        GameObject gameObject = new GameObject();

        SpawnManager spawnplayer = gameObject.AddComponent<SpawnManager>();

        Vector3 playerpos = Vector3.zero;
        spawnplayer.SpawnPlayer(playerpos);

        yield return new WaitForSeconds(1f);

        Assert.IsNotNull(GameObject.FindGameObjectWithTag("Player"));
    }
}
