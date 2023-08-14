using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum goodsType { ingrediant, chest, player, special }
public class GoodsManager : MonoBehaviour
{
    [SerializeField] ItemDatabaseManager itemDatabaseManager;
    [SerializeField] Transform slotsParent;
    [SerializeField] goodsType goodsType;
    [SerializeField] GoodsSlot[] slots;

    public goodsType GoodsType { get => goodsType; set => goodsType = value; }

    public List<Goods> GetGoodsId()
    {
        List<Goods> ids = new List<Goods>();
        for(int i = 0; i< slots.Length; i++)
        {
            if(slots[i].Goods != null)
            ids.Add(slots[i].Goods);
        }
        return ids;
    }

    private void OnValidate()
    {
        slots = slotsParent.GetComponentsInChildren<GoodsSlot>();
    }

    public void FreshSlots()
    {
        for(int i= 0;i< slots.Length; i++)
        {
            slots[i].ClearSlot();
        }
    }
    public void SetItems(List<Goods> goods)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(goods[i].count > 0)
            {
                slots[i].gameObject.SetActive(true);
                Item item = null;
                if (goodsType == goodsType.ingrediant) { item = itemDatabaseManager.FIndItemWithId(goods[i].id, inventoryType.ingrediant).item; }
                if (goodsType == goodsType.chest)  { item = itemDatabaseManager.FIndItemWithId(goods[i].id, inventoryType.chest).item; }
                slots[i].SetItem(item, goods[i].count);
            }
            else slots[i].gameObject.SetActive(false);
        }
    }
    public void SetNotItems()
    {
        if (goodsType == goodsType.player)
        {
            if (LockManager.Instance.GetLastLevel(lockType.craftCoolTime)) slots[0].IsMaxSlot();
            else slots[0].SetLock(LockManager.Instance.GetLevel(lockType.craftCoolTime) * 1000, lockType.craftCoolTime);
            if (LockManager.Instance.GetLastLevel(lockType.maxCraftCounter)) slots[1].IsMaxSlot();
            else slots[1].SetLock(LockManager.Instance.GetLevel(lockType.maxCraftCounter) * 1000, lockType.maxCraftCounter);
            if (LockManager.Instance.GetLastLevel(lockType.openSlotCount)) slots[2].IsMaxSlot();
            else slots[2].SetLock(LockManager.Instance.GetLevel(lockType.openSlotCount) * 1000, lockType.openSlotCount);
            return;
        }
        if(goodsType == goodsType.special)
        {
            for(int i = 0; i< slots.Length; i++)
            {
                if(i < ResourseManager.Instance.GetLevel())
                {
                    slots[i].SetButton(true, i);
                    continue;
                }
                slots[i].SetButton(false, i);
            }
        }
    }
    public void RandomSetting()//1 아이템, 0 상자
    {
        int index;
        int count;
        List<int> indexs = new List<int>();
        List<ItemInfo> items = new List<ItemInfo>();
        if (goodsType == goodsType.ingrediant) { items = itemDatabaseManager.GetItemListWithType(inventoryType.ingrediant); }
        if (goodsType == goodsType.chest) { items = itemDatabaseManager.GetItemListWithType(inventoryType.chest); }
        //아이템의 인덱스 수집
        for (int j = 0; j < items.Count; j++)
        {
            indexs.Add(j);
        }
        int length = indexs.Count;
       //아이템 랜덤 진열
        for (int i = 0 ; i < slots.Length; i++)
        {
            slots[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < slots.Length && i < length; i++)
        {
            if (goodsType == goodsType.chest && indexs.Count < 2) break;
            count = Random.Range(1, 5 + 1);
            index = Random.Range(0, indexs.Count);
            slots[i].gameObject.SetActive(true);
            slots[i].SetItem(items[indexs[index]].item, count);
            indexs.Remove(indexs[index]);
        }
    }
}
