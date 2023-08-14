using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Slot : MonoBehaviour
{
    public bool isSet = false;
    int count = 0;
    [SerializeField] int id;
    [SerializeField] bool isShowCount = true;
    [SerializeField] popupType popupType;
    [SerializeField] Item item;
    [SerializeField] protected Image image;
    [SerializeField] Image countFrame;
    [SerializeField] TextMeshProUGUI numberText;
    public int Id { get => id; set => id = value; }
    public popupType PopupType { get => popupType; set => popupType = value; }
    public bool IsShowCount { get => isShowCount; set => isShowCount = value; }
    public Item Item { get => item; set => item = value; }
    public int Count { get => count; set => count = value; }
  
    #region 슬롯 세팅
    public void FreshSlot(bool isFresh)
    {
        isSet = false;
        count = 0;
        gameObject.SetActive(!isFresh);
    }
    public void AddItemInfo(Item item, int num)
    {
        if (!IsShowCount) countFrame.gameObject.SetActive(false);
        this.item = item;
        count += num;
        numberText.text = count.ToString();
        image.sprite = item.itemImage;
        countFrame.color = new Color(1, 1, 1, 1);
        image.color = new Color(1, 1, 1, 1);
    }
    public void Drop(int num)
    {
        count -= num;
        numberText.text = count.ToString();
        //시작 아이템
        if (count <= 0 && item != null)
        {
            if (item.id == 1 && item is Chest)
            {
                gameObject.SetActive(false);
            }
        }
    }
    #endregion
}