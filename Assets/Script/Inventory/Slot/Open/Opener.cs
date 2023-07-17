using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opener : MonoBehaviour
{

    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private OpenSlot[] slots;

    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<OpenSlot>();
    }
    void Start()
    {
        FreshSlot();
    }

    public void FreshSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
    }
}
