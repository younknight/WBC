using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GoodsSlot : MonoBehaviour
{
    Chest chest;
    Item item;
    int count;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] TextMeshProUGUI countText;
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
    }
    public void Purchase()
    {
        if (chest != null)
        {
            if (GameManager.Gold >= chest.price)
            {
                InventoryManager.instance.AddChest(chest,count);
                GameManager.Gold -= chest.price;
                button.interactable = false;
            }
            else
            {
                //ÀÜ¾×ºÎÁ·
            }
        }
        if (item != null)
        {
            if (GameManager.Gold >= item.price)
            {
                InventoryManager.instance.AddItem(item, count);
                GameManager.Gold -= item.price;
                button.interactable = false;
            }
            else
            {
                //»à
            }
        }
        PopupManager.instance.CloesPopup(popupType.purchase);
    }
    public void OpenPopup()
    {
        string name = "";
        string ranking = ""; 
        Sprite sprite = null; 
        int price = -1;
        int id = -1;
        if (chest != null)
        {
            name = chest.chestName;
            ranking = chest.ranking;
            sprite = chest.chetImage;
            price = chest.price;
            id = chest.id;
        }
        if (item != null)
        {
            name = item.itemName;
            ranking = item.ranking;
            sprite = item.itemImage;
            price = item.price;
            id = item.id;
        }
        PopupManager.instance.OpenPurchasePopup(this, name, ranking, sprite, price, count, id);
    }
    public void ClearSlot()
    {
        button.interactable = true;
        chest = null;
        item = null;
        image.sprite = null;
        priceText.text = "none";
        countText.text = "none";
    }
    public void SetItem(Item item, int count)
    {
        this.item = item;
        this.count = count;
        image.sprite = item.itemImage;
        priceText.text = "" + (item.price * count);
        countText.text = "x" + count;
    }
    public void SetChest(Chest chest, int count)
    {
        this.chest = chest;
        this.count = count;
        image.sprite = chest.chetImage;
        priceText.text = "" + (chest.price * count);
        countText.text = "x" + count;
    }
}
