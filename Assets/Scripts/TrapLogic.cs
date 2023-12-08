using System.Collections;
using UnityEngine;

public class TrapLogic : MonoBehaviour
{
    public enum trapType
    {
        Mine,
        BearTrap
    }
    public trapType type;

    [Header("Bear Trap Settings")]
    public float bearTrapTime;
    private float moveSpeedBase;

    [Header("Mine Settings")]
    public GameObject mineRange;
    public float mineArmTime;

    public void Start()
    {
        switch (Random.Range(0, 2))
        {
            case 0:
                type = trapType.Mine;
                gameObject.name = "Mine";
                break;
            case 1:
                type = trapType.BearTrap;
                gameObject.name = "Bear Trap";
                break;
            default:
                break;
        }
        moveSpeedBase = PlayerController.instance.moveSpeed;
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            switch (type)
            {
                case trapType.Mine:
                    StartCoroutine(MineArm());
                    break;
                case trapType.BearTrap:
                    PlayerController.instance.moveSpeed = 0f;
                    StartCoroutine(BearTrapTimer());
                    break;
            }
        }
    }

    public IEnumerator MineArm()
    {
        yield return new WaitForSeconds(mineArmTime);

        mineRange.SetActive(true);
    }

    public IEnumerator BearTrapTimer()
    {
        yield return new WaitForSeconds(bearTrapTime);

        if (GameObject.FindWithTag("Player") != null)
        {
            PlayerController.instance.moveSpeed = moveSpeedBase;
        }

        SpawnManager.instance.spawnedTraps--;
        Destroy(gameObject);
    }
}
