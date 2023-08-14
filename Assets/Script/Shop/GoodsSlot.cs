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
    InventoryManager inventoryManager;
    [SerializeField] bool isSpecialChest;
    [SerializeField] Item item;
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
        inventoryManager = GameObject.FindWithTag("Manager").GetComponent<InventoryManager>();
    }
    public void Purchase()
    {
        if (item != null)
        {
            if (isSpecialChest)//특수상자
            {
                if (ResourseManager.Instance.GetPrimo() >= price)
                {
                    PurchasePopup.Instance.Close();
                    ResourseManager.Instance.PurchaseWithPrimo(true, price);
                    GetItemPopup.Instance.SetGetItem((Chest)item);
                }
                else
                {
                    //잔액부족
                }
            }
            else//일반상자
            {
                if (ResourseManager.Instance.GetGold() >= price)
                {
                    inventoryManager.AddItems(item, count);
                    ResourseManager.Instance.Purchase(true, price);
                    button.interactable = false;
                }
                else
                {
                    //잔액부족
                }
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
                //삑
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
        //image.sprite = item.itemImage;
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
    public void SetLock(int price, lockType lockType)
    {
        this.lockType = lockType;
        this.price = price;
        image.sprite = defaultSprite;
        priceText.text = price.ToString();
        countText.text = "Lv." + (price / 1000);
    }
}
