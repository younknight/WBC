using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GetItemPopup : Popup
{
    [SerializeField] List<GetItemSlot> slots;
    static GetItemPopup instance;
    public static GetItemPopup Instance { get => instance; set => instance = value; }
    private void OnDestroy() { Instance = null; }
    void Awake() { if (Instance == null) Instance = this; }
    public void SetGetItem(Dictionary<IInformation, int> drops)
    {
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
            slots[i++].Setup(isNew, entry.Key.GetSprite(), "x" + entry.Value.ToString()) ;
        }
        for (; i < slots.Count; i++)
        {
            slots[i].gameObject.SetActive(false);
        }
    }

}
