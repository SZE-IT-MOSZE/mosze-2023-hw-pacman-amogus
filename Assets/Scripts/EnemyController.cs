using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Az EnemyController osztály felelős az ellenségek mozgásának és viselkedésének vezérléséért.
/// </summary>
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

    /// <summary>
    /// Az ellenség mozgásának lehetséges irányai.
    /// </summary>
    [HideInInspector]
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    /// <summary>
    /// Az objektum létrehozásakor inicializálja az ellenséget.
    /// </summary>
    private void Start()
    {
        enemyRb = GetComponent<Rigidbody>();

        ChangeDir();
        firstSpawn = false;
    }

    /// <summary>
    /// A frissítés minden frame-ben ellenõrzi az irányt és a mozgást.
    /// </summary>
    private void Update()
    {
        CheckDir();
        if (dirChange == false)
        {
            enemyRb.velocity = moveDir * moveSpeed;
        }
    }

    /// <summary>
    /// Az ellenség mozgás irányát ellenõrzi.
    /// </summary>
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

    // <summary>
    /// Az ellenség mozgás irányának változtatása.
    /// </summary>
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

    /// <summary>
    /// Az ütközést az adott irányban ellenõrzi egy-egy raycast segítségével.
    /// </summary>
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

    /// <summary>
    /// Az ellenség modelljének irányát változtatja.
    /// </summary>
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

    /// <summary>
    /// Az ellenség elpusztítása.
    /// </summary>
    public void KillEnemy()
    {
        if (isTest == false)
        {
            GameLogic.instance.SetScore(1000);
            SpawnManager.instance.spawnedEnemies--;
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// Eseménykezelõ, amely akkor hívódik meg, ha az ellenség beleütközik más objektumba.
    /// </summary>
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

    /// <summary>
    /// Várakozik egy adott idõt (megadva másodpercben), majd végrehajtja a szükséges mûveleteket.
    /// </summary>
    /// <param name="waitTime">A várakozási idõ másodpercben.</param>
    private IEnumerator WaitForNodeTrigger(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        // Ha nem teszt módban vagyunk, akkor végrehajtja a modell irányának változtatását.
        if (isTest == false)
        {
            ChangeModelDirection();

        }

        dirChange = false;
    }
}
