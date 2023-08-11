using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [SerializeField] Shop shop;
    public void GetReward(rewardType selectedMode)
    {
        switch (selectedMode)
        {
            case rewardType.stamina:
                StaminaManager.Instance.AddStamina();
                break;
            case rewardType.shopReroll:
                shop.RerollShop(false, true);
                break;
        }
    }
}
