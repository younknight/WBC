using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct ListMapInfos
{
   public List<MapInfo> maps;
}
public class MapManager : MonoBehaviour
{
    [SerializeField] List<MapWorld> worlds = new List<MapWorld>();
    [SerializeField] List<ListMapInfos> maps = new List<ListMapInfos>();
    Dictionary<MapWorld, List<MapInfo>> allMaps = new Dictionary<MapWorld, List<MapInfo>>();
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
        if (worlds.Count != maps.Count) throw new System.Exception("dic error : not count matching");
        for(int i = 0;i < worlds.Count; i++)
        {
            allMaps.Add(worlds[i], maps[i].maps);
        }
    }

    void Start()
    {
        SelectedMap = allMaps[0][0];//defult
    }
    public void SelectMap(MapWorld world, int id)
    {
        SelectedMap = allMaps[world][id];
    }
    public string GetStageName(MapWorld world, int id)
    {
        return allMaps[world][id].mapName;
    }
}
