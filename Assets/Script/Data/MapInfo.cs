using System;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct EnenmySwarm
{
    public List<GameObject> enemyInfos;
}

[CreateAssetMenu]
public class MapInfo : ScriptableObject
{
    public int id;
    public string mapName;
    public MapWorld world;
    [Multiline(5)]
    public string mapExplain;
    public string ranking;
    public int isStory;
    public bool isLastStage = false;
    public List<EnenmySwarm> enenmies;

    

    private void OnValidate()
    {
        string[] idNum = name.Split('.');
        mapName = idNum[1];
        id = Convert.ToInt32(idNum[0]);
    }
}
