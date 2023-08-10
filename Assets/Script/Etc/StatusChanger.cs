using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StatusChanger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hp;
    [SerializeField] TextMeshProUGUI attack;
    [SerializeField] TextMeshProUGUI attackSpeed;
    [SerializeField] TextMeshProUGUI defence;
    [SerializeField] TextMeshProUGUI criRate;
    [SerializeField] TextMeshProUGUI criDamage;
    Dictionary<statusType, TextMeshProUGUI> statusText = new Dictionary<statusType, TextMeshProUGUI>();
    void Awake() { SetDic(); }
    void SetDic()
    {
        statusText.Add(statusType.maxHp, hp);
        statusText.Add(statusType.attack, attack);
        statusText.Add(statusType.attackSpeed, attackSpeed);
        statusText.Add(statusType.defence, defence);
        statusText.Add(statusType.criRate, criRate);
        statusText.Add(statusType.criDamage, criDamage);
    }
    public void SetText(statusType status, string value) { statusText[status].text = value; }
}
