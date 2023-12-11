using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A SpawnManager osztály felelős az objektumok létrehozásáért és elhelyezéséért a játékban.
/// </summary>

public class SpawnManager : MonoBehaviour
{

    /// <summary>
    /// A SpawnManager egyetlen példánya (singleton).
    /// </summary>
    public static SpawnManager instance;

    private void Awake()
    {
        instance = this;
    }

    [Header("Actor GameObjects")]
    public PlayerController player;
    public EnemyController enemy;

    [Header("Pickup Settings")]
    public List<GameObject> pickups = new List<GameObject>();
    public GameObject pickupGameObject;

    [Header("Trap Settings")]
    public GameObject trapGameObject;
    public int trapNumber;
    [HideInInspector]
    public int spawnedTraps;

    [Header("Spawn Settings")]
    public GameObject nodeGameObject;
    public bool playerSpawned = false;
    public int spawnNumber;
    [HideInInspector]
    public int spawnedPickups;
    public int enemyNumber;
    [HideInInspector]
    public int spawnedEnemies;

    [Header("Spawn Lists")]
    public List<Transform> children = new List<Transform>();
    public List<Transform> spawnPoints = new List<Transform>();

    [Header("Test Settings")]
    public bool isTest = true;

    /// <summary>
    ///Az aktuális gyermek objektumból lekéri az elhelyezkedése poyíciókat.
    /// </summary>
    public void GetSpawnPositions()
    {
        children.Clear();
        spawnPoints.Clear();

        foreach (Transform child in transform)
        {
            if (child.gameObject.activeInHierarchy == false)
            {
                children.Add(child);
            }
        }

        for (int i = 0; i < spawnNumber; i++)
        {
            int spawnIndex = Random.Range(0, children.Count);
            spawnPoints.Add(children[spawnIndex]);

            children.RemoveAt(spawnIndex);
        }

        if (playerSpawned == false)
        {
            GetPlayerSpawnPos();
        }
    }

    /// <summary>
    /// Felvehető objektumokat spawn-ol a megfelelő pozíciókon.
    /// </summary>

    public void SpawnPickups()
    {
        GetSpawnPositions();

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            Vector3 spawnPosition = new Vector3(0, 0, 0);
            spawnPosition = spawnPoints[i].transform.position;
            spawnPosition.y += 1.5f;

            pickupGameObject = pickups[Random.Range(0, pickups.Count)];

            Instantiate(pickupGameObject, spawnPosition, Quaternion.identity);

            spawnedPickups++;
        }
    }

    /// <summary>
    /// Ellenségeket spawn-ol a megfelelő pozíciókon.
    /// </summary>

    public void SpawnEnemies()
    {
        GetSpawnPositions();

        for (int i = 0; i <= enemyNumber; i++)
        {
            Vector3 spawnPosition = new Vector3(0, 0, 0);
            int spawnIndex = Random.Range(0, children.Count);
            spawnPosition = children[spawnIndex].transform.position;
            spawnPosition.y += 1f;

            Instantiate(enemy, spawnPosition, Quaternion.identity);

            spawnedEnemies++;
        }
    }

    /// <summary>
    /// Node objektumokat spawn-ol a megfelelő pozíciókon.
    /// </summary>

    public void SpawnNodes()
    {
        GetSpawnPositions();

        for (int i = 0; i < children.Count; i++)
        {
            Vector3 spawnPosition = new Vector3(0, 0, 0);
            spawnPosition = children[i].transform.position;
            spawnPosition.y += 2f;

            Instantiate(nodeGameObject, spawnPosition, Quaternion.identity);
        }
    }

    /// <summary>
    /// Csapda objektumokat spawn-ol a megfelelő pozíciókon.
    /// </summary>

    public void SpawnTraps()
    {
        GetSpawnPositions();

        for (int i = 0; i <= trapNumber; i++)
        {
            Vector3 spawnPosition = new Vector3(0, 0, 0);
            int spawnIndex = Random.Range(0, children.Count);
            spawnPosition = children[spawnIndex].transform.position;
            spawnPosition.y += 1f;

            Instantiate(trapGameObject, spawnPosition, Quaternion.identity);

            spawnedTraps++;
        }
    }

    /// <summary>
    /// Lekéri a játékos elhelyezési pozícióját.
    /// </summary>

    public void GetPlayerSpawnPos()
    {
        Vector3 spawnPosition = new Vector3(0, 0, 0);
        int spawnIndex = Random.Range(0, children.Count);
        spawnPosition = children[spawnIndex].transform.position;
        spawnPosition.y += 1f;

        children.RemoveAt(spawnIndex);

        SpawnPlayer(spawnPosition);
    }
    /// <summary>
    /// Le spawn-olja a játékost a megadott pozícióban.
    /// </summary>
    /// <param name="playerPos">A játékos pozíciója.</param>

    public void SpawnPlayer(Vector3 playerPos)
    {
        if (playerSpawned == false)
        {
            if (isTest == false)
            {
                Instantiate(player, playerPos, Quaternion.identity);
                playerSpawned = true;
            }
            else
            {
                GameObject playerPlaceholder = new GameObject();
                playerPlaceholder.tag = "Player";
            }
        }
    }
}
