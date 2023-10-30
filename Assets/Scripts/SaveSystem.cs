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
    public sceneType type;

    public GameObject labPiece;
    public List<Transform> objects;

    public void OnLoad()
    {
        switch (type)
        {
            case sceneType.Generator:
                Debug.Log("Generator scene");
                AddObjectsFromGenerator();
                break;
            case sceneType.Play:
                Debug.Log("Play scene");
                AddObjectsFromSave();
                SpawnManager.instance.SpawnObjects();
                break;
            default:
                Debug.Log("Valami nem jó tesi xddd");
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
        XMLSave.instance.Load(SaveIndexCheck.instance.saveIndex);

        for (int i = 0; i < XMLSave.instance.saveData.transforms.Count; i++)
        {
            Vector3 spawnPoint = new Vector3(0, 0, 0);
            spawnPoint = XMLSave.instance.saveData.transforms[i].position;

            Instantiate(labPiece, spawnPoint, Quaternion.identity, transform).gameObject.SetActive(XMLSave.instance.saveData.transforms[i].isActive);
        }
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