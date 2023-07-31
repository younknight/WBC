using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GetChestPopup : Popup
{

    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI rankingText;
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] GameObject isNewFrame;
    static GetChestPopup instance;
    public static GetChestPopup Instance { get => instance; set => instance = value; }
    private void OnDestroy() { Instance = null; }
    void Awake() { if (Instance == null) Instance = this; }
    public void SetGetChest(string name, string count, Sprite sprite, bool isNew, string ranking)
    {
        nameText.text = name;
        countText.text = count;
        rankingText.text = ranking;
        image.sprite = sprite;
        isNewFrame.SetActive(isNew);
    }
}
