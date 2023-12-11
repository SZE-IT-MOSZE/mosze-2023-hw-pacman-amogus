using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// Tutorial logikát kezelő osztály.
/// </summary>
public class TutorialLogic : MonoBehaviour
{
    /// <summary>
    /// Tutorial típusokat definiáló felsorolás.
    /// </summary>
    public enum TutorialType
    {
        Menu,           // Menü
        Generation,     // Generáció
        TutorialCheck   // Tutorial elvégeztének ellenőrzése
    }
    public TutorialType type; // Az aktuális tutorial típusa

    private string filePath; // A tutorial elvégzését bizonyító fájl elérési útja

    /// <summary>
    /// Az első, második és generációs szakaszhoz kapcsolódó játékobjektumok.
    /// </summary>
    [Header("Stage Settings")]
    public GameObject firstStage;
    public GameObject secondStage;
    public GameObject generationStage;

    /// <summary>
    /// Az első szakasz szövegdobozaihoz tartozó játékobjektumok.
    /// </summary>
    [Header("First Stage Textbox Settings")]
    public GameObject textBox1_1;
    public GameObject textBox1_2;
    public GameObject textBox1_3;

    /// <summary>
    /// A második szakasz szövegdobozaihoz tartozó játékobjektumok.
    /// </summary>
    [Header("Second Stage Textbox Settings")]
    public GameObject textBox2_1;
    public GameObject textBox2_2;

    /// <summary>
    /// A generációs szakasz szövegdobozaihoz tartozó játékobjektumok.
    /// </summary>
    [Header("Generation Stage Textbox Settings")]
    public GameObject genTextBox1;
    public GameObject genTextBox2;
    public GameObject genTextBox3;

    /// <summary>
    /// Az első szakaszhoz tartozó gombokhoz kapcsolódó játékobjektumok.
    /// </summary>
    [Header("First Stage Buttons Settings")]
    public GameObject skipTutorialButton;
    public GameObject nextTutorialButton;

    /// <summary>
    /// A második szakaszhoz tartozó gombhoz kapcsolódó játékobjektum.
    /// </summary>
    [Header("Second Stage Buttons Settings")]
    public Button generateButton;

    /// <summary>
    /// A játék indításakor végrehajtott műveletek a tutorial típusától függően.
    /// </summary>
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

    /// <summary>
    /// Az első tutorial szakasz folyamata.
    /// </summary>
    public IEnumerator FirstStage()
    {
        yield return StartCoroutine(TutorialWait(null, null, null, 5f));
        yield return StartCoroutine(TutorialWait(textBox1_2, textBox1_1, skipTutorialButton, 5f));
        yield return StartCoroutine(TutorialWait(textBox1_3, textBox1_2, nextTutorialButton, 5f));
    }

    /// <summary>
    /// Várakozás a tutorial során.
    /// </summary>
    public IEnumerator TutorialWait(GameObject textToShow, GameObject textToHide, GameObject buttonToShow, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (textToHide != null) textToHide.SetActive(false);
        if (textToShow != null) textToShow.SetActive(true);
        if (buttonToShow != null) buttonToShow.SetActive(true);
    }

    /// <summary>
    /// A második tutorial szakasz elindítása.
    /// </summary>
    public void StageTwo()
    {
        firstStage.SetActive(false);
        secondStage.SetActive(true);
        StartCoroutine(SecondStage());
    }

    /// <summary>
    /// A második szakasz tutorial folyamata.
    /// </summary>
    public IEnumerator SecondStage()
    {
        yield return new WaitForSeconds(5f);
        textBox2_1.SetActive(false);
        textBox2_2.SetActive(true);
        generateButton.interactable = true;
    }

    /// <summary>
    /// A generációs tutorial szakasz első lépése.
    /// </summary>
    public void GenerationStage1()
    {
        genTextBox1.SetActive(false);
        genTextBox2.SetActive(true);
    }

    /// <summary>
    /// A generációs tutorial szakasz második lépése.
    /// </summary>
    public void GenerationStage2()
    {
        genTextBox2.SetActive(false);
        genTextBox3.SetActive(true);
    }

    /// <summary>
    /// Új tutorialCheck fájl létrehozása.
    /// </summary>
    public void CreateFile()
    {
        File.Create(filePath);
    }

    /// <summary>
    /// Tutorial átugrása és a menü betöltése.
    /// </summary>
    public void SkipTutorial()
    {
        SceneManager.LoadScene("Menu");
    }
}
