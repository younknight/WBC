
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DropItem
{
    public Item drop;
    public int maxDrop;
    public int minDrop;
    public int percent;
}

[CreateAssetMenu]

public class Chest : Item
{
    public float openTime;
    public Sprite chestOpenImage;

    [Space(10f)]
    [Header("Recipe")]
    public List<Recipe> recipes;

    [Space(10f)]
    [Tooltip("각 확률은 독립적으로 계산")]
    [Header("DropItem")]
    public List<DropItem> dropItems;

    public int GetRandomCount(Item item)
    {
        DropItem drop = dropItems.Find(x => x.drop == item);
        return UnityEngine.Random.Range(drop.minDrop, drop.maxDrop + 1);
    }
    public List<Item> GetRandomDropItems()
    {
        List<Item> returnValue = new List<Item>();
        for (int i=0; i < dropItems.Count; i++)
        {
            int percent = UnityEngine.Random.Range(1, 100 + 1);
            if (percent <= dropItems[i].percent)
            {
                returnValue.Add(dropItems[i].drop);
            }
        }
        return returnValue;
    }
    public Dictionary<Item, int> GetRandomDropItemsWithCount()
    {
        List<Item> newItems = GetRandomDropItems();
        Dictionary<Item, int> drops = new Dictionary<Item, int>();
        foreach (Item newItem in newItems)
        {
            int count = GetRandomCount(newItem);
            drops.Add(newItem, count);
        }
        return drops;
    }
}
