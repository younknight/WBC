using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusPopup : Popup
{
    [SerializeField] TextMeshProUGUI hp;
    [SerializeField] TextMeshProUGUI attack;
    [SerializeField] TextMeshProUGUI defence;
    [SerializeField] TextMeshProUGUI criDamage;
    [SerializeField] TextMeshProUGUI criRate;
    static StatusPopup instance;
    public static StatusPopup Instance { get => instance; set => instance = value; }
    private void OnDestroy() { Instance = null; }
    void Awake() { if (Instance == null) Instance = this; }
    public void OpenPopup()
    {
        SetStatus(EquipmentManager.instance.Unit);
        Open();
    }
    public void SetStatus(Unit unit)
    {
        hp.text = "" + unit.GetStatus(statusType.maxHp);
        attack.text = "" + unit.GetStatus(statusType.attack);
        defence.text = "" + unit.GetStatus(statusType.defence);
        criDamage.text = "" + unit.GetStatus(statusType.criDamage);
        criRate.text = "" + unit.GetStatus(statusType.criRate);
        //if (player != null)
        //{
        //    hp.text += "(+" + equipmentManager.Equipment.GetDifferce(statusType.maxHp, player.Unit.GetStatus(statusType.maxHp)) + ")";
        //    attack.text += "(+" + equipmentManager.Equipment.GetDifferce(statusType.attack, player.Unit.GetStatus(statusType.attack)) + ")";
        //    defence.text += "(+" + equipmentManager.Equipment.GetDifferce(statusType.defence, player.Unit.GetStatus(statusType.defence)) + ")";
        //    criDamage.text += "(+" + equipmentManager.Equipment.GetDifferce(statusType.criDamage, player.Unit.GetStatus(statusType.criDamage)) + ")";
        //    criRate.text += "(+" + equipmentManager.Equipment.GetDifferce(statusType.criRate, player.Unit.GetStatus(statusType.criRate)) + ")";
        //}
    }
}
