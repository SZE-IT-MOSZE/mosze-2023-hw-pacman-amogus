using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A csomópontok logikáját kezelő osztály.
/// </summary>
public class NodeLogic : MonoBehaviour
{
    /// <summary>
    /// Az elérhető irányok listája.
    /// Alapértelmezett értékekkel inicializálva: fel, le, balra, jobbra.
    /// </summary>
    public List<string> avalibleDirections = new List<string>()
    {
        "Up",
        "Down",
        "Left",
        "Right"
    };

    /// <summary>
    /// Az észlelési távolság.
    /// </summary>
    public float triggerRange;

    /// <summary>
    /// Az osztály példányának létrehozásakor fut le.
    /// </summary>
    private void Start()
    {
        // Végigmegyünk az elérhető irányokon fordított sorrendben
        for (int i = avalibleDirections.Count - 1; i >= 0; i--)
        {
            RaycastHit hit;
            Vector3 direction = Vector3.zero;

            // Irány beállítása a megfelelő irányérték alapján
            switch (avalibleDirections[i])
            {
                case "Up":
                    direction = Vector3.forward;
                    break;
                case "Down":
                    direction = Vector3.back;
                    break;
                case "Left":
                    direction = Vector3.left;
                    break;
                case "Right":
                    direction = Vector3.right;
                    break;
                default:
                    break;
            }

            // Ellenőrizzük, hogy a megadott irányban van-e ütközés
            if (Physics.Raycast(transform.position, direction, out hit, triggerRange) && hit.collider.tag != "Node")
            {
                avalibleDirections.Remove(avalibleDirections[i]); // Irány eltávolítása az elérhető irányok közül, amennyiben van ütközés
            }
        }

        // Ha az elérhető irányok száma kevesebb vagy egyenlő 2-vel, akkor megsemmisítjük a csomópontot
        if (avalibleDirections.Count <= 2)
        {
            Destroy(gameObject);
        }
    }
}
