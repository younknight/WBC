using System.Collections;
using System;
using UnityEngine;

public class AutoCrafter : MonoBehaviour
{
    static OpeningChest[] autoMakingChest = new OpeningChest[10];
    private void OnDestroy()
    {
        autoMakingChest = null;
    }
    public void SetMakingChest(Chest chest,int id,int count)
    {
        autoMakingChest[id] = new OpeningChest(chest.id, count, DateTime.Now);
    }
}
