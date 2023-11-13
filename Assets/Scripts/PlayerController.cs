using Codice.CM.WorkspaceServer.Tree.GameUI.Checkin.Updater;
using System.Collections;
using System.Collections.Generic;
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
    public CapsuleCollider body;
    public BoxCollider trigger;

    [Header("Invulnerability settings")]
    public bool invulnerable;
    public float invulnerabilityTime = 3f;

    [Header("PowerUp settings")]
    public bool speedUp;
    private float speedUpMoveSpeed = 14f;
    public float speedUpTime = 5f;
    public bool powerUp;
    public float powerUpTime = 5f;

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
                body.enabled = false;
                trigger.enabled = false;
                StartCoroutine(InvulnerabilityTimer());
                break;
            case false:
                body.enabled = true;
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
                    StartCoroutine(SpeedUpTimer());
                }
                break;
            case false:
                moveSpeed = 7f;
                break;
        }
    }

    public IEnumerator InvulnerabilityTimer()
    {
        yield return new WaitForSeconds(invulnerabilityTime);

        invulnerable = false;
        Setinvulnerability();
    }

    public IEnumerator SpeedUpTimer()
    {
        yield return new WaitForSeconds(speedUpTime);

        speedUp = false;
        SetSpeedUp();
    }

    public IEnumerator PowerUpTimer()
    {
        yield return new WaitForSeconds(powerUpTime);
        powerUp = false;
    }
}
