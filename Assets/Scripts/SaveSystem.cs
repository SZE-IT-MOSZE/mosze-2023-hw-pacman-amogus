using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;

    private void Awake()
    {
        instance = this;
    }

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

    public void OnLoad()
    {
        switch (type)
        {
            case sceneType.Generator:
                AddObjectsFromGenerator();
                break;
            case sceneType.Play:
                XMLSave.instance.Load(SaveIndexCheck.instance.saveIndex);
                AddObjectsFromSave();
                SetGameParameters();
                SpawnManager.instance.SpawnObjects();
                break;
            default:
                break;
        }
    }

    public void AddObjectsFromGenerator()
    {
        foreach (Transform child in transform)
        {
            objects.Add(child);
        }

        SaveObjects();
    }

    public void AddObjectsFromSave()
    {
        for (int i = 0; i < XMLSave.instance.saveData.transforms.Count; i++)
        {
            Vector3 spawnPoint = new Vector3(0, 0, 0);
            spawnPoint = XMLSave.instance.saveData.transforms[i].position;

            Instantiate(labPiece, spawnPoint, Quaternion.identity, transform).gameObject.SetActive(XMLSave.instance.saveData.transforms[i].isActive);
        }
    }

    public void SetGameParameters()
    {
        int playerLives = XMLSave.instance.saveData.lives;
        GameLogic.instance.lives = playerLives;

        int scoreGoal = XMLSave.instance.saveData.scoreGoal;
        GameLogic.instance.scoreGoal = scoreGoal;
    }

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
}