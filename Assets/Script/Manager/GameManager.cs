using System;
using System.Collections.Generic;
using UnityEngine;

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
[System.Serializable]//
public struct weaponInfo
{
    public Weapon weapon;
    public int num;
    public int level;
    public int enforceGauge;

    public weaponInfo(Weapon weapon, int num, int level, int enforceGauge)
    {
        this.weapon = weapon;
        this.num = num;
        this.level = level;
        this.enforceGauge = enforceGauge;
    }
}
public class GameManager : MonoBehaviour
{
    [SerializeField] Opener opener;
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] CraftDatabase craftDatabase;
    public static GameManager instance;
    [SerializeField] Chest firstChest;
    [SerializeField] List<Item> itemDatas = new List<Item>();
    [SerializeField] List<Chest> chestDatas = new List<Chest>();
    [SerializeField] List<Weapon> weaponDatas = new List<Weapon>();
    static List<itemInfo> fullItems;
    static List<chestInfo> fullChests;
    static List<weaponInfo> fullWeapons;
    static int gold;
    #region getter, setter
    public static int Gold { get => gold; set => gold = value; }
    public List<Item> ItemDatas { get => itemDatas; set => itemDatas = value; }
    public List<Chest> ChestDatas { get => chestDatas; set => chestDatas = value; }
    public List<Weapon> WeaponDatas { get => weaponDatas; set => weaponDatas = value; }
    #endregion
    private void OnDestroy()
    {
        instance = null;
        fullItems = null;
        fullChests = null;
        fullWeapons = null;
    }
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {

        EquipmentManager.instance.SetEquipManager();//의존성 최대로ㅗㅗㅗㅗㅗㅗㅗㅗㅗㅗㅗ
    }
    private void OnValidate()
    {
        fullItems = new List<itemInfo>();
        fullChests = new List<chestInfo>();
        fullWeapons = new List<weaponInfo>();
        for (int i = 0; i < ItemDatas.Count; i++)
        {
            fullItems.Add(new itemInfo(ItemDatas[i], 99));
        }
        for (int i = 0; i < ChestDatas.Count; i++)
        {
            fullChests.Add(new chestInfo(ChestDatas[i], 99));
        }
        for (int i = 0; i < WeaponDatas.Count; i++)
        {
            fullWeapons.Add(new weaponInfo(WeaponDatas[i], 10, 1, 0));
        }
    }
    public static int GetCount(inventoryType inventoryType)
    {
        if (inventoryType == inventoryType.item) return fullItems.Count + 1;
        if (inventoryType == inventoryType.chest) return fullChests.Count + 1;
        if (inventoryType == inventoryType.weapon) return fullWeapons.Count + 1;
        return 0;
    }
    public void ResetPlayer()//초기화
    {
        Inventory.Items = new List<itemInfo>();
        Inventory.Chests = new List<chestInfo>();
        Inventory.Chests.Add(new chestInfo(firstChest, 1));
        Inventory.Weapons = new List<weaponInfo>();
        LockManager.LockInfo = new LockInfo(2, 20, 5);
        AutoCrafter.AutoCounter = new AutoCraftMaxCounter(5, DateTime.Now);
        inventoryManager.Initalize();
        gold = 0;
        CommonData();
        SetData();
    }
    public void GetAllItems()
    {
        Inventory.Items = fullItems;
        Inventory.Chests = fullChests;
        Inventory.Weapons = fullWeapons;
        LockManager.LockInfo = new LockInfo(16, 1, 20);
        AutoCrafter.AutoCounter = new AutoCraftMaxCounter(20, DateTime.Now);
        gold = 98765;
        CommonData();
        SetData();
    }
    void CommonData()
    {
        craftDatabase.WeirdRecipe = new List<List<int>>();
        Opener.Instance.SetUnlock();
    }
    void SetData()
    {
        TextManager.instance.SetGold();
        inventoryManager.Initalize();
    }
}
