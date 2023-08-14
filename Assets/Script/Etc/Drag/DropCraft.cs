using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropCraft : MonoBehaviour, IDropHandler
{
    public CraftSlot craftSlot;//
    public Slot originalSlot;//
    [SerializeField] InventoryManager inventoryManager;//
    void Awake()
    {
        craftSlot = GetComponent<CraftSlot>();
    }
    void IDropHandler.OnDrop(PointerEventData eventData)//���������
    {
        if (eventData.pointerDrag.GetComponent<Drag>() != null)//��� �ִ� �༮
        {
            originalSlot = eventData.pointerDrag.transform.GetComponent<Drag>().slot;
            if(originalSlot.Count > 0)
            {
                SoundEffecter.Instance.PlayEffect(soundEffectType.drop);
                if (craftSlot.IsNull())
                {
                    craftSlot.SetItem((Ingredient)originalSlot.Item);
                    inventoryManager.DropItems(originalSlot.Item, 1);
                    craftSlot.AddResource(craftSlot.Item.id);
                }
                else
                {
                    //���Һ�
                    inventoryManager.AddItems(craftSlot.Item, 1);
                    craftSlot.RemoveResource(craftSlot.Item.id);
                    craftSlot.SetItem((Ingredient)originalSlot.Item);
                    craftSlot.AddResource(craftSlot.Item.id);
                    inventoryManager.DropItems(originalSlot.Item, 1);
                }
            }
        }
    }
}
