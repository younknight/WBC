using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetItemSlot : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] GameObject isNewFrame;

    public void Setup(bool isNew, Sprite sprite, string count)
    {
        itemImage.sprite = sprite;
        countText.text =  count;
        isNewFrame.SetActive(isNew);
    }
    public void SetupDrop(DropItem dropItem)
    {
        itemImage.sprite = dropItem.drop.itemImage;
        countText.text = dropItem.percent + "%";
    }
}
