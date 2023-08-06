using System.Collections;
using UnityEngine;

public class Opener : MonoBehaviour
{
    static Opener instance;
    TimeManager timeManager = new TimeManager();
    static OpeningChest[] openingChests = new OpeningChest[16];//
    [SerializeField] Transform slotParent;

    [SerializeField] OpenSlot[] slots;

    public static OpeningChest[] OpeningChests { get => openingChests; set => openingChests = value; }
    public static Opener Instance { get => instance; set => instance = value; }

    private void OnDestroy()
    {
        openingChests = null;
        instance = null;
    }
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    void Start()
    {
        FreshSlot();
    }
    public void SetUnlock()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].Chest == null) slots[i].RemoveSlot(!(i < LockManager.LockInfo.maxOpenerCount));
        }
    }
    public void FreshSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].Id = i;
            slots[i].RemoveSlot(!(i < LockManager.LockInfo.maxOpenerCount));
            slots[i].Opener = this;

            if(openingChests[i] != null)
            {
                slots[i].SetChest(GameManager.instance.ChestDatas[openingChests[i].chestId], timeManager.CalculaterDate(openingChests[i].openTime));
            }
        }
    }
}
