using System.Collections;
using UnityEngine;

/// <summary>
/// Csapdák logikáját kezelő osztály.
/// </summary>
public class TrapLogic : MonoBehaviour
{
    /// <summary>
    /// Csapda típusokat definiáló felsorolás.
    /// </summary>
    public enum trapType
    {
        Mine,       // Akna
        BearTrap    // Medvecsapda
    }
    public trapType type; // Az aktuális csapda típusa

    /// <summary>
    /// A BearTrap típusú csapda objektumai.
    /// </summary>
    [Header("Bear Trap Settings")]
    public GameObject bearTrapModel;
    public float bearTrapTime;
    private float moveSpeedBase;
    public Animator bearTrapAnimator;

    /// <summary>
    /// A Mine típusú csapda objektumai.
    /// </summary>
    [Header("Mine Settings")]
    public GameObject mineModel;
    public GameObject mineRange;
    public float mineArmTime;

    /// <summary>
    /// Csapda inicializálása a játék elején.
    /// </summary>
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

    /// <summary>
    /// A playerrel történő ütközés eseményének kezelése.
    /// </summary>
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

    /// <summary>
    /// A Mine aktiválási ideje.
    /// </summary>
    public IEnumerator MineArm()
    {
        yield return new WaitForSeconds(mineArmTime);

        mineRange.SetActive(true);
    }

    /// <summary>
    /// Medvecsapda időzített funkciója.
    /// </summary>
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
