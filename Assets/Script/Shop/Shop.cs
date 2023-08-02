using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] GoodsManager itemBox;
    [SerializeField] GoodsManager chestBox;
    [SerializeField] GoodsManager playerBox;

    private void Start()
    {
        RerollShop();
    }
    public void RerollShop()
    {
        itemBox.FreshSlots();
        chestBox.FreshSlots();
        playerBox.FreshSlots();
        itemBox.Setting(1);
        chestBox.Setting(0);
        playerBox.Setting(2);
    }
}
