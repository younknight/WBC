using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabaseManager : MonoBehaviour
{
    private static List<ItemInfo> ingrediants = new List<ItemInfo>();
    private static List<ItemInfo> chests = new List<ItemInfo>();
    private static List<ItemInfo> weapons = new List<ItemInfo>();
    private static List<WeaponInfo> weaponLevels = new List<WeaponInfo>();
    public static List<ItemInfo> Ingrediants { get => ingrediants; set => ingrediants = value; }
    public static List<ItemInfo> Chests { get => chests; set => chests = value; }
    public static List<ItemInfo> Weapons { get => weapons; set => weapons = value; }
    public static List<WeaponInfo> WeaponLevels { get => weaponLevels; set => weaponLevels = value; }

    public bool CheckNew(Item item)
    {
        List<ItemInfo> items = GetItemListWithType(GetInventoryType(item));
        for (int i = 0; i < items.Count; i++)
        {
            if (item == items[i].item) return false;
        }
        return true;
    }
    public List<ItemInfo> GetItemListWithType(inventoryType inventoryType)
    {
        List<ItemInfo> items = new List<ItemInfo>();
        if (inventoryType == inventoryType.ingrediant) items = Ingrediants;
        if (inventoryType == inventoryType.chest) items = chests;
        if (inventoryType == inventoryType.weapon) items = weapons;
        return items;
    }
    public inventoryType GetInventoryType(Item item)
    {
        if (item is Ingredient) return inventoryType.ingrediant;
        if (item is Weapon) return inventoryType.weapon;
        if (item is Chest) return inventoryType.chest;
        return inventoryType.ingrediant;
    }
    public ItemInfo FIndItemWithId(int id, inventoryType inventoryType)
    {
        List<ItemInfo> items = GetItemListWithType(inventoryType);
        return items.Find(x => x.item.id == id);
    }
}
