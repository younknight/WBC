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
    [SerializeField] GameObject primo;
    [SerializeField] GameObject gold;
    static PurchasePopup instance;
    public static PurchasePopup Instance { get => instance; set => instance = value; }
    private void OnDestroy() { Instance = null; }
    void Awake() { if (Instance == null) Instance = this; }
    public void SetPurchase(GoodsSlot goodsSlot, string name, string ranking, Sprite sprite, int price, int count, string id)
    {
        goods = goodsSlot;
        nameText.text = name;
        image.sprite = sprite;
        if (count == 0) countText.transform.parent.gameObject.SetActive(false);
        bool isCash = ranking.Equals("cash");
        countText.transform.parent.gameObject.SetActive(!isCash);
        primo.SetActive(isCash);
        gold.SetActive(!isCash);
        if (isCash)
        {
            idText.text = "No.Primo";
            rankingText.text = "SSR";
        }
        else
        {
            idText.text = id;
            rankingText.text = ranking;
            priceText.text = price.ToString();
            countText.text = "x" + count;
        }
    }
    public void Purchase()
    {
        goods.Purchase();
    }
}
