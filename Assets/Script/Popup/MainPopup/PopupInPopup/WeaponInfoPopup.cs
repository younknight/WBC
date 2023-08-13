using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WeaponInfoPopup : Popup
{
    [SerializeField] Toggle toggle;
    [SerializeField] StatusChanger status;
    [SerializeField] TextMeshProUGUI skillName;
    [SerializeField] TextMeshProUGUI skillExplian;
    public void Setup(Weapon weapon)
    {
        toggle.isOn = false;
        SetStatus(weapon);
        SetSkill(weapon);
    }
    void SetSkill(Weapon weapon)
    {
        if(weapon.skills != null)
        {
            skillName.text = weapon.skills.GetName();
            skillExplian.text = weapon.skills.GetExplain();
        }
        else
        {
            skillName.text = "스킬 없음";
            skillExplian.text = "스킬이 없는 순수한 무기다.";
        }
    }
    void SetStatus(Weapon weapon)
    {
        status.SetText(statusType.maxHp, weapon.status.hp.ToString());
        status.SetText(statusType.attack, weapon.status.attack.ToString());
        status.SetText(statusType.attackSpeed, weapon.status.attackSpeed.ToString());
        status.SetText(statusType.defence, weapon.status.defence.ToString());
        status.SetText(statusType.criRate, weapon.status.criRate.ToString());
        status.SetText(statusType.criDamage, weapon.status.criDamage.ToString());
    }
    public void TogglePopup()
    {
        if (toggle.isOn) Open();
        else CloseStart();
    }
}
