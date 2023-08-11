using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum lockType { none, maxCraftCounter, craftCoolTime, openSlotCount}
[System.Serializable]
public class Goods
{
    public int id;
    public int count;

    public Goods(int id, int count)
    {
        this.id = id;
        this.count = count;
    }
}
public class GoodsSlot : MonoBehaviour
{
    [SerializeField] InventoryManager inventoryManager;//
    [SerializeField] bool isSpecialChest;
    [SerializeField] Chest chest;
    Item item;
    int count;
    int price;
    [SerializeField] Goods goods;



    [SerializeField] lockType lockType;//
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] Sprite defaultSprite;
    Button button;
    List<int> requestPrimoCost = new List<int>() { 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000 };

    public Goods Goods { get => goods; set => goods = value; }

    private void Awake()
    {
        button = GetComponent<Button>();
    }
    public void Purchase()
    {
        if (chest != null)
        {
            if (isSpecialChest)//Æ¯¼ö»óÀÚ
            {
                if (ResourseManager.Instance.GetPrimo() >= price)
                {
                    PurchasePopup.Instance.Close();
                    ResourseManager.Instance.PurchaseWithPrimo(true, price);
                    GetItemPopup.Instance.SetGetItem(chest);
                    GetItemPopup.Instance.Open();
                }
                else
                {
                    //ÀÜ¾×ºÎÁ·
                }
            }
            else//ÀÏ¹Ý»óÀÚ
            {
                if (ResourseManager.Instance.GetGold() >= price)
                {
                    inventoryManager.AddItems<Chest>(chest, count);
                    ResourseManager.Instance.Purchase(true, price);
                    button.interactable = false;
                }
                else
                {
                    //ÀÜ¾×ºÎÁ·
                }
            }
        }
        if (item != null)
        {
            if (ResourseManager.Instance.GetGold() >= price)
            {
                inventoryManager.AddItems<Item>(item, count);
                ResourseManager.Instance.Purchase(true,price);
                button.interactable = false;
            }
            else
            {
                //»à
            }
        }
        if(lockType != lockType.none)
        {
            if (ResourseManager.Instance.GetGold() >= price)
            {

                ResourseManager.Instance.Purchase(true,this.price);
                int price = -1;
                if (lockType == lockType.craftCoolTime)
                {
                    LockManager.LockInfo.craftCoolTime--;
                }
                if (lockType == lockType.openSlotCount)
                {
                    LockManager.LockInfo.maxOpenerCount++;
                    Opener.Instance.SetUnlock();
                }
                if (lockType == lockType.maxCraftCounter)
                {
                    LockManager.LockInfo.maxCraftCount++;
                }
                price = LockManager.Instance.GetLevel(lockType) * 1000;
                DataManager.instance.JsonSave();
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
        Sprite sprite = defaultSprite; 
        int price = -1;
        string id = "";
        if (chest != null)
        {
            name = chest.chestName;
            ranking = chest.ranking;
            sprite = chest.chetImage;
            price = chest.price;
            id = "No." + chest.id;
        }
        if (item != null)
        {
            name = item.itemName;
            ranking = item.ranking;
            sprite = item.itemImage;
            price = item.price;
            id = "No." + item.id;
        }
        if(lockType != lockType.none)
        {
            name = lockType.ToString();
            id = "Lv";
            ranking += (this.price / 1000);
            price = this.price;
        }
        PurchasePopup.Instance.SetPurchase(this, name, ranking, sprite, price, count, id);
        PurchasePopup.Instance.Open();
    }
    public void SetButton(bool interactive,int level)
    {
        image.sprite = chest.GetSprite();
        count = 1;
        countText.text = "LV." + (level + 1);
        priceText.text = "" + requestPrimoCost[level];
        button.interactable = interactive;
    }
    public void IsMaxSlot()
    {
        button.interactable = false;
        image.sprite = defaultSprite;
        priceText.text = "Max";
        countText.text = "Lv.Max";
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
        goods = new Goods(item.id, count);
    }
    public void SetChest(Chest chest, int count)
    {
        this.chest = chest;
        this.count = count;
        price = chest.price;
        image.sprite = chest.chetImage;
        priceText.text = "" + (chest.price * count);
        countText.text = "x" + count;
        goods = new Goods(chest.id, count);
    }
    public void SetLock(int price, lockType lockType)
    {
        this.lockType = lockType;
        this.price = price;
        image.sprite = defaultSprite;
        priceText.text = price.ToString();
        countText.text = "Lv." + (price / 1000);
    }
}
