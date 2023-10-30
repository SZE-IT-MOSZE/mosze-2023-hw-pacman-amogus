using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Xml.Serialization;

using UnityEditor;

public class XMLSave : MonoBehaviour
{
    public static XMLSave instance;

    private void Awake()
    {
        instance = this;
    }

    public SaveData saveData;

    public void Save()
    {
        Debug.Log("Saving data");

        string dataPath = Application.persistentDataPath;

        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + "/testSave.save", FileMode.Create);

        serializer.Serialize(stream, saveData);
        stream.Close();
    }

     public void Load()
    {
        string dataPath = Application.persistentDataPath;

        if (File.Exists(dataPath + "/testSave.save"))
        {
            Debug.Log("Loading data");

            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/testSave.save", FileMode.Open);
            saveData = serializer.Deserialize(stream) as SaveData;
            stream.Close();
        }
        else
        {
            Debug.LogWarning("Couldn't find data to load!");
        }
    }

    public void ClearSave()
    {
        Debug.Log("Deleted save file");

        File.Delete(Application.persistentDataPath + "/testSave.save");
    }
}
