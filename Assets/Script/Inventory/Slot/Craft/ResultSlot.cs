using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResultSlot : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Image questioImage;
    [SerializeField] Button button;
    [SerializeField] Sprite unknownChest;
    [SerializeField] Sprite questionMark;
    Crafter crafter;
    Chest chest;
    public Chest Chest { get => chest; set => chest = value; }

    private void Start()
    {
        ClearChest();
        crafter = transform.parent.GetComponent<Crafter>();
        button.interactable = false;
    }
    public void SetChest(Chest chest)
    {
        button.interactable = true;
        this.chest = chest;
        if (Inventory.CheckNewChest(chest))//만약 새로운 상자라면
        {
            image.sprite = unknownChest;
            questioImage.color = new Color(1, 1, 1, 1);
        }
        else
        {
            image.sprite = chest.chetImage;
        }
        image.color = new Color(1, 1, 1, 1);
    }
    public void ClearChest()
    {
        button.interactable = false;
        chest = null;
        image.color = new Color(1, 1, 1, 0);
        questioImage.color = new Color(1, 1, 1, 0);
    }
    public void OpenChest()
    {
        PopupManager.instance.OpenGetItemPopup(chest.chestName + " 상자", "x1", chest.chetImage, Inventory.CheckNewChest(chest), chest.ranking);
        InventoryManager.instance.AddChest(chest,1);
        crafter.FreshSlot();
    }
}
