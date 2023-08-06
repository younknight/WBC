using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayChecker : MonoBehaviour
{
    [SerializeField] Shop shop;
    void Start()
    {
        if (StaminaManager.Instance.OverDay())
        {
            shop.RerollShop();
            StaminaManager.Instance.CheckDay();
        }
    }
}
