using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMoveManager : MonoBehaviour
{
    public void GoDungeon()
    {
        SceneManager.LoadScene("Dungeon");
    }
    public void GoHome()//
    {
        SceneManager.LoadScene("Main");
    }
}
