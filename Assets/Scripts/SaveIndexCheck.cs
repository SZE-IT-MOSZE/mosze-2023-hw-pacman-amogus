using UnityEngine;

/// <summary>
/// A mentés indexét ellenőrző osztály.
/// </summary>
public class SaveIndexCheck : MonoBehaviour
{
    /// <summary>
    /// Az osztály példányát tároló statikus referencia.
    /// </summary>
    public static SaveIndexCheck instance;

    private void Awake()
    {
        SetupInstance();
    }

    /// <summary>
    /// Az osztály példányának beállítása.
    /// Ellenõrzi, hogy van-e már példány létrehozva, és ha nincs, akkor létrehozza.
    /// </summary>
    public void SetupInstance()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// A mentés indexét tároló változó.
    /// </summary>
    public int saveIndex;
}
