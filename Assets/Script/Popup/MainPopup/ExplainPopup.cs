using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ExplainPopup : Popup
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI rankingText;
    [SerializeField] TextMeshProUGUI explainText;
    [SerializeField] TextMeshProUGUI idText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] GameObject weaponBtn;
    [SerializeField] GameObject chestBtn;
    [SerializeField] GameObject itemBtn;
    [SerializeField] DropPopup chestPopup;
    [SerializeField] WeaponInfoPopup weaponInfoPopup;
    [SerializeField] ItemSellPopup itemPopup;
    static ExplainPopup instance;
    public static ExplainPopup Instance { get => instance; set => instance = value; }
    private void OnDestroy() { Instance = null; }
    void Awake() { if (Instance == null) Instance = this; }
    public void SetExplain(Item item)
    {
        ResetObj();
        CommonSet(item);
        if (item is Weapon)
        {
            weaponBtn.gameObject.SetActive(true);
            Weapon weapon = (Weapon)item;
            weaponInfoPopup.Setup(weapon);
            levelText.transform.parent.gameObject.SetActive(true);
            levelText.text = "Lv." + ItemDatabaseManager.WeaponLevels.Find(x => x.item == item).level;
        }
        if (item is Chest)
        {
            chestBtn.gameObject.SetActive(true);
            Chest chest = (Chest)item;
            chestPopup.Setup(chest.dropItems);
        }
        if(item is Ingredient)
        {
            itemBtn.gameObject.SetActive(true);
            Ingredient ingredient = (Ingredient)item;
            itemPopup.Setup(false, ingredient);
            priceText.text = ingredient.sellPrice.ToString();
        }
    }
    void CommonSet(Item item)
    {
        idText.text = "No." + item.id;
        nameText.text = item.itemName;
        explainText.text = item.itemExplain;
        image.sprite = item.itemImage;
        rankingText.text = item.ranking;
    }
    void ResetObj()
    {
        levelText.transform.parent.gameObject.SetActive(false);
        itemBtn.gameObject.SetActive(false);
        chestBtn.gameObject.SetActive(false);
        weaponBtn.gameObject.SetActive(false);
        chestPopup.gameObject.SetActive(false);
        weaponInfoPopup.gameObject.SetActive(false);
        itemPopup.gameObject.SetActive(false);
    }
}
