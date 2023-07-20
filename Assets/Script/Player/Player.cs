using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] HpManager hpBar;
    [SerializeField] Unit unit;

    public Unit Unit { get => unit; set => unit = value; }


    private void Start()
    {
        if (EquipmentManager.instance != null)
        {
            unit = EquipmentManager.instance.Unit;
        }
        SetUp();
    }
    public void SetUp()
    {
        Debug.Log(unit.gameObject.name);
        hpBar.Setup(unit);
    }
}
