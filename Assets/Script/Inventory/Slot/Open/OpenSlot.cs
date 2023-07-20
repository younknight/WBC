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
        Opener.OpeningChests[Id] = new openingChest(chest.id, DateTime.Now);
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
            int min = 0;
            int percent = UnityEngine.Random.Range(1, 100 + 1);
            for (int i = 0; i < chest.dropItems.Count; i++)
            {
                if (i != 0) min = chest.dropItems[i - 1].percent;
                if (min < percent && percent <= chest.dropItems[i].percent)
                {
                    DeleteOpenChest();
                    //실제 오픈 부
                    int count = UnityEngine.Random.Range(chest.dropItems[i].minDrop, chest.dropItems[i].maxDrop + 1);

                    Item item = chest.dropItems[i].dropItems;
                    PopupManager.instance.OpenGetItemPopup(item.itemName, "x" + count.ToString(), item.itemImage, Inventory.CheckNewItem(item), item.ranking);//////isNew확인바람
                    InventoryManager.instance.AddItem(item, count);
                    SoundEffecter.Instance.PlayEffect(soundEffectType.chestOpen);
                    RemoveSlot();
                }
            }
        }
        else
        {
            Debug.Log("열 수 없음");
        }
    }
}
