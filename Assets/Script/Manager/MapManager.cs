using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] List<MapInfo> maps = new List<MapInfo>();
    private static MapInfo selectedMap;

    public static MapInfo SelectedMap { get => selectedMap; set => selectedMap = value; }
    private static MapManager instance;

    public static MapManager Instance { get => instance; set => instance = value; }
    private void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }

    void Start()
    {
        SelectedMap = maps[0];//defult
    }
    public void SelectMap(int id)
    {
        SelectedMap = maps[id];
    }
}
