using System.Collections;
using UnityEngine;

public class MineLogic : MonoBehaviour
{
    public void Start()
    {
        StartCoroutine(ExplodeMine());
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            GameLogic.instance.KillPlayer();
            SpawnManager.instance.spawnedTraps--;
        }
    }

    public IEnumerator ExplodeMine()
    {
        yield return new WaitForSeconds(1f);

        Destroy(transform.parent.gameObject);
    }
}
