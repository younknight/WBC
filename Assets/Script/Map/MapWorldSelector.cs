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
    AlphaBtn alphaBtn;
    private void Awake()
    {
        alphaBtn = GetComponent<AlphaBtn>();
    }
    private void Start()
    {
        LockSlot();
    }
    public void OpenPopup()
    {
        MapPopupManager.instance.OpenPopup(world);
    }
    public void LockSlot()
    {
        alphaBtn.ActiveBtn(StoryManager.Instance.StoryData.worldLockProgress >= (int)world);
    }
}
