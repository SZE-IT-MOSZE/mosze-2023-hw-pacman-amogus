using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class UILogic : MonoBehaviour
{
    public static UILogic instance;

    private void Awake()
    {
        instance = this; 
    }

    public GameObject saveButtons;
    public GameObject loadButtons;
    public List<GameObject> loadList;

    private void Start()
    {
        ShowLoadButtons();
    }

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
}
