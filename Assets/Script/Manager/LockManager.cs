using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockManager : MonoBehaviour
{
    static LockInfo lockInfo;
    static LockManager instance;
    public static LockInfo LockInfo { get => lockInfo; set => lockInfo = value; }
    public static LockManager Instance { get => instance; set => instance = value; }
    #region 아이템 최대치
    //초기 값
    int minMaxCraftCount = 5;
    int maxCraftCoolTime = 20;
    int minMaxOpenerCount = 2;
    //최대 값
    int maxMaxCraftCount = 20;
    int minCraftCoolTime = 1;
    int maxMaxOpenerCount = 16;
    #endregion

    private void OnDestroy()
    {
        lockInfo = null;
        instance = null;
    }
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    public bool GetLastLevel(lockType LockType)
    {
        switch (LockType)
        {
            case lockType.maxCraftCounter:
                return LockInfo.maxCraftCount >= maxMaxCraftCount;
            case lockType.craftCoolTime:
                return LockInfo.craftCoolTime <= minCraftCoolTime;
            case lockType.openSlotCount:
                return LockInfo.maxOpenerCount >= maxMaxOpenerCount;
        }
        return false;
    }
    public int GetLevel(lockType LockType)
    {
        switch (LockType)
        {
            case lockType.maxCraftCounter:
                return (int)(LockInfo.maxCraftCount - minMaxCraftCount + 1);
            case lockType.craftCoolTime:
                return (int)(maxCraftCoolTime - LockInfo.craftCoolTime + 1);
            case lockType.openSlotCount:
                return (int)(LockInfo.maxOpenerCount - minMaxOpenerCount + 1);
        }
        return 0;
    }
}
