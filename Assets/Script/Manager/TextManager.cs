using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] StaminaGauge staminaText;
    [SerializeField] TextMeshProUGUI primoText;
    public static TextManager instance;

    private void OnDestroy() { instance = null; }
    private void Awake()
    {
        if (instance == null) instance = this;
        goldText = GameObject.Find("playerGoldText").GetComponent<TextMeshProUGUI>();
        primoText = GameObject.Find("primoText").GetComponent<TextMeshProUGUI>();
    }
    public void SetText()
    {
        SetGold();
        SetPrim();
    }
    void SetGold() { if(goldText != null) goldText.text = ResourseManager.Instance.GetGold().ToString(); }
            //staminaText.text = StaminaManager.Instance.StaminaData.currentStamina.ToString() + "/5"; }
    void SetPrim() { if (primoText != null) primoText.text = ResourseManager.Instance.GetPrimo().ToString(); }

}
