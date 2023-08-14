using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] ItemDatabaseManager itemDatabase;
    [SerializeField] List<Inventory> inventories = new List<Inventory>();


   
    public void Initalize()
    {
        for (int i = 0; i < inventories.Count; i++)
        {
            inventories[i].FreshSlot(itemDatabase.GetItemListWithType(inventories[i].InventoryType));
        }
    }
    public void Setup()
    {
        for (int i = 0; i < inventories.Count; i++)
        {
            inventories[i].Setup();
        }

    }
    #region 아이템 추가 및 사용
    public void AddItems(Item item, int num)
    {
        for (int i = 0; i < inventories.Count; i++)
        {
            if (inventories[i].InventoryType == itemDatabase.GetInventoryType(item))
            {
                inventories[i].AddItems(itemDatabase.GetItemListWithType(inventories[i].InventoryType), item, num);
                Initalize();
                return;
            }
        }
    }
    public void DropItems(Item item, int num)
    {
        for (int i = 0; i < inventories.Count; i++)
        {
            if (inventories[i].InventoryType == itemDatabase.GetInventoryType(item))
            {
                inventories[i].DropItems(itemDatabase.GetItemListWithType(inventories[i].InventoryType), item, num);
            }
        }
    }
    
    #endregion
}
