using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// Az SpawnManager j�t�kbeli tesztjeihez tartoz� oszt�ly.
/// </summary>
public class SpawnManagerPlayTests
{
    /// <summary>
    /// A j�t�kos el�hoz�s�t tesztel� f�ggv�ny.
    /// </summary>
    /// <returns>A f�ggv�ny IEnumerator t�pussal t�r vissza, mivel UnityTest attrib�tumot haszn�l.</returns>
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
