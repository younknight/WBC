using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using UnityEngine.UI;

public class OpenSlot : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] Opener opener;
    bool isOpening = false;
    [SerializeField] Image image;
    [SerializeField] Timer timer;
    [SerializeField] Button button;
    Animator animator;
    [SerializeField] Chest chest = null;//

    public bool IsOpening { get => isOpening; set => isOpening = value; }
    public Opener Opener { get => opener; set => opener = value; }
    public int Id { get => id; set => id = value; }

    private void Start()
    {
        animator = GetComponent<Animator>();
        timer.Animator = animator;
    }
    public bool IsNull()
    {
        return !isOpening;
    }
    public void RemoveSlot()
    {
        isOpening = false;
        button.interactable = false;
        image.color = new Color(1, 1, 1, 0);
        timer.gameObject.SetActive(false); 
    }


    void AddOpenChest()
    {
        Opener.OpeningChests[Id] = new OpeningChest(chest.id, 1,DateTime.Now);
    }
    void DeleteOpenChest()
    {
        Opener.OpeningChests[Id] = null;
    }



    public void SetChest(Chest chest, float current)
    {
        isOpening = true;
        this.chest = chest;
        button.interactable = true;
        image.color = new Color(1, 1, 1, 1);
        image.sprite = chest.chetImage;
        timer.gameObject.SetActive(true);
        timer.StartTimer(chest.openTime, current);
        AddOpenChest();
    }
    public void OpenChest()
    {
        animator.SetBool("ready", false);
        if (isOpening && timer.canOpen)
        {
            DeleteOpenChest();
            IInformation newItem = chest.GetRandomDrop();
            int count = chest.GetRandomCount(newItem);
            bool isNew = false;
            if (InfoManager.CheckInterfaceType(newItem) == "item")
            {
                isNew = Inventory.CheckNewItem((Item)newItem);
                InventoryManager.instance.AddItems<Item>((Item)newItem, count);
            }
            if (InfoManager.CheckInterfaceType(newItem) == "weapon")
            {
                isNew = Inventory.CheckNewWeapon((Weapon)newItem);
                InventoryManager.instance.AddItems<Weapon>((Weapon)newItem, count);
            }
            GetItemPopup.Instance.SetGetItem(newItem.GetName(), "x" + count.ToString(), newItem.GetSprite(), isNew, newItem.GetRanking());
            GetItemPopup.Instance.Open();
            //PopupManager.Instance.OpenGetItemPopup(); ;-----------------------------------------------------
            SoundEffecter.Instance.PlayEffect(soundEffectType.chestOpen);
            RemoveSlot();
        }
        else
        {
            Debug.Log("열 수 없음");
        }
    }
}
