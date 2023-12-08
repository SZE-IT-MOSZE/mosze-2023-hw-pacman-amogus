using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }

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

    private void Update()
    {
        Vector3 movement = Vector3.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        playerRb.velocity = movement * moveSpeed;
    }

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

    public void SetPowerUp()
    {
        powerUp = true;
        StartCoroutine(PowerUpTimer("PowerUp", powerUpTime));
    }

    public IEnumerator InvulnerabilityTimer()
    {
        yield return new WaitForSeconds(invulnerabilityTime);

        invulnerable = false;
        Setinvulnerability();
    }

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
