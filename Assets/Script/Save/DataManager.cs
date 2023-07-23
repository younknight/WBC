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
    public int maxCraftChest = 1;
    public int maxCraftCount = 1;
}
public class OpeningChest
{
    public int chestId;
    public int count;
    public DateTime openTime;
    public OpeningChest(int id, int count, DateTime openTime)
    {
        this.chestId = id;
        this.count = count;
        this.openTime = openTime;
    }
}
#endregion
public class SaveData
{
    public List<HasItem> items = new List<HasItem>();
    public List<HasItem> chests = new List<HasItem>();
    public List<HasWeaponWithLevel> weapons = new List<HasWeaponWithLevel>();
    public OpeningChest[] openingChests = new OpeningChest[16];//열고 있는 상자 정보
    public List<List<int>> weirdRecipe = new List<List<int>>();//이상한 상자 레시피
    public LockInfo lockInfo;
    public int[] equipWeapons = new int[6];//장착한 장비
    public int gold = 0;
}

public class DataManager : MonoBehaviour
{
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
    void JsonLateLoad()
    {
        inventoryManager.Initalize();
    }
    public void JsonLoad()
    {

        SaveData saveData = new SaveData();
        if (!File.Exists(path))
        {
            //경로가 존재 안함
            Debug.Log("경로가 존재 안함");
            gameManager.ResetPlayer();
            JsonSave();
        }
        else
        {
            string loadJson = File.ReadAllText(path);
            saveData = JsonToOject<SaveData>(loadJson);
            // saveData = JsonUtility.FromJson<SaveData>(loadJson);

            if (saveData != null)
            {
                saveData = JsonConvert.DeserializeObject<SaveData>(loadJson);

                //불러오기
                GameManager.Gold = saveData.gold;
                List<itemInfo> itemInfos = new List<itemInfo>();
                for (int i = 0; i < saveData.items.Count; i++)
                {
                    itemInfos.Add(new itemInfo(gameManager.ItemDatas[saveData.items[i].itemId], saveData.items[i].count));
                }
                List<chestInfo> chestInfos = new List<chestInfo>();
                for (int i = 0; i < saveData.chests.Count; i++)
                {
                    chestInfos.Add(new chestInfo(gameManager.ChestDatas[saveData.chests[i].itemId], saveData.chests[i].count));
                }
                List<weaponInfo> weaponInfos = new List<weaponInfo>();
                for (int i = 0; i < saveData.weapons.Count; i++)
                {
                    weaponInfos.Add(new weaponInfo(gameManager.WeaponDatas[saveData.weapons[i].weaponId], saveData.weapons[i].count, saveData.weapons[i].level, saveData.weapons[i].enforceGauge));
                }
                Opener.OpeningChests = saveData.openingChests;
                Inventory.Items = itemInfos;
                Inventory.Chests = chestInfos;
                Inventory.Weapons = weaponInfos;
                craftDatabase.WeirdRecipe = saveData.weirdRecipe;
                //장비 장착
                Weapon[] weapons = new Weapon[saveData.equipWeapons.Length];
                for (int i = 0; i < saveData.equipWeapons.Length; i++)
                {
                    if (saveData.equipWeapons[i] != -1) weapons[i] = gameManager.WeaponDatas[saveData.equipWeapons[i]];
                    else weapons[i] = null;
                }
                EquipmentManager.EquipWeapon = weapons;
            }
        }
    }
    void SetData()
    {
        TextManager.instance.SetGold();
    }
    public void JsonSave()
    {
        SaveData saveData = new SaveData();

        #region json파일에 저장
        List<HasItem> items = new List<HasItem>();
        for (int i = 0; i < Inventory.Items.Count; i++)
        {
            items.Add(new HasItem(Inventory.Items[i].item.id, Inventory.Items[i].num));
        }
        List<HasItem> chests = new List<HasItem>();
        for (int i = 0; i < Inventory.Chests.Count; i++)
        {
            chests.Add(new HasItem(Inventory.Chests[i].chest.id, Inventory.Chests[i].num));
        }
        List<HasWeaponWithLevel> weapons = new List<HasWeaponWithLevel>();
        for (int i = 0; i < Inventory.Weapons.Count; i++)
        {
            weapons.Add(new HasWeaponWithLevel(Inventory.Weapons[i].weapon.id, Inventory.Weapons[i].num, Inventory.Weapons[i].level, Inventory.Weapons[i].enforceGauge));
        }
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
        saveData.openingChests = Opener.OpeningChests;
        saveData.weirdRecipe = craftDatabase.WeirdRecipe;
        #endregion

        string jsonData = ObjectToJson(saveData);
        File.WriteAllText(path, jsonData);
    }
    string ObjectToJson(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }
    T JsonToOject<T>(string jsonData)
    {
        return JsonConvert.DeserializeObject<T>(jsonData);
    }
}
