using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GetItemPopup : Popup
{
    Chest chest;
    [SerializeField] InventoryManager inventoryManager;//
    [SerializeField] ItemDatabaseManager itemDatabaseManager;
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
        Dictionary<Item, int> drops = chest.GetRandomDropItemsWithCount();
        int i = 0;
        foreach(KeyValuePair<Item, int> entry in drops)
        {
            bool isNew = false;
            isNew = itemDatabaseManager.CheckNew(entry.Key);
            inventoryManager.AddItems(entry.Key, entry.Value);
            slots[i].gameObject.SetActive(true);
            slots[i].Setup(isNew, entry.Key.itemImage, "x" + entry.Value.ToString()) ;
            i++;
        }
        for (; i < slots.Count; i++)
        {
            slots[i].gameObject.SetActive(false);
        }
        popup.SetActive(false);
        chestImage.gameObject.SetActive(true);
        chestImage.sprite = chest.itemImage;
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
