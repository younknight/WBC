using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Resource
{
    public int level;
    public int gold;
    public int primo;

    public Resource(int level, int gold, int primo)
    {
        this.level = level;
        this.gold = gold;
        this.primo = primo;
    }
}
public class ResourseManager : MonoBehaviour
{
    #region ½Ì±ÛÅæ
    static ResourseManager instance;
    public static ResourseManager Instance { get => instance; set => instance = value; }
    public Resource Resource { get => resource; set => resource = value; }

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }
    #endregion
    private void Start()
    {
        TextManager.instance.SetText();
    }
    Resource resource = new Resource(1, 0, 0);
    public int GetLevel() { return resource.level; }
    public int GetGold()  {  return resource.gold; }
    public int GetPrimo() {  return resource.primo; }
    public void SetGold(int value)  { resource.gold = value; }
    public void SetPrimo(int value) { resource.primo = value; }
    public void LevelUp()
    {
        resource.level++;
        TextManager.instance.SetText();
        DataManager.instance.JsonSave();
    }
    public void Purchase(bool isPurchase, int cost)
    {
        resource.gold += isPurchase ? -cost : cost;
        TextManager.instance.SetText();
        DataManager.instance.JsonSave();
    }
    public void PurchaseWithPrimo(bool isPurchase, int cost)
    {
        resource.primo += isPurchase ? -cost : cost;
        TextManager.instance.SetText();
        DataManager.instance.JsonSave();
    }
}
