using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipePopup : Popup
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI rankingText;
    [SerializeField] List<Slot> resources;
    [SerializeField] GameObject weirdRecipe;
    static RecipePopup instance;
    public static RecipePopup Instance { get => instance; set => instance = value; }
    private void OnDestroy() { Instance = null; }
    void Awake() { if (Instance == null) Instance = this; }
    public void SetRecipe(Chest chest)
    {
        nameText.text = chest.chestName + "상자";
        rankingText.text = chest.ranking;
        image.sprite = chest.chetImage;
        weirdRecipe.SetActive(false);
        if (chest.id == 0)
        {
            weirdRecipe.SetActive(true);
            return;
        }
        for (int i = 0; i < chest.recipes.Count; i++)//만일 제조법이 여러개라면
        {
            int index = 0;
            for (; index < chest.recipes[i].items.Count; index++)
            {
                resources[index].FreshSlot(false);
                Debug.Log(chest.recipes[i].items[index]);
                resources[index].NewAddItemInfo<Item>(chest.recipes[i].items[index], 0);
            }
            for (; index < resources.Count; index++)
            {
                resources[index].FreshSlot(true);
            }
        }
    }
}
