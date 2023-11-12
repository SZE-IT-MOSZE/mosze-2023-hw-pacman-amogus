using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int points = 200;

    public float triggerRange = 1.8f;

    private bool firstSpawn = true;
    private bool dirChange;
    private Direction direction;
    private Vector3 moveDir;

    public Rigidbody enemyRb;

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    private void Start()
    {
        ChangeDir();
        firstSpawn = false;
    }

    private void Update()
    {
        CheckDir();
        if (dirChange == false)
        {
            enemyRb.velocity = moveDir * moveSpeed;
        }
    }

    public void CheckDir()
    {
        if (!CheckCollision(direction))
        {
            switch (direction)
            {
                case Direction.Up:
                    moveDir = new Vector3(0, 0, 1);
                    break;
                case Direction.Down:
                    moveDir = new Vector3(0, 0, -1);
                    break;
                case Direction.Left:
                    moveDir = new Vector3(-1, 0, 0);
                    break;
                case Direction.Right:
                    moveDir = new Vector3(1, 0, 0);
                    break;
            }
        }
        else
        {
            ChangeDir();
        }
    }

    public void ChangeDir()
    {
        dirChange = true;

        List<Direction> availableDirections = new List<Direction>
        {
            Direction.Up,
            Direction.Down,
            Direction.Left,
            Direction.Right
        };

        if (firstSpawn == false)
        {
            int currentDirectionIndex = (int)direction;
            availableDirections.RemoveAt(currentDirectionIndex);
        }

        int dir = UnityEngine.Random.Range(0, availableDirections.Count);

        direction = availableDirections[dir];

        dirChange = false;
    }

    private bool CheckCollision(Direction checkDirection)
    {
        Vector3 rayDirection = Vector3.zero;

        switch (checkDirection)
        {
            case Direction.Up:
                rayDirection = Vector3.forward;
                break;
            case Direction.Down:
                rayDirection = Vector3.back;
                break;
            case Direction.Left:
                rayDirection = Vector3.left;
                break;
            case Direction.Right:
                rayDirection = Vector3.right;
                break;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, rayDirection, out hit, triggerRange) && hit.collider.tag != "Node" && hit.collider.tag != "Player")
        {
            return true;
        }

        return false;
    }

    public void KillEnemy()
    {
        GameLogic.instance.score += 1000;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Node")
        {
            ChangeDir();
        }

        if (collision.tag == "Player" && PlayerController.instance.powerUp == false && PlayerController.instance.invulnerable == false)
        {
            GameLogic.instance.KillPlayer();
        }

        if (collision.tag == "Player" && PlayerController.instance.powerUp == true)
        {
            KillEnemy();
        }
    }
}
