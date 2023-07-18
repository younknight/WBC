using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] float maxHp = 1;
    [SerializeField] float attack = 1;//
    [SerializeField] float defence = 0;//
    [SerializeField] float criDamage = 0;//
    [SerializeField] float criRate = 0;//
    int roundIndex = 0;
    float hp;

    public int RoundIndex { get => roundIndex; set => roundIndex = value; }

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
    }
    public void Damaged(float damage)
    {
        float totalDamage = damage - defence > 1 ? damage - defence : 1; 
        hp -= totalDamage;
        if(hp <= 0)
        {
            if(gameObject.tag == "Enemy")
            {
                EnemySpawner.Instance.DestroyEnemy(this, roundIndex);
                Destroy(gameObject);
            }
            //»ç¸Á
        }
    }
    public float GetAttackDamage()
    {
        float totalDamgae = Random.Range(0,100) < criRate ? attack : attack + criDamage;
        return totalDamgae;
    }
}
