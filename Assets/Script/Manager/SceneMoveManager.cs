using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMoveManager : MonoBehaviour
{
    public void MoveStoryScene(int index)
    {
        string nextScene = index == StoryManager.Instance.StoryData.progress ? "Story" : "Main";
        LoadingSceneManager.LoadScene(nextScene);
    }
    public void MoveScene(string nextScene)
    {
        LoadingSceneManager.LoadScene(nextScene);
    }
}
