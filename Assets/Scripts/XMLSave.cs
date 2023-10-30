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

    public void Save(int saveIndex)
    {
        Debug.Log("Saving data");

        string dataPath = Application.persistentDataPath;

        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + "/testSave" + saveIndex + ".save", FileMode.Create);

        serializer.Serialize(stream, saveData);
        stream.Close();
    }

     public void Load(int saveIndex)
    {
        string dataPath = Application.persistentDataPath;

        if (File.Exists(dataPath + "/testSave" + saveIndex + ".save"))
        {
            Debug.Log("Loading data");

            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/testSave" + saveIndex + ".save", FileMode.Open);
            saveData = serializer.Deserialize(stream) as SaveData;
            stream.Close();
        }
        else
        {
            Debug.LogWarning("Couldn't find data to load!");
        }
    }

    public void ClearSave(int saveIndex)
    {
        Debug.Log("Deleted save file");

        File.Delete(Application.persistentDataPath + "/testSave" + saveIndex + ".save");
    }
}
