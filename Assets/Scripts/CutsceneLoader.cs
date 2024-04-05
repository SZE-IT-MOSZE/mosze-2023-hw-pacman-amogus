using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneLoader : MonoBehaviour
{    
    public void Start()
    {
        SceneManager.LoadScene("Play");
    }
}
