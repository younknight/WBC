using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]
public class StoryData
{
    public int progress;
    public Dictionary<MapWorld, int> mapLockProgress;
    public int worldLockProgress;
}
public class StoryManager : MonoBehaviour
{
    #region ½Ì±ÛÅæ
    static StoryManager instance;
    public static StoryManager Instance { get => instance; set => instance = value; }
    public StoryData StoryData { get => storyData; set => storyData = value; }
    string path;
    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
        path = Application.persistentDataPath + "/"+ "Story.json";
        LoadProgress();
    }
    #endregion
    StoryData storyData = new StoryData();
    JsonParser jsonParser = new JsonParser();

    [ContextMenu("Reset Progress Json Data")]
    public void ResetProgress()
    {
        storyData = new StoryData();
        storyData.progress = 0;
        storyData.worldLockProgress = 0;
        SetDic();
        jsonParser.SaveJson<StoryData>(storyData, path);
    }
    void SetDic()
    {
        StoryData.mapLockProgress = new Dictionary<MapWorld, int>();
        storyData.mapLockProgress.Add(MapWorld.fire, 0);
        storyData.mapLockProgress.Add(MapWorld.ice, 0);
        storyData.mapLockProgress.Add(MapWorld.thunder, 0);
        storyData.mapLockProgress.Add(MapWorld.earth, 0);
    }
    public void LoadProgress()
    {
        if (!File.Exists(path))
        {
            ResetProgress();

        }
        storyData = jsonParser.LoadJson<StoryData>(path);
    }
    public void AddMapProgress(MapWorld world)
    {
        LoadProgress();
        storyData.mapLockProgress[world]++;
        jsonParser.SaveJson<StoryData>(storyData, path);
    }
    public void AddWolrdProgress()
    {
        LoadProgress();
        storyData.worldLockProgress++;
        jsonParser.SaveJson<StoryData>(storyData, path);
    }
    public void AddProgress()
    {
        LoadProgress();
        storyData.progress++;
        jsonParser.SaveJson<StoryData>(storyData, path);
    }
}
