using System.Collections;
using UnityEngine;

/// <summary>
/// Az osztály ami reprezentálja egy akna logikai tartalmát
/// </summary>
public class MineLogic : MonoBehaviour
{

    /// <summary>
    /// Referencia az akna robbanás effektus GameObject-re.
    /// </summary>
    public GameObject explosionEffect;

    /// <summary>
    /// Flag, ami jelzi hogy az adott akna felrobbant-e.
    /// </summary>
    private bool exploded;

    /// <summary>
    /// Start metódus.
    /// </summary>

    public void Start()
    {
        StartCoroutine(ExplodeMine());
    }

    /// <summary>
    /// OnTriggerEnter akkor hívódik meg, amikor egy másik Collider belép a triggerbe.
    /// </summary>
    /// <param name="collision">A Collider, amely belép a triggerbe.</param>

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player" && exploded != true)
        {
            exploded = true;
            StartCoroutine(KillPlayerWait());
        }
    }

    /// <summary>
    /// Coroutine a bánya felrobbanásának kezelésére.
    /// </summary>
    /// <returns>Utasítás a robbanás effektus befejezésére való várakozáshoz.</returns>

    public IEnumerator ExplodeMine()
    {
        explosionEffect.SetActive(true);
        SFXLogic.instance.PlaySFX(2);

        yield return new WaitForSeconds(.4f);

        Destroy(transform.parent.gameObject);
    }
    /// <summary>
    /// Coroutine a várakozás kezelésére a játékos megölése előtt.
    /// </summary>
    /// <returns>Utasítás a játékos életének frissítésére és a spawn manager csökkentése előtti várakozáshoz.</returns>

    public IEnumerator KillPlayerWait()
    {
        yield return new WaitForSeconds(.1f);


        //A játékos életének frissítése/csöentése az aknák számával
        GameLogic.instance.SetLives();
        SpawnManager.instance.spawnedTraps--;
    }
}
