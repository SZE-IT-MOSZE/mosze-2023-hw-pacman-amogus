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
    public float moveSpeed = 5f;

    [Header("Collider settings")]
    public Rigidbody playerRb;
    public CapsuleCollider body;
    public BoxCollider trigger;

    [Header("Invulnerability settings")]
    public bool invulnerable;
    public float invulnerabilityTime = 3f;


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

    public IEnumerator InvulnerabilityTimer()
    {
        yield return new WaitForSeconds(invulnerabilityTime);

        invulnerable = false;
        Setinvulnerability();
    }
}
