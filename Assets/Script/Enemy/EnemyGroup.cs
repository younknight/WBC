using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    [SerializeField] Transform center;
    [SerializeField] List<Transform> outLine;

    [SerializeField] List<Transform> randomSpawnPoints = new List<Transform>();//



    void Awake()
    {
        while(outLine.Count > 0)
        {
            int i = Random.Range(0, outLine.Count);
            randomSpawnPoints.Add(outLine[i]);
            outLine.RemoveAt(i);
        }
        randomSpawnPoints.Insert(0,center);
    }
    public Transform GetRandomPoint()
    {
        Transform point = randomSpawnPoints[0];
        randomSpawnPoints.RemoveAt(0);
        return point;
    }
}
