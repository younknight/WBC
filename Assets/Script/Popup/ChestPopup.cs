using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPopup : Popup
{
    #region ½Ì±ÛÅæ
    static ChestPopup instance;
    public static ChestPopup Instance { get => instance; set => instance = value; }
    private void OnDestroy() { Instance = null; }
    void Awake() { if (Instance == null) Instance = this; }
    #endregion
    [SerializeField] GameObject recipe;
    [SerializeField] GameObject autoCraft;
    public void OpenPopup(bool IsAutoCraft)
    {
        autoCraft.SetActive(IsAutoCraft);
        recipe.SetActive(!IsAutoCraft);
        Open();
    }
}
