using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] List<MapInfo> maps = new List<MapInfo>();
    public static MapInfo selectedMap;
    void Start()
    {
        selectedMap = maps[0];//defult
    }
    public void SelectMap(int id)
    {
        selectedMap = maps[id];
    }
}
