using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] GoodsManager itemBox;
    [SerializeField] GoodsManager chestBox;
    [SerializeField] GoodsManager playerBox;
    [SerializeField] ShopGoodsInfoManager shopInfo;
    private void Start()
    {
        itemBox.Setting(shopInfo.GoodsInfo.goodsDic[goodsType.item]);
        chestBox.Setting(shopInfo.GoodsInfo.goodsDic[goodsType.chest]);
        playerBox.RandomSetting();
    }
    public void RerollShop()
    {
        SetSlots();
        shopInfo.Save();
    }
    void SetSlots()
    {
        itemBox.FreshSlots();
        chestBox.FreshSlots();
        playerBox.FreshSlots();
        itemBox.RandomSetting();
        chestBox.RandomSetting();
        playerBox.RandomSetting();
    }
}
