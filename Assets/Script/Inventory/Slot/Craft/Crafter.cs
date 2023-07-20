using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafter : MonoBehaviour
{
    [SerializeField] ResultSlot slot;
    [SerializeField] Transform slotParent;
    [SerializeField] CraftSlot[] slots;

    public List<int> resourceList = new List<int>();//

    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<CraftSlot>();
    }
    void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].Crafter = this;
        }
        FreshSlot();
    }

    public void FreshSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
    }
    public void AddResource(int id)
    {
        resourceList.Add(id);
        resourceList.Sort();
        CheckRecipe();
    }
    public void DeleteResource(int id)
    {
        resourceList.Remove(id);
        CheckRecipe();
    }
    void CheckRecipe()
    {
        if(resourceList.Count >= 2)
        {
            Chest chest = CraftDatabase.instance.CheckRecipe(resourceList);
            bool isNewWeird = false;
            if (chest.id == 0)
            {
                isNewWeird = CraftDatabase.instance.isNewWeirdChest(resourceList);
                if(isNewWeird) slot.WeirdRecipe = resourceList;
            }
            else slot.WeirdRecipe = null;
            slot.ClearChest();
            slot.SetChest(chest, isNewWeird);
            return;
        }
        slot.ClearChest();
    }
}
