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
    void IDropHandler.OnDrop(PointerEventData eventData)//���������
    {
        if (eventData.pointerDrag.GetComponent<Drag>() != null)//��� �ִ� �༮
        {
            originalSlot = eventData.pointerDrag.transform.GetComponent<Drag>().slot;
            if(originalSlot.number > 0)
            {
                SoundEffecter.Instance.PlayEffect(soundEffectType.drop);
                if (craftSlot.IsNull())
                {
                    craftSlot.SetItem(originalSlot.Item);
                    InventoryManager.instance.DropItem(originalSlot.Item, 1);
                    craftSlot.AddResource(craftSlot.Item.id);
                }
                else
                {
                    //���Һ�
                    InventoryManager.instance.AddItem(craftSlot.Item, 1);
                    craftSlot.RemoveResource(craftSlot.Item.id);
                    craftSlot.SetItem(originalSlot.Item);
                    craftSlot.AddResource(craftSlot.Item.id);
                    InventoryManager.instance.DropItem(originalSlot.Item, 1);
                }
            }
        }
    }
}
