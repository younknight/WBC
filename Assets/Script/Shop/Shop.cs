using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] GoodsManager itemBox;
    [SerializeField] GoodsManager chestBox;
    [SerializeField] GoodsManager specialChestBox;
    [SerializeField] GoodsManager playerBox;
    [SerializeField] ShopGoodsInfoManager shopInfo;
    [SerializeField] Reroller rerollBtn;
    int[] rerollCost = new int[3] { 1000, 5000, 10000 };
    public void Setting()
    {
        itemBox.Setting(shopInfo.GoodsInfo.goodsDic[goodsType.item]);
        chestBox.Setting(shopInfo.GoodsInfo.goodsDic[goodsType.chest]);
        playerBox.StaticSetting();
        specialChestBox.StaticSetting();
        if (shopInfo.GoodsInfo.CanReroll()) rerollBtn.SetActive(true, rerollCost[shopInfo.GoodsInfo.GetCount()].ToString());
        else rerollBtn.SetActive(false, "X");
    }
    public void OpenAdPopup()
    {
        ADPopup.Instance.OpenPopup(rewardType.shopReroll);
    }
    public void RerollShop(bool isDayOver)
    {
        if (shopInfo.GoodsInfo.CanReroll())
        {
            if (!isDayOver) ResourseManager.Instance.Purchase(true, rerollCost[shopInfo.GoodsInfo.GetCount()]);
            shopInfo.GoodsInfo.AddRerollCount();
            if (isDayOver) rerollBtn.SetActive(true, rerollCost[0].ToString());
            else
            {
                if (shopInfo.GoodsInfo.CanReroll()) rerollBtn.SetActive(true, rerollCost[shopInfo.GoodsInfo.GetCount()].ToString());
                else rerollBtn.SetActive(false, "X");
            }
            // Debug.Log("reroll");
            SetSlots();
            shopInfo.Save();
        }
    }
    void SetSlots()
    {
        itemBox.FreshSlots();
        chestBox.FreshSlots();
        playerBox.FreshSlots();
        itemBox.RandomSetting();
        chestBox.RandomSetting();
        playerBox.StaticSetting();
        specialChestBox.StaticSetting();
    }
}
