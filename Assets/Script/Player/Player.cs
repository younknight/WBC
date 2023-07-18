using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Status status = new Status();
    // Start is called before the first frame update
    void Start()
    {
       // status = EquipmentManager.instance.Equipment.DefaultStatus;
    }
}
