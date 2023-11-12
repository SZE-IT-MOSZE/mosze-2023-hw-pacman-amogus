using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
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

    [Header("Spawn Settings")]
    public bool playerSpawned = false;
    public int spawnNumber;
    public int spawnedPickups;
    public int enemyNumber;

    [Header("Spawn Lists")]
    public List<Transform> children = new List<Transform>();
    public List<Transform> spawnPoints = new List<Transform>();

    public void SpawnObjects()
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

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            Vector3 spawnPosition = new Vector3(0, 0, 0);
            spawnPosition = spawnPoints[i].transform.position;
            spawnPosition.y += 1.5f;

            pickupGameObject = pickups[Random.Range(0, pickups.Count)];

            Instantiate(pickupGameObject, spawnPosition, Quaternion.identity);

            spawnedPickups++;
        }

        for (int i = 0; i <= enemyNumber; i++)
        {
            Vector3 spawnPosition = new Vector3(0, 0, 0);
            int spawnIndex = Random.Range(0, children.Count);
            spawnPosition = children[spawnIndex].transform.position;
            spawnPosition.y += 1f;

            Instantiate(enemy, spawnPosition, Quaternion.identity);
        }

        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        if (playerSpawned == false)
        {
            Vector3 spawnPosition = new Vector3(0, 0, 0);
            int spawnIndex = Random.Range(0, children.Count);
            spawnPosition = children[spawnIndex].transform.position;
            spawnPosition.y += 1f;

            Instantiate(player, spawnPosition, Quaternion.identity);
            playerSpawned = true;

            children.RemoveAt(spawnIndex);
        }
    }
}
