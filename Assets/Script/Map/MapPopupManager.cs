using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPopupManager : MonoBehaviour
{
    public List<MapWorld> mapWorld;
    public List<MapPopup> popups;
    Dictionary<MapWorld, MapPopup> maps = new Dictionary<MapWorld, MapPopup>();
    static public MapPopupManager instance;
    private void OnDestroy()
    {
        instance = null;
    }
    private void Awake()
    {
        if (instance == null) instance = this;
        if (mapWorld.Count != popups.Count) throw new System.Exception("dicError: not count match");
        for(int i = 0; i < mapWorld.Count; i++)
        {
            maps.Add(mapWorld[i],popups[i]);
        }
    }
    public void OpenPopup(MapWorld world)
    {
        maps[world].Open();
        maps[world].Setup(maps[world].name, StoryManager.Instance.StoryData.mapLockProgress[world]);
    }
}
