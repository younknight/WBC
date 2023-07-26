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
    void IDropHandler.OnDrop(PointerEventData eventData)//���������
    {
        if (eventData.pointerDrag.GetComponent<Drag>() != null && !openSlot.IsLock)//��� �ִ� �༮
        {
            Slot slot = eventData.pointerDrag.transform.GetComponent<Drag>().slot;
            if(slot.Number > 0)
            {
                if (openSlot.IsNull())
                {
                    {
                        SoundEffecter.Instance.PlayEffect(soundEffectType.drop);
                        openSlot.SetChest((Chest)slot.ItemInformation, 0);
                        InventoryManager.instance.DropItems<Chest>((Chest)slot.ItemInformation, 1);
                    }

                }
            }
        }
    }
}
