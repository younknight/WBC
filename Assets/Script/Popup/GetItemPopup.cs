using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GetItemPopup : Popup
{

    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI rankingText;
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] GameObject isNewFrame;
    static GetItemPopup instance;
    public static GetItemPopup Instance { get => instance; set => instance = value; }
    private void OnDestroy() { Instance = null; }
    void Awake() { if (Instance == null) Instance = this; }
    public void SetGetItem(string name, string count, Sprite sprite, bool isNew, string ranking)
    {
        nameText.text = name;
        countText.text = count;
        rankingText.text = ranking;
        image.sprite = sprite;
        isNewFrame.SetActive(isNew);
    }

}
