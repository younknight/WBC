
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DropItem<T>
{
    public T dropItems;
    public int maxDrop;
    public int minDrop;
    public int percent;
}

[CreateAssetMenu]
public class Chest : ScriptableObject
{
    [Header ("Information")]
    public int id;
    public string chestName;
    public int price;
    [Multiline(5)]
    public string chestExplain;
    public string ranking;
    public float openTime;

    [Space(10f)]
    [Header("Sprite")]
    public Sprite chetImage;

    [Space(10f)]
    [Header("Recipe")]
    public List<Recipe> recipes;

    [Space(10f)]
    [Header("DropItem")]
    public List<DropItem<Item>> dropItems;
    public List<DropItem<Weapon>> dropWeapons;
    private void OnValidate()
    {
        string[] idNum = name.Split('.');
        chestName = idNum[1];
        id = Convert.ToInt32(idNum[0]);
    }
}
