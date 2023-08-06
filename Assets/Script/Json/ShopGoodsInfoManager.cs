using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class GoodsInfo
{
    public int maxRerollcount = 3;
    public int rerollCount;
    public Dictionary<goodsType, List<Goods>> goodsDic = new Dictionary<goodsType, List<Goods>>();
    public bool CanReroll() { return rerollCount < maxRerollcount; }
    public void AddRerollCount() { rerollCount++; }
    public void ResetCount() { rerollCount = 0; }
    public int GetCount() { return rerollCount; }
}
public class ShopGoodsInfoManager : MonoBehaviour
{
    JsonParser jsonParser = new JsonParser();
    [SerializeField] List<GoodsManager> managers;
    GoodsInfo goodsInfo = new GoodsInfo();

    string path;

    public GoodsInfo GoodsInfo { get => goodsInfo; set => goodsInfo = value; }

    private void Awake()
    {
        path = Path.Combine(Application.dataPath, "Goods.json");
        for (int i = 0; i < managers.Count; i++)
        {
            goodsInfo.goodsDic.Add(managers[i].GoodsType, null);
        }
        Load();
    }
    public void Load()
    {
        goodsInfo = jsonParser.LoadJson<GoodsInfo>(path);
    }
    public void Save()
    {
        for(int i = 0; i< managers.Count; i++)
        {
            goodsInfo.goodsDic[managers[i].GoodsType] = managers[i].GetGoodsId();
        }
        jsonParser.SaveJson<GoodsInfo>(goodsInfo, path);
    }
}
