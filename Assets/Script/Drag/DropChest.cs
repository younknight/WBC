using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropChest : MonoBehaviour, IDropHandler
{
    public OpenSlot openSlot;//
    void Awake()
    {
        openSlot = GetComponent<OpenSlot>();
    }
    void IDropHandler.OnDrop(PointerEventData eventData)//드랍됬을때
    {
        if (eventData.pointerDrag.GetComponent<Drag>() != null)//들고 있는 녀석
        {
            Slot slot = eventData.pointerDrag.transform.GetComponent<Drag>().slot;
            if(slot.number > 0)
            {
                if (openSlot.IsNull())
                {
                    {
                        openSlot.SetChest(slot.Chest, 0);
                        InventoryManager.instance.DropChest(slot.Chest, 1);
                    }

                }
            }
        }
    }
}
