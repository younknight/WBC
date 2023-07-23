using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Image fill;
    Slider slTimer;
    Animator animator;
    public bool canOpen = false;
    public Animator Animator { get => animator; set => animator = value; }
    Coroutine coroutine;
    void Start()
    {
        slTimer = GetComponent<Slider>();
    }
    public void SetReapeatTimer(float maxValue, float current)
    {
        AutoCraftMaxCounter counter = AutoCrafter.AutoCounter;
        int count = 0;
        while (maxValue < current && counter.currentCount + count < counter.maxCount)
        {
            current -= maxValue;
            count++;
        }
        Debug.Log("w" + count);
        DateTime date = count > 0 ? DateTime.Now : counter.lastTime;
        AutoCrafter.AutoCounter = new AutoCraftMaxCounter(counter.coolTime, counter.maxCount, counter.currentCount + count, date);
        AutoCrafter.Instance.FreshCount();
        if (AutoCrafter.AutoCounter.currentCount < AutoCrafter.AutoCounter.maxCount)
        {
            slTimer.maxValue = maxValue;
            slTimer.value = current;
            if (coroutine != null) StopCoroutine(coroutine);
            coroutine = StartCoroutine(startRepeatSlider());
        }
        else
        {
            slTimer.value = slTimer.maxValue;
            fill.color = Color.green;
        }
    }
    public void StartTimer(float maxValue, float current)
    {
        canOpen = false;
        slTimer.maxValue = maxValue;
        slTimer.value = current;
        if(maxValue <= current)
        {
            CanOpenChest();
        }
        else StartCoroutine(startSlider());
    }
    IEnumerator startSlider()
    {
        fill.color = new Color(0.7f, 0.7f, 0.7f, 1f);
        while (slTimer.value < slTimer.maxValue)
        {
            slTimer.value += Time.deltaTime;
            yield return null;
            if (slTimer.value >= slTimer.maxValue)
            {
                CanOpenChest();
            }
        }
    }
    IEnumerator startRepeatSlider()
    {
        fill.color = new Color(0.7f, 0.7f, 0.7f, 1f);
        while (slTimer.value < slTimer.maxValue)
        {
            slTimer.value += Time.deltaTime;
            yield return null;
            if (slTimer.value >= slTimer.maxValue)
            {
                AutoCraftMaxCounter counter = AutoCrafter.AutoCounter;
                AutoCrafter.AutoCounter = new AutoCraftMaxCounter(counter.coolTime, counter.maxCount, counter.currentCount + 1, DateTime.Now);
                DataManager.instance.JsonSave();
            }
        }
        AutoCrafter.Instance.FreshCount();
        SetReapeatTimer(AutoCrafter.AutoCounter.coolTime, 0);
    }
    void CanOpenChest()
    {
        Animator.SetBool("ready", true);
        fill.color = Color.green;
        canOpen = true;
    }
}
