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

    public void Setup(bool isNew, Sprite sprite, int count)
    {
        itemImage.sprite = sprite;
        countText.text = "x" + count;
        isNewFrame.SetActive(isNew);
    }
    public void SetupDrop(DropItem<IInformation> dropItem)
    {
        itemImage.sprite = dropItem.drop.GetSprite();
        countText.text = dropItem.percent + "%";
    }
}
