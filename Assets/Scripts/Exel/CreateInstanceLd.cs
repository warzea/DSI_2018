using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NewBehaviourScript
{

    [MenuItem("Assets/Create/ChunkData")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<ChunkData>();
    }
}
