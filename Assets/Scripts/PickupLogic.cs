using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupLogic : MonoBehaviour
{
    public int scoreValue;

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            GameLogic.instance.SetScore(scoreValue);
            SpawnManager.instance.spawnedPickups--;

            Destroy(gameObject);
        }
    }
}
