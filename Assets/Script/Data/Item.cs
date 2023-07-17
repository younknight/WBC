using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public int price;
    [Multiline(5)]
    public string itemExplain;
    public string ranking;
    public Sprite itemImage;
    private void OnValidate()
    {
        string[] idNum = name.Split('.');
        itemName = idNum[1];
        id = Convert.ToInt32(idNum[0]);
    } 
}