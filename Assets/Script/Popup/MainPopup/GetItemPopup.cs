using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GetItemPopup : Popup
{
    Chest chest;
    [SerializeField] InventoryManager inventoryManager;//
    [SerializeField] GameObject popup;
    [SerializeField] Image chestImage;
    [SerializeField] List<GetItemSlot> slots;
    static GetItemPopup instance;
    public static GetItemPopup Instance { get => instance; set => instance = value; }
    private void OnDestroy() { Instance = null; }
    void Awake() { if (Instance == null) Instance = this; }
    public void SetGetItem(Chest chest)
    {
        Open();
        this.chest = chest;
        Dictionary<IInformation, int> drops = chest.GetDropItem();
        int i = 0;
        foreach(KeyValuePair<IInformation, int> entry in drops)
        {
            bool isNew = false;
            if (InfoManager.CheckInterfaceType(entry.Key) == "item")
            {
                isNew = Inventory.CheckNewItem((Item)entry.Key);
                inventoryManager.AddItems<Item>((Item)entry.Key, entry.Value);
            }
            if (InfoManager.CheckInterfaceType(entry.Key) == "weapon")
            {
                isNew = Inventory.CheckNewWeapon((Weapon)entry.Key);
                inventoryManager.AddItems<Weapon>((Weapon)entry.Key, entry.Value);
            }
            slots[i].gameObject.SetActive(true);
            slots[i].Setup(isNew, entry.Key.GetSprite(), "x" + entry.Value.ToString()) ;
            i++;
        }
        for (; i < slots.Count; i++)
        {
            slots[i].gameObject.SetActive(false);
        }
        popup.SetActive(false);
        chestImage.gameObject.SetActive(true);
        chestImage.sprite = chest.chetImage;
    }
    public void TurnOpenImage()
    {
        chestImage.sprite = chest.chestOpenImage;
    }
    public void GetItem()
    {
        SoundEffecter.Instance.PlayEffect(soundEffectType.getPositive);//¾ÆÀÌÅÛ
        chestImage.gameObject.SetActive(false);
        popup.SetActive(true);
        Debug.Log("asd");
        animator.SetTrigger("open");
    }
}
