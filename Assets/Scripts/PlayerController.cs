using System.Collections;
using UnityEngine;

/// <summary>
/// A j�t�kos ir�ny�t�s�t kezel� oszt�ly.
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Az oszt�ly statikus p�ld�ny�t t�rol� referencia.
    /// </summary>
    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// A j�t�kos modellje.
    /// </summary>
    public GameObject playerModel;

    [Header("Movement settings")]
    public float moveSpeed = 7f;

    [Header("Collider settings")]
    public Rigidbody playerRb;
    public BoxCollider trigger;

    [Header("Invulnerability settings")]
    public bool invulnerable;
    public float invulnerabilityTime = 3f;

    [Header("PowerUp settings")]
    public bool speedUp;
    private float speedUpMoveSpeed = 14f;
    public float speedUpTime = 5f;
    public bool powerUp;
    public float powerUpTime = 10f;

    [Header("Test settings")]
    public bool isTest = true;

    /// <summary>
    /// Az Update f�ggv�ny, amely a j�t�kos mozg�s�t kezeli.
    /// </summary>
    private void Update()
    {
        Vector3 movement = Vector3.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        if (movement != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(movement);
            playerModel.transform.rotation = Quaternion.Lerp(playerModel.transform.rotation, newRotation, Time.deltaTime * moveSpeed);
        }

        playerRb.velocity = movement * moveSpeed;
    }

    /// <summary>
    /// Be�ll�tja a sebezhetetlens�get.
    /// </summary>
    public void Setinvulnerability()
    {
        switch (invulnerable)
        {
            case true:
                trigger.enabled = false;
                StartCoroutine(InvulnerabilityTimer());
                break;
            case false:
                trigger.enabled = true;
                break;
        }

        UILogic.instance.ShowInvulnerabilityText();
    }

    /// <summary>
    /// Be�ll�tja a sebess�g fokoz�s�t.
    /// </summary>
    public void SetSpeedUp()
    {
        switch (speedUp)
        {
            case true:
                moveSpeed = speedUpMoveSpeed;
                if (isTest == false)
                {
                    StartCoroutine(PowerUpTimer("SpeedUp", speedUpTime));
                }
                break;
            case false:
                moveSpeed = 7f;
                break;
        }
    }

    /// <summary>
    /// Be�ll�tja a PowerUp-ot.
    /// </summary>
    public void SetPowerUp()
    {
        powerUp = true;
        StartCoroutine(PowerUpTimer("PowerUp", powerUpTime));
    }

    /// <summary>
    /// Id�z�t� a sebezhetetlens�ghez.
    /// </summary>
    public IEnumerator InvulnerabilityTimer()
    {
        yield return new WaitForSeconds(invulnerabilityTime);

        invulnerable = false;
        Setinvulnerability();
    }

    /// <summary>
    /// Id�z�t� a PowerUp-hoz.
    /// </summary>
    public IEnumerator PowerUpTimer(string powerUpType, float powerUpTime)
    {
        yield return new WaitForSeconds(powerUpTime);

        switch (powerUpType)
        {
            case "SpeedUp":
                speedUp = false;
                SetSpeedUp();
                break;
            case "PowerUp":
                powerUp = false;
                break;
        }
    }
}
