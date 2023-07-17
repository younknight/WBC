using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Image fill;
    Slider slTimer;
    Animator animator;
    public bool canOpen = false;
    float fSliderBarTime;

    public Animator Animator { get => animator; set => animator = value; }

    void Start()
    {
        slTimer = GetComponent<Slider>();
    }

    public void StartTimer(float time)
    {
        canOpen = false;
        slTimer.maxValue = time;
        slTimer.value = 0;
        StartCoroutine("startSlider");
    }
    IEnumerator startSlider()
    {
        fill.color = new Color(0.7f,0.7f,0.7f,1f);
        while (slTimer.value <  slTimer.maxValue)
        {
            slTimer.value += Time.deltaTime;
            yield return null;
            if (slTimer.value >= slTimer.maxValue)
            {
                Animator.SetBool("ready", true);
                fill.color = Color.green;
                canOpen = true;
            }
        }
    }
}
