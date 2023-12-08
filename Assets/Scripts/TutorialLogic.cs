using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialLogic : MonoBehaviour
{
    public enum TutorialType
    {
        Menu,
        Generation,
        TutorialCheck
    }
    public TutorialType type;

    private string filePath;

    [Header("Stage Settings")]
    public GameObject firstStage;
    public GameObject secondStage;
    public GameObject generationStage;

    [Header("First Stage Textbox Settings")]
    public GameObject textBox1_1;
    public GameObject textBox1_2;
    public GameObject textBox1_3;

    [Header("Second Stage Textbox Settings")]
    public GameObject textBox2_1;
    public GameObject textBox2_2;

    [Header("Generation Stage Textbox Settings")]
    public GameObject genTextBox1;
    public GameObject genTextBox2;
    public GameObject genTextBox3;

    [Header("First Stage Buttons Settings")]
    public GameObject skipTutorialButton;
    public GameObject nextTutorialButton;

    [Header("Second Stage Buttons Settings")]
    public Button generateButton;

    private void Start()
    {
        switch (type)
        {
            case TutorialType.Menu:
                firstStage.SetActive(true);
                StartCoroutine(FirstStage());
                break;
            case TutorialType.Generation:
                for (int i = 0; i <= 3; i++)
                {
                    XMLSave.instance.ClearSave(i);
                }
                UILogic.instance.ShowLoadButtons();
                generationStage.SetActive(true);
                break;
            case TutorialType.TutorialCheck:
                filePath = Path.Combine(Application.persistentDataPath, "tutorialCheck");

                if (!File.Exists(filePath))
                {
                    CreateFile();
                    SceneManager.LoadScene("MenuTutorial");
                }
                else
                {
                    SceneManager.LoadScene("Menu");
                }
                break;
            default:
                break;
        }
    }

    public IEnumerator FirstStage()
    {
        yield return StartCoroutine(TutorialWait(null, null, null, 5f));
        yield return StartCoroutine(TutorialWait(textBox1_2, textBox1_1, skipTutorialButton, 5f));
        yield return StartCoroutine(TutorialWait(textBox1_3, textBox1_2, nextTutorialButton, 5f));
    }

    public IEnumerator TutorialWait(GameObject textToShow, GameObject textToHide, GameObject buttonToShow, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (textToHide != null) textToHide.SetActive(false);
        if (textToShow != null) textToShow.SetActive(true);
        if (buttonToShow != null) buttonToShow.SetActive(true);
    }

    public void StageTwo()
    {
        firstStage.SetActive(false);
        secondStage.SetActive(true);
        StartCoroutine(SecondStage());
    }

    public IEnumerator SecondStage()
    {
        yield return new WaitForSeconds(5f);
        textBox2_1.SetActive(false);
        textBox2_2.SetActive(true);
        generateButton.interactable = true;
    }

    public void GenerationStage1()
    {
        genTextBox1.SetActive(false);
        genTextBox2.SetActive(true);
    }

    public void GenerationStage2()
    {
        genTextBox2.SetActive(false);
        genTextBox3.SetActive(true);
    }

    public void CreateFile()
    {
        File.Create(filePath);
    }

    public void SkipTutorial()
    {
        SceneManager.LoadScene("Menu");
    }
}
