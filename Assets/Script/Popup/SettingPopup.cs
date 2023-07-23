using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPopup : Popup
{
    static SettingPopup instance;
    public static SettingPopup Instance { get => instance; set => instance = value; }
    private void OnDestroy() { Instance = null; }
    void Awake() { if (Instance == null) Instance = this; }
    //setting
}
