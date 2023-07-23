using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ExplainPopup : Popup
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI rankingText;
    [SerializeField] TextMeshProUGUI explainText;
    [SerializeField] TextMeshProUGUI idText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI priceText;
    static ExplainPopup instance;
    public static ExplainPopup Instance { get => instance; set => instance = value; }
    private void OnDestroy() { Instance = null; }
    void Awake() { if (Instance == null) Instance = this; }
    public void SetExplain(string type, string name, string explain, Sprite sprite, int id, string ranking, int sellPrice)
    {
        if (id == 0) id = 0;
        idText.text = "No." + id;
        nameText.text = name;
        explainText.text = explain;
        image.sprite = sprite;
        rankingText.text = ranking;
        priceText.text = sellPrice.ToString();
        levelText.transform.parent.gameObject.SetActive(false);
        if (type == "weapon")
        {
            levelText.transform.parent.gameObject.SetActive(true);
            levelText.text = "Lv." + Inventory.Weapons.Find(x => x.weapon.id == id).level;
        }
    }
}
