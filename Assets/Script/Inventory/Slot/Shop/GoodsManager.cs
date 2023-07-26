using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsManager : MonoBehaviour
{
    [SerializeField] Transform slotsParent;
    [SerializeField] GoodsSlot[] slots;

    public GoodsSlot[] Slots { get => slots; set => slots = value; }

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
    public void Setting(int isChest)//1 아이템, 0 상자
    {
        int index = 0;
        int count = 0;
        List<int> indexs = new List<int>();
        if (isChest == 1)
        {
            for (int j = 0; j < Inventory.Items.Count; j++)
            {
                indexs.Add(j);
            }
        }
        if(isChest == 0)
        {
            for (int j = 0; j < Inventory.Chests.Count; j++)
            {
                indexs.Add(j);
            }
        }
        if (isChest == 2)
        {
            slots[0].SetLock((int)(20 - LockManager.LockInfo.craftCoolTime) * 1000, lockType.craftCoolTime);
            slots[1].SetLock(LockManager.LockInfo.maxCraftCount * 1000, lockType.maxCraftCounter);
            slots[2].SetLock(LockManager.LockInfo.maxOpenerCount * 1000, lockType.openSlotCount);
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
            if (isChest == 1)//아이템
            {
                slots[i].gameObject.SetActive(true);
                index = Random.Range(0, indexs.Count);
                slots[i].SetItem(Inventory.Items[indexs[index]].item, count);
                indexs.Remove(indexs[index]);
            }
            if (isChest == 0)//상자
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
