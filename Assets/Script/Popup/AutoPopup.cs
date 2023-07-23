using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPopup : Popup
{
    #region �̱۹Z
    static AutoPopup instance;
    public static AutoPopup Instance { get => instance; set => instance = value; }
    private void OnDestroy() { Instance = null; }
    void Awake() { if (Instance == null) Instance = this; }
    #endregion
    [SerializeField] AutoCraftSlot slot;

    public void OpenPopup()
    {
        slot.FreashSlot();
        Open();
    }
}
