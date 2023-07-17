using System.Collections.Generic;
using UnityEngine;

using System;
[CreateAssetMenu]
public class Recipe : ScriptableObject
{
    public List<int> recipe;
    public List<Item> items;
    public Chest dropChest;
    private void OnValidate()
    {
        SortRecipe();
    }
    void CheckRecipe()
    {
        if(dropChest != null)
        {
            string[] idNum = name.Split('.');
            if(dropChest.id != Convert.ToInt32(idNum[0]))
            {
                Debug.Log("조합 이상");
            }
        }
    }
    void SortRecipe()
    {
        if (items.Count != 0)
        {
            List<int> newRecipe = new List<int>();
            for (int i = 0; i < items.Count; i++)
            {
                newRecipe.Add(items[i].id);
            }
            recipe = newRecipe;
        }
        recipe.Sort();
    }
}
