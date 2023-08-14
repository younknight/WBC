using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

#region 정보 목록
public class HasItem
{
    public int id;
    public int count;

    public HasItem(int id, int count)
    {
        this.id = id;
        this.count = count;
    }
}
public class WeaponLevel
{
    public int id;
    public int level;
    public int enforceGauge;

    public WeaponLevel(int id, int level, int enforceGauge)
    {
        this.id = id;
        this.level = level;
        this.enforceGauge = enforceGauge;
    }
}
public class LockInfo
{
    public int maxOpenerCount = 2;
    public int craftCoolTime = 20;
    public int maxCraftCount = 5;

    public LockInfo(int maxOpenerCount, int craftCoolTime, int maxCraftCount)
    {
        this.maxOpenerCount = maxOpenerCount;
        this.craftCoolTime = craftCoolTime;
        this.maxCraftCount = maxCraftCount;
    }
}
public class OpeningChest
{
    public int chestId;
    public DateTime openTime;
    public OpeningChest(int id, DateTime openTime)
    {
        this.chestId = id;
        this.openTime = openTime;
    }
}
public class AutoCraftMaxCounter
{
    public int currentCount;
    public DateTime lastTime;

    public AutoCraftMaxCounter(int currentCount, DateTime lastTime)
    {
        this.currentCount = currentCount;
        this.lastTime = lastTime;
    }
}
#endregion
public class SaveData
{
    //아이템들
    public List<HasItem> items = new List<HasItem>();
    public List<HasItem> chests = new List<HasItem>();
    public List<HasItem> weapons = new List<HasItem>();
    public List<WeaponLevel> weaponLevels = new List<WeaponLevel>();
    //플레이어 정보
    public OpeningChest[] openingChests = new OpeningChest[16];//열고 있는 상자 정보
    public List<List<int>> weirdRecipe = new List<List<int>>();//이상한 상자 레시피
    public AutoCraftMaxCounter autoCounter;
    public LockInfo lockInfo;
    public int[] equipWeapons = new int[6];//장착한 장비
    public Resource resource;
}

public class DataManager : MonoBehaviour
{
    JsonParser jsonParser = new JsonParser();
    [SerializeField] CraftDatabase craftDatabase;
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] DayChecker dayChecker;
    [SerializeField] GameManager gameManager;
    [SerializeField] StaminaGauge stamina;
    [SerializeField] Opener opener;
    [SerializeField] Chest firstChest;
    public static DataManager instance;
    string path;
    private void OnDestroy()
    {
        instance = null;
    }
    void Awake()
    {
        if (instance == null) instance = this;
        path =  Application.persistentDataPath + "/"+ "database.json" ;
        JsonLoad();
    }
    private void Start()
    {
        JsonLateLoad();
    }
    void JsonLateLoad()
    {
        inventoryManager.Setup();
        inventoryManager.Initalize();
        opener.FreshSlot();
        dayChecker.DayCheck();
        stamina.Setup(StaminaManager.Instance.StaminaData.currentStamina);
    }
    public void JsonLoad()
    {
        SaveData saveData = new SaveData();
        if (!File.Exists(path))
        {
            ItemDatabaseManager.Ingrediants = new List<ItemInfo>();
            ItemDatabaseManager.Chests = new List<ItemInfo>();
            ItemDatabaseManager.Chests.Add(new ItemInfo(firstChest, 1));
            ItemDatabaseManager.Weapons = new List<ItemInfo>();
            ItemDatabaseManager.WeaponLevels = new List<WeaponInfo>();
            LockManager.LockInfo = new LockInfo(2, 20, 5);
            AutoCrafter.AutoCounter = new AutoCraftMaxCounter(5, DateTime.Now);
            Opener.OpeningChests = new OpeningChest[16];
            ResourseManager.Instance.Resource = new Resource(0,0,0);
        }
        else
        {
            string loadJson = File.ReadAllText(path);
            saveData = jsonParser.JsonToOject<SaveData>(loadJson);
            if (saveData != null)
            {
                saveData = JsonConvert.DeserializeObject<SaveData>(loadJson);
                #region 로드
                //불러오기
                List<ItemInfo> itemInfos = new List<ItemInfo>();
                for (int i = 0; i < saveData.items.Count; i++) { itemInfos.Add(new ItemInfo(gameManager.ItemDatas[saveData.items[i].id], saveData.items[i].count)); }
                List<ItemInfo> chestInfos = new List<ItemInfo>();
                for (int i = 0; i < saveData.chests.Count; i++) { chestInfos.Add(new ItemInfo(gameManager.ChestDatas[saveData.chests[i].id], saveData.chests[i].count)); }
                List<ItemInfo> weaponInfos = new List<ItemInfo>();
                for (int i = 0; i < saveData.weapons.Count; i++) { weaponInfos.Add(new ItemInfo(gameManager.WeaponDatas[saveData.weapons[i].id], saveData.weapons[i].count)); }
                List<WeaponInfo> weaponLevelInfos = new List<WeaponInfo>();
                for (int i = 0; i < saveData.weaponLevels.Count; i++) { weaponLevelInfos.Add(new WeaponInfo(gameManager.WeaponDatas[saveData.weaponLevels[i].id], saveData.weaponLevels[i].level, saveData.weaponLevels[i].enforceGauge)); }
                Opener.OpeningChests = saveData.openingChests;
                ItemDatabaseManager.Ingrediants = itemInfos;
                ItemDatabaseManager.Chests = chestInfos;
                ItemDatabaseManager.Weapons = weaponInfos;
                ItemDatabaseManager.WeaponLevels = weaponLevelInfos;
                AutoCrafter.AutoCounter = saveData.autoCounter;
                LockManager.LockInfo = saveData.lockInfo;
                ResourseManager.Instance.Resource = saveData.resource;
                craftDatabase.WeirdRecipe = saveData.weirdRecipe;
                //장비 장착
                Weapon[] weapons = new Weapon[saveData.equipWeapons.Length];
                for (int i = 0; i < saveData.equipWeapons.Length; i++)
                {
                    if (saveData.equipWeapons[i] != -1) weapons[i] = gameManager.WeaponDatas[saveData.equipWeapons[i]];
                    else weapons[i] = null;
                }
                EquipmentManager.EquipWeapon = weapons;
                #endregion
            }
        }
    }
    public void JsonSave()
    {
        SaveData saveData = new SaveData();
        #region json파일에 저장
        List<HasItem> ingrediants = new List<HasItem>();
        for (int i = 0; i < ItemDatabaseManager.Ingrediants.Count; i++) { ingrediants.Add(new HasItem(ItemDatabaseManager.Ingrediants[i].item.id, ItemDatabaseManager.Ingrediants[i].num)); }
        List<HasItem> chests = new List<HasItem>();
        for (int i = 0; i < ItemDatabaseManager.Chests.Count; i++) { chests.Add(new HasItem(ItemDatabaseManager.Chests[i].item.id, ItemDatabaseManager.Chests[i].num)); }
        List<HasItem> weapons = new List<HasItem>();
        for (int i = 0; i < ItemDatabaseManager.Weapons.Count; i++) { weapons.Add(new HasItem(ItemDatabaseManager.Weapons[i].item.id, ItemDatabaseManager.Weapons[i].num)); }
        List<WeaponLevel> weaponLevelInfos = new List<WeaponLevel>();
        for (int i = 0; i < ItemDatabaseManager.WeaponLevels.Count; i++) { weaponLevelInfos.Add(new WeaponLevel(ItemDatabaseManager.WeaponLevels[i].item.id, ItemDatabaseManager.WeaponLevels[i].level, ItemDatabaseManager.WeaponLevels[i].gauge)); }
        saveData.items = ingrediants;
        saveData.chests = chests;
        saveData.weapons = weapons;
        saveData.weaponLevels = weaponLevelInfos;
        saveData.resource = ResourseManager.Instance.Resource;
        int[] equip = new int[6];
        for (int i = 0; i < equip.Length; i++)
        {
            if (EquipmentManager.EquipWeapon[i] != null) equip[i] = EquipmentManager.EquipWeapon[i].id;
            else equip[i] = -1;
        }
        saveData.equipWeapons = equip;
        saveData.weirdRecipe = craftDatabase.WeirdRecipe;
        saveData.openingChests = Opener.OpeningChests;
        saveData.autoCounter = AutoCrafter.AutoCounter;
        saveData.lockInfo = LockManager.LockInfo;
        #endregion
        jsonParser.SaveJson<SaveData>(saveData, path);
    }
}
