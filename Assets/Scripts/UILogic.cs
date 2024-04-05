using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// A UI-logikát kezelő osztály a játék különböző jelenetei között.
/// </summary>
public class UILogic : MonoBehaviour
{
    /// <summary>
    /// A UILogic osztály singletonja.
    /// </summary>
    public static UILogic instance;

    /// <summary>
    /// A singleton inicializálása.
    /// </summary>
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Felhasználói felület típusait reprezentáló felsorolás.
    /// </summary>
    public enum UItype
    {
        Menu,
        Generator,
        Play
    }
    public UItype type; /// Az aktuális felület típusa.

    /// <summary>
    /// A Menu típusú felhasználói felület objektumai.
    /// </summary>
    [Header("Menu Scene settings")]
    public GameObject deleteButton;
    public GameObject mainScreen;
    public GameObject optionsScreen;
    public AudioMixer mainMixer;
    public Toggle sfxToggle;
    public Toggle bgmToggle;

    /// <summary>
    /// A Generator típusú felhasználói felület objektumai.
    /// </summary>
    [Header("Generator Scene settings")]
    public GameObject saveButtons;
    public TMP_Text saveButtonText;
    public GameObject loadButtons;
    public List<GameObject> loadList;
    public TMP_Text difficultyText;
    public Toggle infiniteToggle;

    /// <summary>
    /// A Play típusú felhasználói felület objektumai.
    /// </summary>
    [Header("Play Scene settings")]
    public GameObject pauseMenu;
    public GameObject gameOverText;
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject invulnerabilityText;
    public TMP_Text livesText;
    public TMP_Text scoreText;
    [HideInInspector]
    public bool isPaused;
    [HideInInspector]
    public bool audioPlayed;

    /// <summary>
    /// A script kezdeti értékeinek beállítása.
    /// </summary>
    private void Start()
    {
        Time.timeScale = 1f;

        switch (type)
        {
            case UItype.Menu:
                ShowLoadButtons();
                ShowDeleteButton();
                AudioOptionChange();
                break;
            case UItype.Generator:
                ShowLoadButtons();
                break;
            case UItype.Play:
                Time.timeScale = 1f;
                SetLivesText(GameLogic.instance.lives);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Folyamatos frissítés kereteiben történő vizsgálatok és műveletek.
    /// </summary>
    private void Update()
    {
        if (type == UItype.Play)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
            {
                isPaused = true;
                ShowPauseMenu();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
            {
                isPaused = false;
                ShowPauseMenu();
            }
        }
    }

    //////MENU UI//////

    /// <summary>
    /// Megadott jelenet betöltése.
    /// </summary>
    /// <param name="sceneName">A betöltendő jelenet neve.</param>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Törlés gomb megjelenítése vagy elrejtése az elmentett fájloknak megfelelően.
    /// </summary>
    public void ShowDeleteButton()
    {
        List<int> saves = new List<int>();

        for (int i = 0; i < loadList.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + "/" + XMLSave.saveName + (i + 1).ToString() + ".save"))
            {
                saves.Add(i);
            }
        }

        if (saves.Count > 0)
        {
            deleteButton.gameObject.SetActive(true);
        }
        else
        {
            deleteButton.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Elmentett fájlok törlése, majd az állapotnak megfelelő gombok megjelenítése vagy elrejtése.
    /// </summary>
    public void DeleteSaves()
    {
        for (int i = 0; i < loadList.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + "/" + XMLSave.saveName + (i + 1).ToString() + ".save"))
            {
                File.Delete(Application.persistentDataPath + "/" + XMLSave.saveName + (i + 1).ToString() + ".save");
            }
        }

        ShowDeleteButton();
        ShowLoadButtons();
    }

    /// <summary>
    /// Beállítások képernyő megjelenítése vagy elrejtése.
    /// </summary>
    public void ShowOptions()
    {
        if (optionsScreen.activeInHierarchy == false)
        {
            optionsScreen.SetActive(true);
            mainScreen.SetActive(false);
        }
        else
        {
            optionsScreen.SetActive(false);
            mainScreen.SetActive(true);
        }
    }

    /// <summary>
    /// Hangbeállítások változtatása.
    /// </summary>
    public void AudioOptionChange()
    {
        if (sfxToggle.isOn)
        {
            mainMixer.SetFloat("SFXvolume", 0f);
        }
        else
        {
            mainMixer.SetFloat("SFXvolume", -80f);
        }

        if (bgmToggle.isOn)
        {
            mainMixer.SetFloat("BGMvolume", -20f);
        }
        else
        {
            mainMixer.SetFloat("BGMvolume", -80f);
        }
    }

    /// <summary>
    /// Kilépés a játékból.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    //////GENERATOR UI//////

    /// <summary>
    /// A labirintus újragenerálása.
    /// </summary>
    public void RegenerateMaze()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Betöltés gombok megjelenítése az elmentett állapotok alapján.
    /// </summary>
    public void ShowLoadButtons()
    {
        for (int i = 0; i < loadList.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + "/" + XMLSave.saveName + (i + 1).ToString() + ".save"))
            {
                loadList[i].gameObject.SetActive(true);
            }
            else
            {
                loadList[i].gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Mentés gombok megjelenítése vagy elrejtése a gomb aktuális állapotától függően.
    /// </summary>
    public void ShowSaveButtons()
    {
        if (saveButtons.activeSelf)
        {
            saveButtonText.text = "Save";
            saveButtons.SetActive(false);
            loadButtons.SetActive(true);
        }
        else
        {
            saveButtonText.text = "Back";
            saveButtons.SetActive(true);
            loadButtons.SetActive(false);
        }
    }

    /// <summary>
    /// Elmentett játékállás mentése a megadott mentési index helyére.
    /// </summary>
    /// <param name="saveIndex">A mentés indexe.</param>
    public void SaveSlot(int saveIndex)
    {
        XMLSave.instance.Save(saveIndex);

        ShowSaveButtons();
        ShowLoadButtons();
    }

    /// <summary>
    /// Adott indexű mentés betöltése.
    /// </summary>
    /// <param name="loadIndex">A betöltendő mentés indexe.</param>
    public void LoadSlot(int loadIndex)
    {
        SaveIndexCheck.instance.saveIndex = loadIndex;

        SceneManager.LoadScene("Cutscene");
    }

    /// <summary>
    /// Nehézségi szint szövegének beállítása.
    /// </summary>
    /// <param name="difficulty">A beállítandó nehézségi szint szövege.</param>
    public void SetDifficultyText(string difficulty)
    {
        difficultyText.text = difficulty;
    }

    /// <summary>
    /// Végtelen mód beállítása a kapcsoló állapotától függően.
    /// </summary>
    public void SetInfinite()
    {
        if (infiniteToggle.isOn)
        {
            XMLSave.instance.saveData.isEndless = true;
        }
        else
        {
            XMLSave.instance.saveData.isEndless = false;
        }
    }

    //////PLAY UI//////

    /// <summary>
    /// Életek számának beállítása és megjelenítése.
    /// </summary>
    /// <param name="lives">Beállítandó életek száma.</param>
    public void SetLivesText(int lives)
    {
        livesText.text = "Lives: " + lives.ToString();
    }

    /// <summary>
    /// Pontszám beállítása és megjelenítése.
    /// </summary>
    /// <param name="score">Beállítandó pontszám.</param>
    public void SetScoreText(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    /// <summary>
    /// Végeredmény képernyő megjelenítése (győzelem/vereség).
    /// </summary>
    /// <param name="isWin">Igaz érték esetén győzelem, hamis érték esetén vereség.</param>
    public void ShowEndScreen(bool isWin)
    {
        if (isWin == true)
        {
            SFXLogic.instance.PlayBGM(true);
            winScreen.gameObject.SetActive(true);
            if (audioPlayed == false)
            {
                audioPlayed = true;
                SFXLogic.instance.PlaySFX(4);
            }
        }
        else if (isWin == false)
        {
            loseScreen.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Sebezhetetlenség szövegének megjelenítése vagy elrejtése.
    /// </summary>
    public void ShowInvulnerabilityText()
    {
        if (PlayerController.instance.invulnerable == true)
        {
            invulnerabilityText.gameObject.SetActive(true);
        }
        else
        {
            invulnerabilityText.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Szünetmenü megjelenítése vagy elrejtése a játék szüneteltetése mellett.
    /// </summary>
    public void ShowPauseMenu()
    {
        switch (isPaused)
        {
            case true:
                Time.timeScale = 0f;
                SFXLogic.instance.PlayBGM(isPaused);
                pauseMenu.gameObject.SetActive(true);
                break;
            case false:
                Time.timeScale = 1f;
                SFXLogic.instance.PlayBGM(isPaused);
                pauseMenu.gameObject.SetActive(false);
                break;
        }
    }
}