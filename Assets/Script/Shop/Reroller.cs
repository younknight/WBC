using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Reroller : MonoBehaviour
{
    [SerializeField] Button rerollBtn;
    [SerializeField] TextMeshProUGUI priceText;
    public void SetActive(bool isActive, string price)
    {
        rerollBtn.interactable = isActive;
        priceText.text = price;
    }
}
