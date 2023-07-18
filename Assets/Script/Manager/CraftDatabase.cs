using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[System.Serializable]
public struct craftRecipe
{
    public List<int> recipe;
    public Chest chest;

    public craftRecipe(List<int> recipe, Chest chest)
    {
        this.recipe = recipe;
        this.chest = chest;
    }
}
public class CraftDatabase : MonoBehaviour
{
    public static CraftDatabase instance;

    [SerializeField] List<Recipe> SettingRecipes = new List<Recipe>();
    List<craftRecipe> recipes = new List<craftRecipe>();
    private void OnDestroy()
    {
        instance = null;
    }
    private void Awake()
    {
        List<craftRecipe> newRecipeList = new List<craftRecipe>();
        if (instance == null) instance = this;
        for (int i = 0; i < SettingRecipes.Count; i++)
        {
            craftRecipe newRecipe = new craftRecipe(SettingRecipes[i].recipe, SettingRecipes[i].dropChest);
            newRecipeList.Add(newRecipe);
        }
        recipes = newRecipeList.Distinct().ToList();
    }
    public void ShowDIc()
    {

        for (int i = 0; i < recipes.Count; i++)
        {
            Debug.Log(i + "===========");
            for(int j = 0; j < recipes[i].recipe.Count; j++)
            {
                Debug.Log(SettingRecipes[i].recipe[j]);
            }
        }
    }
    public Chest CheckRecipe(List<int> grid)
    {
        for(int i = 0; i< recipes.Count; i++)
        {
            if (Match(grid, recipes[i].recipe)) return recipes[i].chest;
        }
        return null;
    }
    bool Match(List<int> grid, List<int> recipe)
    {
        if (grid.Count != recipe.Count) return false;
        for(int i=0;i< grid.Count; i++)
        {
            if (grid[i] != recipe[i]) return false;
        }
        return true;
    }
}
