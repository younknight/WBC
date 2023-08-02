using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SellSlider : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] Slider slider;
    int maxItemCount;
    int currentCount;
    int sellPrice;

    public int CurrentCount { get => currentCount; set => currentCount = value; }

    public void Setup(int itemCount, int sellPrice)
    {
        this.sellPrice = sellPrice;
        maxItemCount = itemCount;
        currentCount = 0;
        SetUI();
    }
    public void Carcluate()
    {
        if (maxItemCount == 0)
        {
            slider.value = 0;
            return;
        }
        float value = slider.value;
        float interval = 1 / (float)maxItemCount; //any interval you want to round to
        value = Mathf.Round(value / interval) * interval;
        slider.value = value;
        currentCount = (int)(value * (float)maxItemCount);
        SetText();
    }
    void SetUI()
    {
        SetText();
        if (maxItemCount == 0)
        {
            slider.value = 0;
            return;
        }
        slider.value = (float)currentCount / (float)maxItemCount;
    }
    void SetText()
    {
        countText.text = currentCount + "/" + maxItemCount;
        goldText.text = (currentCount * sellPrice) + "g";
    }
    public void PlusCount(bool isPlus)
    {
        if (isPlus && currentCount < maxItemCount) currentCount++;
        if (!isPlus && currentCount > 0) currentCount--;
        SetUI();
    }
    public int GetTotalPrice()
    {
        return currentCount * sellPrice;
    }
}
