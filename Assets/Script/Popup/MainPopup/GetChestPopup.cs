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
    [SerializeField] ItemDatabaseManager itemDatabaseManager;
    static GetChestPopup instance;
    public static GetChestPopup Instance { get => instance; set => instance = value; }
    private void OnDestroy() { Instance = null; }
    void Awake() { if (Instance == null) Instance = this; }
    public void SetGetChest(Chest chest)
    {
        nameText.text = chest.itemName;
        countText.text = "x1";
        rankingText.text = chest.ranking;
        image.sprite = chest.itemImage;
        isNewFrame.SetActive(itemDatabaseManager.CheckNew(chest));

        if (chest.id == 0) SoundEffecter.Instance.PlayEffect(soundEffectType.getNegative);//이상한 상자
        else SoundEffecter.Instance.PlayEffect(soundEffectType.getPositive);//그 외
    }
}
