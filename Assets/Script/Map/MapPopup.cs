using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MapPopup : Popup
{
    public MapWorld world;
    public Transform slotsParent;
    public TextMeshProUGUI worldName;
    [SerializeField] MapSlot[] slots;//
    private void OnValidate()
    {
        if(slotsParent!= null)
        {
            slots = slotsParent.GetComponentsInChildren<MapSlot>();
        }
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].World = world;
            slots[i].Id = i;
        }
    }
    public void Setup(string name)
    {
        worldName.text = name;
    }
}
