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
    [SerializeField] GameObject btn;
    [SerializeField] DropPopup dropPopup;
    static ExplainPopup instance;
    public static ExplainPopup Instance { get => instance; set => instance = value; }
    private void OnDestroy() { Instance = null; }
    void Awake() { if (Instance == null) Instance = this; }
    public void SetExplain(IInformation item)
    {
        btn.gameObject.SetActive(false);
        dropPopup.gameObject.SetActive(false);
        idText.text = "No." + item.GetId();
        nameText.text = item.GetName();
        explainText.text = item.GetExplain();
        image.sprite = item.GetSprite();
        rankingText.text = item.GetRanking();
        priceText.text = item.GetSellPrice().ToString();
        levelText.transform.parent.gameObject.SetActive(false); 
        string type = InfoManager.CheckInterfaceType(item);
        if (type == "weapon")
        {
            levelText.transform.parent.gameObject.SetActive(true);
            levelText.text = "Lv." + Inventory.Weapons.Find(x => x.weapon.id == item.GetId()).level;
        }
        if (type == "chest")
        {
            nameText.text += " »óÀÚ";
            btn.gameObject.SetActive(true);
            List<IInformation> drops = new List<IInformation>();
            Chest chest = InfoManager.GetCharacter<Chest>(item);
            dropPopup.Setup(chest.Drops);
        }
    }
}
