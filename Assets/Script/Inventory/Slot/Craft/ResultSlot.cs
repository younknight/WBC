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
    [SerializeField] Crafter crafter;
    [SerializeField] Chest chest;//
    [SerializeField] List<int> weirdRecipe = new List<int>();
    public Chest Chest { get => chest; set => chest = value; }
    public List<int> WeirdRecipe { get => weirdRecipe; set => weirdRecipe = value; }

    private void Start()
    {
        ClearChest();
        button.interactable = false;
    }
    public void SetChest(Chest chest, bool isNew)
    {
        button.interactable = true;
        this.chest = chest;
        if (Inventory.CheckNewChest(chest) || isNew)//만약 새로운 상자라면
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
        if (weirdRecipe != null)
        {
            CraftDatabase.instance.AddWierd(weirdRecipe);
        }
        GetChestPopup.Instance.SetGetChest(chest);
        GetChestPopup.Instance.Open();
        //PopupManager.Instance.OpenGetItemPopup();-----------------------------------------------------
        //Debug.Log(chest.ToString() + "/메이크 체스트/" + 1);
        InventoryManager.instance.AddItems<Chest>(chest,1);
        ClearChest();
        crafter.FreshSlot();

    }
}
