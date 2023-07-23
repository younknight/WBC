using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPopup : Popup
{
    static ChestPopup instance;
    public static ChestPopup Instance { get => instance; set => instance = value; }
    private void OnDestroy() { Instance = null; }
    void Awake() { if (Instance == null) Instance = this; }
    //chest
}
