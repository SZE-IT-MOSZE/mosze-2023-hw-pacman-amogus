using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int scoreGoal;
    public bool isEndless;
    public int lives;

    public List<ObjectTransforms> transforms;
}

[System.Serializable]
public class ObjectTransforms
{
    public bool isActive;
    public Vector3 position;
}
