using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// Az SpawnManager játékbeli tesztjeihez tartozó osztály.
/// </summary>
public class SpawnManagerPlayTests
{
    /// <summary>
    /// A játékos elõhozását tesztelõ függvény.
    /// </summary>
    /// <returns>A függvény IEnumerator típussal tér vissza, mivel UnityTest attribútumot használ.</returns>
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
