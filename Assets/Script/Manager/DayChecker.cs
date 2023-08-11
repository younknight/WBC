using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayChecker : MonoBehaviour
{
    [SerializeField] Shop shop;
    [SerializeField] ShopGoodsInfoManager shopInfo;
    bool isOverDay = false;
    private void Awake()
    {
        if (StaminaManager.Instance.OverDay())
        {
            StaminaManager.Instance.CheckDay();
            isOverDay = true;
        }
    }
    void Start()
    {
        if (isOverDay)
        {
            shop.RerollShop(true, false);
            shopInfo.GoodsInfo.ResetCount();
            shopInfo.Save();
        }
        shop.Setting();
    }
}
