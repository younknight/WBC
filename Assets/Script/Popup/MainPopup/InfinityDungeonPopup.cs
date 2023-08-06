using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityDungeonPopup : Popup
{
    [SerializeField] SceneMoveManager moveManager;
    [SerializeField] StaminaGauge staminaGauge;
    [SerializeField] TmpAlpha tmpAlpha;
    public void OpenPopup()
    {
        Open();
        tmpAlpha.FadeOut(StaminaManager.Instance.StaminaData.currentStamina - 1);
    }
    public void ClosePopup()
    {
        tmpAlpha.StopFadeOut();
        CloseStart();
    }
    public void TryEnterDungeon()
    {
        if(StaminaManager.Instance.StaminaData.currentStamina > 0)
        {
            StaminaManager.Instance.UseStamina();
            moveManager.MoveScene("InfinityDungeon");
        }
        else
        {
            Close();
            ADPopup.Instance.OpenPopup(rewardType.stamina);
        }
    }
}
