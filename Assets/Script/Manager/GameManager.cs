using System;
using System.Collections.Generic;
using UnityEngine;
public class ItemInfo
{
    public Item item;
    public int num;

    public ItemInfo(Item item, int num)
    {
        this.item = item;
        this.num = num;
    }
}
public class WeaponInfo
{
    public Item item;
    public int level;
    public int gauge;

    public WeaponInfo(Item item, int level, int gauge)
    {
        this.item = item;
        this.level = level;
        this.gauge = gauge;
    }
}
public class GameManager : MonoBehaviour
{
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] CraftDatabase craftDatabase;
    public static GameManager instance;
    [SerializeField] Chest firstChest;
    [SerializeField] List<Ingredient> itemDatas = new List<Ingredient>();
    [SerializeField] List<Chest> chestDatas = new List<Chest>();
    [SerializeField] List<Weapon> weaponDatas = new List<Weapon>();
    static List<ItemInfo> fullItems;
    static List<ItemInfo> fullChests;
    static List<ItemInfo> fullWeapons;
    #region getter, setter
    public List<Ingredient> ItemDatas { get => itemDatas; set => itemDatas = value; }
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
        fullItems = new List<ItemInfo>();
        fullChests = new List<ItemInfo>();
        fullWeapons = new List<ItemInfo>();
        for (int i = 0; i < ItemDatas.Count; i++)
        {
            fullItems.Add(new ItemInfo(ItemDatas[i], 99));
        }
        for (int i = 0; i < ChestDatas.Count; i++)
        {
            fullChests.Add(new ItemInfo(ChestDatas[i], 99));
        }
        for (int i = 0; i < WeaponDatas.Count; i++)
        {
            fullWeapons.Add(new ItemInfo(WeaponDatas[i], 10));
        }
    }
    private void Start()
    {
        EquipmentManager.instance.SetEquipManager();//의존성 최대로ㅗㅗㅗㅗㅗㅗㅗㅗㅗㅗㅗ
    }
    public static int GetFullItemCount(inventoryType inventoryType)
    {
        if (inventoryType == inventoryType.ingrediant) return fullItems.Count;
        if (inventoryType == inventoryType.chest) return fullChests.Count;
        if (inventoryType == inventoryType.weapon) return fullWeapons.Count;
        return 0;
    }
    public void ResetPlayer()//초기화
    {
        ItemDatabaseManager.Ingrediants = new List<ItemInfo>();
        ItemDatabaseManager.Chests = new List<ItemInfo>();
        ItemDatabaseManager.Chests.Add(new ItemInfo(firstChest, 1));
        ItemDatabaseManager.Weapons = new List<ItemInfo>();
        ItemDatabaseManager.WeaponLevels = new List<WeaponInfo>();
        LockManager.LockInfo = new LockInfo(2, 20, 5);
        AutoCrafter.AutoCounter = new AutoCraftMaxCounter(5, DateTime.Now);
        ResourseManager.Instance.SetGold(11100);
        ResourseManager.Instance.SetPrimo(22200);
        CommonData();
        SetData();
    }
    public void GetAllItems()
    {
        ItemDatabaseManager.Ingrediants = fullItems;
        ItemDatabaseManager.Chests = fullChests;
        ItemDatabaseManager.Weapons = fullWeapons;
        ItemDatabaseManager.WeaponLevels = fullWeaponLevels();
        LockManager.LockInfo = new LockInfo(16, 1, 20);
        AutoCrafter.AutoCounter = new AutoCraftMaxCounter(20, DateTime.Now);
        ResourseManager.Instance.SetGold(98765);
        ResourseManager.Instance.SetPrimo(56789);
        CommonData();
        SetData();
    }
    List<WeaponInfo> fullWeaponLevels()
    {
        List<WeaponInfo> weaponInfos = new List<WeaponInfo>();
        for(int i = 0; i< fullWeapons.Count; i++)
        {
            weaponInfos.Add(new WeaponInfo(fullWeapons[i].item,1,0));
        }
        return weaponInfos;
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
