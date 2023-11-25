using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

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

    [Header("Generator Scene settings")]
    public GameObject saveButtons;
    public TMP_Text saveButtonText;
    public GameObject loadButtons;
    public List<GameObject> loadList;

    [Header("Play Scene settings")]
    public GameObject pauseMenu;
    public GameObject gameOverText;
    public GameObject invulnerabilityText;
    public TMP_Text livesText;
    public TMP_Text scoreText;
    public bool isPaused;

    private void Start()
    {
        switch (type)
        {
            case UItype.Menu:
                ShowLoadButtons();
                ShowDeleteButton();
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
            if (File.Exists(Application.persistentDataPath + "/" + XMLSave.saveName + (i+1).ToString() + ".save"))
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

        SceneManager.LoadScene("Play");
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

    public void ShowGameOverText()
    {
        gameOverText.gameObject.SetActive(true);
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
                pauseMenu.gameObject.SetActive(true);
                break;
            case false:
                Time.timeScale = 1f;
                pauseMenu.gameObject.SetActive(false);
                break;
        }
    }
}
