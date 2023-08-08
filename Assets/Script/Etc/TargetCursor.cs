using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCursor : MonoBehaviour
{


    public float circleR = 1f; //반지름
    public float deg; //각도
    public float objSpeed = 60f; //원운동 속도

    List<Transform> summons = new List<Transform>();
    void Update()
    {
            deg += Time.deltaTime * objSpeed;
            if (deg < 360)
            {
                for (int i = 0; i < summons.Count; i++)
                {
                    var rad = Mathf.Deg2Rad * (deg + (i * (360 / summons.Count)));
                    var x = circleR * Mathf.Sin(rad);
                    var y = circleR * Mathf.Cos(rad);
                    summons[i].transform.position = transform.position + new Vector3(x, y);
                }

            }
            else
            {
                deg = 0;
            }
    }
    public void AddSummons(Transform transform)
    {
        summons.Add(transform);
        ResetDeg();
    }
    public void DeleteSummon(Transform transform)
    {
        summons.Remove(transform);
        ResetDeg();
    }
    void ResetDeg()
    {
        deg = 0;
        for (int i = 0; i < summons.Count; i++)
        {
            var rad = Mathf.Deg2Rad * (deg + (i * (360 / summons.Count)));
            var x = circleR * Mathf.Sin(rad);
            var y = circleR * Mathf.Cos(rad);
            summons[i].transform.position = transform.position + new Vector3(x, y);
        }
    }
}