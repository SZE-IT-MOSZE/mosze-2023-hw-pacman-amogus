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
        Generator,
        Play
    }
    public UItype type;

    [Header("Generator Scene settings")]
    public GameObject saveButtons;
    public GameObject loadButtons;
    public List<GameObject> loadList;

    [Header("Play Scene settings")]
    public GameObject gameOverText;
    public GameObject invulnerabilityText;
    public TMP_Text livesText;
    public TMP_Text scoreText;

    private void Start()
    {
        switch (type)
        {
            case UItype.Generator:
                ShowLoadButtons();
                break;
            case UItype.Play:
                SetLivesText(GameLogic.instance.lives);
                break;
            default:
                break;
        }
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
            if (File.Exists(Application.persistentDataPath + "/testSave" + (i+1).ToString() + ".save"))
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
            saveButtons.SetActive(false);
            loadButtons.SetActive(true);
        }
        else
        {
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
}
