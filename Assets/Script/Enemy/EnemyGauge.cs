using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnemyGauge : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI totalRoundText;
    [SerializeField] Image bossIcon;
    private void Start()
    {
        bossIcon.gameObject.SetActive(false);
    }
    public void Setup(int round)
    {
        bossIcon.gameObject.SetActive(round % 10 == 0);
        totalRoundText.gameObject.SetActive(!(round % 10 == 0));
        totalRoundText.text = round + "m";
    }
}
