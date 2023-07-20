using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropCraft : MonoBehaviour, IDropHandler
{
    public CraftSlot craftSlot;//
    public Slot originalSlot;//
    void Awake()
    {
        craftSlot = GetComponent<CraftSlot>();
    }
    void IDropHandler.OnDrop(PointerEventData eventData)//드랍됬을때
    {
        if (eventData.pointerDrag.GetComponent<Drag>() != null)//들고 있는 녀석
        {
            originalSlot = eventData.pointerDrag.transform.GetComponent<Drag>().slot;
            if(originalSlot.Number > 0)
            {
                SoundEffecter.Instance.PlayEffect(soundEffectType.drop);
                if (craftSlot.IsNull())
                {
                    craftSlot.SetItem((Item)originalSlot.ItemInformation);
                    InventoryManager.instance.DropItems<Item>((Item)originalSlot.ItemInformation, 1);
                    craftSlot.AddResource(craftSlot.Item.id);
                }
                else
                {
                    //스왑부
                    InventoryManager.instance.AddItems<Item>(craftSlot.Item, 1);
                    craftSlot.RemoveResource(craftSlot.Item.id);
                    craftSlot.SetItem((Item)originalSlot.ItemInformation);
                    craftSlot.AddResource(craftSlot.Item.id);
                    InventoryManager.instance.DropItems<Item>((Item)originalSlot.ItemInformation, 1);
                }
            }
        }
    }
}
