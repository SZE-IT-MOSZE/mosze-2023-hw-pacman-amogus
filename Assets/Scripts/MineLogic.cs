using System.Collections;
using UnityEngine;

public class MineLogic : MonoBehaviour
{
    public GameObject explosionEffect;
    private bool exploded;

    public void Start()
    {
        StartCoroutine(ExplodeMine());
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player" && exploded != true)
        {
            exploded = true;
            StartCoroutine(KillPlayerWait());
        }
    }

    public IEnumerator ExplodeMine()
    {
        explosionEffect.SetActive(true);
        SFXLogic.instance.PlaySFX(2);

        yield return new WaitForSeconds(.4f);

        Destroy(transform.parent.gameObject);
    }

    public IEnumerator KillPlayerWait()
    {
        yield return new WaitForSeconds(.1f);

        GameLogic.instance.SetLives();
        SpawnManager.instance.spawnedTraps--;
    }
}
