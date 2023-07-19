using System.Collections;
using System;
using UnityEngine;

public class Opener : MonoBehaviour
{
    static openingChest[] openingChests = new openingChest[16];//
    [SerializeField] Transform slotParent;

    [SerializeField] OpenSlot[] slots;

    public static openingChest[] OpeningChests { get => openingChests; set => openingChests = value; }
    private void OnDestroy()
    {
        openingChests = null;
    }
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
            slots[i].Id = i;
            slots[i].RemoveSlot();
            slots[i].Opener = this;

            if(openingChests[i] != null)
            {
                slots[i].SetChest(GameManager.instance.ChestDatas[openingChests[i].id], CalculaterDate(openingChests[i].openTime));
            }
        }
    }
    float CalculaterDate(DateTime date)
    {
        string duration = (date - DateTime.Now).Duration().ToString().Substring(0, 8);
        string[] time = duration.Split(":");
        int totalTime = (Convert.ToInt32(time[0]) * 60 * 60) + (Convert.ToInt32(time[1]) * 60) + (Convert.ToInt32(time[2]));
        //Debug.Log(duration + "/" + time[0] + "/"  + time[1] + "/" + time[2] + "/" + totalTime);
        return (totalTime);
    }
}
