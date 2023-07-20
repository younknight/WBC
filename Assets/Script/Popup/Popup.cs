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
    [SerializeField] GameObject weirdRecipe;
    //status
    [Space(10f)]
    [Header("status")]
    //[SerializeField] TextMeshProUGUI attackSpeed;
    [SerializeField] TextMeshProUGUI hp;
    [SerializeField] TextMeshProUGUI attack;
    [SerializeField] TextMeshProUGUI defence;
    [SerializeField] TextMeshProUGUI criDamage;
    [SerializeField] TextMeshProUGUI criRate;
    [SerializeField] EquipmentManager equipmentManager;
    [SerializeField] Inventory inventory;
    [SerializeField] Player player;
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
    #region 액티브 버튼
    public void FreshSlot()//장비 해제ㅔㅔㅔㅔㅔㅔㅔㅔㅔㅔㅔㅔㅔㅔㅔㅔㅔㅔㅔㅔㅔㅔ
    {
        if (inventory.EquipmentSlot.Weapon != null)
        {
            InventoryManager.instance.AddItems<Weapon>(inventory.EquipmentSlot.Weapon, 1);
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
    #region 팝업 설정해주기
    public void SetStatus()
    {
        hp.text = equipmentManager.GetStatus().Hp.ToString();
        attack.text = equipmentManager.GetStatus().Attack.ToString();
        defence.text = equipmentManager.GetStatus().Defence.ToString();
        criDamage.text = equipmentManager.GetStatus().CriDamage.ToString();
        criRate.text = equipmentManager.GetStatus().CriRate.ToString();
        if (player != null)
        {
            hp.text += "(+" + equipmentManager.Equipment.GetDifferce(statusType.maxHp, player.Unit.GetStatus(statusType.maxHp)) + ")";
            attack.text += "(+" + equipmentManager.Equipment.GetDifferce(statusType.attack, player.Unit.GetStatus(statusType.attack)) + ")";
            defence.text += "(+" + equipmentManager.Equipment.GetDifferce(statusType.defence, player.Unit.GetStatus(statusType.defence)) + ")";
            criDamage.text += "(+" + equipmentManager.Equipment.GetDifferce(statusType.criDamage, player.Unit.GetStatus(statusType.criDamage)) + ")";
            criRate.text += "(+" + equipmentManager.Equipment.GetDifferce(statusType.criRate, player.Unit.GetStatus(statusType.criRate)) + ")";
        }
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
        weirdRecipe.SetActive(false);
        if (chest.id == 0)
        {
            weirdRecipe.SetActive(true);
            return;
        }
        for (int i = 0; i < chest.recipes.Count; i++)//만일 제조법이 여러개라면
        {
            int index = 0;
            for(;index < chest.recipes[i].items.Count; index++)
            {
                resources[index].NewAddItemInfo<Item>(chest.recipes[i].items[index], 0);
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
    #region 팝업 끄고 닫기
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
