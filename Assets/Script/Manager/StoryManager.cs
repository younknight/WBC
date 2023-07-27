using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]
public class StoryData
{
    public int progress;
}
public class StoryManager : MonoBehaviour
{
    #region ½Ì±ÛÅæ
    static StoryManager instance;
    public static StoryManager Instance { get => instance; set => instance = value; }
    public StoryData StoryData { get => storyData; set => storyData = value; }

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
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
        string path = Path.Combine(Application.dataPath, "Story.json");
        jsonParser.SaveJson<StoryData>(storyData, path);
    }
    public void LoadProgress()
    {
        storyData = jsonParser.LoadJson<StoryData>("Story.json");
    }
    public void AddProgress()
    {
        LoadProgress();
        storyData.progress++;
        jsonParser.SaveJson<StoryData>(storyData, "Story.json");
    }
}
