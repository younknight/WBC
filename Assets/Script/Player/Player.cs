using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Status status = new Status();
    [SerializeField] EquipmentManager equipment;
    [SerializeField] HpManager hp;
    Unit unit;

    public Unit Unit { get => unit; set => unit = value; }

    // Start is called before the first frame update
    void Start()
    {
        unit = GetComponent<Unit>();
    }
    public void SetUp()
    {
        status = equipment.Equipment.DefaultStatus;
        unit.Setup(status);
        hp.Setup(unit);
    }
}
