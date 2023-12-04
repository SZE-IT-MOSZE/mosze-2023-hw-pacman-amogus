using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXLogic : MonoBehaviour
{
    public static SFXLogic instance;

    private void Awake()
    {
        instance = this;
    }

    public AudioSource[] soundEffects;
    public AudioSource BGM;

    public void PlaySFX(int sfxToPlay)
    {
        soundEffects[sfxToPlay].Stop();
        soundEffects[sfxToPlay].Play();
    }

    public void PlaySFXPitched(int sfxToPlay)
    {
        soundEffects[sfxToPlay].pitch = Random.Range(.8f, 1.2f);

        PlaySFX(sfxToPlay);
    }

    public void PlayBGM(bool paused)
    {
        if (paused == true)
        {
            BGM.Pause();
        }
        else
        {
            BGM.UnPause();
        }
    }
}
