using UnityEngine;

/// <summary>
/// A hangeffektek és háttérzene kezeléséért felelős osztály.
/// </summary>
public class SFXLogic : MonoBehaviour
{
    public static SFXLogic instance; 

    private void Awake()
    {
        instance = this;
    }

    public AudioSource[] soundEffects; // Hanghatások forrásai tömbben
    public AudioSource BGM; // Háttérzene forrása

    /// <summary>
    /// Hanghatás lejátszása a megadott indexen.
    /// </summary>
    /// <param name="sfxToPlay">Lejátszandó hanghatás indexe</param>
    public void PlaySFX(int sfxToPlay)
    {
        soundEffects[sfxToPlay].Stop(); // Hanghatás megállítása
        soundEffects[sfxToPlay].Play(); // Hanghatás lejátszása
    }

    /// <summary>
    /// Hanghatás lejátszása véletlenszerű hangmagassággal a megadott indexen.
    /// </summary>
    /// <param name="sfxToPlay">Lejátszandó hanghatás indexe</param>
    public void PlaySFXPitched(int sfxToPlay)
    {
        soundEffects[sfxToPlay].pitch = Random.Range(.8f, 1.2f); // Véletlenszerű hangmagasság beállítása

        PlaySFX(sfxToPlay); // Hanghatás lejátszása
    }

    /// <summary>
    /// Háttérzene lejátszása vagy szüneteltetése a megadott állapot alapján.
    /// </summary>
    /// <param name="paused">Szüneteltetés vagy lejátszás állapota</param>
    public void PlayBGM(bool paused)
    {
        if (paused == true)
        {
            BGM.Pause(); // Háttérzene szüneteltetése
        }
        else
        {
            BGM.UnPause(); // Háttérzene folytatása
        }
    }
}
