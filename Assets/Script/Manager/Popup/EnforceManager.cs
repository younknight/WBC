using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnforceManager : MonoBehaviour
{
    static EnforceManager instance;
    [SerializeField] int resourceCount = 0;
    [SerializeField] Weapon weapon;//
    [SerializeField] WeaponInfo weaponInfo;
    [SerializeField] EquipmentSlot equipmentSlot;
    [SerializeField] int level;//
    [SerializeField] int gauge;//
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI gaugeText;
    [SerializeField] TextMeshProUGUI resouceText;
    [SerializeField] Slider gaugeSlider;
    [SerializeField] Slider expectationSlider;
    [SerializeField] InventoryManager inventoryManager;//
    [SerializeField] ItemDatabaseManager itemDatabaseManager;

    public static EnforceManager Instance { get => instance; set => instance = value; }

    private void OnDestroy()
    {
        Instance = null;
    }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        FreshSlot();
    }


    public void FreshSlot()
    {
        weapon = null;
        level = -1;
        gauge = -1;
        resourceCount = 0;
        levelText.text = "Lv.";
        gaugeText.text = "";
        resouceText.text = "";
        equipmentSlot.FreashSlot();
        gaugeSlider.value = 0;
        expectationSlider.value = 0;
    }
    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        equipmentSlot.SetWeapon(weapon);
        weaponInfo = ItemDatabaseManager.WeaponLevels[ItemDatabaseManager.WeaponLevels.FindIndex(x => x.item == weapon)];
        SetGauge(weaponInfo.level, weaponInfo.gauge);
    }
    public void SetGauge(int level, int gauge)
    {
        int index = ItemDatabaseManager.WeaponLevels.FindIndex(x => x.item == weapon);
        ItemDatabaseManager.WeaponLevels[index] = new WeaponInfo(weapon, level, gauge);
        weaponInfo = new WeaponInfo(weapon, level, gauge);
        this.level = level;
        this.gauge = gauge;
        levelText.text = "Lv." + this.level;
        gaugeText.text = this.gauge + "/" + this.level * 2;
        resourceCount = 0;
        resouceText.text = "" + resourceCount;
        //slider
        gaugeSlider.value = (float)(this.gauge) / (float)(level * 2);
        expectationSlider.value = (float)(gauge + resourceCount) / (float)(level * 2);
    }
    public void TryEnforce()
    {
        gauge += resourceCount;
        while (level * 2 <= gauge)
        {
            gauge -= level * 2;
            level++;
        }
        inventoryManager.DropItems(weapon, resourceCount);
        SetGauge(level, gauge);
        EquipmentManager.instance.SetEquipManager();
        DataManager.instance.JsonSave();
    }
    public void PlusResource(bool isPlus)
    {
        if (weapon != null)
        {
            if (isPlus)
            {
                if (itemDatabaseManager.FIndItemWithId(weapon.id, inventoryType.weapon).num - 1 > resourceCount) resourceCount++;
            }
            else
            {
                if (resourceCount > 0) resourceCount--;
            }
            if (level * 2 < gauge + resourceCount) gaugeText.text = this.gauge + "/" + this.level * 2 + "(+" + (gauge + resourceCount - level * 2) + ")";
            else gaugeText.text = this.gauge + "/" + this.level * 2;
            expectationSlider.value = (float)(gauge + resourceCount) / (float)(level * 2);
        }
        resouceText.text = resourceCount.ToString();
    }
}
