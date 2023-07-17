using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] List<Inventory> inventories = new List<Inventory>();

    public static InventoryManager instance;

    private void Start()
    {
        if (instance == null) instance = this;
    }
    public void Initalize()
    {
        for (int i = 0; i < inventories.Count; i++)
        {
            inventories[i].FreshSlot();
        }
    }
    #region 아이템 추가 및 사용
    public void AddItem(Item item, int num)
    {
        for (int i = 0; i < inventories.Count; i++)
        {
            if (inventories[i].InventoryType == inventoryType.item)
            {
                inventories[i].AddItem(item, num);
                Initalize();
                return;
            }
        }
    }
    public void DropItem(Item item, int num)
    {
        for (int i = 0; i < inventories.Count; i++)
        {
            if (inventories[i].InventoryType == inventoryType.item)
            {
                inventories[i].DropItem(item, num);
            }
        }
    }
    public void AddChest(Chest chest, int num)
    {
        for (int i = 0; i < inventories.Count; i++)
        {
            if (inventories[i].InventoryType == inventoryType.chest)
            {
                inventories[i].AddChest(chest, num);
                Initalize();
                return;
            }
        }
    }
    public void DropChest(Chest chest, int num)
    {
        for (int i = 0; i < inventories.Count; i++)
        {
            if (inventories[i].InventoryType == inventoryType.chest)
            {
                inventories[i].DropChest(chest, num);
            }
        }
    }
    public void AddWeapon(Weapon weapon, int num)
    {
        for (int i = 0; i < inventories.Count; i++)
        {
            if (inventories[i].InventoryType == inventoryType.weapon)
            {
                inventories[i].AddWeapon(weapon, num);
                Initalize();
                return;
            }
        }
    }
    public void DropWeapon(Weapon weapon, int num)
    {
        for (int i = 0; i < inventories.Count; i++)
        {
            if (inventories[i].InventoryType == inventoryType.weapon)
            {
                inventories[i].DropWeapon(weapon, num);
            }
        }
    }
    #endregion
}
