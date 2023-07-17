using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    public static TextManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        SetGold();
    }
    public void SetGold()
    {
        goldText.text = GameManager.Gold.ToString();
    }
}
