using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextManager : MonoBehaviour
{
    [SerializeField] StaminaGauge staminaText;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI primoText;
    [SerializeField] TextMeshProUGUI levelText;
    public static TextManager instance;

    private void OnDestroy() { instance = null; }
    private void Awake()
    {
        if (instance == null) instance = this;
        goldText = GameObject.Find("playerGoldText").GetComponent<TextMeshProUGUI>();
        primoText = GameObject.Find("primoText").GetComponent<TextMeshProUGUI>();
        levelText = GameObject.Find("playerLevelText").GetComponent<TextMeshProUGUI>();
    }
    public void SetText()
    {
        SetLevel();
        SetGold();
        SetPrim();
    }
    void SetLevel() { if (levelText != null) levelText.text = ResourseManager.Instance.GetLevel().ToString(); }
    void SetGold() { if(goldText != null) goldText.text = ResourseManager.Instance.GetGold().ToString(); }
            //staminaText.text = StaminaManager.Instance.StaminaData.currentStamina.ToString() + "/5"; }
    void SetPrim() { if (primoText != null) primoText.text = ResourseManager.Instance.GetPrimo().ToString(); }

}
