using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GetItemPopup : Popup
{
    Chest chest;
    [SerializeField] GameObject popup;
    [SerializeField] Image chestImage;
    [SerializeField] List<GetItemSlot> slots;
    static GetItemPopup instance;
    public static GetItemPopup Instance { get => instance; set => instance = value; }
    private void OnDestroy() { Instance = null; }
    void Awake() { if (Instance == null) Instance = this; }
    public void SetGetItem(Chest chest)
    {
        this.chest = chest;
        Dictionary<IInformation, int> drops = chest.GetDrop();
        int i = 0;
        foreach(KeyValuePair<IInformation, int> entry in drops)
        {
            bool isNew = false;
            if (InfoManager.CheckInterfaceType(entry.Key) == "item")
            {
                isNew = Inventory.CheckNewItem((Item)entry.Key);
                InventoryManager.instance.AddItems<Item>((Item)entry.Key, entry.Value);
            }
            if (InfoManager.CheckInterfaceType(entry.Key) == "weapon")
            {
                isNew = Inventory.CheckNewWeapon((Weapon)entry.Key);
                InventoryManager.instance.AddItems<Weapon>((Weapon)entry.Key, entry.Value);
            }
            Debug.Log(i);
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
        animator.SetTrigger("open");
    }
}
