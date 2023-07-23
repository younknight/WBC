using System.Collections;
using UnityEngine;

public class Opener : MonoBehaviour
{
    TimeManager timeManager = new TimeManager();
    static OpeningChest[] openingChests = new OpeningChest[16];//
    [SerializeField] Transform slotParent;

    [SerializeField] OpenSlot[] slots;

    public static OpeningChest[] OpeningChests { get => openingChests; set => openingChests = value; }
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
                slots[i].SetChest(GameManager.instance.ChestDatas[openingChests[i].chestId], timeManager.CalculaterDate(openingChests[i].openTime));
            }
        }
    }
}
