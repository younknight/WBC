using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MapWorld
{
    fire,
    ice,
    thunder,
    earth
}
public class MapWorldSelector : MonoBehaviour
{
    [SerializeField] MapWorld world;
    public void OpenPopup()
    {
        MapPopupManager.instance.OpenPopup(world);
    }
}
