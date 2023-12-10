using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILogic : MonoBehaviour
{
    public static UILogic instance;

    private void Awake()
    {
        instance = this;
    }

    public enum UItype
    {
        Menu,
        Generator,
        Play
    }
    public UItype type;

    [Header("Menu Scene settings")]
    public GameObject deleteButton;
    public GameObject mainScreen;
    public GameObject optionsScreen;
    public AudioMixer mainMixer;
    public Toggle sfxToggle;
    public Toggle bgmToggle;

    [Header("Generator Scene settings")]
    public GameObject saveButtons;
    public TMP_Text saveButtonText;
    public GameObject loadButtons;
    public List<GameObject> loadList;
    public TMP_Text difficultyText;
    public Toggle infiniteToggle;

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

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

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

    public void QuitGame()
    {
        Application.Quit();
    }

    //////GENERATOR UI//////

    public void RegenerateMaze()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

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

    public void SaveSlot(int saveIndex)
    {
        XMLSave.instance.Save(saveIndex);

        ShowSaveButtons();
        ShowLoadButtons();
    }

    public void LoadSlot(int loadIndex)
    {
        SaveIndexCheck.instance.saveIndex = loadIndex;

        SceneManager.LoadScene("Cutscene");
    }

    public void SetDifficultyText(string difficulty)
    {
        difficultyText.text = difficulty;
    }

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

    public void SetLivesText(int lives)
    {
        livesText.text = "Lives: " + lives.ToString();
    }

    public void SetScoreText(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

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