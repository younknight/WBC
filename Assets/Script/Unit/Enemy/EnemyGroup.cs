using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class EnemyGroup : MonoBehaviour
{
    [SerializeField] Transform center;
    [SerializeField] List<Transform> outLine;

    [SerializeField] List<Transform> randomSpawnPoints = new List<Transform>();//


    public void SetRandomPoint()
    {
        List<Transform> copy = outLine.ToList();
        randomSpawnPoints = new List<Transform>();
        while (copy.Count > 0)
        {
            int i = Random.Range(0, copy.Count);
            randomSpawnPoints.Add(copy[i]);
            copy.RemoveAt(i);
        }
        randomSpawnPoints.Insert(0, center);
    }
    public Transform GetRandomPoint()
    {
        Transform point = randomSpawnPoints[0];
        randomSpawnPoints.RemoveAt(0);
        return point;
    }
}
