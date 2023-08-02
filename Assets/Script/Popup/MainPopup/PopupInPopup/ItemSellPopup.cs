using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSellPopup : Popup
{
    [SerializeField] Toggle toggle;
    [SerializeField] SellSlider sellSlider;
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
        GameManager.instance.Purchase(false, sellSlider.GetTotalPrice());
        InventoryManager.instance.DropItems<Item>(itemInfo.item,sellSlider.CurrentCount);
        Setup(true, itemInfo.item);
        sellSlider.Setup(itemInfo.num,itemInfo.item.sellPrice);
    }
}
