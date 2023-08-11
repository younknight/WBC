using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSellPopup : Popup
{
    [SerializeField] Toggle toggle;
    [SerializeField] SellSlider sellSlider;
    [SerializeField] InventoryManager inventoryManager;//
    itemInfo itemInfo;
    public void TogglePopup()
    {
        if (toggle.isOn) Open();
        else CloseStart();
    }
    public void Setup(bool isReset, Item item)
    {
        toggle.isOn = isReset;
        itemInfo = Inventory.Items.Find(x => x.item == item);
        sellSlider.Setup(itemInfo.num, itemInfo.item.sellPrice);
    }
    public void Sell()
    {
        ResourseManager.Instance.Purchase(false, sellSlider.GetTotalPrice());
        inventoryManager.DropItems<Item>(itemInfo.item,sellSlider.CurrentCount);
        Setup(true, itemInfo.item);
        sellSlider.Setup(itemInfo.num,itemInfo.item.sellPrice);
    }
}
