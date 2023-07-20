using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu]
public class Item : ScriptableObject, IInformation
{
    public int id;
    public string itemName;
    public int price;
    [Multiline(5)]
    public string itemExplain;
    public string ranking;
    public Sprite itemImage;

    #region Getter
    public int GetId() { return id; }
    public string GetName() { return itemName; }
    public string GetExplain() { return itemExplain; }
    public string GetRanking() { return ranking; }
    public Sprite GetSprite() { return itemImage; }
    #endregion
    private void OnValidate()
    {
        string[] idNum = name.Split('.');
        itemName = idNum[1];
        id = Convert.ToInt32(idNum[0]);
    } 
}