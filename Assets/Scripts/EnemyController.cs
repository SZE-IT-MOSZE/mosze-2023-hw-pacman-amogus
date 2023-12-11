using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Az EnemyController oszt�ly felel�s az ellens�gek mozg�s�nak �s viselked�s�nek vez�rl�s��rt.
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
    /// Az ellens�g mozg�s�nak lehets�ges ir�nyai.
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
    /// Az objektum l�trehoz�sakor inicializ�lja az ellens�get.
    /// </summary>
    private void Start()
    {
        enemyRb = GetComponent<Rigidbody>();

        ChangeDir();
        firstSpawn = false;
    }

    /// <summary>
    /// A friss�t�s minden frame-ben ellen�rzi az ir�nyt �s a mozg�st.
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
    /// Az ellens�g mozg�s ir�ny�t ellen�rzi.
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
    /// Az ellens�g mozg�s ir�ny�nak v�ltoztat�sa.
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
    /// Az �tk�z�st az adott ir�nyban ellen�rzi egy-egy raycast seg�ts�g�vel.
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
    /// Az ellens�g modellj�nek ir�ny�t v�ltoztatja.
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
    /// Az ellens�g elpuszt�t�sa.
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
    /// Esem�nykezel�, amely akkor h�v�dik meg, ha az ellens�g bele�tk�zik m�s objektumba.
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
    /// V�rakozik egy adott id�t (megadva m�sodpercben), majd v�grehajtja a sz�ks�ges m�veleteket.
    /// </summary>
    /// <param name="waitTime">A v�rakoz�si id� m�sodpercben.</param>
    private IEnumerator WaitForNodeTrigger(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        // Ha nem teszt m�dban vagyunk, akkor v�grehajtja a modell ir�ny�nak v�ltoztat�s�t.
        if (isTest == false)
        {
            ChangeModelDirection();

        }

        dirChange = false;
    }
}
