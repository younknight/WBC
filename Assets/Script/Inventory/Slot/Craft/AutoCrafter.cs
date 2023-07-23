using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AutoCrafter : MonoBehaviour
{
    #region ½Ì±Û¹Z
    static AutoCrafter instance;
    public static AutoCrafter Instance { get => instance; set => instance = value; }
    public static AutoCraftMaxCounter AutoCounter { get => autoCounter; set => autoCounter = value; }
    public AutoCraftSlot Slot { get => slot; set => slot = value; }

    private void OnDestroy()
    {
        instance = null;
        autoCounter = null;
    }
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    #endregion
    TimeManager timeManager = new TimeManager();
    static AutoCraftMaxCounter autoCounter;
    [SerializeField] AutoCraftSlot slot;
    [SerializeField] Timer timer;
    [SerializeField] Slider gaugeBar;
    [SerializeField] Slider maxGaugeBar;
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] TextMeshProUGUI maxCraftCountText;
    int count = 0;
    int maxCount = 20;
    public void OpenPopup()
    {
        timer.SetReapeatTimer(autoCounter.coolTime, timeManager.CalculaterDate(autoCounter.lastTime));
        FreshCount();
    }
    public void FreshCount()
    {
        maxCraftCountText.text = autoCounter.maxCount.ToString();
        count = 0;
        countText.text = count.ToString();
        maxCraftCountText.text = autoCounter.currentCount.ToString();
        gaugeBar.value = 0;
        maxGaugeBar.value = (float)autoCounter.currentCount / (float)maxCount;
    }
    public void PlusCount(bool isPlus)
    {
        if(slot.Chest != null)
        {
            if (isPlus)
            {
                if (CraftDatabase.instance.CheckResource(slot.Chest, count + 1) && count < maxCount && count < autoCounter.currentCount)
                {
                    count++;
                }
            }
            else
            {
                if (count > 0) count--;
            }
        }
        countText.text = count.ToString();
        gaugeBar.value = (float)(count) / (float)(maxCount);
    }
    public void MakeChest()
    {
        if(count > 0)
        {

            DateTime date = autoCounter.currentCount == autoCounter.maxCount ? DateTime.Now : autoCounter.lastTime;
            autoCounter = new AutoCraftMaxCounter(autoCounter.coolTime, autoCounter.maxCount, autoCounter.currentCount - count, date);
            for (int j = 0; j < slot.Chest.recipes[0].items.Count; j++)
            {
                InventoryManager.instance.DropItems<Item>(slot.Chest.recipes[0].items[j], count);
            }
            InventoryManager.instance.AddItems<Chest>(slot.Chest, count);
            GetItemPopup.Instance.SetGetItem(slot.Chest.chestName + " »óÀÚ", "x" + count, slot.Chest.chetImage, false, slot.Chest.ranking);
            GetItemPopup.Instance.Open();
            OpenPopup();
        }
    }

}
