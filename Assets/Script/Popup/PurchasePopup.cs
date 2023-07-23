using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchasePopup : Popup
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI rankingText;
    [SerializeField] TextMeshProUGUI idText;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] GoodsSlot goods;
    static PurchasePopup instance;
    public static PurchasePopup Instance { get => instance; set => instance = value; }
    private void OnDestroy() { Instance = null; }
    void Awake() { if (Instance == null) Instance = this; }
    public void SetPurchase(GoodsSlot goodsSlot, string name, string ranking, Sprite sprite, int price, int count, int id)
    {
        goods = goodsSlot;
        idText.text = id.ToString();
        nameText.text = name;
        rankingText.text = ranking;
        image.sprite = sprite;
        priceText.text = price.ToString();
        countText.text = "x" + count;
    }
    public void Purchase()
    {
        goods.Purchase();
    }
}
