using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum popupType
{
    explain,
    getItem,
    setting,
    recipe,
    chest,
    status,
    weapon,
    purchase
}
public class Popup : MonoBehaviour
{
    [SerializeField] popupType popupType;
    [SerializeField] GameObject PopupObj;
    Animator animator;
    //default
    [Space(10f)]
    [Header("default")]
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI explainText;
    //explain
    [Space(10f)]
    [Header("explain")]
    [SerializeField] TextMeshProUGUI idText;
    //getItem
    [Space(10f)]
    [Header("getItem")]
    [SerializeField] GameObject isNewFrame;
    [SerializeField] TextMeshProUGUI rankingText;
    //recipe
    [Space(10f)]
    [Header("recipe")]
    [SerializeField] List<Slot> resources;
    //status
    [Space(10f)]
    [Header("status")]
    //[SerializeField] TextMeshProUGUI attackSpeed;
    [SerializeField] TextMeshProUGUI hp;
    [SerializeField] TextMeshProUGUI attack;
    [SerializeField] TextMeshProUGUI defence;
    [SerializeField] TextMeshProUGUI criDamage;
    [SerializeField] TextMeshProUGUI criRate;
    [SerializeField] Inventory inventory;
    [Space(10f)]
    [Header("purchase")]
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] GoodsSlot goods;

    public popupType PopupType { get => popupType; set => popupType = value; }

    private void Start()
    {
        animator = GetComponent<Animator>();
        PopupObj.SetActive(false);
    }
    public void SetSlot(EquipmentSlot equipmentSlot)
    {
        inventory.EquipmentSlot = equipmentSlot;
    }
    #region 衝銅崎 獄動
    public void FreshSlot()//舌搾 背薦つつつつつつつつつつつつつつつつつつつつつつ
    {
        if (inventory.EquipmentSlot.Weapon != null)
        {
            InventoryManager.instance.AddWeapon(inventory.EquipmentSlot.Weapon, 1);
            inventory.EquipmentSlot.DeleteWeapon();
        }
        PopupManager.instance.CloesPopup(popupType.weapon);
        DataManager.instance.JsonSave();
    }
    public void Purchase()
    {
        goods.Purchase();
    }
    #endregion
    #region 橡穣 竺舛背爽奄
    public void SetStatus()
    {
        hp.text = EquipmentManager.instance.GetStatus().Hp.ToString();
        attack.text = EquipmentManager.instance.GetStatus().Attack.ToString();
        defence.text = EquipmentManager.instance.GetStatus().Defence.ToString();
        criDamage.text = EquipmentManager.instance.GetStatus().CriDamage.ToString();
        criRate.text = EquipmentManager.instance.GetStatus().CriRate.ToString();
    }
    public void SetPurchase(GoodsSlot goodsSlot, string name, string ranking, Sprite sprite, int price, int count, int id)
    {
        goods = goodsSlot;
        idText.text = id.ToString();
        nameText.text = name;
        rankingText.text = ranking;
        image.sprite = sprite;
        priceText.text = price.ToString();
        countText.text = "x"+count;
    }
    public void SetRecipe(Chest chest)
    {
        nameText.text = chest.chestName;
        rankingText.text = chest.ranking;
        image.sprite = chest.chetImage;
        for (int i = 0; i < chest.recipes.Count; i++)//幻析 薦繕狛戚 食君鯵虞檎
        {
            int index = 0;
            for(;index < chest.recipes[i].items.Count; index++)
            {
                resources[index].AddItem(chest.recipes[i].items[index], 0);
            }
            for (; index < resources.Count; index++)
            {
                resources[index].FreshSlot(true);
            }
        }
    }
    public void SetGetItem(string name, string count, Sprite sprite, bool isNew,string ranking)
    {
        nameText.text = name;
        explainText.text = count;
        rankingText.text = ranking;
        image.sprite = sprite;
        isNewFrame.SetActive(isNew);
    }
    public void SetExplain(string name, string explain, Sprite sprite, int id, string ranking)
    {
        nameText.text = name;
        explainText.text = explain;
        image.sprite = sprite;
        if (id == 0) id = 0;
        idText.text = "No." + id;
        rankingText.text = ranking;
    }
    #endregion
    #region 橡穣 塊壱 丸奄
    public void Open()
    {
        PopupObj.SetActive(true);
    }
    public void Close()
    {
        PopupObj.SetActive(false);
    }
    public void CloseStart()
    {
        animator.SetTrigger("close");
    }
    #endregion
}
