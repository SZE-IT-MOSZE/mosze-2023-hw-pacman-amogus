using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupLogic : MonoBehaviour
{
    public enum PickupType
    {
        Basic,
        SpeedUp,
        PowerUp
    }
    public PickupType type;

    private int scoreValue;

    public void DestroyPickup()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            switch (type)
            {
                case PickupType.Basic:
                    scoreValue = 100;
                    GameLogic.instance.SetScore(scoreValue);
                    SpawnManager.instance.spawnedPickups--;

                    break;
                case PickupType.SpeedUp:
                    scoreValue = 200;
                    GameLogic.instance.SetScore(scoreValue);
                    SpawnManager.instance.spawnedPickups--;

                    PlayerController.instance.speedUp = true;
                    PlayerController.instance.SetSpeedUp();
                    break;
                case PickupType.PowerUp:
                    scoreValue = 500;
                    GameLogic.instance.SetScore(scoreValue);
                    SpawnManager.instance.spawnedPickups--;

                    PlayerController.instance.SetPowerUp();
                    break;
                default:
                    break;
            }

            DestroyPickup();
        }
    }
}
