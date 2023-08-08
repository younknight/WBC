using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] HpManager hpBar;
    [SerializeField] Unit unit;
    TargetCursor targetCursor;
    Attacker attacker;
    public Unit Unit { get => unit; set => unit = value; }
    public Attacker Attacker { get => attacker; set => attacker = value; }
    public TargetCursor TargetCursor { get => targetCursor; set => targetCursor = value; }

    private void Start()
    {
        if (EquipmentManager.instance != null)
        {
            unit = EquipmentManager.instance.Unit;
            targetCursor = GetComponent<TargetCursor>();
            attacker = GetComponent<Attacker>();
            attacker.UnitSetup(EquipmentManager.instance.Unit);
        }
        SetUp();
    }
    public void SetUp()
    {
        hpBar.Setup(unit);
    }
}
