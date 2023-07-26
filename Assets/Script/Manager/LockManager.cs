using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockManager : MonoBehaviour
{
    static LockInfo lockInfo;

    public static LockInfo LockInfo { get => lockInfo; set => lockInfo = value; }

    private void OnDestroy()
    {
        lockInfo = null;
    }
}
