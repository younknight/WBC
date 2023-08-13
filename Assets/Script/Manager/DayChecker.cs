using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayChecker : MonoBehaviour
{
    [SerializeField] Shop shop;
    [SerializeField] ShopGoodsInfoManager shopInfo;
    public void DayCheck()
    {
        if (StaminaManager.Instance.OverDay())
        {
            StaminaManager.Instance.CheckDay();
            shopInfo.GoodsInfo.ResetCount();
            shop.RerollShop(true, false);
            shopInfo.GoodsInfo.ResetCount();
            shopInfo.Save();
        }
        shop.Setting();
    }
}
