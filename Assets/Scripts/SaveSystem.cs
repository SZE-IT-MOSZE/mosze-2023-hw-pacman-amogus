using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A SaveSystem osztály felelős a játékállapot mentéséért és betöltéséért.
/// </summary>
public class SaveSystem : MonoBehaviour
{

    /// <summary>
    /// A SaveSystem egyetlen példánya (Singleton).
    /// </summary>

    public static SaveSystem instance;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Felsorolás a jelenet típusával kapcsolatos beállításokhoz.
    /// </summary>

    public enum sceneType
    {
        Generator,
        Play
    }
    [Header("Save System settings")]
    public sceneType type;

    [Header("Maze gameObject settings")]
    public GameObject labPiece;
    public List<Transform> objects;

    /// <summary>
    /// Metódus amely az adatok betöltését végzi a jelenet típusától függően.
    /// </summary>

    public void OnLoad()
    {
        switch (type)
        {
            case sceneType.Generator:
                AddObjectsFromGenerator();
                SaveGameParameters(1000);
                UILogic.instance.SetDifficultyText("Medium");
                UILogic.instance.SetInfinite();
                break;
            case sceneType.Play:
                XMLSave.instance.Load(SaveIndexCheck.instance.saveIndex);
                AddObjectsFromSave();
                SetGameParameters();
                SpawnManager.instance.SpawnNodes();
                SpawnManager.instance.SpawnPickups();
                SpawnManager.instance.SpawnEnemies();
                SpawnManager.instance.SpawnTraps();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Gyermek objektum hozzáadása az objektumok listájához mentés előtt.
    /// </summary>

    public void AddObjectsFromGenerator()
    {
        foreach (Transform child in transform)
        {
            objects.Add(child);
        }

        SaveObjects();
    }

     /// <summary>
    /// Az elmentett adatok alapján létrehozza az objektumokat és beállítja azok állapotát.
    /// </summary>

    public void AddObjectsFromSave()
    {
        for (int i = 0; i < XMLSave.instance.saveData.transforms.Count; i++)
        {
            Vector3 spawnPoint = new Vector3(0, 0, 0);
            spawnPoint = XMLSave.instance.saveData.transforms[i].position;

            Instantiate(labPiece, spawnPoint, Quaternion.identity, transform).gameObject.SetActive(XMLSave.instance.saveData.transforms[i].isActive);
        }
    }

    /// <summary>
    /// Beállítja a játékparamétereket az elmentett adatok alapján.
    /// </summary>

    public void SetGameParameters()
    {
        int playerLives = XMLSave.instance.saveData.lives;
        GameLogic.instance.lives = playerLives;

        int scoreGoal = XMLSave.instance.saveData.scoreGoal;
        GameLogic.instance.scoreGoal = scoreGoal;

        bool isEndless = XMLSave.instance.saveData.isEndless;
        GameLogic.instance.isEndless = isEndless;
    }

    /// <summary>
    /// Az adatstruktúrába menti az obketumok állapotát.
    /// </summary>

    public void SaveObjects()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            bool isActive = objects[i].gameObject.activeSelf;
            Vector3 position = objects[i].position;

            ObjectTransforms transformData = new ObjectTransforms();
            transformData.isActive = isActive;
            transformData.position = position;

            XMLSave.instance.saveData.transforms.Add(transformData);
        }
    }

    /// <summary>
    /// Elmenti a játékparamétereket az adatstruktúrába.
    /// </summary>
    /// <param name="scoreGoal">A játék célja (pl. pontszám).</param>

    public void SaveGameParameters(int scoreGoal)
    {
        XMLSave.instance.saveData.scoreGoal = scoreGoal;
    }
}