using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum goodsType { item, chest, player, special }
public class GoodsManager : MonoBehaviour
{
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
    public void Setting(List<Goods> goods)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(goods[i].count > 0)
            {
                if (goodsType == goodsType.item)//아이템
                {
                    slots[i].gameObject.SetActive(true);
                    slots[i].SetItem(Inventory.Items.Find(x => x.item.id == goods[i].id).item, goods[i].count);
                }
                if (goodsType == goodsType.chest)//상자
                {
                    slots[i].gameObject.SetActive(true);
                    slots[i].SetChest(Inventory.Chests.Find(x => x.chest.id == goods[i].id).chest, goods[i].count);
                }
            }
            else slots[i].gameObject.SetActive(false);
        }
    }
    public void RandomSetting()//1 아이템, 0 상자
    {
        int index = 0;
        int count = 0;
        List<int> indexs = new List<int>();
        if (goodsType == goodsType.item)
        {
            for (int j = 0; j < Inventory.Items.Count; j++)
            {
                indexs.Add(j);
            }
        }
        if(goodsType == goodsType.chest)
        {
            for (int j = 0; j < Inventory.Chests.Count; j++)
            {
                indexs.Add(j);
            }
        }
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
        int length = indexs.Count;
       
        for (int i = 0 ; i < slots.Length; i++)
        {
            slots[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < slots.Length && i < length; i++)
        {
            count = Random.Range(1, 5 + 1);
            if (goodsType == goodsType.item)//아이템
            {
                slots[i].gameObject.SetActive(true);
                index = Random.Range(0, indexs.Count);
                slots[i].SetItem(Inventory.Items[indexs[index]].item, count);
                indexs.Remove(indexs[index]);
            }
            if (goodsType == goodsType.chest)//상자
            {
                if(indexs.Count >= 2)
                {
                    slots[i].gameObject.SetActive(true);
                    index = Random.Range(1, indexs.Count);
                    slots[i].SetChest(Inventory.Chests[indexs[index]].chest, count);
                    indexs.Remove(indexs[index]);
                }
            }
        }
    }
}
