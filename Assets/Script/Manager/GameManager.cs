using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;//--------------
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
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] CraftDatabase craftDatabase;
    public static GameManager instance;
    [SerializeField] Chest firstChest;
    [SerializeField] List<Item> itemDatas = new List<Item>();
    [SerializeField] List<Chest> chestDatas = new List<Chest>();
    [SerializeField] List<Weapon> weaponDatas = new List<Weapon>();
    [SerializeField] TextMeshProUGUI countTest;//-------------------
    static List<itemInfo> fullItems;
    static List<chestInfo> fullChests;
    static List<weaponInfo> fullWeapons;
    #region getter, setter
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
    public void SetTest(string text)
    {
        countTest.text = text;
    }
    private void Awake()
    {
        if (instance == null) instance = this;
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
    private void Start()
    {
        countTest.text = fullItems.Count + "/" + fullChests.Count + "/" + fullWeapons.Count;
        EquipmentManager.instance.SetEquipManager();//税糎失 置企稽ででででででででででで
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
        if (inventoryType == inventoryType.item) return fullItems.Count;
        if (inventoryType == inventoryType.chest) return fullChests.Count;
        if (inventoryType == inventoryType.weapon) return fullWeapons.Count;
        return 0;
    }
    public void ResetPlayer()//段奄鉢
    {
        Inventory.Items = new List<itemInfo>();
        Inventory.Chests = new List<chestInfo>();
        Inventory.Chests.Add(new chestInfo(firstChest, 1));
        Inventory.Weapons = new List<weaponInfo>();
        LockManager.LockInfo = new LockInfo(2, 20, 5);
        AutoCrafter.AutoCounter = new AutoCraftMaxCounter(5, DateTime.Now);
        ResourseManager.Instance.SetGold(11100);
        ResourseManager.Instance.SetPrimo(22200);
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
        ResourseManager.Instance.SetGold(98765);
        ResourseManager.Instance.SetPrimo(56789);
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
        TextManager.instance.SetText();
        inventoryManager.Initalize();
    }
}
