using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropPopup : Popup
{//
    [SerializeField] List<GetItemSlot> slots;
    [SerializeField] Toggle toggle;
    public void TogglePopup()
    {
        if (toggle.isOn) Open();
        else CloseStart();
    }
    public void Setup(List<DropItem<IInformation>> drops)
    {
        toggle.isOn = false;
        int i = 0;
        for (; i < drops.Count; i++)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].SetupDrop(drops[i]);
        }
        for (; i < slots.Count; i++)
        {
            slots[i].gameObject.SetActive(false);
        }
    }
}
