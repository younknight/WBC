using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CircleTimer : MonoBehaviour
{
    bool isEnd = true;
    float currentValue;//
    [SerializeField] Image LoadingBar;//
    [SerializeField] float coolTimeValue;//

    public float CoolTimeValue { get => coolTimeValue; set => coolTimeValue = value; }
    public bool IsEnd { get => isEnd; set => isEnd = value; }

    private void Start()
    {
        LoadingBar.gameObject.SetActive(false);
    }
    void TimerStart()
    {
        LoadingBar.gameObject.SetActive(true);
        IsEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsEnd)
        {
            if (currentValue < coolTimeValue)
            {
                currentValue += Time.deltaTime;
            }
            else
            {
                IsEnd = true;
                LoadingBar.gameObject.SetActive(false);
            }

            LoadingBar.fillAmount = (coolTimeValue - currentValue) / 100;
        }
    }
}
