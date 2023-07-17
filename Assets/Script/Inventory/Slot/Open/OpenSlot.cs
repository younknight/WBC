using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class OpenSlot : MonoBehaviour
{

    [SerializeField] Image image;
    [SerializeField] Timer timer;
    [SerializeField] Button button;
    Animator animator;
    private Chest chest;
    private void Start()
    {
        animator = GetComponent<Animator>();
        timer.Animator = animator;
    }
    public bool IsNull()
    {
        if (chest == null) return true;
        return false;
    }
    public void RemoveSlot()
    {
        button.interactable = false;
        chest = null;
        image.color = new Color(1, 1, 1, 0);
        timer.gameObject.SetActive(false);
    }
    public void SetChest(Chest chest)
    {
        this.chest = chest;
        button.interactable = true;
        image.color = new Color(1, 1, 1, 1);
        image.sprite = chest.chetImage;
        timer.gameObject.SetActive(true);
        timer.StartTimer(chest.openTime);
    }
    public void OpenChest()
    {
        animator.SetBool("ready", false);
        if (chest != null && timer.canOpen)
        {
            int min = 0;
            int percent = Random.Range(0, 100);
            for (int i = 0; i < chest.dropItems.Count; i++)
            {
                if (i != 0) min = chest.dropItems[i - 1].percent;
                if (min < percent && percent < chest.dropItems[i].percent)
                {
                    //실제 오픈 부

                    int count = Random.Range(chest.dropItems[i].minDrop, chest.dropItems[i].maxDrop + 1);
                    Item item = chest.dropItems[i].item;
                    PopupManager.instance.OpenGetItemPopup(item.itemName, "x" + count.ToString(), item.itemImage, Inventory.CheckNewItem(item), item.ranking);//////isNew확인바람

                    InventoryManager.instance.AddItem(item, count);
                }
            }
            RemoveSlot();
        }
        else
        {
            Debug.Log("열 수 없음");
        }
    }
}
