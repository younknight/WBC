using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class TmpAlpha : MonoBehaviour
{
    [SerializeField] float lerpTime = 0.5f;
    [SerializeField] Image[] Image;
    IEnumerator coroutine;
    public void FadeOut(int index)
    {
        if(index >= 0)
        {
            coroutine = AlphaLerp(index);
            StartCoroutine(coroutine);
        }
    }
    public void StopFadeOut()
    {
        if(coroutine != null) StopCoroutine(coroutine);
    }
    IEnumerator AlphaLerp(int index)
    {
        float currentTIme = 0.0f;
        float percent = 0.0f;
        float start = 1;
        float end = 0;
        while (true)
        {
            currentTIme += Time.deltaTime;
            percent = currentTIme / lerpTime;
            Color color = Image[index].color;
            color.a = Mathf.Lerp(start, end, percent);
            Image[index].color = color;
            if(percent >= 1)
            {
                percent = 0;
                currentTIme = 0;
                float tmp;
                tmp = start;
                start = end;
                end = tmp;
            }
            yield return null;
        }
    }
}
