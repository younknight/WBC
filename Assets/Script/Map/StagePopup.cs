using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StagePopup : Popup
{ 
    #region �̱۹Z
    static StagePopup instance;
    public static StagePopup Instance { get => instance; set => instance = value; }
    private void OnDestroy() { Instance = null; }
    void Awake() { if (Instance == null) Instance = this; }
    #endregion
    [SerializeField] TextMeshProUGUI stageName;
    [SerializeField] SceneMoveManager sceneMoveManager;
    public void Setup(string name)
    {
        stageName.text = name;
        Open();
    }
    public void GoDungeon()
    {
        sceneMoveManager.MoveScene("Dungeon");
    }
}
