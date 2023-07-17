using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public List<itemInfo> itemInfo = new List<itemInfo>();
    public List<chestInfo> chestInfo = new List<chestInfo>();
    public List<weaponInfo> weaponInfo = new List<weaponInfo>();
    public Weapon[] equipWeapons = new Weapon[6];
    public int gold = 0;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    string path;

    void Awake()
    {
        if (instance == null) instance = this;
        path = Path.Combine(Application.dataPath, "database.json");
        JsonLoad();
    }
    private void Start()
    {
        SetLoad();
    }
    void SetLoad()
    {
        SaveData saveData = new SaveData();
        if (!File.Exists(path))
        {
            //경로가 존재 안함
            Debug.Log("경로가 존재 안함");
            //GameManager.instance.ResetPlayer();
            //JsonSave();
        }
        else
        {
            string loadJson = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            if (saveData != null)
            {
                //불러오기
                EquipmentManager.instance.Equipment.ResetStatus();
                InventoryManager.instance.Initalize();
                SetData();
            }
        }
    }

    public void JsonLoad()
    {

        SaveData saveData = new SaveData();
        if (!File.Exists(path))
        {
            //경로가 존재 안함
            Debug.Log("경로가 존재 안함");
            //GameManager.instance.ResetPlayer();
            //JsonSave();
        }
        else
        {
            string loadJson = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            if (saveData != null)
            {
                //불러오기
                GameManager.Gold = saveData.gold;
                Inventory.items = saveData.itemInfo;
                Inventory.chests = saveData.chestInfo;
                Inventory.weapons = saveData.weaponInfo;
                Equipment.Weapons = saveData.equipWeapons;
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
        saveData.itemInfo = Inventory.items;
        saveData.chestInfo = Inventory.chests;
        saveData.weaponInfo = Inventory.weapons;
        saveData.gold = GameManager.Gold;
        saveData.equipWeapons = Equipment.Weapons;
        #endregion

        string json = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(path, json);
    }
}
