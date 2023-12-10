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
    public GameObject bearTrapModel;
    public float bearTrapTime;
    private float moveSpeedBase;
    public Animator bearTrapAnimator;

    [Header("Mine Settings")]
    public GameObject mineModel;
    public GameObject mineRange;
    public float mineArmTime;

    public void Start()
    {
        switch (Random.Range(0, 2))
        {
            case 0:
                type = trapType.Mine;
                gameObject.name = "Mine";
                mineModel.SetActive(true);
                break;
            case 1:
                type = trapType.BearTrap;
                gameObject.name = "Bear Trap";
                bearTrapModel.SetActive(true);
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
                    SFXLogic.instance.PlaySFX(1);
                    StartCoroutine(MineArm());
                    break;
                case trapType.BearTrap:
                    PlayerController.instance.moveSpeed = 0f;
                    bearTrapAnimator.SetBool("isTriggered", true);
                    SFXLogic.instance.PlaySFX(3);
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
