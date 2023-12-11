using UnityEngine;

/// <summary>
/// A ment�s index�t ellen�rz� oszt�ly.
/// </summary>
public class SaveIndexCheck : MonoBehaviour
{
    /// <summary>
    /// Az oszt�ly p�ld�ny�t t�rol� statikus referencia.
    /// </summary>
    public static SaveIndexCheck instance;

    private void Awake()
    {
        SetupInstance();
    }

    /// <summary>
    /// Az oszt�ly p�ld�ny�nak be�ll�t�sa.
    /// Ellen�rzi, hogy van-e m�r p�ld�ny l�trehozva, �s ha nincs, akkor l�trehozza.
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
    /// A ment�s index�t t�rol� v�ltoz�.
    /// </summary>
    public int saveIndex;
}
