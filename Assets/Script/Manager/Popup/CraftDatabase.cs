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
    [SerializeField] ItemDatabaseManager itemDatabaseManager;
    [SerializeField] List<Recipe> SettingRecipes = new List<Recipe>();
    List<craftRecipe> recipes = new List<craftRecipe>();
    List<List<int>> weirdRecipe = new List<List<int>>();
    public List<List<int>> WeirdRecipe { get => weirdRecipe; set => weirdRecipe = value; }

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
    #region 레시피 체크하여 확인
    public Chest CheckRecipe(List<int> grid)
    {
        for (int i = 0; i < recipes.Count; i++)
        {
            if (Match(grid, recipes[i].recipe)) return recipes[i].chest;
        }
        return GameManager.instance.ChestDatas[0];//이상한상자
    }
    public bool isNewWeirdChest(List<int> grid)
    {
        for (int i = 0; i < weirdRecipe.Count; i++)
        {
            bool flag = Match(weirdRecipe[i], grid);
            if (flag) return false;
        }
        return true;
    }
    public void AddWierd(List<int> grid)
    {
        List<int> newRecipe = new List<int>();
        newRecipe = grid.ToList();
        weirdRecipe.Add(newRecipe);
    }
    bool Match(List<int> grid, List<int> recipe)
    {
        if (grid.Count != recipe.Count) return false;
        for (int i = 0; i < grid.Count; i++)
        {
            if (grid[i] != recipe[i]) return false;
        }
        return true;
    }
    #endregion
    public bool CheckResource(Chest chest, int count)
    {
        Dictionary<Ingredient, int> resources = new Dictionary<Ingredient, int>();
        //아이템 삽입
        for (int i = 0; i < chest.recipes[0].items.Count; i++)
        {
            Ingredient item = chest.recipes[0].items[i];
            if (!resources.ContainsKey(item)) resources.Add(item, 1);
            else resources[item]++;
        }
        //아이템 재고 체크
        foreach (KeyValuePair<Ingredient, int> entry in resources)
        {
            if (itemDatabaseManager.FIndItemWithId(entry.Key.id, inventoryType.ingrediant).num < (entry.Value * count)) return false;
        }
        return true;
    }
}
