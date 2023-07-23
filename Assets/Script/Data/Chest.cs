
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DropItem<T>
{
    public T drop;
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
    public int sellPrice;
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
    [Tooltip("퍼센트는 100퍼 에 맞추고 0부터 시작함")]
    public List<DropItem<Item>> dropItems;
    [Tooltip("무기는 아이템 마지막 퍼센트에서 시작함 마지막은 100")]
    public List<DropItem<Weapon>> dropWeapons;
    #region Getter
    public int GetSellPrice() { return sellPrice; }
    public int GetId() { return id; }
    public string GetName() { return chestName; }
    public string GetExplain() { return chestExplain; }
    public string GetRanking() { return ranking; }
    public Sprite GetSprite() { return chetImage; }
    #endregion
    List<DropItem<IInformation>> drops;
    private void OnValidate()
    {
        string[] nameValue = name.Split('.');
        chestName = nameValue[1];
        id = Convert.ToInt32(nameValue[0]);
        drops = new List<DropItem<IInformation>>();
        for (int i = 0; i < dropItems.Count; i++)
        {
            DropItem<IInformation> newInfo = new DropItem<IInformation>();
            newInfo.drop = dropItems[i].drop;
            newInfo.maxDrop = dropItems[i].maxDrop;
            newInfo.minDrop = dropItems[i].minDrop;
            newInfo.percent = dropItems[i].percent;
            drops.Add(newInfo);
        }
        for (int i = 0; i < dropWeapons.Count; i++)
        {
            DropItem<IInformation> newInfo = new DropItem<IInformation>();
            newInfo.drop = dropWeapons[i].drop;
            newInfo.maxDrop = dropWeapons[i].maxDrop;
            newInfo.minDrop = dropWeapons[i].minDrop;
            newInfo.percent = dropWeapons[i].percent;
            drops.Add(newInfo);
        }
    }
    public int GetRandomCount(IInformation item)
    {
        DropItem<IInformation> drop = drops.Find(x => x.drop == item);
        return UnityEngine.Random.Range(drop.minDrop, drop.maxDrop + 1);
    }
    public IInformation GetRandomDrop()
    {
        int min = 0;
        int percent = UnityEngine.Random.Range(1, 100 + 1);
        IInformation returnValue = null;
        int i = 0;
        for (; i < drops.Count; i++)
        {
            if (i != 0) min = drops[i - 1].percent;
            if (min < percent && percent <= drops[i].percent)
            {
                returnValue = drops[i].drop;
            }
        }
        return returnValue;
    }
}
