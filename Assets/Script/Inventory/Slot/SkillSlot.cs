using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillSlot : MonoBehaviour
{
    [SerializeField] CircleTimer circleTimer;
    [SerializeField] SkillManager skillManager;
    [SerializeField] Weapon weapon;

    private void Start()
    {
        weapon = GetComponent<EquipmentSlot>().Weapon;
        if (weapon == null)
        {
            Destroy(GetComponent<Button>());
        }
        circleTimer = GetComponent<CircleTimer>();
    }
    public void UseSkill()
    {
        //if(weapon.Weapon != null)
        //{
        skillManager.UseSkill(weapon);
        circleTimer.TimerStart(weapon.coolTime);
        //}
    }
}