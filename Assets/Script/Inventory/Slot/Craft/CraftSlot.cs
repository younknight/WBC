using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftSlot : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] GameObject cancelBtn;
    [SerializeField] InventoryManager inventoryManager;//
    Crafter crafter;
    Ingredient item;

    public Ingredient Item { get => item; set => item = value; }
    public Crafter Crafter { get => crafter; set => crafter = value; }


    public void AddResource(int id)
    {
        crafter.AddResource(id);
    }
    public void RemoveResource(int id)
    {
        crafter.DeleteResource(id);
    }
    public bool IsNull()
    {
        if (item == null) return true;
        return false;
    }
    public void RemoveSlot()
    {
        if(item != null) RemoveResource(item.id);
        item = null;
        cancelBtn.SetActive(false);
        image.color = new Color(1, 1, 1, 0);
    }
    public void SetItem(Ingredient item)
    {
        this.item = item;
        cancelBtn.SetActive(true);
        image.color = new Color(1, 1, 1, 1);
        image.sprite = item.itemImage;
    }
    public void Cancel()
    {
        inventoryManager.AddItems(item,1);
        RemoveSlot();
    }
}
