using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeEffect : MonoBehaviour
{
    [SerializeField] GameObject endCursor;
    [SerializeField] float CharPerSeconds;
    string targetMsg;
    TextMeshProUGUI msgText;
    int index;

    private void Awake()
    {
        msgText = GetComponent<TextMeshProUGUI>();
    }
    public void Setup(string msg)
    {
        targetMsg = msg;
        EffectStart();
    }
    void EffectStart()
    {
        endCursor.SetActive(false);
        msgText.text = "";
        index = 0;
        Invoke("Effecting", 1 / CharPerSeconds);
    }
    void Effecting()
    {
        if(msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }
        msgText.text += targetMsg[index++];
        Invoke("Effecting", 1 / CharPerSeconds);
    }
    void EffectEnd()
    {
        endCursor.SetActive(true);

    }
}
