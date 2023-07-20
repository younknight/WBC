
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
public class Chest : ScriptableObject, IInformation
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



    #region Getter
    public int GetId() { return id; }
    public string GetName() { return chestName; }
    public string GetExplain() { return chestExplain; }
    public string GetRanking() { return ranking; }
    public Sprite GetSprite() { return chetImage; }
    #endregion
    private void OnValidate()
    {
        string[] idNum = name.Split('.');
        chestName = idNum[1];
        id = Convert.ToInt32(idNum[0]);
    }
}
