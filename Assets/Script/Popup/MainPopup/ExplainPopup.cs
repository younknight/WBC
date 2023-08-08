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
    public void SetExplain(IInformation item)
    {
        ResetObj();
        CommonSet(item);
        string type = InfoManager.CheckInterfaceType(item);
        if (type == "weapon")
        {
            levelText.transform.parent.gameObject.SetActive(true);
            levelText.text = "Lv." + Inventory.Weapons.Find(x => x.weapon.id == item.GetId()).level;
            weaponBtn.gameObject.SetActive(true);
            weaponInfoPopup.Setup(InfoManager.GetCharacter<Weapon>(item));
        }
        if (type == "chest")
        {
            List<IInformation> drops = new List<IInformation>();
            Chest chest = InfoManager.GetCharacter<Chest>(item);
            chestBtn.gameObject.SetActive(true);
            chestPopup.Setup(chest.Drops);
        }
        if(type == "item")
        {
            Item targetItem = InfoManager.GetCharacter<Item>(item);
            priceText.text = targetItem.sellPrice.ToString();
            itemBtn.gameObject.SetActive(true);
            itemPopup.Setup(false, targetItem);
        }
    }
    void CommonSet(IInformation item)
    {
        idText.text = "No." + item.GetId();
        nameText.text = item.GetName();
        explainText.text = item.GetExplain();
        image.sprite = item.GetSprite();
        rankingText.text = item.GetRanking();
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
