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
    [SerializeField] Transform slotsParent;
    [SerializeField] SceneMoveManager sceneMoveManager;
    GetItemSlot[] slots;
    private void OnValidate()
    {
        slots = slotsParent.GetComponentsInChildren<GetItemSlot>();
    }
    public void Setup(MapInfo stage)
    {
        stageName.text = stage.mapName;
        List<Unit> enemies = MapManager.Instance.GetAllEnemy(stage);
        int i = 0;
        for(;i < enemies.Count; i++)
        {
            slots[i].Setup(true, enemies[i].Portrait, enemies[i].name);
        }
        for (; i < slots.Length; i++)
        {
            slots[i].Setup(false, null, null);
        }
        Open();
    }
    public void GoDungeon()
    {
        sceneMoveManager.MoveScene("Dungeon");
    }
}
