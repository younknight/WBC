using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct itemInfo
{
    public Item item;
    public int num;

    public itemInfo(Item item, int num)
    {
        this.item = item;
        this.num = num;
    }
}
[System.Serializable]
public struct chestInfo
{
    public Chest chest;
    public int num;
    public chestInfo(Chest chest, int num)
    {
        this.chest = chest;
        this.num = num;
    }
}
[System.Serializable]
public struct weaponInfo
{
    public Weapon weapon;
    public int num;
    public weaponInfo(Weapon weapon, int num)
    {
        this.weapon = weapon;
        this.num = num;
    }
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] List<chestInfo> firstChest = new List<chestInfo>();
    [SerializeField] List<Item> itemDatas = new List<Item>();
    [SerializeField] List<Chest> chestDatas = new List<Chest>();
    [SerializeField] List<Weapon> weaponDatas = new List<Weapon>();
    static List<itemInfo> fullItems;
    static List<chestInfo> fullChests;
    static List<weaponInfo> fullWeapons;
    static int gold;
    #region getter, setter
    public static int Gold { get => gold; set => gold = value; }
    #endregion
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void OnValidate()
    {
        fullItems = new List<itemInfo>();
        fullChests = new List<chestInfo>();
        fullWeapons = new List<weaponInfo>();
        for (int i = 0; i < itemDatas.Count; i++)
        {
            fullItems.Add(new itemInfo(itemDatas[i], 99));
        }
        for (int i = 0; i < chestDatas.Count; i++)
        {
            fullChests.Add(new chestInfo(chestDatas[i], 99));
        }
        for (int i = 0; i < weaponDatas.Count; i++)
        {
            fullWeapons.Add(new weaponInfo(weaponDatas[i], 99));
        }
    }
    public static int GetCount(inventoryType inventoryType)
    {
        if (inventoryType == inventoryType.item) return fullItems.Count + 1;
        if (inventoryType == inventoryType.chest) return fullChests.Count + 1;
        if (inventoryType == inventoryType.weapon) return fullWeapons.Count + 1;
        return 0;
    }
    public void ResetPlayer()//ÃÊ±âÈ­
    {
        Inventory.items = new List<itemInfo>();
        Inventory.chests = firstChest;
        Inventory.weapons = new List<weaponInfo>();
        gold = 0;
        SetData();
    }
    public void GetAllItems()
    {
        Inventory.items = fullItems;
        Inventory.chests = fullChests;
        Inventory.weapons = fullWeapons;
        gold = 98765;
        SetData();
    }
    void SetData()
    {

        EquipmentManager.instance.Equipment.DefaultStatus = new Status();
        Equipment.Weapons = new Weapon[6];
        for (int i = 0; i < EquipmentManager.instance.Slots.Length; i++)
        {
            EquipmentManager.instance.Slots[i].ClearSlot();
        }
        TextManager.instance.SetGold();
        InventoryManager.instance.Initalize();
    }
}
