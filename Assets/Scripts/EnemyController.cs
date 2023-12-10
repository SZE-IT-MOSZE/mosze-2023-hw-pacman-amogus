using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public float triggerRange = 1.8f;

    public bool isTest = true;

    private bool firstSpawn = true;
    private bool dirChange;
    private bool dirChangeNode;

    [HideInInspector]
    public Direction direction;
    private List<string> nodeDirections = new List<string>();
    private Vector3 moveDir;

    public Rigidbody enemyRb;
    public GameObject enemyModel;

    [HideInInspector]
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    private void Start()
    {
        enemyRb = GetComponent<Rigidbody>();

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

        if (dirChangeNode == false)
        {
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

            if (isTest == false)
            {
                ChangeModelDirection();
            }

            dirChange = false;
        }
        else
        {
            List<Direction> availableDirections = new List<Direction>();

            for (int i = 0; i < nodeDirections.Count; i++)
            {
                switch (nodeDirections[i])
                {
                    case "Up":
                        availableDirections.Add(Direction.Up);
                        break;
                    case "Down":
                        availableDirections.Add(Direction.Down);
                        break;
                    case "Left":
                        availableDirections.Add(Direction.Left);
                        break;
                    case "Right":
                        availableDirections.Add(Direction.Right);
                        break;
                    default:
                        break;
                }
            }

            int dir = UnityEngine.Random.Range(0, availableDirections.Count);

            direction = availableDirections[dir];

            nodeDirections.Clear();
            dirChangeNode = false;

            StartCoroutine(WaitForNodeTrigger(.35f));
        }
    }

    public bool CheckCollision(Direction checkDirection)
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

    public void ChangeModelDirection()
    {
        switch (direction)
        {
            case Direction.Up:
                enemyModel.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case Direction.Down:
                enemyModel.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
            case Direction.Left:
                enemyModel.gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
                break;
            case Direction.Right:
                enemyModel.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
        }
    }

    public void KillEnemy()
    {
        if (isTest == false)
        {
            GameLogic.instance.SetScore(1000);
            SpawnManager.instance.spawnedEnemies--;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Node")
        {
            nodeDirections = new List<string>(collision.GetComponent<NodeLogic>().avalibleDirections);
            dirChangeNode = true;
            ChangeDir();
        }

        if (collision.tag == "Player" && PlayerController.instance.powerUp == false && PlayerController.instance.invulnerable == false)
        {
            GameLogic.instance.SetLives();
        }

        if (collision.tag == "Player" && PlayerController.instance.powerUp == true)
        {
            KillEnemy();
        }
    }

    private IEnumerator WaitForNodeTrigger(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (isTest == false)
        {
            ChangeModelDirection();

        }

        dirChange = false;
    }
}
