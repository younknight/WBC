using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] EquipmentManager equipmentManager;
    private void Start()
    {
        equipmentManager.FreshSlot();
        equipmentManager.Equipment.ResetStatus();
    }
}
