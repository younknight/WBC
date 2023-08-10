using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) return;
        transform.Translate(new Vector3(18, 0, 0));
        //enemySpawner.SpawnNextEnemy(enemySpawner.CurrentRound);
    }
}
