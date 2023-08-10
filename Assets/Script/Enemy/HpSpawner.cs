using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpSpawner : MonoBehaviour
{
    [SerializeField] Transform canvasTransform;
    [SerializeField] GameObject enemyHp;

    public void SpawnHp(GameObject enemy)
    {
        //Debug.Log("ASdasd");
        GameObject sliderClone = Instantiate(enemyHp);
        sliderClone.transform.SetParent(canvasTransform);
        sliderClone.transform.localScale = Vector3.one;
        sliderClone.GetComponent<HpPositionSetter>().Setup(enemy.transform);
        sliderClone.GetComponent<HpManager>().Setup(enemy.GetComponent<Unit>());
        sliderClone.transform.SetAsFirstSibling();
    }
}
