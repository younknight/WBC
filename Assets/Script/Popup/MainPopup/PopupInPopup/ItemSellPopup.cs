using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSellPopup : Popup
{
    [SerializeField] Toggle toggle;
    [SerializeField] SellSlider sellSlider;
    [SerializeField] InventoryManager inventoryManager;//
    [SerializeField] ItemDatabaseManager itemDatabaseManager;
    ItemInfo itemInfo;
    public void TogglePopup()
    {
        if (toggle.isOn) Open();
        else CloseStart();
    }
    public void Setup(bool isReset, Ingredient item)
    {
        toggle.isOn = isReset;
        itemInfo = itemDatabaseManager.FIndItemWithId(item.id, inventoryType.ingrediant);
        sellSlider.Setup(itemInfo.num, item.sellPrice);
    }
    public void Sell()
    {
        ResourseManager.Instance.Purchase(false, sellSlider.GetTotalPrice());
        inventoryManager.DropItems(itemInfo.item,sellSlider.CurrentCount);
        Setup(true, (Ingredient)itemInfo.item);
        Ingredient item = (Ingredient)itemInfo.item;
        sellSlider.Setup(itemInfo.num,item.sellPrice);
    }
}
