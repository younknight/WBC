using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CircleTimer : MonoBehaviour
{
    float currentValue;//
    [SerializeField] Button button;//
    [SerializeField] Image LoadingBar;//
    [SerializeField] float coolTimeValue;//

    public float CoolTimeValue { get => coolTimeValue; set => coolTimeValue = value; }

    private void Start()
    {
        button = GetComponent<Button>();
        LoadingBar.gameObject.SetActive(false);
    }
    public void TimerStart(float value)
    {
        currentValue = 0;
        coolTimeValue = value;
        LoadingBar.gameObject.SetActive(true);
        button.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!button.interactable)
        {
            if (currentValue < coolTimeValue)
            {
                currentValue += Time.deltaTime;
            }
            else
            {
                button.interactable = true;
                LoadingBar.gameObject.SetActive(false);
            }

            LoadingBar.fillAmount = (coolTimeValue - currentValue) / coolTimeValue;
        }
    }
}
