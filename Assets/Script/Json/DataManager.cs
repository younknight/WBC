using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

#region 정보 목록
public class HasItem
{
    public int itemId;
    public int count;

    public HasItem(int id, int count)
    {
        this.itemId = id;
        this.count = count;
    }
}
public class HasWeaponWithLevel
{
    public int weaponId;
    public int count;
    public int level;
    public int enforceGauge;

    public HasWeaponWithLevel(int id, int count, int level, int enforceGauge)
    {
        this.weaponId = id;
        this.count = count;
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
    public List<HasWeaponWithLevel> weapons = new List<HasWeaponWithLevel>();
    //플레이어 정보
    public OpeningChest[] openingChests = new OpeningChest[16];//열고 있는 상자 정보
    public List<List<int>> weirdRecipe = new List<List<int>>();//이상한 상자 레시피
    public AutoCraftMaxCounter autoCounter;
    public LockInfo lockInfo;
    public int[] equipWeapons = new int[6];//장착한 장비
    public int gold = 0;
}

public class DataManager : MonoBehaviour
{
    JsonParser jsonParser = new JsonParser();
    [SerializeField] CraftDatabase craftDatabase;
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] GameManager gameManager;
    public static DataManager instance;
    string path;
    private void OnDestroy()
    {
        instance = null;
    }
    void Awake()
    {
        if (instance == null) instance = this;
        path = Path.Combine(Application.dataPath, "database.json");
        JsonLoad();
    }
    private void Start()
    {
        JsonLateLoad();
        SetData();
    }
    void JsonLateLoad() { inventoryManager.Initalize(); }
    public void JsonLoad()
    {
        SaveData saveData = new SaveData();
        if (!File.Exists(path))
        {
            Debug.Log("경로가 존재 안함");
            gameManager.ResetPlayer();
            JsonSave();
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
                List<itemInfo> itemInfos = new List<itemInfo>();
                for (int i = 0; i < saveData.items.Count; i++) { itemInfos.Add(new itemInfo(gameManager.ItemDatas[saveData.items[i].itemId], saveData.items[i].count)); }
                List<chestInfo> chestInfos = new List<chestInfo>();
                for (int i = 0; i < saveData.chests.Count; i++) { chestInfos.Add(new chestInfo(gameManager.ChestDatas[saveData.chests[i].itemId], saveData.chests[i].count)); }
                List<weaponInfo> weaponInfos = new List<weaponInfo>();
                for (int i = 0; i < saveData.weapons.Count; i++) { weaponInfos.Add(new weaponInfo(gameManager.WeaponDatas[saveData.weapons[i].weaponId], saveData.weapons[i].count, saveData.weapons[i].level, saveData.weapons[i].enforceGauge)); }
                Opener.OpeningChests = saveData.openingChests;
                Inventory.Items = itemInfos;
                Inventory.Chests = chestInfos;
                Inventory.Weapons = weaponInfos;
                AutoCrafter.AutoCounter = saveData.autoCounter;
                GameManager.Gold = saveData.gold;
                LockManager.LockInfo = saveData.lockInfo;
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
    void SetData() { TextManager.instance.SetGold(); }
    public void JsonSave()
    {
        SaveData saveData = new SaveData();
        #region json파일에 저장
        List<HasItem> items = new List<HasItem>();
        for (int i = 0; i < Inventory.Items.Count; i++) { items.Add(new HasItem(Inventory.Items[i].item.id, Inventory.Items[i].num)); }
        List<HasItem> chests = new List<HasItem>();
        for (int i = 0; i < Inventory.Chests.Count; i++) { chests.Add(new HasItem(Inventory.Chests[i].chest.id, Inventory.Chests[i].num)); }
        List<HasWeaponWithLevel> weapons = new List<HasWeaponWithLevel>();
        for (int i = 0; i < Inventory.Weapons.Count; i++) { weapons.Add(new HasWeaponWithLevel(Inventory.Weapons[i].weapon.id, Inventory.Weapons[i].num, Inventory.Weapons[i].level, Inventory.Weapons[i].enforceGauge)); }
        saveData.items = items;
        saveData.chests = chests;
        saveData.weapons = weapons;
        saveData.gold = GameManager.Gold;
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
