using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AutoCraftSlot : Slot
{
    [SerializeField] Sprite defaultImage;
    public static AutoCraftSlot currentSelectedSlot;

    public Chest Chest { get => (Chest)Item;}

    public void OpenPopup()
    {
        ChestPopup.Instance.OpenPopup(true);
    }
    public void FreshAutoCraftSlot()
    {
        FreshSlot(false);
        currentSelectedSlot = null;
        image.sprite = defaultImage;
        image.color = new Color(1, 1, 1, 0);
    }
    public void SetChest(Chest chest)
    {
        Item = chest;
        image.sprite = chest.itemImage;
        image.color = new Color(1, 1, 1, 1);
    }
    private void OnDestroy()
    {
        currentSelectedSlot = null;
    }
}
