
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
    [Tooltip("각 확률은 독립적으로 계산")]
    [Header("DropItem")]
    public List<DropItem<Item>> dropItems;
    public List<DropItem<Weapon>> dropWeapons;
    #region Getter
    public int GetId() { return id; }
    public string GetName() { return chestName; }
    public string GetExplain() { return chestExplain; }
    public string GetRanking() { return ranking; }
    public Sprite GetSprite() { return chetImage; }
    public List<DropItem<IInformation>> Drops { get => drops; set => drops = value; }
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
    public List<IInformation> GetRandomDrop()
    {
        List<IInformation> returnValue = new List<IInformation>();
        for (int i=0; i < drops.Count; i++)
        {
            int percent = UnityEngine.Random.Range(1, 100 + 1);
            if (percent <= drops[i].percent)
            {
                returnValue.Add(drops[i].drop);
            }
        }
        return returnValue;
    }
}
