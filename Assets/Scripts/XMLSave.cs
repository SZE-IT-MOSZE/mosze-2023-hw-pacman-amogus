using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Xml.Serialization;

public class XMLSave : MonoBehaviour
{
    public static XMLSave instance;

    private void Awake()
    {
        instance = this;
    }

    public static string saveName = "Save";

    public SaveData saveData;

    public void Save(int saveIndex)
    {
        string dataPath = Application.persistentDataPath;

        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + "/" + saveName + saveIndex + ".save", FileMode.Create);

        serializer.Serialize(stream, saveData);
        stream.Close();
    }

     public void Load(int saveIndex)
    {
        string dataPath = Application.persistentDataPath;

        if (File.Exists(dataPath + "/" + saveName + saveIndex + ".save"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/" + saveName + saveIndex + ".save", FileMode.Open);
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
        File.Delete(Application.persistentDataPath + "/" + saveName + saveIndex + ".save");
    }
}
