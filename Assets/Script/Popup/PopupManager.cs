using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager instance;
    [SerializeField] Transform popupsParents;
    [SerializeField] Popup[] settingPopups;
    //[SerializeField] List<Popup> settingPopups;
    Dictionary<popupType, Popup> popups = new Dictionary<popupType, Popup>();

    public Dictionary<popupType, Popup> Popups { get => popups; set => popups = value; }
    private void OnDestroy()
    {
        instance = null;
    }
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) instance = this;
        settingPopups = popupsParents.GetComponentsInChildren<Popup>();
        for(int i = 0; i < settingPopups.Length; i++)
        {
            popups.Add(settingPopups[i].PopupType, settingPopups[i]);
        }
    }
    public void CloesPopup(popupType popupType)
    {
        popups[popupType].CloseStart();
    }
    public void OpenPopup(popupType popupType)
    {
        popups[popupType].Open();
    }
    //±¸¸Å ÆË¾÷
    public void OpenPurchasePopup(GoodsSlot goodsSlot,string name, string ranking, Sprite sprite, int price, int count, int id)
    {
        popups[popupType.purchase].SetPurchase(goodsSlot,name, ranking, sprite, price, count,id);
        OpenPopup(popupType.purchase);
    }
    //½ºÅÝ ÆË¾÷
    public void OpenStatusPopup()
    {
        popups[popupType.status].SetStatus(EquipmentManager.instance.Unit);
        OpenPopup(popupType.status);
    }
    //¼³Á¤ ÆË¾÷
    public void OpenSettingPopup()
    {
        OpenPopup(popupType.setting);
    }
    //»óÀÚ ÀÎº¥Åä¸® ÆË¾÷
    public void OpenChestPopup()
    {
        OpenPopup(popupType.chest);
    }
    //¿þÆù ÀÎº¥Åä¸® ÆË¾÷
    public void OpenWeaponPopup()
    {
        OpenPopup(popupType.weapon);
    }
    //»óÀÚ ·¹½ÃÇÇ ÆË¾÷
    public void OpenRecipePopup(Chest chest)
    {
        popups[popupType.recipe].SetRecipe(chest);
        OpenPopup(popupType.recipe);
    }
    //¼³¸í ÆË¾÷
    public void OpenExplainPopup(string name, string explain,Sprite sprite, int id, string ranking, int sellPrice)
    {
        popups[popupType.explain].SetExplain(name,explain,sprite,id, ranking, sellPrice);
        OpenPopup(popupType.explain);
    }
    //¾ÆÀÌÅÛ È¹µæ ÆË¾÷
    public void OpenGetItemPopup(string name, string count, Sprite sprite,bool isNew,string ranking)/////////////isNewÈ®ÀÎ
    {
        popups[popupType.getItem].SetGetItem(name,count,sprite,isNew,ranking);
        OpenPopup(popupType.getItem);

    }
}
