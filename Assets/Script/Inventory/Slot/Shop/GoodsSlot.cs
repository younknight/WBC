using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum lockType { none, maxCraftCounter, craftCoolTime, openSlotCount}
public class GoodsSlot : MonoBehaviour
{
    Chest chest;
    Item item;
    int count;
    int price;
    [SerializeField] lockType lockType;//

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
            if (GameManager.Gold >= price)
            {
                InventoryManager.instance.AddItems<Chest>(chest,count);
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
            if (GameManager.Gold >= price)
            {
                InventoryManager.instance.AddItems<Item>(item, count);
                GameManager.Gold -= item.price;
                button.interactable = false;
            }
            else
            {
                //»à
            }
        }
        if(lockType != lockType.none)
        {
            if (GameManager.Gold >= price)
            {

                GameManager.Gold -= item.price;
                int price = -1;
                if (lockType == lockType.craftCoolTime)
                {
                    LockManager.LockInfo.craftCoolTime--;
                    price = (int)(20 - LockManager.LockInfo.craftCoolTime) * 1000;
                }
                if (lockType == lockType.openSlotCount)
                {
                    LockManager.LockInfo.maxOpenerCount++;
                    Opener.Instance.SetUnlock();
                    price = LockManager.LockInfo.maxOpenerCount * 1000;
                }
                if (lockType == lockType.maxCraftCounter)
                {
                    LockManager.LockInfo.maxCraftCount++;
                    price = LockManager.LockInfo.maxCraftCount * 1000;
                }
                SetLock(price,lockType);
            }
            else
            {
                //»à
            }
        }
        PurchasePopup.Instance.CloseStart();
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
        PurchasePopup.Instance.SetPurchase(this, name, ranking, sprite, price, count, id);
        PurchasePopup.Instance.Open();
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
        price = item.price;
        image.sprite = item.itemImage;
        priceText.text = "" + (item.price * count);
        countText.text = "x" + count;
    }
    public void SetChest(Chest chest, int count)
    {
        this.chest = chest;
        this.count = count;
        price = chest.price;
        image.sprite = chest.chetImage;
        priceText.text = "" + (chest.price * count);
        countText.text = "x" + count;
    }
    public void SetLock(int price, lockType lockType)
    {
        this.lockType = lockType;
        this.price = price;
        priceText.text = price.ToString();
        countText.text = "Lv." + (price / 1000);
    }
}
