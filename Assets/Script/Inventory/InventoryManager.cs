using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] List<Inventory> inventories = new List<Inventory>();

    public static InventoryManager instance;
    private void OnDestroy()
    {
        instance = null;
    }
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    public void Initalize()
    {
        for (int i = 0; i < inventories.Count; i++)
        {
            inventories[i].FreshSlot(true);
        }
    }
    #region 아이템 추가 및 사용
    public void AddItems<T>(T item, int num) where T : IInformation
    {

        if(InfoManager.CheckInterfaceType(item) == "chest")
        {
            if (item.GetId() == 0) SoundEffecter.Instance.PlayEffect(soundEffectType.getNegative);//이상한 상자
            else SoundEffecter.Instance.PlayEffect(soundEffectType.getPositive);//그 외
        }
        else SoundEffecter.Instance.PlayEffect(soundEffectType.getPositive);//아이템
        for (int i = 0; i < inventories.Count; i++)
        {
            if (inventories[i].InventoryType == GetInventoryType<T>(item))
            {
                inventories[i].AddItems<T>(item, num);
                Initalize();
                return;
            }
        }
    }
    public void DropItems<T>(T item, int num) where T : IInformation
    {
        for (int i = 0; i < inventories.Count; i++)
        {
            if (inventories[i].InventoryType == GetInventoryType<T>(item))
            {
                inventories[i].DropItems<T>(item, num);
            }
        }
    }
    inventoryType GetInventoryType<T>(T item) where T : IInformation
    {
        string type = InfoManager.GetClassName(item);
        //Debug.Log(type);
        switch (type)
        {
            case "Item":
                return inventoryType.item;
            case "Chest":
                return inventoryType.chest;
            case "Weapon":
                return inventoryType.weapon;
        }
        return inventoryType.item;
    }
    #endregion
}
