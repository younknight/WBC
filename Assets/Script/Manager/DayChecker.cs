using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayChecker : MonoBehaviour
{
    [SerializeField] Shop shop;
    [SerializeField] ShopGoodsInfoManager shopInfo;
    private void Awake()
    {
        if (StaminaManager.Instance.OverDay())
        {
            StaminaManager.Instance.CheckDay();
        }
    }
    void Start()
    {
        if (StaminaManager.Instance.OverDay())
        {
            shop.RerollShop(true);
            shopInfo.GoodsInfo.ResetCount();
            shopInfo.Save();
        }
    }
}
