using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AutoCraftSlot : MonoBehaviour
{
    [SerializeField] Chest chest;//
    [Header("기본 이미지")]
    [SerializeField] Image chestImage;
    [SerializeField] Sprite defaultImage;
    public static AutoCraftSlot currentSelectedSlot;

    public Chest Chest { get => chest; set => chest = value; }

    public void OpenPopup()
    {
        ChestPopup.Instance.OpenPopup(true);
    }
    public void FreashSlot()
    {
        chest = null;
        currentSelectedSlot = null;
        chestImage.sprite = defaultImage;
        chestImage.color = new Color(1, 1, 1, 0.5f);
    }
    public void SetChest(Chest chest)
    {
        this.chest = chest;
        chestImage.sprite = chest.chetImage;
        chestImage.color = new Color(1, 1, 1, 1);
    }
    private void OnDestroy()
    {
        currentSelectedSlot = null;
    }
}
