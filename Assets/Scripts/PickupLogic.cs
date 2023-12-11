using UnityEngine;

/// <summary>
/// A felvehető tárgyak logikáját kezelő osztály.
/// </summary>
public class PickupLogic : MonoBehaviour
{
    /// <summary>
    /// Felvehető tárgy típusokat definiáló felsorolás.
    /// </summary>
    public enum PickupType
    {
        Basic,
        SpeedUp,
        PowerUp
    }
    public PickupType type; // Az aktuális felvehető tárgy típusa

    private int scoreValue; // Pontérték

    /// <summary>
    /// A felvehető tárgy megsemmisítése.
    /// </summary>
    public void DestroyPickup()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// A playerrel történő ütközés eseményének kezelése.
    /// </summary>
    /// <param name="collision">Az ütközés collider-objektuma</param>
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            switch (type)
            {
                case PickupType.Basic:
                    scoreValue = 100;
                    SFXLogic.instance.PlaySFX(5);
                    GameLogic.instance.SetScore(scoreValue);
                    SpawnManager.instance.spawnedPickups--;
                    break;
                case PickupType.SpeedUp:
                    scoreValue = 200;
                    SFXLogic.instance.PlaySFX(6);
                    GameLogic.instance.SetScore(scoreValue);
                    SpawnManager.instance.spawnedPickups--;

                    PlayerController.instance.speedUp = true;
                    PlayerController.instance.SetSpeedUp();
                    break;
                case PickupType.PowerUp:
                    scoreValue = 500;
                    SFXLogic.instance.PlaySFX(7);
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
