using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Nontarget : MonoBehaviour
{
    [SerializeField] GameObject attackRangePrefab;
    [SerializeField] GameObject followArea = null;

    public GameObject FollowArea { get => followArea; set => followArea = value; }

    public void Setup(float radius)
    {
        followArea.transform.localScale = new Vector3(radius, radius, 1);
    }
}
