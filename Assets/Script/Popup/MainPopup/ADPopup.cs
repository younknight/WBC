using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum rewardType { stamina, shopReroll}
public class ADPopup : Popup
{
    #region �̱۹Z
    static ADPopup instance;
    public static ADPopup Instance { get => instance; set => instance = value; }
    private void OnDestroy() { Instance = null; }
    #endregion
    [SerializeField] TextMeshProUGUI rewardText;
    [SerializeField] rewardType[] rewardTypes;
    [SerializeField] string[] texts;
    Dictionary<rewardType, string> rewardPhrases = new Dictionary<rewardType, string>();
    [SerializeField] rewardType selectedMode;//
    [SerializeField] AdManager adManager;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        if (rewardTypes.Length != texts.Length) throw new System.Exception("dicError:count not match");
        for(int i=0;i< texts.Length; i++)
        {
            rewardPhrases.Add(rewardTypes[i], texts[i]);
        }
    }
    public void OpenPopup(rewardType rewardType)
    {
        selectedMode = rewardType;
        rewardText.text = rewardPhrases[rewardType];
        Open();
    }
    public void Confirm()
    {
        //adManager.LoadRewardedAd();
        //adManager.ShowRewardedAd();
        CloseStart();
    }
}
