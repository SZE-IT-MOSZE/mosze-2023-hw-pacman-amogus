using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    //public int points;
    //public int lives;

    public List<ObjectTransforms> transforms;
}

[System.Serializable]
public class ObjectTransforms
{
    public bool isActive;
    public Vector3 position;
}
