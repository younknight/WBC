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
    [SerializeField]  bool isLock;//
    [SerializeField] Image image;
    [SerializeField] Timer timer;
    [SerializeField] Button button;
    Animator animator;
    [SerializeField] Chest chest = null;//

    public bool IsOpening { get => isOpening; set => isOpening = value; }
    public Opener Opener { get => opener; set => opener = value; }
    public int Id { get => id; set => id = value; }
    public bool IsLock { get => isLock; set => isLock = value; }

    private void Start()
    {
        animator = GetComponent<Animator>();
        timer.Animator = animator;
    }
    public bool IsNull()
    {
        return !isOpening;
    }
    public void RemoveSlot(bool isLock)
    {
        isOpening = false;
        button.interactable = false;
        this.isLock = isLock;
        if (!isLock) image.color = new Color(1, 1, 1, 0);
        else
        {
            image.sprite = Resources.Load<Sprite>("Icon/lock");
            image.color = new Color(1, 1, 1, 1);
        }
        timer.gameObject.SetActive(false); 
    }


    void AddOpenChest()
    {
        Opener.OpeningChests[Id] = new OpeningChest(chest.id, DateTime.Now);
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
            RemoveSlot(false);
        }
        else
        {
            Debug.Log("�� �� ����");
        }
    }
}
