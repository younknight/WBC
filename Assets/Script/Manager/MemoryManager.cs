using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryManager : MonoBehaviour
{

    private void Start()//
    {
        Resources.UnloadUnusedAssets();
    }
}
