using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
public class StaminaGauge : MonoBehaviour
{
    [SerializeField] List<Image> lanterns;
    [SerializeField] TextMeshProUGUI countText;
    int count;
    private void Start()
    {
        Setup(StaminaManager.Instance.StaminaData.currentStamina);
    }
    public void Setup(int count)
    {
        this.count = count;
        countText.text = count.ToString();
        for(int i =0; i< lanterns.Count; i++)
        {
            Color color = lanterns[i].color;
            if (i < count) color.a = 1;
            else color.a = 0;
            lanterns[i].color = color;
        }
    }
}
