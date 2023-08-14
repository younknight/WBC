using System.Collections;
using System;
using UnityEngine;


public class Item : ScriptableObject
{
    [Header("Information")]
    public int id;
    public string itemName;
    public int price;
    [Multiline(5)]
    public string itemExplain;
    public string ranking;
    [Header("Sprite")]
    public Sprite itemImage;

    private void OnValidate()
    {
        string[] nameValue = name.Split('.');
        itemName = nameValue[1];
        id = Convert.ToInt32(nameValue[0]);
    }
}
