using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu]
public class Item : ScriptableObject, IInformation
{
    [Header("Information")]
    public int id;
    public string itemName;
    public int price;
    public int sellPrice;
    [Multiline(5)]
    public string itemExplain;
    public string ranking;
    [Header("Sprite")]
    public Sprite itemImage;

    #region Getter
    public int GetSellPrice() { return sellPrice; }
    public int GetId() { return id; }
    public string GetName() { return itemName; }
    public string GetExplain() { return itemExplain; }
    public string GetRanking() { return ranking; }
    public Sprite GetSprite() { return itemImage; }
    #endregion
    private void OnValidate()
    {
        string[] nameValue = name.Split('.');
        itemName = nameValue[1];
        id = Convert.ToInt32(nameValue[0]);
    } 
}